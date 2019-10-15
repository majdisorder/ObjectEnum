namespace System.ObjectEnum.Tests.Models
{
    internal abstract class AltTestEnum : ObjectEnum<AltTestEnum.Value>
    {
        public enum Value
        {
            Unknown = 0,
            First = 1,
            Second = 2,
            Third = 3
        }

        protected AltTestEnum(Value value)
            : base(value)
        {
        }
    }
}