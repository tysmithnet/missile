﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;

namespace Missile.TextLauncher.ListPlugin
{
    public class StringListDestinationItem : TextBlock, IListDestinationItem
    {
        [ExcludeFromCodeCoverage]
        public Guid Id { get; } = Guid.NewGuid();
    }
}