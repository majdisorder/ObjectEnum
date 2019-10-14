using System.Collections.Generic;

namespace Sample.Models
{
    public class AnySize : Size
    {
        public AnySize(Value value)
            : base(value)
        {
        }

        private static readonly Value[] Values = new[]
        {
            Value.Unknown, Value.ExtraSmall, Value.Small, 
            Value.Medium, Value.ExtraLarge, Value.Large
        };

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }
}