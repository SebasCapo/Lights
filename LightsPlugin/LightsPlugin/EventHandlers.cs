using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.Permissions.Extensions;
using MEC;

namespace Lights {

    public class EventHandlers {
        public CoroutineHandle automaticHandler, lightsBack;
        Dictionary<Door, bool> doorsToRestore;
        public Plugin plugin;
        Door[] doorsToChange;
        bool TeslasDisabled;
        LightsConfig Config;
        public int count;

        public EventHandlers( Plugin plugin ) {
            this.plugin = plugin;
            Config = plugin.Config;

            doorsToRestore = new Dictionary<Door, bool>();
        }

        public void OnRoundStart() {
            TeslasDisabled = false;

            if(plugin.Config.ModifyDoors) {
                doorsToChange = Map.Doors.Where(d =>
                Config.OpenableDoors.Contains(d.doorType)
                && !Config.BlacklistedDoors.Any(d.DoorName.Contains)).ToArray();
            }

            if(Config.DoAutomaticBlackout) {
                if(Config.doMultipleBlackouts)
                    automaticHandler = Timing.RunCoroutine(MultipleBlackouts());
                else automaticHandler = Timing.RunCoroutine(SingleBlackout());
            }
        }

        public void OnRoundEnd(RoundEndedEventArgs ev) {
            Timing.KillCoroutines(automaticHandler);
            Timing.KillCoroutines(lightsBack);
        }

        public void OnDetonate() => Timing.KillCoroutines(automaticHandler);

        public void TeslaTrigger(TriggeringTeslaEventArgs ev) {
            if(TeslasDisabled)
                ev.IsTriggerable = false;
        }

        #region Commands
        public void OnCommand( SendingRemoteAdminCommandEventArgs ev ) {
            try {
                if(ev.Name.Contains("REQUEST_DATA PLAYER_LIST SILENT"))
                    return;

                if(ev.Name.ToLower().Equals(Config.Command) || ev.Name.ToLower().Equals(Config.CommandAlias)) {
                    ev.IsAllowed = false;
                    if(!ev.Sender.CheckPermission("lights.light")) {
                        ev.ReplyMessage = "<color=red>Access denied.</color>";
                        return;
                    } else if(ev.Arguments.Count() >= 1) {
                        if(!float.TryParse(ev.Arguments[0], out float duration)) {
                            ev.ReplyMessage = $"<color=red>Can't use {ev.Arguments[0]} as a duration.</color>";
                            return;
                        }

                        bool HczOnly = false;
                        if(ev.Arguments.Count() >= 2)
                            HczOnly = Config.AcceptedArguments.Contains(ev.Arguments[1].ToLower());

                        TurnOffLights(duration, HczOnly);
                        ev.ReplyMessage = $"<color=#13c706>The lights have been turned off for {(int) duration}s. HczOnly? {HczOnly}</color>";
                        return;
                    }
                    ev.ReplyMessage = "<color=red>Correct usage: \"lights <duration> [HczOnly?]\"</color>";
                }
                return;
            } catch ( Exception e ) {
                Log.Error("Command error: " + e.StackTrace);
            }
        }
        #endregion

        public IEnumerator<float> MultipleBlackouts() {
            yield return Timing.WaitForSeconds(UnityEngine.Random.Range(Config.StartTimerMin, Config.StartTimerMax));
            while(Config.doMultipleBlackouts && Round.IsStarted) {
                float duration = UnityEngine.Random.Range(Config.BlackoutDurationMin, Config.BlackoutDurationMax);
                bool HczOnly = UnityEngine.Random.Range(1, 101) <= Config.HczOnlyChance;
                TurnOffLights(duration, HczOnly);

                float timeBetween = Config.AddDuration ? UnityEngine.Random.Range(Config.TimeBetweenMin, Config.TimeBetweenMax) + duration : UnityEngine.Random.Range(Config.TimeBetweenMin, Config.TimeBetweenMax);
                yield return Timing.WaitForSeconds(timeBetween);
            }
        }

        public IEnumerator<float> SingleBlackout() {
            yield return Timing.WaitForSeconds(UnityEngine.Random.Range(Config.StartTimerMin, Config.StartTimerMax));
            float duration = UnityEngine.Random.Range(Config.BlackoutDurationMin, Config.BlackoutDurationMax);
            bool HczOnly = UnityEngine.Random.Range(1, 101) <= Config.HczOnlyChance;
            TurnOffLights(duration, HczOnly);
        }

        public void TurnOffLights(float duration, bool hczOnly) {
            if(plugin.Config.DisableTeslas) {
                Timing.KillCoroutines(lightsBack);

                TeslasDisabled = true;

                if(Config.ModifyDoors) {
                    if(!doorsToRestore.IsEmpty())
                        doorsToRestore.Clear();

                    foreach(Door d in doorsToChange) {
                        if(!Warhead.IsInProgress && !d.Networkdestroyed && !d.Networklocked) {
                            if(Config.RestoreDoors)
                                doorsToRestore.Add(d, d.NetworkisOpen);
                            d.SetState(!d.NetworkisOpen);
                        }
                    }
                }

                lightsBack = Timing.CallDelayed(duration, () => {
                    TeslasDisabled = false;    

                    if(Config.ModifyDoors && Config.RestoreDoors) {
                        foreach(KeyValuePair<Door, bool> pair in doorsToRestore)
                            pair.Key.SetState(pair.Value);
                    }
                });

            }

            if(Config.DoBroadcastMessage) {
                ushort dur = hczOnly ? Config.BroadcastMessageHczOnlyDuration : Config.BroadcastMessageBothDuration;
                string msg = hczOnly ? Config.BroadcastMessageHczOnly.Replace("%ss", $"{duration}") : Config.BroadcastMessageBoth.Replace("%ss", $"{duration}");
                if(Config.ClearBroadcasts)
                    Map.ClearBroadcasts();
                Map.Broadcast(dur, msg);
            }

            if(Config.DoCassieMessages) {
                string msg = hczOnly ? Config.CassieMessageHczOnly.Replace("%ss", $"{duration}") : Config.CassieMessageBoth.Replace("%ss", $"{duration}");
                Cassie.Message(msg, Config.MakeHold, Config.MakeNoise);
            }



            foreach(Room r in Map.Rooms) {
                if(hczOnly && r.Zone == ZoneType.HeavyContainment)
                r.TurnOffLights(duration);
            }
        }

        private bool ToggleLights(float duration, bool hczOnly) {

        }
    }
}