// -----------------------------------------------------------------------
// <copyright file="Cassie.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights.Configs
{
    using System.ComponentModel;

    /// <summary>
    /// Configs relating to how cassie should be handled during a blackout.
    /// </summary>
    public class Cassie
    {
        /// <summary>
        /// Gets or sets a value indicating whether Cassie should announce when the lights are turned off.
        /// </summary>
        [Description("Indicates whether Cassie should announce when the lights are turned off.")]
        public bool DoCassieMessages { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the cassie announcement should be held.
        /// </summary>
        [Description("Indicates whether the cassie announcement should be held.")]
        public bool MakeHold { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the cassie announcement should have background noise.
        /// </summary>
        [Description("Indicates whether the cassie announcement should have background noise.")]
        public bool MakeNoise { get; set; } = false;

        /// <summary>
        /// Gets or sets what cassie says if the lights are only turned off in <see cref="Exiled.API.Enums.ZoneType.HeavyContainment"/>.
        /// </summary>
        [Description("What Cassie says if the lights are only turned off in Heavy Containment Zone. (%ss = blackout duration)")]
        public string HczOnly { get; set; } = "heavy containment zone generator .g3 malfunction detected .g4 .g3 .g3 .g4";

        /// <summary>
        /// Gets or sets what cassie says if the lights are turned off in the entire facility.
        /// </summary>
        [Description("What Cassie says if the lights are turned off in the entire facility. (%ss = blackout duration)")]
        public string Both { get; set; } = "generator .g3 malfunction detected .g4 .g3 .g3 .g4";
    }
}