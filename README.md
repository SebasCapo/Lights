[![Github All Releases](https://img.shields.io/github/downloads/SebasCapo/Lights/total.svg)](https://github.com/SebasCapo/Lights/releases) [![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://github.com/SebasCapo/Lights/graphs/commit-activity) [![GitHub license](https://img.shields.io/github/license/Naereen/StrapDown.js.svg)](https://github.com/SebasCapo/Lights/blob/main/LICENSE)
<a href="https://github.com/SebasCapo/Lights/releases"><img src="https://img.shields.io/github/v/release/SebasCapo/Lights?include_prereleases&label=Release" alt="Releases"></a>
<a href="https://discord.gg/PyUkWTg"><img src="https://img.shields.io/discord/656673194693885975?color=%23aa0000&label=EXILED" alt="Support"></a>

# Lights 4.0
An extremely configurable SCP:SL-EXILED Plugin that allows you to control many aspects of it's facility's power systems.

### What does it do?
This plugin lets you change a variety of different settings per room and/or zone, these settings vary from changing a lights color, to turning them off! You can even lock all the doors in a room indefinitely or temporarily.

You can use this to have automatic events using the **preset** system, or affecting a room/zone of your liking using the `lights` RA/Console command.

This plugin also has a **Smart Tesla Gates** mode, which makes tesla gates not trigger when the light settings in their room reach a certain minimum, such as a specific light intensity, color or simply them being turned off. 

### Installation
As with any EXILED plugin, you must place the Lights.dll file inside of your "/Exiled/Plugins" folder.

([EXILED](https://github.com/galaxy119/EXILED "EXILED") is required for this plugin to work).

### Commands
Arguments inside &lt;&gt; are required. [] means it's optional.

- **WARNING:** The main command's name (and it's aliases) can be modified inside the config, so it might be different on the server you are in.
- **2nd WARNING:** Depending on your system's culture settings, decimal values in-game might need to use commas instead of dots. (You can most likely ignore this warning, but here's an example for those suffering from this: `0.7` > `0,7`)

| Command | Description |
| ------------------ | ------------------------------ |
| `lights` | This will print you a help message with examples on how to use this command. |
| `lights` `<presetId>` | This will run one of the presets from the server's config. |
| `lights` `<roomType/zoneType>` `<duration>` `<modifierType>` `[parameters]` | This will run a temporary/permanent custom effect on a specific room/entire zone, [click here](https://github.com/SebasCapo/Lights/blob/master/Lights/ModifierType.cs "ModifierType.cs") for a more up-to-date list of all modifier types |

| Modifiers | Description | Arguments | Example |
| ------------ | -------------------------- | ------------------------------ | --------- |
| `Color` | Will modify the color of all the lights in a room. | `<r>` `<g>` `<b>` | `255` `100` `255` |
| `Intensity` | Will modify the light's intensity in a room. | `<intensity>` | `0.5` |
| `Blackout` | Will turn all lights in a room off. | `N/A` | `N/A` |
| `Lockdown` | Will lock all doors in a room. (Works weird) | `N/A` | `N/A` |

### Configuration

Exiled 2.0 or above has auto-generated config files, alongside documentation! So check out your config file for more information on it!

### Permissions
These are the permissions that should be added to your permissions.yml inside your "/Exiled/Configs" folder.
| Permission  | Description |
| ------------- | ------------- |
| lights.presets | Gives access to the `lights <PresetID>` command |
| lights.custom | Gives access to the `lights <roomType/zoneType> <duration> etcetc...` command |
| lights.* | Gives all the permissions above. | 

### That'd be all
Thank you for passing by, have a nice day! :)
