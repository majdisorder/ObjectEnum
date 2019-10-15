using System;

namespace Sample.Models
{
    public abstract class Size : ObjectEnum<Size.Value>
    {
        public enum Value
        {
            Unknown = 0,
            ExtraSmall = 1,
            Small = 2,
            Medium = 3,
            Large = 4,
            ExtraLarge = 5
        }

        protected Size(Value value)
            : base(value)
        {
        }
    }
}