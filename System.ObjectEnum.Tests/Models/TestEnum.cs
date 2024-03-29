﻿using System.Diagnostics.CodeAnalysis;

namespace System.ObjectEnum.Tests.Models
{
    
    internal abstract class TestEnum : ObjectEnum<TestEnum.Value>
    {
        public enum Value
        {
            Unknown = 0,
            First = 1,
            Second = 2,
            Third = 3
        }

        protected TestEnum(Value value)
            : base(value)
        {
        }
    }
}