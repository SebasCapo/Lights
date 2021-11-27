// -----------------------------------------------------------------------
// <copyright file="Lights.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;

    /// <summary>
    /// A command to disable lights for a given duration.
    /// </summary>
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Lights : ICommand, IUsageProvider
    {
        /// <inheritdoc />
        public string Command { get; } = Plugin.Instance.Config.Command.Name;

        /// <inheritdoc />
        public string[] Aliases { get; } = Plugin.Instance.Config.Command.Aliases;

        /// <inheritdoc />
        public string Description { get; } = Plugin.Instance.Config.Command.Description;

        /// <inheritdoc />
        public string[] Usage { get; } = Plugin.Instance.Config.Command.Usage;

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count == 0)
            {
                response = HelpMessage();
                return false;
            }

            if (sender.CheckPermission("lights.presets"))
            {
                if (Plugin.Instance.Config.Presets.PerZone.TryTriggerPreset(arguments.At(0)))
                {
                    response = $"Used preset \"{arguments.At(0)}\" successfully.";
                    return true;
                }
                else if (Plugin.Instance.Config.Presets.PerRoom.TryTriggerPreset(arguments.At(0)))
                {
                    response = $"Used preset \"{arguments.At(0)}\" successfully.";
                    return true;
                }
            }
            else if (!sender.CheckPermission("lights.custom"))
            {
                response = "Insufficient permission. Required: lights.custom";
                return false;
            }

            // Command layout, for testing purposes.
            // -1          0      1   2   3  4   5
            // /lights Room/Zone 12 Color 5 255 255
            if (arguments.Count > 1 && Enum.TryParse(arguments.At(2), true, out ModifierType modifierType))
            {
                var rgb = new float[] { 0.75f, 255, 255 };
                var duration = 15f;

                if (float.TryParse(arguments.At(1), out var dur))
                    duration = dur;

                if (arguments.Count > 3 && float.TryParse(arguments.At(3), out var r))
                    rgb[0] = r;

                if (arguments.Count > 4 && float.TryParse(arguments.At(4), out var g))
                    rgb[1] = g;

                if (arguments.Count > 5 && float.TryParse(arguments.At(5), out var b))
                    rgb[2] = b;

                if (Enum.TryParse(arguments.At(0), out RoomType roomType))
                {
                    foreach (var item in Map.Rooms)
                    {
                        if (item.Type != roomType)
                            continue;

                        item.TryExecute(modifierType, duration, rgb);
                    }

                    response = $"Successfully used {modifierType} mode on all rooms of type {roomType}.";
                    return true;
                }
                else if (Enum.TryParse(arguments.At(0), out ZoneType zoneType))
                {
                    foreach (var item in Map.Rooms)
                    {
                        if (item.Zone != zoneType)
                            continue;

                        item.TryExecute(modifierType, duration, rgb);
                    }

                    response = $"Successfully used {modifierType} mode on all rooms inside {zoneType}.";
                    return true;
                }
                else
                {
                    response = HelpMessage();
                    return false;
                }
            }
            else
            {
                response = $"Could not recognize that mode, type \"{Command}\" for the correct usage.";
                return false;
            }
        }

        private string HelpMessage() =>
            "<color=#2fb562>Usage:</color>" +
            $"\n  <color=#03b6fc>- \"{Command} <preset ID>\"</color>" +
            $"\n  <color=#03b6fc>- \"{Command} <roomType/zoneType> <duration> <modifierType> [parameters]\"</color>" +
            "\n" +
            $"\n<color=#2fb562>Modifiers:</color> <color=yellow>{string.Join(", ", Enum.GetNames(typeof(ModifierType)))}</color>" +
            "\n<color=#a15bc9>Examples:</color>" +
            $"\n  <color=#03b6fc>- \"{Command} Lcz173 8 Color 255 80 255\"</color>" +
            $"\n  <color=#03b6fc>- \"{Command} LightContainment 8 Intensity 0.75\"</color>";
    }
}