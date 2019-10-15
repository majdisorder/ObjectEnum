using System.Collections.Generic;

namespace System.ObjectEnum.Tests.Models
{
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
}