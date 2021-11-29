// -----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights
{
    using System.Collections.Generic;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Lights.Configs;
    using MEC;
    using Cassie = Exiled.API.Features.Cassie;

    /// <summary>
    /// A bunch of useful extensions used in this plugin. Most are used to ease some work on it, not necessarily useful on other projects.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Executes a preset's settings on a room.
        /// </summary>
        /// <param name="room">The affected room.</param>
        /// <param name="modifierType">The modifier setting used.</param>
        /// <param name="duration">The duration of this modification.</param>
        /// <param name="arguments">The extra arguments used differently depending of the <see cref="ModifierType"/>.</param>
        /// <returns>Whether the preset was executed properly.</returns>
        public static bool TryExecute(this Room room, ModifierType modifierType, float duration = -1, params float[] arguments)
        {
            var id = room.GetInstanceID();

            switch (modifierType)
            {
                case ModifierType.Lockdown:
                    room.LockDown(duration);
                    return true;
                case ModifierType.Intensity:
                    var intensity = room.LightIntensity;

                    room.LightIntensity = arguments[0];

                    if (!Plugin.Instance.Config.TeslaGates.SmartGates
                        && room.LightIntensity < Plugin.Instance.Config.TeslaGates.IntensityMinimum)
                    {
                        if (Plugin.EventHandlers.DisabledTeslas.Contains(id))
                            Plugin.EventHandlers.DisabledTeslas.Add(id);
                    }

                    if (duration > -1)
                    {
                        Plugin.Coroutines.Add(Timing.CallDelayed(duration, () =>
                        {
                            room.LightIntensity = intensity;
                            Plugin.EventHandlers.DisabledTeslas.Remove(id);
                        }));
                    }

                    return true;
                case ModifierType.Color:
                    if (arguments.Length < 3)
                    {
                        Log.Error("Missing arguments on preset with Color modifier type.");
                        return false;
                    }

                    var color = room.Color;

                    room.Color = new UnityEngine.Color(arguments[0] / 255f, arguments[1] / 255f, arguments[2] / 255f);

                    if (!Plugin.Instance.Config.TeslaGates.SmartGates)
                    {
                        // There's a price you gotta pay for customization. :')
                        var r = room.Color.r < Plugin.Instance.Config.TeslaGates.ColorSettings.R;
                        var g = room.Color.g < Plugin.Instance.Config.TeslaGates.ColorSettings.G;
                        var b = room.Color.b < Plugin.Instance.Config.TeslaGates.ColorSettings.B;
                        var shouldDisableTeslas =
                            Plugin.Instance.Config.TeslaGates.ColorSettings.RequireAllMinimums ? r && g && b : r || g || b;

                        if (shouldDisableTeslas)
                        {
                            if (Plugin.EventHandlers.DisabledTeslas.Contains(id))
                                Plugin.EventHandlers.DisabledTeslas.Add(id);
                        }
                    }

                    if (duration > -1)
                    {
                        Plugin.Coroutines.Add(Timing.CallDelayed(duration, () =>
                        {
                            room.ResetColor();
                            Plugin.EventHandlers.DisabledTeslas.Remove(id);
                        }));
                    }

                    return true;
                case ModifierType.Blackout:
                default:
                    room.TurnOffLights(duration);

                    if (!Plugin.Instance.Config.TeslaGates.SmartGates && Plugin.Instance.Config.TeslaGates.DisableOnBlackout)
                    {
                        if (Plugin.EventHandlers.DisabledTeslas.Contains(id))
                            Plugin.EventHandlers.DisabledTeslas.Add(id);
                    }

                    if (duration > -1)
                        Plugin.Coroutines.Add(Timing.CallDelayed(duration, () => Plugin.EventHandlers.DisabledTeslas.Remove(id)));
                    return true;
            }
        }

        /// <summary>
        /// Tries to trigger a specific preset inside a <see cref="IDictionary{TKey, TValue}">presets dictionary</see>.
        /// <para>DEV Note: This will get replaced with a cleaner method.</para>
        /// </summary>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}">presets dictionary</see>.</param>
        /// <param name="key">The key that will be used to search the specific preset.</param>
        /// <returns>Whether the key was present in the dictionary.
        /// <para>It <b>DOES NOT</b> return false if it errors.</para>
        /// </returns>
        public static bool TryTriggerPreset(this IDictionary<string, Preset<ZoneType>[]> dictionary, string key)
        {
            if (Plugin.Instance.Config.Cassie.DoCassieMessages
                && Plugin.Instance.Config.Cassie.Messages.TryGetValue(key, out var message))
            {
                Cassie.Message(message, Plugin.Instance.Config.Cassie.MakeHold, Plugin.Instance.Config.Cassie.MakeNoise);
            }

            if (dictionary.TryGetValue(key, out var presets))
            {
                foreach (var preset in presets)
                {
                    foreach (var room in Map.Rooms)
                    {
                        if (room.Zone != preset.Location)
                            continue;

                        if (!room.TryExecute(preset.Modifier, preset.Duration, preset.Arguments))
                        {
                            Log.Error($"Couldn't trigger following preset ({key}) on {room.Name} ({room.Type})");
                        }
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to trigger a specific preset inside a <see cref="IDictionary{TKey, TValue}">presets dictionary</see>.
        /// <para>DEV Note: This will get replaced with a cleaner method.</para>
        /// </summary>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}">presets dictionary</see>.</param>
        /// <param name="key">The key that will be used to search the specific preset.</param>
        /// <returns>Whether the key was present in the dictionary.
        /// <para>It <b>DOES NOT</b> return false if it errors.</para>
        /// </returns>
        public static bool TryTriggerPreset(this IDictionary<string, Preset<RoomType>[]> dictionary, string key)
        {
            if (Plugin.Instance.Config.Cassie.DoCassieMessages
                && Plugin.Instance.Config.Cassie.Messages.TryGetValue(key, out var message))
            {
                Cassie.Message(message, Plugin.Instance.Config.Cassie.MakeHold, Plugin.Instance.Config.Cassie.MakeNoise);
            }

            if (dictionary.TryGetValue(key, out var presets))
            {
                foreach (var preset in presets)
                {
                    foreach (var room in Map.Rooms)
                    {
                        if (room.Type != preset.Location)
                            continue;

                        if (!room.TryExecute(preset.Modifier, preset.Duration, preset.Arguments))
                        {
                            Log.Error($"Couldn't trigger following preset ({key}) on {room.Name} ({room.Type})");
                        }
                    }
                }

                return true;
            }

            return false;
        }
    }
}
