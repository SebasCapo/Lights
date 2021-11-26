// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights
{
    using System;
    using System.Collections.Generic;
    using Exiled.API.Features;
    using MEC;
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

        /// <summary>
        /// Gets all coroutines used by this plugin.
        /// </summary>
        public static List<CoroutineHandle> Coroutines { get; } = new List<CoroutineHandle>();

        /// <inheritdoc />
        public override string Name => "LightsRE";

        /// <inheritdoc />
        public override string Author => "Beryl - (Contributors: BuildBoy12)";

        /// <inheritdoc />
        public override string Prefix => "lights";

        /// <inheritdoc />
        public override Version Version => new Version(4, 0, 0);

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);

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

        private void RegisterEvents()
        {
            ServerHandlers.RoundStarted += EventHandlers.OnRoundStarted;
            ServerHandlers.RoundEnded += EventHandlers.OnRoundEnded;
            PlayerHandlers.TriggeringTesla += EventHandlers.OnTriggeringTesla;
        }

        private void UnregisterEvents()
        {
            ServerHandlers.RoundStarted -= EventHandlers.OnRoundStarted;
            ServerHandlers.RoundEnded -= EventHandlers.OnRoundEnded;
            PlayerHandlers.TriggeringTesla -= EventHandlers.OnTriggeringTesla;
        }
    }
}