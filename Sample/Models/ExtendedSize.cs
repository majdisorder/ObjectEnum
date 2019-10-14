using System.Collections.Generic;

namespace Sample.Models
{
    public class ExtendedSize : BasicSize
    {
        public ExtendedSize(Value value)
            : base(value)
        {
        }

        private static readonly Value[] Values = new[]
        {
            Value.Unknown, Value.ExtraSmall, Value.Small, Value.Medium, Value.Large, Value.ExtraLarge
        };

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }
}