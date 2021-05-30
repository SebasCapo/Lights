// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights
{
    using System;
    using Exiled.API.Features;
    using PlayerHandlers = Exiled.Events.Handlers.Player;
    using ServerHandlers = Exiled.Events.Handlers.Server;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config>
    {
        private static readonly Plugin InstanceValue = new Plugin();

        private Plugin()
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="Plugin"/> class.
        /// </summary>
        public static Plugin Instance { get; } = InstanceValue;

        /// <summary>
        /// Gets an instance of the <see cref="Lights.EventHandlers"/> class.
        /// </summary>
        public static EventHandlers EventHandlers { get; private set; }

        /// <inheritdoc />
        public override string Prefix => "lights";

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(2, 10, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            EventHandlers = new EventHandlers(this);
            RegisterEvents();
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            UnregisterEvents();
            EventHandlers = null;
            base.OnDisabled();
        }

        private static void RegisterEvents()
        {
            ServerHandlers.RoundStarted += EventHandlers.OnRoundStarted;
            ServerHandlers.RoundEnded += EventHandlers.OnRoundEnded;
            PlayerHandlers.TriggeringTesla += EventHandlers.OnTriggeringTesla;
        }

        private static void UnregisterEvents()
        {
            ServerHandlers.RoundStarted -= EventHandlers.OnRoundStarted;
            ServerHandlers.RoundEnded -= EventHandlers.OnRoundEnded;
            PlayerHandlers.TriggeringTesla -= EventHandlers.OnTriggeringTesla;
        }
    }
}