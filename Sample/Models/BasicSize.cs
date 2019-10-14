using System.Collections.Generic;

namespace Sample.Models
{
    public class BasicSize : Size
    {
        public BasicSize(Value value)
            : base(value)
        {
        }

        private static readonly Value[] Values = new[]
        {
            Value.Unknown, Value.Small, Value.Medium, Value.Large
        };

        protected override IReadOnlyCollection<Value> GetDefinedValues()
            => Values;
    }
}