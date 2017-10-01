﻿using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Missile.TextLauncher.Destination;

namespace Missile.TextLauncher.ListPlugin
{
    [Export(typeof(IDestination))]
    public class ListDestination : IDestination<UIElement>
    {
        [Import]
        protected internal IUiFacade UiFacade { get; set; }

        public string Name { get; set; } = "list";

        public Task ProcessAsync(IObservable<UIElement> source)
        {
            var outputControl = new ListDestinationOutput(source);
            UiFacade.SetOutputControl(outputControl);
            var tcs = new TaskCompletionSource<object>();
            source.Subscribe(item =>
                {
                    ;
                },
                exception => { tcs.TrySetException(exception); }, () => { tcs.TrySetResult(null); });
            return tcs.Task;
        }
    }
}