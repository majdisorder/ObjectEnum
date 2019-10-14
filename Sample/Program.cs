using Sample.Models;
using System;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //system enums
            //initializing a WeekDay with a value of Sunday  value won't work
            Console.WriteLine("System enum examples");
            Console.WriteLine(
                $"Initialize {nameof(WeekDay)} with {nameof(DayOfWeek.Sunday)} shouldn't be possible'");
            try
            {
                var s = new WeekDay(DayOfWeek.Sunday);
                Console.WriteLine("FAIL");
            }
            catch
            {
                Console.WriteLine("OK");
            }

            //mondays will always be mondays (sorry...)
            Console.WriteLine();
            Console.WriteLine(
                $"A {nameof(WeekDay)} with value {nameof(DayOfWeek.Monday)} should equal {nameof(DayOfWeek)} with value  {nameof(DayOfWeek.Monday)}");
            var monday = new WeekDay(DayOfWeek.Monday);
            Console.WriteLine(monday == DayOfWeek.Monday ? "OK" : "FAIL");


            //custom enums
            //initializing with an invalid enum value won't work
            Console.WriteLine();
            Console.WriteLine("Custom enum examples");
            Console.WriteLine(
                $"Initialize {nameof(SmallSize)} with {nameof(Size.Value.Large)} shouldn't be possible'");
            try
            {
                var s = new SmallSize(Size.Value.Large);
                Console.WriteLine("FAIL");
            }
            catch
            {
                Console.WriteLine("OK");
            }

            var xSmall = new SmallSize(Size.Value.ExtraSmall);

            Console.WriteLine();
            Console.WriteLine($"{nameof(SmallSize)} with value {nameof(Size.Value.ExtraSmall)} ToString:");
            Console.WriteLine(xSmall);

            //cast object to  underlying enum representation
            Console.WriteLine();
            Console.WriteLine($"Cast {nameof(SmallSize)} to {nameof(Size.Value)}");
            Console.WriteLine($"{(Size.Value)xSmall} = {(Size.Value)xSmall}");

            //cast object to integer representation
            Console.WriteLine();
            Console.WriteLine($"Cast {nameof(SmallSize)} to int");
            Console.WriteLine($"{xSmall} = {(int)xSmall}");

            //compare object against underlying enum value in a switch 
            Console.WriteLine();
            Console.WriteLine($"Switch {nameof(SmallSize)} comparing to {nameof(Size.Value)}");
            switch ((Size.Value)xSmall)
            {
                case Size.Value.ExtraSmall:
                    Console.WriteLine("OK");
                    break;
                default:
                    Console.WriteLine("FAIL");
                    break;
            }

            //compare object against underlying int value in a switch 
            Console.WriteLine();
            Console.WriteLine($"Switch {nameof(SmallSize)} comparing to int");
            switch ((int)xSmall)
            {
                case (int)Size.Value.ExtraSmall:
                    Console.WriteLine("OK");
                    break;
                default:
                    Console.WriteLine("FAIL");
                    break;
            }

            //comparing two objects with the same underlying enum,
            //and no inheritance relation between the types
            var realSmall = new SmallSize(Size.Value.Small);
            var anySmall = new AnySize(Size.Value.Small);

            Console.WriteLine();
            Console.WriteLine($"{nameof(realSmall)} does not equal {nameof(anySmall)} (2 tests)");
            Console.WriteLine(realSmall != anySmall ? "OK" : "FAIL");
            Console.WriteLine(anySmall != realSmall ? "OK" : "FAIL");


            //comparing two objects with the same underlying enum,
            //and derived types
            var basicSmall = new BasicSize(Size.Value.Small);
            var extendedSmall = new ExtendedSize(Size.Value.Small);

            Console.WriteLine();
            Console.WriteLine($"{nameof(basicSmall)} equals {nameof(extendedSmall)} (2 tests)");
            Console.WriteLine(basicSmall == extendedSmall ? "OK" : "FAIL");
            Console.WriteLine(extendedSmall == basicSmall ? "OK" : "FAIL");
        }
    }
}