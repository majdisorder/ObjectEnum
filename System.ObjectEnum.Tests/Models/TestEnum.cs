using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.ObjectEnum.Tests.Models
{
    [ExcludeFromCodeCoverage]
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

    [ExcludeFromCodeCoverage]
    internal abstract class AltTestEnum : ObjectEnum<AltTestEnum.Value>
    {
        public enum Value
        {
            Unknown = 0,
            First = 1,
            Second = 2,
            Third = 3
        }

        protected AltTestEnum(Value value)
            : base(value)
        {
        }
    }
    
    [ExcludeFromCodeCoverage]
    internal class SelfContainedEnum : TestEnum
    {
        protected SelfContainedEnum(Value value)
            : base(value)
        {
        }

        private static readonly IReadOnlyCollection<Value> Values = new[] {Value.Unknown, Value.First};

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }

    [ExcludeFromCodeCoverage]
    internal class FirstEnum : TestEnum
    {
        public FirstEnum(Value value)
            : base(value)
        {
        }

        private static readonly IReadOnlyCollection<Value> Values = new[] {Value.Unknown, Value.First};

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }
    
    [ExcludeFromCodeCoverage]
    internal class AltFirstEnum : AltTestEnum
    {
        public AltFirstEnum(Value value)
            : base(value)
        {
        }

        private static readonly IReadOnlyCollection<Value> Values = new[] {Value.Unknown, Value.First};

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }

    [ExcludeFromCodeCoverage]
    internal class SecondEnum : TestEnum
    {
        public SecondEnum(Value value)
            : base(value)
        {
        }

        private static readonly IReadOnlyCollection<Value> Values = new[] {Value.Unknown, Value.Second};

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }

    [ExcludeFromCodeCoverage]
    internal class CombinedEnum : TestEnum
    {
        public CombinedEnum(Value value)
            : base(value)
        {
        }

        private static readonly IReadOnlyCollection<Value> Values = new[] {Value.Unknown, Value.First, Value.Second};

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }

    [ExcludeFromCodeCoverage]
    internal class DerivedCombinedEnum : CombinedEnum
    {
        public DerivedCombinedEnum(Value value)
            : base(value)
        {
        }

        private static readonly IReadOnlyCollection<Value> Values = new[]
            {Value.Unknown, Value.First, Value.Second, Value.Third};

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }

    [ExcludeFromCodeCoverage]
    internal abstract class AbstractDerivedCombinedEnum : CombinedEnum
    {
        protected AbstractDerivedCombinedEnum(Value value)
            : base(value)
        {
        }

        private static readonly IReadOnlyCollection<Value> Values = new[]
            {Value.Unknown, Value.First, Value.Second, Value.Third};

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }

    [ExcludeFromCodeCoverage]
    internal class ConcreteDerivedCombinedEnum : CombinedEnum
    {
        public ConcreteDerivedCombinedEnum(Value value)
            : base(value)
        {
        }
    }
}