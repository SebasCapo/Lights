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
| `lights_reload` | Reload plugin's variables inside 7777-config.yml | **None. Even more yay!** |

### Configuration
These are the variables that should be added to your 7777-config.yml. Or simply download/copy the [config-file example](https://github.com/SebasCapo/Lights/blob/master/LightsPlugin/Example/7777-config.yml)
| Variable  | Description | Default value |
| ------------- | ------------- | ------------- |
| lights_command | Changes the main command. ("lights_reload" stays unchanged to avoid problems) | `light` |
| lights_alias | Changes the main command's alias | `ls` |
| lights_true_arguments | All different arguments that are accepted as "true". **(Don't use spaces!)** | `true,t,yes,y` |
| lights_announce | Whether or not to broadcast a message when lights go spooky | `true` |
| lights_announce_duration | For how long should the broadcast display | `10` |
| lights_announce_text | What should the broadcast display? | `<color=aqua>Lights have been turned off by %player for %ss! SpooOOOººOoooky!</color>` |
| lights_message_access_denied | Message displayed when player doesn't have permissions to use the command | `<color=red>Access denied.</color>` |
| lights_message_help | Message displayed when syntax is incorrect 1/2 | `<color=red>Command usage: " </color><color=yellow>%cmd <seconds> </color><color=#ab0be6>[true/yes] </color><color=red>"</color>` |
| lights_message_help2 | Message display when syntax is incorrect 2/2 | `<color=red><> is necessary -- [] is optional</color>` |
| lights_message_not_recognized | Warning message if third argument is invalid | `<color=red>Argument "%arg" can't be recognized, using default value</color>` |
| lights_message_success | Message displayed when you're a good boy | `<color=#13c706>Lights have been turned off for:<b> %ss </b>(HCZ Only? : <b>%value</b>)</color>` |

### Permissions
These are the permissions that should be added to your permissions.yml inside your "%appdata%/Roaming/Plugins/Exiled Permissions" folder.
| Permission  | Command |
| ------------- | ------------- |
| lights.ls | `lights` | 
| lights.reload | `lights_reload` | 
| lights.* | `All` | 

### That'd be all
Thanks for passing by, have a nice day! :)
