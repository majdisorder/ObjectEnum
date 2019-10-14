using System.Collections.Generic;

namespace Sample.Models
{
    public class SmallSize : Size
    {
        public SmallSize(Value value)
            : base(value)
        {
        }

        private static readonly Value[] Values = new[]
        {
            Value.Unknown, Value.ExtraSmall, Value.Small
        };

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }
}