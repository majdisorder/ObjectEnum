using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System
{
    public abstract class ValueEnum<TEnum> : IEquatable<ValueEnum<TEnum>>
        where TEnum : struct
    {
        static ValueEnum()
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TEnum)))
            {
                throw new TypeInitializationException(
                    nameof(ValueEnum<TEnum>),
                    new ArgumentException("Type parameter must be an enum.", nameof(TEnum))
                );
            }
        }

        protected ValueEnum(TEnum value)
        {
            if (!IsDefined(value))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    value,
                    $"The provided value is not defined for this {nameof(ValueEnum<TEnum>)}"
                );
            }

            this.value = value;
        }

        private readonly TEnum value;

        protected abstract IReadOnlyCollection<TEnum> GetDefinedValues();

        private bool IsTypeEquivalent(Type otherType)
        {
            var myType = GetType();

            return otherType != null &&
                   (otherType.IsAssignableFrom(myType) ||
                    myType.IsAssignableFrom(otherType));
        }

        public virtual bool IsDefined(TEnum size)
            => GetDefinedValues().Contains(size);

        public override string ToString()
            => value.ToString();

        public override bool Equals(object other)
            => other is ValueEnum<TEnum> otherValueEnum &&
               IsTypeEquivalent(other.GetType()) &&
               value.Equals(otherValueEnum.value);

        public bool Equals(ValueEnum<TEnum> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return value.Equals(other.value);
        }

        public override int GetHashCode()
            => value.GetHashCode();

        public static bool operator ==(ValueEnum<TEnum> left, ValueEnum<TEnum> right)
            => Equals(left, right);

        public static bool operator !=(ValueEnum<TEnum> left, ValueEnum<TEnum> right)
            => !Equals(left, right);

        public static explicit operator int(ValueEnum<TEnum> x)
            => (int)(x.value as object);

        public static implicit operator TEnum(ValueEnum<TEnum> x)
            => x.value;

        public static bool TryParse<TConcrete>(string input, out TConcrete value)
            where TConcrete : ValueEnum<TEnum>
            => TryParse(input, false, out value);

        public static bool TryParse<TConcrete>(string input, bool ignoreCase, out TConcrete value)
            where TConcrete : ValueEnum<TEnum>
            => TryParse(input, ignoreCase, out value, false);

        private static bool TryParse<TConcrete>(string input, bool ignoreCase, out TConcrete value, bool rethrow)
            where TConcrete : ValueEnum<TEnum>
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                try
                {
                    if (Enum.TryParse<TEnum>(input, ignoreCase, out var enumVal))
                    {
                        value = Activator.CreateInstance(
                            typeof(TConcrete),
                            BindingFlags.CreateInstance | BindingFlags.NonPublic,
                            null,
                            new object[] { enumVal },
                            CultureInfo.InvariantCulture
                        ) as TConcrete;

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug
                        .Write($"{nameof(ValueEnum<TEnum>)}.{nameof(TryParse)} failed with: {ex.Message}. Safe to ignore.");

                    if (rethrow) throw;
                }
            }

            value = default;

            return false;
        }

        public static TConcrete Parse<TConcrete>(string input)
            where TConcrete : ValueEnum<TEnum>
        {
            try
            {
                return TryParse<TConcrete>(input, true, out var value, true) ?
                    value :
                    default;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Unable to convert {input} to {typeof(TConcrete)}. See {nameof(Exception.InnerException)} for more details.",
                    ex
                );
            }
        }
    }
}