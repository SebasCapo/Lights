// -----------------------------------------------------------------------
// <copyright file="Blackouts.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights.Configs
{
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Configs relating to how all blackouts should be handled.
    /// </summary>
    public class Blackouts
    {
        /// <summary>
        /// Gets or sets a value indicating whether tesla gates should be disabled during a blackout.
        /// </summary>
        [Description("Indicates whether tesla gates should be disabled during a blackout.")]
        public bool DisableTeslas { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether doors should be opened/closed when a blackout starts.
        /// </summary>
        [Description("Whether doors should be opened/closed when a blackout starts.")]
        public bool ModifyDoors { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether doors should go back to their original state when the blackout ends.
        /// </summary>
        [Description("Indicates whether doors should go back to their original state when lights come back. (This may cause issues to your server performance, clients won't be affected by this)")]
        public bool RestoreDoors { get; set; } = true;

        /// <summary>
        /// Gets or sets a collection of strings where doors that contain them should be ignored.
        /// </summary>
        [Description("Doors that have the following strings in their name will be ignored. (This and the previous configs will be ignored if 'OpenDoors' is set to 'false')")]
        public List<string> BlacklistedDoors { get; set; } = new List<string>
        {
            "106_BOTTOM", "173_CONNECTOR",
        };
    }
}