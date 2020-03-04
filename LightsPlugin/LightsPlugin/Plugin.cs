using System;
using EXILED;

namespace Lights {
    public class Plugin : EXILED.Plugin {

        public EventHandlers EventHandlers;
        MTFRespawn CASSIE;

        #region Config Variables

        // Text
        public string AccessDenied;
        public string HelpOne;
        public string HelpTwo;
        public string NotRecognized;
        public string Success;

        // Command
        public string CmdName;
        public string CmdAlias;
        public string TrueArguments;

        // Broadcast
        public uint AnnounceDuration;
        public bool DoAnnouncement;
        public string Announcement;

        // C.A.S.S.I.E.
        public string CassieAnnouncement;
        public string CassieAnnouncementBoth;
        public bool CassieAnnounceBoth;
        public bool CassieAnnounceHCZ;
        public bool CassieMakeNoise;
        #endregion

        public override void OnEnable() {
            try {
                Log.Debug("Lights plugin detected, loading configuration file...");
                reloadConfig();
                Log.Debug("Initializing EventHandlers...");
                EventHandlers = new EventHandlers(this);
                Events.RemoteAdminCommandEvent += EventHandlers.OnCommand;
                Log.Info("Plugin loaded correctly!");
            } catch ( Exception e ) {
                Log.Error("Problem loading plugin: " + e.StackTrace);
            }
        }

        public override void OnDisable() {
            Events.RemoteAdminCommandEvent -= EventHandlers.OnCommand;

            EventHandlers = null;
        }

        public override void OnReload() {
            reloadConfig();
        }

        public void cassieMessage( string message ) {
            if ( CASSIE == null )
                CASSIE = PlayerManager.localPlayer.GetComponent<MTFRespawn>();
            CASSIE.RpcPlayCustomAnnouncement(message, false, CassieMakeNoise);
        }

        public void reloadConfig() {
            Config.Reload();

            #region Lights Config

            // Text
            AccessDenied = Config.GetString("lights_message_access_denied", "<color=red>Access Denied.</color>");
            HelpOne = Config.GetString("lights_message_help", "<color=red>Command usage: " + '"' + "</color><color=yellow>%cmd <seconds> </color><color=#ab0be6>[true/yes] </color><color=red>" + '"' + "</color>");
            HelpTwo = Config.GetString("lights_message_help2", "<color=red><> is necessary -- [] is optional.</color>");
            NotRecognized = Config.GetString("lights_message_not_recognized", "<color=red>Argument " + '"' + "%arg" + '"' + " can't be recognized, using default value.</color>");
            Success = Config.GetString("lights_message_success", "<color=#13c706>Lights have been turned off for:<b> %ss </b>(HCZ Only? : <b>%value</b>)</color>");

            // Command
            CmdName = Config.GetString("lights_command", "lights");
            CmdAlias = Config.GetString("lights_alias", "ls");
            TrueArguments = Config.GetString("lights_true_arguments", "true,t,yes,y").ToLower();

            // Broadcast
            AnnounceDuration = Config.GetUInt("lights_announce_duration", 10);
            DoAnnouncement = Config.GetBool("lights_announce", true);
            Announcement = Config.GetString("lights_announce_text", "<color=aqua>Lights have been turned off by %player for %ss! SpooOOOººOoooky!</color>");

            // C.A.S.S.I.E.
            CassieAnnouncement = Config.GetString("lights_cassie_announcement_hcz", "heavy containment zone generator .g3 malfunction detected .g4 .g3 .g3 .g3 .g3 .g4");
            CassieAnnouncementBoth = Config.GetString("lights_cassie_announcement_both", "generator .g3 malfunction detected .g4 .g3 .g3 .g3 .g3 .g4");
            CassieAnnounceHCZ = Config.GetBool("lights_cassie_announceforhcz", false);
            CassieAnnounceBoth = Config.GetBool("lights_cassie_announceforboth", false);
            CassieMakeNoise = Config.GetBool("lights_cassie_makenoise", true);
            #endregion
        }

        public override string getName { get; } = "LightsPlugin - SebasCapo";
    }
}