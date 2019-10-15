using System.Collections.Generic;

namespace System.ObjectEnum.Tests.Models
{
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
}