using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;

namespace Lights {
    public class LightsConfig : IConfig {

        public bool IsEnabled { get; set; } = true;
        public bool IsEnabledCustom { get; set; } = true;

        [Description("Should the lights be turned off automatically.")]
        public string Command { get; set; } = "lights";
        public string CommandAlias { get; set; } = "ls";
        [Description("Which arguments are considered as \"true\" for the third argument in the Light command. (Example: Adding \"yes\" to this list, will make it so using \"ls 15 yes\", turns off lights in HCZ only). By the way, don't use uppercase!")]
        public List<string> AcceptedArguments { get; set; } = new List<string> 
        {
            "yes", "y", "true", "t", "hczonly"
        };

        [Description("Should the lights be turned off automatically.")]
        public bool DoAutomaticBlackout { get; set; } = true;

        [Description("Should teslas be disabled during a blackout.")]
        public bool DisableTeslas { get; set; } = true;

        [Description("Should doors be open/closed when a blackout starts?")]
        public bool ModifyDoors { get; set; } = false;
        [Description("Should the doors go back to their original state when lights come back? (This may cause issues to your server performance, clients won't be affected by this)")]
        public bool RestoreDoors { get; set; } = true;
        [Description("Types of Door that can be open by the blackout. (Types: Standard, HeavyGate & Checkpoint)")]
        public List<Door.DoorTypes> OpenableDoors { get; set; } = new List<Door.DoorTypes> {
            Door.DoorTypes.Checkpoint, Door.DoorTypes.HeavyGate, Door.DoorTypes.Standard
        };
        [Description("Doors that have the following strings in their name will be ignored. (This and the previous configs will be ignored if 'OpenDoors' is set to 'false')")]
        public List<string> BlacklistedDoors { get; set; } = new List<string> {
            "106", "173"
        };

        [Description("How many seconds should this wait before shutting off the lights for the first time.")]
        public float startTimerMin { get; set; } = 30f;
        public float startTimerMax { get; set; } = 45f;


        [Description("Should the lights be turned off automatically repeatedly after the first one.")]
        public bool doMultipleBlackouts { get; set; } = true;

        [Description("How many seconds should this wait between each \"blackout\".")]
        public float timeBetweenMin { get; set; } = 45f;
        public float timeBetweenMax { get; set; } = 60f;

        [Description("For how long will the lights be turned off?")]
        public float blackoutDurationMin { get; set; } = 15f;
        public float blackoutDurationMax { get; set; } = 20f;
        [Description("Should the blackout duration be added to the delay between each automatic blackout?")]
        public bool addDuration { get; set; } = true;

        [Description("Chance for the automatic blackout to happen in Heavy Containment Only.")]
        public int hczOnlyChance { get; set; } = 50;

        #region Broadcasts
        [Description("Should a broadcast be shown when the lights are turned off?")]
        public bool DoBroadcastMessage { get; set; } = false;
        [Description("Should Broadcasts be cleared when this one's shown.")]
        public bool ClearBroadcasts { get; set; } = true;

        [Description("This is the broadcast shown when the lights are turned off in Heavy Containment Zone only. (%ss = blackout duration)")]
        public string broadcastMessageHczOnly { get; set; } = "<color=aqua>Lights have been turned off for %ss! SpooOOOOoooky!</color>";
        public ushort broadcastMessageHczOnlyDuration { get; set; } = 5;

        [Description("This is the broadcast shown when the lights are turned off in the entire facility. (%ss = blackout duration)")]
        public string broadcastMessageBoth { get; set; } = "<color=aqua>Lights have been turned off for %ss! SpooOOOOoooky!</color>";
        public ushort broadcastMessageBothDuration { get; set; } = 5;
        #endregion

        #region Cassie
        [Description("Should Cassie announce when the lights are turned off?")]
        public bool DoCassieMessages { get; set; } = true;
        [Description("Cassie Settings")]
        public bool MakeHold { get; set; } = false;
        public bool MakeNoise { get; set; } = false;

        [Description("This is what Cassie says if the lights are turned off in Heavy Containment Zone only. (%ss = blackout duration)")]
        public string cassieMessageHczOnly { get; set; } = "heavy containment zone generator .g3 malfunction detected .g4 .g3 .g3 .g4";
        [Description("This is what Cassie says if the lights are turned off in the entire facility. (%ss = blackout duration)")]
        public string cassieMessageBoth { get; set; } = "generator .g3 malfunction detected .g4 .g3 .g3 .g4";
        #endregion

    }
}
