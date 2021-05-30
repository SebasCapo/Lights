// -----------------------------------------------------------------------
// <copyright file="Methods.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights
{
    using System.Collections.Generic;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Interactables.Interobjects.DoorUtils;
    using MEC;

    /// <summary>
    /// Contains varying methods required by the plugin.
    /// </summary>
    public partial class EventHandlers
    {
        /// <summary>
        /// Handles the main blackout logic.
        /// </summary>
        /// <param name="duration">The amount of seconds the blackout should last.</param>
        /// <param name="hczOnly">Whether the blackout should only occur in <see cref="Exiled.API.Enums.ZoneType.HeavyContainment"/>.</param>
        public void TurnOffLights(float duration, bool hczOnly)
        {
            if (plugin.Config.Blackouts.DisableTeslas)
            {
                Timing.KillCoroutines(lightsBack);

                teslasDisabled = true;

                if (config.Blackouts.ModifyDoors)
                {
                    if (!doorsToRestore.IsEmpty())
                        doorsToRestore.Clear();

                    foreach (var door in doorsToChange)
                    {
                        if (!Warhead.IsInProgress && door.NetworkActiveLocks == 0)
                        {
                            if (config.Blackouts.RestoreDoors)
                                doorsToRestore.Add(door, door.IsConsideredOpen());
                            door.NetworkTargetState = !door.IsConsideredOpen();
                        }
                    }
                }

                lightsBack = Timing.CallDelayed(duration, () =>
                {
                    teslasDisabled = false;

                    if (config.Blackouts.ModifyDoors && config.Blackouts.RestoreDoors)
                    {
                        foreach (KeyValuePair<DoorVariant, bool> pair in doorsToRestore)
                            pair.Key.NetworkTargetState = pair.Value;
                    }
                });
            }

            if (config.Broadcasts.DoBroadcastMessage)
            {
                ushort dur = hczOnly ? config.Broadcasts.HczOnly.Duration : config.Broadcasts.Both.Duration;
                string msg = hczOnly
                    ? config.Broadcasts.HczOnly.Content.Replace("%ss", $"{duration}")
                    : config.Broadcasts.Both.Content.Replace("%ss", $"{duration}");
                if (config.Broadcasts.ClearBroadcasts)
                    Map.ClearBroadcasts();
                Map.Broadcast(dur, msg);
            }

            if (config.Cassie.DoCassieMessages)
            {
                string msg = hczOnly
                    ? config.Cassie.HczOnly.Replace("%ss", $"{duration}")
                    : config.Cassie.Both.Replace("%ss", $"{duration}");
                Cassie.Message(msg, config.Cassie.MakeHold, config.Cassie.MakeNoise);
            }

            foreach (Room r in Map.Rooms)
            {
                if (hczOnly && r.Zone == ZoneType.HeavyContainment)
                    r.TurnOffLights(duration);
            }
        }

        private IEnumerator<float> MultipleBlackouts()
        {
            yield return Timing.WaitForSeconds(UnityEngine.Random.Range(config.AutomaticBlackouts.StartTimerMin, config.AutomaticBlackouts.StartTimerMax));
            while (config.AutomaticBlackouts.DoMultipleBlackouts && Round.IsStarted)
            {
                float duration = UnityEngine.Random.Range(config.AutomaticBlackouts.BlackoutDurationMin, config.AutomaticBlackouts.BlackoutDurationMax);
                bool hczOnly = UnityEngine.Random.Range(1, 101) <= config.AutomaticBlackouts.HczOnlyChance;
                TurnOffLights(duration, hczOnly);

                float timeBetween = config.AutomaticBlackouts.AddDuration
                    ? UnityEngine.Random.Range(config.AutomaticBlackouts.TimeBetweenMin, config.AutomaticBlackouts.TimeBetweenMax) + duration
                    : UnityEngine.Random.Range(config.AutomaticBlackouts.TimeBetweenMin, config.AutomaticBlackouts.TimeBetweenMax);
                yield return Timing.WaitForSeconds(timeBetween);
            }
        }

        private IEnumerator<float> SingleBlackout()
        {
            yield return Timing.WaitForSeconds(UnityEngine.Random.Range(config.AutomaticBlackouts.StartTimerMin, config.AutomaticBlackouts.StartTimerMax));
            float duration = UnityEngine.Random.Range(config.AutomaticBlackouts.BlackoutDurationMin, config.AutomaticBlackouts.BlackoutDurationMax);
            bool hczOnly = UnityEngine.Random.Range(1, 101) <= config.AutomaticBlackouts.HczOnlyChance;
            TurnOffLights(duration, hczOnly);
        }
    }
}