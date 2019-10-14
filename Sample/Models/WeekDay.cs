using System;
using System.Collections.Generic;

namespace Sample.Models
{
    public class WeekDay : ValueEnum<DayOfWeek>
    {
        public WeekDay(DayOfWeek value)
            : base(value) { }

        private static readonly DayOfWeek[] Values = new[]
        {
            DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
            DayOfWeek.Thursday, DayOfWeek.Friday
        };

        protected override IReadOnlyCollection<DayOfWeek> GetDefinedValues()
            => Values;
    }
}
