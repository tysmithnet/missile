﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Missile.TextLauncher.Tests
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class FooSettings : ISettings
    {
        [Setting]
        public int X { get; set; }

        [Setting]
        public int Y;

        [SubSettings]
        public FooSubSettings FooSubSettings { get; set; } = new FooSubSettings();

        public string SomethingElse { get; set; }
    }                       
    
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class FooSubSettings : ISettings
    {
        public string X2 { get; set; }
    }
}