using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.ObjectEnum.Tests.Models
{
    
    internal class InvalidEnum : ObjectEnum<int>
    {
        public InvalidEnum(int value)
            : base(value) { }

        protected override IReadOnlyCollection<int> GetDefinedValues()
            => new[] { 1, 2, 3 };
    }
}