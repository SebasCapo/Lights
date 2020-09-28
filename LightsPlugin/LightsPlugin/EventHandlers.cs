using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.Permissions.Extensions;
using MEC;

namespace Lights {

    public class EventHandlers {
        public CoroutineHandle automaticHandler, teslaDisabler;
        public Plugin plugin;
        bool TeslasDisabled;
        public int count;
        LightsConfig Config;

        public EventHandlers( Plugin plugin ) {
            this.plugin = plugin;
            Config = plugin.Config;
        }

        public void OnRoundStart() {
            TeslasDisabled = false;

            if(Config.DoAutomaticBlackout) {
                if(Config.doMultipleBlackouts)
                    automaticHandler = Timing.RunCoroutine(MultipleBlackouts());
                else automaticHandler = Timing.RunCoroutine(SingleBlackout());
            }
        }

        public void OnRoundEnd(RoundEndedEventArgs ev) {
            Timing.KillCoroutines(automaticHandler);
            Timing.KillCoroutines(teslaDisabler);
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
            yield return Timing.WaitForSeconds(UnityEngine.Random.Range(Config.startTimerMin, Config.startTimerMax));
            while(Config.doMultipleBlackouts && Round.IsStarted) {
                float duration = UnityEngine.Random.Range(Config.blackoutDurationMin, Config.blackoutDurationMax);
                bool HczOnly = UnityEngine.Random.Range(1, 101) <= Config.hczOnlyChance;
                TurnOffLights(duration, HczOnly);

                float timeBetween = Config.addDuration ? UnityEngine.Random.Range(Config.timeBetweenMin, Config.timeBetweenMax) + duration : UnityEngine.Random.Range(Config.timeBetweenMin, Config.timeBetweenMax);
                yield return Timing.WaitForSeconds(timeBetween);
            }
        }

        public IEnumerator<float> SingleBlackout() {
            yield return Timing.WaitForSeconds(UnityEngine.Random.Range(Config.startTimerMin, Config.startTimerMax));
            float duration = UnityEngine.Random.Range(Config.blackoutDurationMin, Config.blackoutDurationMax);
            bool HczOnly = UnityEngine.Random.Range(1, 101) <= Config.hczOnlyChance;
            TurnOffLights(duration, HczOnly);
        }

        public void TurnOffLights(float duration, bool HczOnly) {
            if(plugin.Config.DisableTeslas) {
                Timing.KillCoroutines(teslaDisabler);

                TeslasDisabled = true;

                teslaDisabler = Timing.CallDelayed(duration, () => TeslasDisabled = false);
            }

            if(Config.DoBroadcastMessage) {
                ushort dur = HczOnly ? Config.broadcastMessageHczOnlyDuration : Config.broadcastMessageBothDuration;
                string msg = HczOnly ? Config.broadcastMessageHczOnly.Replace("%ss", $"{duration}") : Config.broadcastMessageBoth.Replace("%ss", $"{duration}");
                if(Config.ClearBroadcasts)
                    Map.ClearBroadcasts();
                Map.Broadcast(dur, msg);
            }

            if(Config.DoCassieMessages) {
                string msg = HczOnly ? Config.cassieMessageHczOnly.Replace("%ss", $"{duration}") : Config.cassieMessageBoth.Replace("%ss", $"{duration}");
                Cassie.Message(msg, Config.MakeHold, Config.MakeNoise);
            }

            Map.TurnOffAllLights(duration, HczOnly);
        }
    }
}