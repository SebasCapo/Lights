// -----------------------------------------------------------------------
// <copyright file="Command.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights.Configs
{
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Configs relating to the plugin's <see cref="Lights.Commands.Lights"/> command.
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Gets or sets the main command name.
        /// </summary>
        public string Name { get; set; } = "lights";

        /// <summary>
        /// Gets or sets all accepted command aliases.
        /// </summary>
        public string[] Aliases { get; set; } = { "ls" };

        /// <summary>
        /// Gets or sets a collection of arguments which are to be considered as true for the hczOnly argument in the <see cref="Lights.Commands.Lights"/> command.
        /// </summary>
        [Description("Which arguments are considered as \"true\" for the third argument in the Light command. (Example: Adding \"yes\" to this list, will make it so using \"ls 15 yes\", turns off lights in HCZ only). By the way, don't use uppercase!")]
        public List<string> AcceptedArguments { get; set; } = new List<string>
        {
            "yes", "y", "true", "t", "hczonly",
        };
    }
}