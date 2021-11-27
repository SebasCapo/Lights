// -----------------------------------------------------------------------
// <copyright file="ModifierType.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights
{
    /// <summary>
    /// Every type of Modifier possible for <see cref="Configs.Preset{TEnum}">presets</see>.
    /// </summary>
    public enum ModifierType
    {
        /// <summary>
        /// Will modify light's intensity in a room.
        /// </summary>
        Intensity,

        /// <summary>
        /// Will turn all lights in a room off.
        /// </summary>
        Blackout,

        /// <summary>
        /// Will lock all doors in a room.
        /// </summary>
        Lockdown,

        /// <summary>
        /// Will change the color of all lights in a room.
        /// </summary>
        Color,
    }
}
