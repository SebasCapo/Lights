// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights
{
    using Exiled.API.Interfaces;
    using Lights.Configs;

    /// <inheritdoc cref="IConfig"/>
    public class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc cref="Lights.Configs.AutomaticBlackouts"/>
        public AutomaticBlackouts AutomaticBlackouts { get; set; } = new AutomaticBlackouts();

        /// <inheritdoc cref="Lights.Configs.Blackouts"/>
        public Blackouts Blackouts { get; set; } = new Blackouts();

        /// <inheritdoc cref="Lights.Configs.Broadcasts"/>
        public Broadcasts Broadcasts { get; set; } = new Broadcasts();

        /// <inheritdoc cref="Lights.Configs.Cassie"/>
        public Cassie Cassie { get; set; } = new Cassie();

        /// <inheritdoc cref="Lights.Configs.Command"/>
        public Command Command { get; set; } = new Command();
    }
}