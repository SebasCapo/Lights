using System;
using System.Collections.Generic;
using System.Linq;
using EXILED;
using EXILED.Extensions;
using MEC;
using Log = EXILED.Log;

namespace Lights {

    public class EventHandlers {
        public Plugin plugin;
        public int count;

        public EventHandlers( Plugin plugin ) {
            this.plugin = plugin;
        }

        public void OnRoundStart() {
            if(plugin.DoFirstBlackout)
            Timing.RunCoroutine(StartTimer(generateRandomNumber(plugin.FirstBlackoutTimerMin, plugin.FirstBlackoutTimerMax)));
        }

        #region Commands

        public void OnCommand( ref RACommandEvent ev ) {
            try {
                if ( ev.Command.Contains("REQUEST_DATA PLAYER_LIST SILENT") ) return;
                string [] args = ev.Command.Split(' ');
                ReferenceHub sender = ev.Sender.SenderId == "SERVER CONSOLE" || ev.Sender.SenderId == "GAME CONSOLE" ? Player.GetPlayer(PlayerManager.localPlayer) : Player.GetPlayer(ev.Sender.SenderId);
                if ( args [ 0 ].ToLower() == "lightsconfig" ) {
                    ev.Allow = false;
                    if ( !checkPermission(ev, sender, "config") ) {
                        ev.Sender.RAMessage(plugin.AccessDenied);
                        return;
                    }
                    if(args.Length >= 2) {
                        if(args[1].ToLower() == "set") {
                            if(!checkPermission(ev, sender, "set")) return;
                            if(args.Length >= 4) {
                                if(args[2].ToLower().StartsWith("lights_")) {
                                    Plugin.Config.SetString(args[2].ToLower(), ev.Command.Substring(ev.Command.LastIndexOf(args[3])));
                                    ev.Sender.RAMessage($"<color=yellow>{args[2].ToLower()}</color> <color=green>has been set to:</color> <color=yellow>{args[3]}</color><color=green>.</color>");
                                    return;
                                }
                            }
                        } else if(args[1].ToLower() == "reload") {
                            if(!checkPermission(ev, sender, "reload")) return;
                            plugin.reloadConfig();
                            ev.Sender.RAMessage("<color=green>Configuration variables reloaded.</color>");
                            return;
                        } else if(args[1].ToLower() == "variables") {
                            ev.Sender.RAMessage(
                                "<color=red>Each color shows the type of each variable.</color>\n" +
                                "<color=yellow>String</color> - <color=green>Integer</color> - <color=red>Float</color> - <color=#ff00ffff>Boolean</color>\n" +
                                "<color=yellow>lights_command -</color>" +
                                "<color=yellow>lights_alias -</color>" +
                                "<color=yellow>lights_true_arguments -</color>" +
                                "<color=#ff00ffff>lights_announce -</color>" +
                                "<color=green>lights_announce_duration -</color>" +
                                "<color=yellow>lights_announce_text -</color>" +
                                "<color=yellow>lights_message_access_denied -</color>" +
                                "<color=yellow>lights_message_help -</color>" +
                                "<color=yellow>lights_message_not_recognized -</color>" +
                                "<color=yellow>lights_message_success -</color>" +
                                "<color=yellow>lights_cassie_announcement_hcz -</color>" +
                                "<color=yellow>lights_cassie_announcement_both -</color>" +
                                "<color=#ff00ffff>lights_cassie_announceforhcz -</color>" +
                                "<color=#ff00ffff>lights_cassie_announceforboth -</color>" +
                                "<color=#ff00ffff>lights_cassie_makenoise -</color>" +
                                "<color=#ff00ffff>lights_startblackout_toggle -</color>" +
                                "<color=red>lights_startblackout_delay_max -</color>" +
                                "<color=red>lights_startblackout_delay_min -</color>" +
                                "<color=#ff00ffff>lights_multipleblackout_toggle -</color>" +
                                "<color=red>lights_multipleblackout_duration_max -</color>" +
                                "<color=red>lights_multipleblackout_duration_min -</color>" +
                                "<color=red>lights_multipleblackout_timebetween_max -</color>" +
                                "<color=red>lights_multipleblackout_timebetween_min -</color>" +
                                "<color=green>lights_multipleblackout_maxamount -</color>" +
                                "<color=yellow>lights_cassie_announcement_auto -</color>" +
                                "<color=#ff00ffff>lights_cassie_announceauto</color>");
                            return;
                        }
                    }
                    ev.Sender.RAMessage("<color=red>Please try one of the following:</color> " +
                        "\n<color=#f29f05>\"lightsconfig set <config_variable> <value>\" -> Change the value of a config variable of the plugin.</color>" +
                        "\n<color=#f29f05>\"lightsconfig variables\" -> Gives you a list of all variables available.</color>" +
                        "\n<color=#f29f05>\"lightsconfig reload\" -> Reloads config variables.</color>");
                    return;
                }

                #region Command: Lights
                if ( args [ 0 ].ToLower() == plugin.CmdName || args [ 0 ].ToLower() == plugin.CmdAlias ) {
                    ev.Allow = false;
                    if ( !checkPermission(ev, sender, "light") ) {
                        ev.Sender.RAMessage(plugin.AccessDenied);
                        return;
                    }
                    if ( args.Length < 2 ) {
                        ev.Sender.RAMessage(plugin.HelpMessage.Replace("%cmd", args [ 0 ]));
                        return;
                    } else if ( args.Length >= 2 ) {
                        if ( !args [ 1 ].All(char.IsDigit) ) {
                            ev.Sender.RAMessage(plugin.HelpMessage.Replace("%cmd", args [ 0 ]));
                            return;
                        }
                        bool OnlyHCZ = false;
                        if ( args.Length >= 3 ) {
                            string [] _t = plugin.TrueArguments.Split(',');
                            if(_t.Contains(args[2].ToLower())) OnlyHCZ = true;
                            else ev.Sender.RAMessage(plugin.NotRecognized.Replace("%arg", args [ 2 ]));
                        }
                        ev.Sender.RAMessage(plugin.Success.Replace("%s", args [ 1 ]).Replace("%value" , OnlyHCZ + ""));
                        if ( plugin.DoAnnouncement ) {
                            foreach ( ReferenceHub h in Player.GetHubs() )
                                h.Broadcast(plugin.AnnounceDuration, plugin.Announcement.Replace("%player", ev.Sender.Nickname).Replace("%s", args[1]));
                        }

                        if ( OnlyHCZ && plugin.CassieAnnounceHCZ) plugin.cassieMessage(plugin.CassieAnnouncement);
                        else if ( !OnlyHCZ && plugin.CassieAnnounceBoth ) plugin.cassieMessage(plugin.CassieAnnouncementBoth);
                        lightsOff(float.Parse(args [ 1 ]), OnlyHCZ);
                        return;
                    }
                    return;
                }
                #endregion
                return;
            } catch ( Exception e ) {
                Log.Error("Command error: " + e.StackTrace);
            }
        }
        #endregion

