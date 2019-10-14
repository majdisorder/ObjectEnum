using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System
{
    public abstract class ValueEnum<TEnum> : IEquatable<ValueEnum<TEnum>>
        where TEnum : struct
    {
        private readonly TEnum _value;
        private int? hashCode;

        #region ctors

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
                throw new TypeInitializationException(
                    nameof(ValueEnum<TEnum>),
                    new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        $"The provided value is not defined for this {nameof(ValueEnum<TEnum>)}"
                    )
                );
            }

            this._value = value;
        }

        #endregion ctors

        #region private methods

        private bool IsTypeEquivalent(Type otherType)
        {
            var myType = GetType();

            return otherType != null &&
                   (otherType.IsAssignableFrom(myType) ||
                    myType.IsAssignableFrom(otherType));
        }

        private static bool TryParse<TConcrete>(string input, bool ignoreCase, out TConcrete value, bool rethrow)
            where TConcrete : ValueEnum<TEnum>
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                try
                {
                    if (!Enum.TryParse<TEnum>(input, ignoreCase, out var enumVal))
                        throw new FormatException("Input string was not in a correct format");

                    value = NewValueEnum<TConcrete>(enumVal);

                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug
                        .Write(
                            $"{nameof(ValueEnum<TEnum>)}.{nameof(TryParse)} failed with: {ex.Message}. Safe to ignore.");

                    if (rethrow) throw;
                }
            }

            value = default;

            return false;
        }

        #endregion private methods

        /// <summary>
        /// Gets an immutable list of <c>Enum</c> values supported by this type.
        /// The result of this call is used by <see cref="IsDefined"/>.
        /// </summary>
        /// <returns>An immutable list of <c>Enum</c> values supported by this type.</returns>
        protected abstract IReadOnlyCollection<TEnum> GetDefinedValues();

        /// <summary>
        /// Indicates whether a specified value exists for this <see cref="ValueEnum&lt;TEnum&gt;"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns><see langword="true" /> if <paramref name="value" />is defined; otherwise, <see langword="false" />.</returns>
        public bool IsDefined(TEnum value)
            => GetDefinedValues().Contains(value); 

        /// <summary>
        /// Creates a new <see cref="ValueEnum&lt;TEnum&gt;"/> instance of the given concrete type.
        /// </summary>
        /// <param name="value">Underlying enum value.</param>
        /// <typeparam name="TConcrete">Type of the concrete <see cref="ValueEnum&lt;TEnum&gt;"/> implementation.</typeparam>
        /// <returns>A new instance of <see cref="TConcrete"/> with underlying enum value of <see cref="TEnum"/>.</returns>
        /// <exception cref="TypeInitializationException"></exception>
        /// <exception cref="TargetInvocationException"></exception>
        /// <exception cref="Exception"></exception>
        public static TConcrete NewValueEnum<TConcrete>(TEnum value)
            where TConcrete : ValueEnum<TEnum>
        {
            try
            {
                return Activator.CreateInstance(
                    typeof(TConcrete),
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                    null,
                    new object[] {value},
                    CultureInfo.InvariantCulture
                ) as TConcrete;
            }
            catch (TargetInvocationException e)
            {
                throw e?.InnerException ?? e;
            }
        }

        public override string ToString()
            => _value.ToString();

        #region equality

        public override bool Equals(object other)
            => other is ValueEnum<TEnum> otherValueEnum &&
               IsTypeEquivalent(other.GetType()) &&
               _value.Equals(otherValueEnum._value);

        public bool Equals(ValueEnum<TEnum> other)
            => Equals(other as object);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            if (hashCode.HasValue) return hashCode.Value;

            unchecked
            {
                hashCode = 0;
                hashCode = (hashCode * 397) ^ _value.GetHashCode();
                hashCode = (hashCode * 397) ^ GetType().GetHashCode();

                return hashCode.Value;
            }
        }

        public static bool operator ==(ValueEnum<TEnum> left, ValueEnum<TEnum> right)
            => Equals(left, right);

        public static bool operator !=(ValueEnum<TEnum> left, ValueEnum<TEnum> right)
            => !Equals(left, right);

        #endregion equality

        #region cast operators

        public static explicit operator int(ValueEnum<TEnum> x)
            => (int) (x._value as object);

        public static implicit operator TEnum(ValueEnum<TEnum> x)
            => x._value;

        #endregion cast operators

        #region parsing

        /// <summary>
        /// Converts the string representation of an enum to its <see cref="ValueEnum&lt;TEnum&gt;"/> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="input">A case-sensitive string containing a value to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="ValueEnum&lt;TEnum&gt;"/> equivalent of the value contained in <paramref name="input" />, if the conversion succeeded, or null if the conversion failed.
        /// The conversion fails if the <paramref name="input" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, or is not of the correct format.
        /// This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <typeparam name="TConcrete">Type of the concrete <see cref="ValueEnum&lt;TEnum&gt;"/> implementation.</typeparam>
        /// <returns><see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />.</returns>
        public static bool TryParse<TConcrete>(string input, out TConcrete result)
            where TConcrete : ValueEnum<TEnum>
            => TryParse(input, false, out result);

        /// <summary>
        /// Converts the string representation of an enum to its <see cref="ValueEnum&lt;TEnum&gt;"/> equivalent. A parameter specifies whether the operation is case-sensitive.  A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="input">A string containing a value to convert.</param>
        /// <param name="ignoreCase" />
        /// <param name="result">
        /// When this method returns, contains the <see cref="ValueEnum&lt;TEnum&gt;"/> equivalent of the value contained in <paramref name="input" />, if the conversion succeeded, or null if the conversion failed.
        /// The conversion fails if the <paramref name="input" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, or is not of the correct format.
        /// This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <typeparam name="TConcrete">Type of the concrete <see cref="ValueEnum&lt;TEnum&gt;"/> implementation.</typeparam>
        /// <returns><see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />.</returns>
        public static bool TryParse<TConcrete>(string input, bool ignoreCase, out TConcrete result)
            where TConcrete : ValueEnum<TEnum>
            => TryParse(input, ignoreCase, out result, false);

        /// <summary>
        /// Converts the string representation of an enum to its <see cref="ValueEnum&lt;TEnum&gt;"/> equivalent.
        /// </summary>
        /// <param name="input">A case-sensitive string containing a value to convert.</param>
        /// <typeparam name="TConcrete">Type of the concrete <see cref="ValueEnum&lt;TEnum&gt;"/> implementation.</typeparam>
        /// <returns>The <see cref="ValueEnum&lt;TEnum&gt;"/> equivalent of the value contained in <paramref name="input" /></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static TConcrete Parse<TConcrete>(string input)
            where TConcrete : ValueEnum<TEnum>
        {
            try
            {
                return TryParse<TConcrete>(input, true, out var value, true) ? value : default;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Unable to convert {input} to {typeof(TConcrete)}. See {nameof(Exception.InnerException)} for more details.",
                    ex
                );
            }
        }

        #endregion parsing
    }
}