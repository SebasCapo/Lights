[![Github All Releases](https://img.shields.io/github/downloads/SebasCapo/Lights/total.svg)](https://github.com/SebasCapo/Lights/releases) [![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://github.com/SebasCapo/Lights/graphs/commit-activity) [![GitHub license](https://img.shields.io/github/license/Naereen/StrapDown.js.svg)](https://github.com/SebasCapo/Lights/blob/main/LICENSE)
<a href="https://github.com/SebasCapo/Lights/releases"><img src="https://img.shields.io/github/v/release/SebasCapo/Lights?include_prereleases&label=Release" alt="Releases"></a>
<a href="https://discord.gg/PyUkWTg"><img src="https://img.shields.io/discord/656673194693885975?color=%23aa0000&label=EXILED" alt="Support"></a>

# Lights 3.0
A simple yet EXTREMELY configurable EXILED Plugin that lets you turn the lights off.

### What does it do?
This plugin lets you turn off the lights of Light Containment Zone (LCZ) and/or Heavy Containment Zone (HCZ) using a command in the RemoteAdmin console. Overall, pretty simple! (It still needs [EXILED](https://github.com/galaxy119/EXILED "EXILED") to work tho!).

### Installation
As with any EXILED plugin, you must place the Lights.dll file inside of your "/Exiled/Plugins" folder.

### Commands
Arguments inside &lt;&gt; are required. [] means it's optional.
| Command | Description | Arguments |
| ------------- | ------------------------------ | -------------------- |
| `lights`   | Plugin's main command. (Can be modified! Yay!) | **&lt;Seconds&gt;** [Turn off in HCZ only?] |

**Info on lightsconfig's arguments:**
Set: *Lets you change a variable from in-game. (You still need to type "lightsconfig reload" for it to update)*
Variables: "Gives you a list of all variables available. (I still recommend checking this page as it contains more info)"
Reload: "Reload plugin's variables inside 7777-config.yml. (The other two had parenthesis, this one wants to be part of the cool variables too)"

### Configuration

Exiled 2.0 now has auto-generated config files, alongside documentation! So check out your config file for more information on it!

### Permissions
These are the permissions that should be added to your permissions.yml inside your "/Exiled/Configs" folder.
| Permission  | Command |
| ------------- | ------------- |
| lights.light | `lights` |
| lights.* | `All` | 

### That'd be all
Thanks for passing by, have a nice day! :)
