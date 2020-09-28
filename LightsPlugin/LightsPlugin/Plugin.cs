using System;
using Exiled.API.Features;
using MEC;
using Server = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.Handlers.Player;

namespace Lights {
    public class Plugin : Plugin<LightsConfig> {
        
        public override string Prefix => "lights";
        public override string Name => "Lights";
        public override string Author => "Beryl";
        public override Version Version { get; } = new Version(2, 1);
        public override Version RequiredExiledVersion { get; } = new Version(2, 0, 0);
        public EventHandlers handlers;

        public override void OnEnabled() {
            handlers = new EventHandlers(this);

            Server.SendingRemoteAdminCommand += handlers.OnCommand;
            RegisterEvents();
        }

        public override void OnDisabled() {

            UnregisterEvents();
        }

        public void RegisterEvents() {
            Server.RoundStarted += handlers.OnRoundStart;
            Server.RoundEnded += handlers.OnRoundEnd;
            Player.TriggeringTesla += handlers.TeslaTrigger;
        }

        public void UnregisterEvents() {
            Server.RoundStarted -= handlers.OnRoundStart;
            Server.RoundEnded -= handlers.OnRoundEnd;
            Player.TriggeringTesla -= handlers.TeslaTrigger;
        }

    }
}