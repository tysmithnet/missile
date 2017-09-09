﻿using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace Missile.TextLauncher
{
    [Export(typeof(Filter<object, object>))]
    public class TakeFilter : Filter<object, object>
    {
        public override IObservable<object> Process(IObservable<object> source)
        {
            return source.Take(5);
        }
    }
}