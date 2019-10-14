using System.Collections.Generic;

namespace Sample.Models
{
    public class LargeSize : Size
    {
        public LargeSize(Value value)
            : base(value)
        {
        }

        private static readonly Value[] Values = new[]
        {
            Value.Unknown, Value.ExtraLarge, Value.Large
        };

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }
}