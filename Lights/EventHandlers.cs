// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;
    using UnityEngine;

    /// <summary>
    /// Handles events subscribed from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public class EventHandlers
    {
        private readonly Config config;
        private int presetIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public EventHandlers(Plugin plugin)
        {
            config = plugin.Config;

            DisabledTeslas = new List<int>();
        }

        /// <summary>
        /// Gets a <see cref="List{T}"/> containing the <see cref="UnityEngine.Object.GetInstanceID()">Instance IDs</see> of all rooms
        /// whose lights were affected (Depending on config settings), teslas in these rooms won't trigger.
        /// </summary>
        public List<int> DisabledTeslas { get; }

        /// <inheritdoc cref="Exiled.Events.Handlers.Server.OnRoundStarted"/>
        public void OnRoundStarted() => Plugin.Coroutines.Add(Timing.RunCoroutine(SetupPresets()));

        /// <inheritdoc cref="Exiled.Events.Handlers.Server.OnRoundEnded(RoundEndedEventArgs)"/>
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            foreach (CoroutineHandle item in Plugin.Coroutines)
                Timing.KillCoroutines(item);

            Plugin.Coroutines.Clear();
            DisabledTeslas.Clear();
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnTriggeringTesla(TriggeringTeslaEventArgs)"/>
        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            Room room = ev.Player.CurrentRoom;

            if (room == null)
                return;

            if (config.TeslaGates.SmartGates)
            {
                var r = room.Color.r < config.TeslaGates.ColorSettings.R;
                var g = room.Color.g < config.TeslaGates.ColorSettings.G;
                var b = room.Color.b < config.TeslaGates.ColorSettings.B;
                var belowColorMinimum = config.TeslaGates.ColorSettings.RequireAllMinimums ? r && g && b : r || g || b;

                if (room.LightsOff || belowColorMinimum || room.LightIntensity < config.TeslaGates.IntensityMinimum)
                    ev.IsTriggerable = false;

                return;
            }

            if (DisabledTeslas.Contains(room.GetInstanceID()))
                ev.IsTriggerable = false;
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Warhead.OnStopping(StoppingEventArgs)"/>
        public void OnStopping(StoppingEventArgs ev) => Timing.RunCoroutine(SetupPresets());

        private IEnumerator<float> SetupPresets()
        {
            if (!config.Presets.AreEnabled)
                yield break;

            yield return Timing.WaitForSeconds(config.Presets.InitialDelay);

            if (!config.Presets.InitialPreset.IsEmpty())
            {
                var id = config.Presets.InitialPreset[Random.Range(0, config.Presets.InitialPreset.Length)];

                if (!string.IsNullOrEmpty(id) && !id.StartsWith("!"))
                {
                    if (config.Presets.PerZone.TryTriggerPreset(id))
                    {
                        Log.Debug($"Automatically ran initial zone preset: \"{id}\"", config.Debug);
                        yield return Timing.WaitForSeconds(Random.Range(config.Presets.TimeBetweenMin, config.Presets.TimeBetweenMax));
                    }
                    else if (config.Presets.PerRoom.TryTriggerPreset(id))
                    {
                        Log.Debug($"Automatically ran initial room preset: \"{id}\"", config.Debug);
                        yield return Timing.WaitForSeconds(Random.Range(config.Presets.TimeBetweenMin, config.Presets.TimeBetweenMax));
                    }
                    else
                    {
                        Log.Error($"Couldn't find any presets with ID \"{id}\", make sure there's no typos.");
                    }
                }
            }

            for (int i = 0; i < config.Presets.LoopCount; i++)
            {
                string id;
                if (config.Presets.RandomOrder)
                {
                    id = config.Presets.Order[Random.Range(0, config.Presets.Order.Length)];
                }
                else
                {
                    id = config.Presets.Order[presetIndex++];

                    if (presetIndex >= config.Presets.Order.Length)
                        presetIndex = 0;
                }

                if (config.Presets.PerZone.TryTriggerPreset(id))
                    Log.Debug($"Automatically ran zone preset: \"{id}\"", config.Debug);
                else if (config.Presets.PerRoom.TryTriggerPreset(id))
                    Log.Debug($"Automatically ran room preset: \"{id}\"", config.Debug);
                else
                    Log.Error($"Couldn't find any presets with ID \"{id}\", make sure there's no typos.");

                yield return Timing.WaitForSeconds(Random.Range(config.Presets.TimeBetweenMin, config.Presets.TimeBetweenMax));
            }
        }
    }
}