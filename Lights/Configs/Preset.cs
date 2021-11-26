// -----------------------------------------------------------------------
// <copyright file="Preset.cs" company="Beryl">
// Copyright (c) Beryl. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lights.Configs
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Missing XML comment for publicly visible type or member
    public class Preset<TEnum>
    {
        public Preset(TEnum location, ModifierType modifier, float duration = 15, params float[] arguments)
        {
            Duration = duration;
            Modifier = modifier;
            Location = location;
            Arguments = arguments;
        }

        public Preset()
        {
        }

        public TEnum Location { get; set; }

        public ModifierType Modifier { get; set; }

        public float Duration { get; set; }

        public float[] Arguments { get; set; }
    }
}
