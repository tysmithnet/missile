﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Missile.TextLauncher.Conversion
{
    /// <summary>
    ///     Convenience class for a Type's inheritance
    /// </summary>
    public class TypeBreakDown
    {
        /// <summary>
        ///     Initializes a new instance of TypeBreakDown
        /// </summary>
        /// <param name="type"></param>
        public TypeBreakDown(Type type)
        {
            Interfaces.AddRange(type.GetInterfaces() ?? new Type[0]);
            BaseTypes = type.GetBaseTypes().ToList();
        }

        /// <summary>
        ///     Any interfaces a type implements
        /// </summary>
        public List<Type> Interfaces { get; } = new List<Type>();

        /// <summary>
        ///     Ancestor types
        /// </summary>
        public List<Type> BaseTypes { get; }
    }
}