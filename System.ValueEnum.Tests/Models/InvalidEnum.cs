using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.ValueEnum.Tests.Models
{
    [ExcludeFromCodeCoverage]
    internal class InvalidEnum : ValueEnum<int>
    {
        public InvalidEnum(int value)
            : base(value) { }

        protected override IReadOnlyCollection<int> GetDefinedValues()
            => new[] { 1, 2, 3 };
    }
}