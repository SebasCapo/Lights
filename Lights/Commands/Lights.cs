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
    using Exiled.Permissions.Extensions;

    /// <summary>
    /// A command to disable lights for a given duration.
    /// </summary>
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Lights : ICommand
    {
        private const string RequiredPermission = "lights.light";

        /// <inheritdoc />
        public string Command { get; } = Plugin.Instance.Config.Command.Name;

        /// <inheritdoc />
        public string[] Aliases { get; } = Plugin.Instance.Config.Command.Aliases;

        /// <inheritdoc />
        public string Description { get; } = "Disables the facilities lights for the specified duration.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(RequiredPermission))
            {
                response = $"Insufficient permission. Required: {RequiredPermission}";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "Usage: \"lights <duration> [HczOnly?]\"";
                return false;
            }

            if (!float.TryParse(arguments.At(0), out float duration))
            {
                response = $"Could not parse \"{arguments.At(0)}\" as a duration.";
                return false;
            }

            bool hczOnly = false;
            if (arguments.Count > 1)
                 hczOnly = Plugin.Instance.Config.Command.AcceptedArguments.Contains(arguments.At(1), StringComparison.OrdinalIgnoreCase);

            int shortDuration = (int)duration;
            Plugin.EventHandlers.TurnOffLights(duration, hczOnly);
            response = $"The lights have been turned off for {shortDuration} {(shortDuration == 1 ? "second" : "seconds")} {(hczOnly ? "in heavy containment" : "facility-wide")}.";
            return true;
        }
    }
}