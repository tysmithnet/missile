﻿using System;
using System.Reflection;

namespace Missile.TextLauncher.Provision
{
    public class RegisteredProvider
    {
        public RegisteredProvider(IProvider instance, Type iProviderInterface)
        {
            ProviderInstance = instance;
            var namePropertyInfo = iProviderInterface.GetProperty("Name");
            Name = (string) namePropertyInfo.GetValue(instance);
            DestinationType = iProviderInterface.GenericTypeArguments[0];
            ProvideMethodInfo = iProviderInterface.GetMethod("Provide");
        }

        protected internal RegisteredProvider()
        {
        }

        public string Name { get; internal set; }
        public Type DestinationType { get; internal set; }
        public IProvider ProviderInstance { get; internal set; }
        public MethodInfo ProvideMethodInfo { get; internal set; }

        public virtual object Provide(string[] args)
        {
            return ProvideMethodInfo.Invoke(ProviderInstance, new object[] {args});
        }
    }
}