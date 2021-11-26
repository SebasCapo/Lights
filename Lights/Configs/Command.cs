// -----------------------------------------------------------------------
// <copyright file="Command.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights.Configs
{
    using System.ComponentModel;

    /// <summary>
    /// Configs relating to the plugin's <see cref="Commands.Lights"/> command.
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Gets or sets the main command name.
        /// </summary>
        [Description("The main command's name.")]
        public string Name { get; set; } = "lightsre";

        /// <summary>
        /// Gets or sets all accepted command aliases.
        /// </summary>
        [Description("The main command's aliases.")]
        public string[] Aliases { get; set; } = { "lights", "lre", "ls" };

        /// <summary>
        /// Gets or sets the command description.
        /// </summary>
        [Description("The main command's description.")]
        public string Description { get; set; } = "Modifies a room/zone's lights depending on your input, or using a pre-made preset!";

        /// <summary>
        /// Gets or sets the command usage info.
        /// </summary>
        [Description("The main command's usage info.")]
        public string[] Usage { get; set; } = { "PresetID" };
    }
}