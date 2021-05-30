// -----------------------------------------------------------------------
// <copyright file="Broadcasts.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights.Configs
{
    using System.ComponentModel;
    using Exiled.API.Features;

    /// <summary>
    /// Configs relating to how broadcasts should be handled during a blackout.
    /// </summary>
    public class Broadcasts
    {
        /// <summary>
        /// Gets or sets a value indicating whether a broadcast should be shown when the lights are turned off.
        /// </summary>
        [Description("Whether a broadcast should be shown when the lights are turned off.")]
        public bool DoBroadcastMessage { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether broadcasts should be cleared prior to this one being shown.
        /// </summary>
        [Description("Whether broadcasts should be cleared prior to this one being shown.")]
        public bool ClearBroadcasts { get; set; } = true;

        /// <summary>
        /// Gets or sets the broadcast shown when the lights are only turned off in <see cref="Exiled.API.Enums.ZoneType.HeavyContainment"/>.
        /// </summary>
        [Description("The broadcast shown when the lights are only turned off in Heavy Containment Zone. (%ss = blackout duration)")]
        public Broadcast HczOnly { get; set; } = new Broadcast("<color=aqua>Lights have been turned off for %ss seconds! SpooOOOOoooky!</color>", 5);

        /// <summary>
        /// Gets or sets the broadcast shown when the lights are turned off in the entire facility.
        /// </summary>
        [Description("The broadcast shown when the lights are turned off in the entire facility. (%ss = blackout duration)")]
        public Broadcast Both { get; set; } = new Broadcast("<color=aqua>Lights have been turned off for %ss seconds! SpooOOOOoooky!</color>", 5);
    }
}