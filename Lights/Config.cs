// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights
{
    using System.ComponentModel;
    using Exiled.API.Interfaces;
    using Lights.Configs;

    /// <inheritdoc cref="IConfig"/>
    public class Config : IConfig
    {
        /// <inheritdoc />
        [Description("Whether this plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether some debug messages will be shown.
        /// </summary>
        [Description("Whether some debug messages will be shown.")]
        public bool Debug { get; set; } = true;

        /// <inheritdoc cref="Configs.Command"/>
        [Description("All configuration settings relating this plugin's command(s).")]
        public Command Command { get; set; } = new Command();

        /// <inheritdoc cref="Configs.Cassie"/>
        [Description("All configuration settings relating Cassie when presets are ran.")]
        public Cassie Cassie { get; set; } = new Cassie();
        /*/// <inheritdoc cref="Configs.Broadcasts"/>
        public Broadcasts Broadcasts { get; set; } = new Broadcasts();*/

        /// <inheritdoc cref="Configs.TeslaGates"/>
        [Description("All configuration settings relating Tesla Gates during blackouts, lockdowns and other light settings.")]
        public TeslaGates TeslaGates { get; set; } = new TeslaGates();

        /// <inheritdoc cref="Configs.Presets"/>
        [Description("This plugin's main feature, presets have fun. :)")]
        public Presets Presets { get; set; } = new Presets();
    }
}