# Lights
A simple yet EXTREMELY configurable EXILED Plugin that lets you turn the lights off.

### What does it do?
This plugin lets you turn off the lights of Light Containment Zone (LCZ) and/or Heavy Containment Zone (HCZ) using a command in the RemoteAdmin console. Overall, pretty simple! (It still needs [EXILED](https://github.com/galaxy119/EXILED "EXILED") to work tho!).

### Installation
As with any EXILED plugin, you must place the LightsCommand.dll file inside of your "%appdata%/Roaming/Plugins" folder.

### Commands
Arguments inside &lt;&gt; are required. [] means it's optional.
| Command | Description | Arguments |
| ------------- | ------------------------------ | -------------------- |
| `lights`   | Plugin's main command. (Can be modified! Yay!) | **&lt;Seconds&gt;** [Turn off in HCZ only?] |
| `lightsconfig` | Plugin's main configuration command | **&lt;set/variables/reload&gt;** |

**Info on lightsconfig's arguments:**
Set: *Lets you change a variable from in-game. (You still need to type "lightsconfig reload" for it to update)*
Variables: "Gives you a list of all variables available. (I still recommend checking this page as it contains more info)"
Reload: "Reload plugin's variables inside 7777-config.yml. (The other two had parenthesis, this one wants to be part of the cool variables too)"

### Configuration
These are the variables that should be added to your 7777-config.yml. Or simply download/copy the [config-file example](https://github.com/SebasCapo/Lights/blob/master/LightsPlugin/Example/7777-config.yml)
| Variable  | Description | Default value |
| ------------- | ------------- | ------------- |
| lights_command | Changes the main command. ("lights_reload" stays unchanged to avoid problems) | `light` |
| lights_alias | Changes the main command's alias | `ls` |
| lights_true_arguments | All different arguments that are accepted as "true". **(Don't use spaces!)** | `true,t,yes,y` |
| lights_announce | Whether or not to broadcast a message when lights go spooky | `true` |
| lights_announce_duration | For how long should the broadcast display | `10` |
| lights_announce_text | What should the broadcast display? (%player = Whoever used the command and %s = How long the blackout is going to last. | `<color=aqua>Lights have been turned off by %player for %ss! SpooOOOººOoooky!</color>` |
| lights_message_access_denied | Message displayed when player doesn't have permissions to use the command | `<color=red>Access denied.</color>` |
| lights_message_help | Message displayed when syntax is incorrect (%cmd = Displays the command used, it's just a cute detail) 1/2 | `<color=red>Command usage: " </color><color=yellow>%cmd <seconds> </color><color=#ab0be6>[true/yes] </color><color=red>"</color>\n<color=red><> is necessary -- [] is optional</color>` |
| ~~lights_message_help2~~ | This has become unused, add "\n" on your text to add a new line | `<color=red><> is necessary -- [] is optional</color>` |
| lights_message_not_recognized | Warning message if third argument is invalid (%arg = 3rd argument used on the command, default is "true") | `<color=red>Argument "%arg" can't be recognized, using default value</color>` |
| lights_message_success | Message displayed when you're a good boy (%s = How long the blackout's gonna last and %value = Whether or not the blackout's gonna be HCZ only or HCZ & LCZ) | `<color=#13c706>Lights have been turned off for:<b> %ss </b>(HCZ Only? : <b>%value</b>)</color>` |
| lights_cassie_announcement_hcz | This is what CASSIE says when light's are turned off in HCZ | heavy containment zone generator .g3 malfunction detected .g4 .g3 .g3 .g4 |
| lights_cassie_announcement_both | This is what CASSIE says when light's are turned off in HCZ & LCZ | generator .g3 malfunction detected .g4 .g3 .g3 .g4 |
| lights_cassie_announceforhcz | Toggles whether CASSIE announces lights are being turned off in HCZ | false |
| lights_cassie_announceforboth | Toggles whether CASSIE announces lights are being turned off in  | false |
| lights_cassie_makenoise | Should CASSIE's announcement make a sound at the start/end of his sentence? | true |
| lights_startblackout_toggle | Toggles whether there is automatic blackout(s) | false |
| lights_startblackout_delay_max | Once the round starts, how many seconds can pass for the first automatic blackout to happen? | 60f |
|lights_startblackout_delay_min | Once the round starts,  how many seconds should pass for the first auto-blackout to happen? | 50f |
| lights_startblackout_duration_max | Once the round starts, how many seconds can pass for the first automatic blackout to happen? | 15f |
|lights_startblackout_duration_min | Once the round starts,  how many seconds should pass for the first auto-blackout to happen? | 10f |
| lights_multipleblackout_toggle | Should there be multiple auto-blackouts after the first one? | false |
| lights_multipleblackout_duration_max | How long can the auto-blackout last for? | 15f |
| lights_multipleblackout_duration_min | How long will the auto-blackout at least last for? | 10f |
| lights_multipleblackout_timebetween_max | **NEW!** Maximum time between each auto-blackout | 35f |
| lights_multipleblackout_timebetween_min | **NEW!** Minimum time between each auto-blackout | 20f |
| ~lights_multipleblackout_timebetween~ | **REPLACED** Time between each auto-blackout | 5 |
| lights_multipleblackout_maxamount | **NEW!** Limits how many automatic blackouts happen instead of looping forever *(The "Start-Blackout" doesn't count towards the counter of auto-blackouts)* | 2 |
| lights_cassie_announcement_auto | What CASSIE says during an auto-blackout | generator .g3 automatic reactivation started .g3 .g4 |
| lights_cassie_announceauto | Toggles whether CASSIE announces auto-blackouts | true |

*Note: All the "%whatever" variables are **NOT** needed. They're there to make this plugin more customizable.

***IMPORTANT: Check [THIS](https://steamcommunity.com/sharedfiles/filedetails/?id=1577299753) post to see how CASSIE's system works.**

### Permissions
These are the permissions that should be added to your permissions.yml inside your "%appdata%/Roaming/Plugins/Exiled Permissions" folder.
| Permission  | Command |
| ------------- | ------------- |
| lights.light | `lights` | 
| lights.config | `lightsconfig` | 
| lights.set | `lightsconfig set <config_variable> <value..>` | 
| lights.reload | `lightsconfig reload` |
| lights.* | `All` | 

### That'd be all
Thanks for passing by, have a nice day! :)