        public bool checkPermission( RACommandEvent ev , ReferenceHub sender , string perm) {
            if ( !sender.CheckPermission("lights.*") || !sender.CheckPermission("lights.") ) {
                ev.Sender.RAMessage(plugin.AccessDenied);
                return false;
            }
            return true;
        }

        public IEnumerator<float> StartTimer( float s ) {
            yield return Timing.WaitForSeconds(s);
            float _tmp = generateRandomNumber(plugin.FirstBlackoutMinDuration, plugin.FirstBlackoutMaxDuration);
            lightsOff(_tmp, false);
            if(plugin.DoMultipleBlackouts) Timing.RunCoroutine(StartTimers(_tmp + generateRandomNumber(plugin.TimeBetweenMin, plugin.TimeBetweenMax)));
        }

        public IEnumerator<float> StartTimers( float s ) {
            yield return Timing.WaitForSeconds(s);
            float _tmp = generateRandomNumber(plugin.BlackoutMinDuration, plugin.BlackoutMaxDuration);
            lightsOff(_tmp, false);
            if(count <= plugin.maxAmount)
            Timing.RunCoroutine(StartTimers(_tmp + generateRandomNumber(plugin.TimeBetweenMin, plugin.TimeBetweenMax)));
            count++;
        }

        public void lightsOff(float time, bool hczonly) {
            Generator079.generators [ 0 ].RpcCustomOverchargeForOurBeautifulModCreators(time, hczonly);
            if ( plugin.CassieAnnounceAuto ) plugin.cassieMessage(plugin.CassieAutoAnnouncement);
        }

        public float generateRandomNumber( float min, float max ) {
            if ( max <= min ) return min;
            return (float) new Random().NextDouble() * (max - min) + min;
        }

        public int generateRandomNumber( int min, int max ) {
            if ( max <= min ) return min;
            return new Random().Next(min, max);
        }
    }
}