using System.Collections.Generic;

namespace System.ObjectEnum.Tests.Models
{
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
}