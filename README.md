# ObjectEnum

ObjectEnum is a wrapper for enums, adding a certain degree of polymorphism.

## Examples

### Restrict enum values to a subset

Conssider the WorkDay class:

```csharp
public class WorkDay : ObjectEnum<DayOfWeek>
{
    public WorkDay(DayOfWeek value)
        : base(value) { }

    private static readonly DayOfWeek[] Values = new[]
    {
        DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
        DayOfWeek.Thursday, DayOfWeek.Friday
    };

    protected override IReadOnlyCollection<DayOfWeek> GetDefinedValues()
        => Values;
}

```

Which will produce the following results:

```csharp
var sunday = new WorkDay(DayOfWeek.Sunday); //throws exception

var monday = new WorkDay(DayOfWeek.Monday); //works fine
var label = $"{monday} is day {(int)monday}." //produces: "Monday is day 1."
var mondayIsAlwaysMonday = monday == DayOfWeek.Monday; //true, sorry...
```

### Inheritance

Conssider these classes:

```csharp
public class ChillDay : ObjectEnum<DayOfWeek>
{
    public ChillDay(DayOfWeek value)
        : base(value) { }

    private static readonly DayOfWeek[] Values = new[]
    {
        DayOfWeek.Saturday, DayOfWeek.Sunday
    };

    protected override IReadOnlyCollection<DayOfWeek> GetDefinedValues()
        => Values;
}

public class FunDay : ChillDay
{
    public FunDay(DayOfWeek value)
        : base(value) { }

    private static readonly DayOfWeek[] Values = new[]
    {
        DayOfWeek.Saturday, DayOfWeek.Sunday, DayOfWeek.Friday
    };

    protected override IReadOnlyCollection<DayOfWeek> GetDefinedValues()
        => Values;
}
```

Which will produce the following results:

```csharp
var sunday = new ChillDay(DayOfWeek.Sunday);
var funday = new FundDay(DayOfWeek.Sunday);

var sundayIsFunday = sunday == funday; //true
```

In this case the comparison will return true, because FundDay inherits ChillDay. However in the case of a Workday, this would not be the case as there is no relation between a WorkDay and a FundDay.

```csharp
var friday = new WorkDay(DayOfWeek.Friday);
var funday = new FundDay(DayOfWeek.Friday);

var workFridayIsNoFunFriday = friday == funday; //false
```

### Switch cases

You can easily use a ObjectEnum in a switch case, however you will need to cast it to the appropriate enum.

```csharp
var friday = new WorkDay(DayOfWeek.Friday);

switch((DayOfWeek)friday){
    case DayOfWeek.Monday:
        //do something monday related
        break;
        /*...*/
    case DayOfWeek.Friday:
        //do something friday related
        break;
}
```

Or cast it to an integer.

```csharp
var friday = new WorkDay(DayOfWeek.Friday);

switch((int)friday){
    case 1:
        //do something monday related
        break;
        /*...*/
    case 5:
        //do something friday related
        break;
}
```

### Declaring your own ObjectEnums

```csharp

//you can optionally declare an abstract base type for your implementations
public abstract class Size : ObjectEnum<Size.Value>
{
    //define a single enum containing all possible values
    //it can be a nested enum like it is here,
    //but this is not required
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
        : base(value) { }
}

public class BasicSize : Size
{
    public BasicSize(Value value)
        : base(value) { }

    //declaring a static Values array reduces
    //the overhead on creating a new array each time
    //we instantiate a BasicSize object
    private static readonly Value[] Values = new[]
    {
        Value.Unknown, Value.Small, Value.Medium, Value.Large
    };

    //override GetDefinedValues to restrict the possible values for BasicSize
    protected override IReadOnlyCollection<Value> GetDefinedValues()
        => Values;
}

//inherit BasicSize so we can compare the values
public class ExtendedSize : BasicSize
{
    public ExtendedSize(Value value)
        : base(value) { }

    //not the prefered way, but should work
    protected override IReadOnlyCollection<Value> GetDefinedValues()
        => new[] {
            Value.Unknown, Value.ExtraSmall, Value.Small, Value.Medium, Value.Large, Value.ExtraLarge
        };
}
```

### Other helpful stuff

As you may have noticed, it is easy to cast between a ObjectEnum and its underlying enum Type, as well as casting to the int representation.

Besides this, a number of helper methods are defined to help you instantiate ObjectEnums

```csharp
var isFriday = WorkDay.TryParse("Friday", out var friday); //returns true, friday = new WorkDay(DayOfWeek.Friday)
var isMonday = WorkDay.TryParse("monday", ignoreCase: false, out var mondaytues); //returns true, monday = new WorkDay(DayOfWeek.Monday)
var tuesday =  WorkDay.Parse<WorkDay>("tuesday"); //returns new WorkDay(DayOfWeek.Tuesday)
var wednessday = WorkDay.Create<WorkDay>(DayOfWeek.Wednessday);  //returns new WorkDay(DayOfWeek.Wednessday)
```

You can also use the various overloads of `IsDefined` to determine whether a certain value is valid for the given ObjectEnum.
