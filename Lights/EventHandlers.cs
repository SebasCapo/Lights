// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights
{
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using Interactables.Interobjects.DoorUtils;
    using MEC;

    /// <summary>
    /// Handles events subscribed from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public partial class EventHandlers
    {
        private readonly Dictionary<DoorVariant, bool> doorsToRestore;
        private readonly Plugin plugin;
        private readonly Config config;
        private CoroutineHandle automaticHandler;
        private CoroutineHandle lightsBack;
        private IEnumerable<DoorVariant> doorsToChange;
        private bool teslasDisabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public EventHandlers(Plugin plugin)
        {
            this.plugin = plugin;
            config = plugin.Config;

            doorsToRestore = new Dictionary<DoorVariant, bool>();
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Server.OnRoundStarted"/>
        public void OnRoundStarted()
        {
            teslasDisabled = false;

            if (plugin.Config.Blackouts.ModifyDoors)
            {
                doorsToChange = Map.Doors.Where(d => config.Blackouts.BlacklistedDoors.All(s => d != Map.GetDoorByName(s)));
            }

            if (config.AutomaticBlackouts.DoAutomaticBlackouts)
            {
                if (config.AutomaticBlackouts.DoMultipleBlackouts)
                    automaticHandler = Timing.RunCoroutine(MultipleBlackouts());
                else
                    automaticHandler = Timing.RunCoroutine(SingleBlackout());
            }
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Server.OnRoundEnded(RoundEndedEventArgs)"/>
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines(automaticHandler);
            Timing.KillCoroutines(lightsBack);
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnTriggeringTesla(TriggeringTeslaEventArgs)"/>
        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (teslasDisabled)
                ev.IsTriggerable = false;
        }
    }
}