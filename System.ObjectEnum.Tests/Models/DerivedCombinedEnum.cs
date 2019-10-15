using System.Collections.Generic;

namespace System.ObjectEnum.Tests.Models
{
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
}