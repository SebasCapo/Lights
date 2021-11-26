// -----------------------------------------------------------------------
// <copyright file="TeslaGates.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights.Configs
{
    using System.ComponentModel;

    /// <summary>
    /// Configs relating <see cref="TeslaGate">Tesla Gates</see> during blackouts, lockdowns and other light settings.
    /// </summary>
    public class TeslaGates
    {
        /// <summary>
        /// Gets or sets whether teslas will be disabled when below a certain light intensity level.
        /// </summary>
        [Description("The minimum intensity required for tesla gates to be functional.")]
        public float IntensityMinimum { get; set; } = 0.7f;

        /// <summary>
        /// Gets or sets a value indicating whether teslas will be disabled during a lockdown.
        /// </summary>
        [Description("Whether tesla gates will stay functional during lockdowns.")]
        public bool DisableOnLockdown { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether teslas will be disabled during a lockdown.
        /// </summary>
        [Description("Whether tesla gates will stay functional during lockdowns.")]
        public bool DisableOnBlackout { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether tesla gates will be affected if their light settings change from other plugins. (EXPERIMENTAL)
        /// </summary>
        [Description("Whether tesla gates will be affected if their room's light settings change by other plugins/commands. (EXPERIMENTAL)")]
        public bool SmartGates { get; set; } = true;

        /// <inheritdoc cref="RGBSettings"/>
        public RGBSettings ColorSettings { get; set; } = new RGBSettings();

        /// <inheritdoc cref="TeslaGates"/>
        public class RGBSettings
        {
            /// <summary>
            /// Gets or sets whether teslas will be disabled when below a certain red value.
            /// </summary>
            [Description("The minimum red value required for tesla gates to be functional.")]
            public float R { get; set; } = 95f;

            /// <summary>
            /// Gets or sets whether teslas will be disabled when below a certain green value.
            /// </summary>
            [Description("The minimum green value required for tesla gates to be functional.")]
            public float G { get; set; } = 95f;

            /// <summary>
            /// Gets or sets whether teslas will be disabled when below a certain blue value.
            /// </summary>
            [Description("The minimum blue value required for tesla gates to be functional.")]
            public float B { get; set; } = 95f;

            /// <summary>
            /// Gets or sets a value indicating whether all color values must be below their minimum before getting disabled.
            /// </summary>
            [Description("Whether all color values must be below their minimum before getting disabled.")]
            public bool RequireAllMinimums { get; set; } = true;
        }
    }
}