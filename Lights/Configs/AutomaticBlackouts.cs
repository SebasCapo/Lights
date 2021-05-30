// -----------------------------------------------------------------------
// <copyright file="AutomaticBlackouts.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights.Configs
{
    using System.ComponentModel;

    /// <summary>
    /// Configs relating to automated blackouts should be handled.
    /// </summary>
    public class AutomaticBlackouts
    {
        /// <summary>
        /// Gets or sets a value indicating whether the lights should be turned off automatically.
        /// </summary>
        [Description("Indicates whether the lights should be turned off automatically.")]
        public bool DoAutomaticBlackouts { get; set; } = true;

        /// <summary>
        /// Gets or sets the minimum amount of seconds of delay prior to shutting off the lights for the first time.
        /// </summary>
        [Description("The minimum amount of seconds of delay prior to shutting off the lights for the first time.")]
        public float StartTimerMin { get; set; } = 30f;

        /// <summary>
        /// Gets or sets the maximum amount of seconds of delay prior to shutting off the lights for the first time.
        /// </summary>
        [Description("The maximum amount of seconds of delay prior to shutting off the lights for the first time.")]
        public float StartTimerMax { get; set; } = 45f;

        /// <summary>
        /// Gets or sets a value indicating whether blackouts should occur repeatedly throughout the game.
        /// </summary>
        [Description("Indicates whether blackouts should occur repeatedly throughout the game.")]
        public bool DoMultipleBlackouts { get; set; } = true;

        /// <summary>
        /// Gets or sets the minimum duration of a delay between each blackout.
        /// </summary>
        [Description("How many seconds should this wait between each \"blackout\".")]
        public float TimeBetweenMin { get; set; } = 45f;

        /// <summary>
        /// Gets or sets the maximum duration of a delay between each blackout.
        /// </summary>
        public float TimeBetweenMax { get; set; } = 60f;

        /// <summary>
        /// Gets or sets the minimum duration of a blackout.
        /// </summary>
        [Description("The minimum duration of a blackout.")]
        public float BlackoutDurationMin { get; set; } = 15f;

        /// <summary>
        /// Gets or sets the maximum duration of a blackout.
        /// </summary>
        [Description("The maximum duration of a blackout.")]
        public float BlackoutDurationMax { get; set; } = 20f;

        /// <summary>
        /// Gets or sets a value indicating whether the blackout duration should be added to the delay between each automatic blackout.
        /// </summary>
        [Description("Should the blackout duration be added to the delay between each automatic blackout?")]
        public bool AddDuration { get; set; } = true;

        /// <summary>
        /// Gets or sets the chance for the automatic blackout to only occur in <see cref="Exiled.API.Enums.ZoneType.HeavyContainment"/>.
        /// </summary>
        [Description("The chance for the automatic blackout to only occur in heavy containment zone.")]
        public int HczOnlyChance { get; set; } = 50;
    }
}