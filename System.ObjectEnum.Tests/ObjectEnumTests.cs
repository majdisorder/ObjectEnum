using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using System.ObjectEnum.Tests.Models;

[assembly: ExcludeFromCodeCoverage]

namespace System.ObjectEnum.Tests
{
    public class ObjectEnumTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Initialize_WithNonEnumTypeParameter_ShouldThrowTypeInitializationException()
        {
            Assert.Throws<TypeInitializationException>(
                () =>
                {
                    var a = new InvalidEnum(1);
                }
            );
        }

        [Test]
        public void Initialize_WithOuOfRangeEnumValueArgument_ShouldThrowTypeInitializationException()
        {
            Assert.Throws<TypeInitializationException>(
                () =>
                {
                    var a = new FirstEnum(TestEnum.Value.Second);
                }
            );
        }

        [Test]
        public void Cast_ToUnderlyingEnum_ShouldReturnInstanceOfUnderlyingEnum()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = (TestEnum.Value)testValue;

            Assert.IsInstanceOf<TestEnum.Value>(result);
        }

        [Test]
        public void Cast_ToUnderlyingEnum_EnumValuesShouldBeEqual()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = (TestEnum.Value)testValue;

            Assert.AreEqual(TestEnum.Value.First, result);
        }

        [Test]
        public void Cast_ToInteger_ShouldReturnInstanceOfInt()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = (int)testValue;

            Assert.IsInstanceOf<int>(result);
        }

        [Test]
        public void Cast_ToInteger_IntegerValuesShouldBeEqual()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = (int)testValue;

            Assert.AreEqual((int)TestEnum.Value.First, result);
        }

        [Test]
        public void ToString_WithValidEnumValue_ShouldEqualToStringOfEnumValue()
        {
            var testString = TestEnum.Value.First.ToString();
            var subject = new FirstEnum(TestEnum.Value.First);

            var result = subject.ToString();

            Assert.AreEqual(testString, result);
        }

        [Test]
        public void Parse_WithValidName_ShouldReturnObjectEnumWithCorrespondingValue()
        {
            var testString = TestEnum.Value.First.ToString();

            var result = (TestEnum.Value)FirstEnum.Parse<FirstEnum>(testString);

            Assert.AreEqual(
                TestEnum.Value.First,
                result
            );
        }

        [Test]
        public void Parse_WithInvalidName_ShouldThrowFormatException()
        {
            Assert.Throws<FormatException>(
                () =>
                {
                    var result = FirstEnum.Parse<FirstEnum>("SomeString");
                }
            );
        }

        [Test]
        public void Parse_WithValidNameButUndefined_ShouldThrowInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    var result = FirstEnum.Parse<FirstEnum>(TestEnum.Value.Second.ToString());
                }
            );
        }

        [Test]
        public void TryParse_WithValidName_ShouldReturnTrue()
        {
            var testString = TestEnum.Value.First.ToString();

            var result = FirstEnum.TryParse<FirstEnum>(testString, out var value);

            Assert.IsTrue(result);
        }

        [Test]
        public void TryParse_WithValidName_ShouldSetOutObjectEnumWithCorrespondingValue()
        {
            var testString = TestEnum.Value.First.ToString();

            FirstEnum.TryParse<FirstEnum>(testString, out var result);

            Assert.AreEqual(
                TestEnum.Value.First,
                (TestEnum.Value)result
            );
        }

        [Test]
        public void TryParse_WithInvalidName_ShouldReturnFalse()
        {
            var testString = "SomeString";

            var result = FirstEnum.TryParse<FirstEnum>(testString, out var value);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryParse_WithInvalidCaseInName_ShouldReturnFalse()
        {
            var testString = TestEnum.Value.First.ToString().ToLower();

            var result = FirstEnum.TryParse<FirstEnum>(testString, out var value);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryParse_WithInvalidCaseInNameAndIgnoreCase_ShouldReturnTrue()
        {
            var testString = TestEnum.Value.First.ToString().ToLower();

            var result = FirstEnum.TryParse<FirstEnum>(testString, true, out var value);

            Assert.IsTrue(result);
        }

        [Test]
        public void TryParse_WithInvalidName_ShouldSetOutObjectEnumWithDefault()
        {
            var testString = "SomeString";

            FirstEnum.TryParse<FirstEnum>(testString, out var result);

            Assert.IsNull(result);
        }

        [Test]
        public void TryParse_WithValidNameButUndefined_ShouldReturnFalse()
        {
            var testString = TestEnum.Value.Second.ToString();

            var result = FirstEnum.TryParse<FirstEnum>(testString, out var value);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryParse_WithValidNameButUndefined_ShouldSetOutObjectEnumWithDefault()
        {
            var testString = TestEnum.Value.Second.ToString();

            FirstEnum.TryParse<FirstEnum>(testString, out var result);

            Assert.IsNull(result);
        }

        [Test]
        public void GetHashCode_ForSameTypeAndSameValue_ShouldBeEqual()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            var result1 = testValue1.GetHashCode();
            var result2 = testValue2.GetHashCode();

            Assert.AreEqual(result1, result2);
        }

        [Test]
        public void GetHashCode_ForSameTypeAndDifferentValue_ShouldNotBeEqual()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.Unknown);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            var result1 = testValue1.GetHashCode();
            var result2 = testValue2.GetHashCode();

            Assert.AreNotEqual(result1, result2);
        }

        [Test]
        public void GetHashCode_ForDifferentTypeAndSameValue_ShouldNotBeEqual()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result1 = testValue1.GetHashCode();
            var result2 = testValue2.GetHashCode();

            Assert.AreNotEqual(result1, result2);
        }


        //TODO: find a better way to calculate HashCode, keeping in mind different inheritance scenario's
        //        [Test]
        //        public void GetHashCode_ForDerivedTypeAndSameValue_ShouldBeEqual()
        //        {
        //            var testValue1 = new CombinedEnum(TestEnum.Value.First);
        //            var testValue2 = new DerivedCombinedEnum(TestEnum.Value.First);
        //
        //            var result1 = testValue1.GetHashCode();
        //            var result2 = testValue2.GetHashCode();
        //
        //            Assert.AreEqual(result1, result2);
        //        }

        [Test]
        public void GetHashCode_ForDifferentTypeAndDifferentValue_ShouldNotBeEqual()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new CombinedEnum(TestEnum.Value.Second);

            var result1 = testValue1.GetHashCode();
            var result2 = testValue2.GetHashCode();

            Assert.AreNotEqual(result1, result2);
        }

        [Test]
        public void GetHashCode_MultipleCallsOnSameObject_ShouldBeEqual()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result1 = testValue.GetHashCode();
            var result2 = testValue.GetHashCode();

            Assert.AreEqual(result1, result2);
        }

        [Test]
        public void Equals_TwoObjectsOfSameClassWithSameUnderlyingValue_ShouldBeEqual()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            Assert.AreEqual(testValue1, testValue2);
        }

        [Test]
        public void Equals_TwoObjectsOfSameClassWithDifferentUnderlyingValue_ShouldNotBeEqual()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new FirstEnum(TestEnum.Value.Unknown);

            Assert.AreNotEqual(testValue1, testValue2);
        }


        [Test]
        public void Equals_TwoObjectsOfDifferentClassWithSameUnderlyingValue_ShouldNotBeEqual()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            Assert.AreNotEqual(testValue1, testValue2);
        }

        [Test]
        public void Equals_TwoObjectsOfDerivedClassWithSameUnderlyingValue_ShouldBeEqual()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new DerivedCombinedEnum(TestEnum.Value.First);

            Assert.AreEqual(testValue1, testValue2);
        }

        [Test]
        public void Equals_TwoObjectsOfDifferentClassWithEquivalentUnderlyingValue_ShouldNotBeEqual()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new AltFirstEnum(AltTestEnum.Value.First);

            Assert.AreNotEqual(testValue1, testValue2);
        }

        [Test]
        public void Equals_TwoObjectsOfDerivedClassWithDifferentUnderlyingValue_ShouldNotBeEqual()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new DerivedCombinedEnum(TestEnum.Value.Second);

            Assert.AreNotEqual(testValue1, testValue2);
        }

        [Test]
        public void OperatorEquality_TwoObjectsOfSameClassWithSameUnderlyingValue_ShouldBeEqual()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            var result = testValue1 == testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void OperatorInequality_TwoObjectsOfSameClassWithDifferentUnderlyingValue_ShouldNotBeEqual()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new FirstEnum(TestEnum.Value.Unknown);

            var result = testValue1 != testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void NewObjectEnum_WithValidEnumValue_ShouldReturnInstanceOfSpecifiedType()
        {
            var testValue = SelfContainedEnum.Create<SelfContainedEnum>(TestEnum.Value.First);

            Assert.IsInstanceOf<SelfContainedEnum>(testValue);
        }

        [Test]
        public void NewObjectEnum_WithInValidEnumValue_ShouldThrowTypeInitializationException()
        {
            Assert.Throws<TypeInitializationException>(
                () =>
                {
                    var testValue = SelfContainedEnum.Create<SelfContainedEnum>(TestEnum.Value.Second);
                }
            );
        }

        [Test]
        public void IsDefined_WithValidEnumValue_ShouldReturnTrue()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = testValue.IsDefined(TestEnum.Value.First);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsDefined_WithInvalidEnumValue_ShouldReturnFalse()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = testValue.IsDefined(TestEnum.Value.Second);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsDefined_WithValidIntegerValue_ShouldReturnTrue()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = testValue.IsDefined((int)TestEnum.Value.First);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsDefined_WithInvalidIntegerValue_ShouldReturnFalse()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = testValue.IsDefined((int)TestEnum.Value.Second);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsDefined_WithValidEnumAsObjectValue_ShouldReturnTrue()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);
            var testObject = (object)TestEnum.Value.First;

            var result = testValue.IsDefined(testObject);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsDefined_WithInvalidEnumAsObjectValue_ShouldReturnFalse()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);
            var testObject = (object)TestEnum.Value.Second;

            var result = testValue.IsDefined(testObject);

            Assert.IsFalse(result);
        }


        [Test]
        public void IsDefined_WithValidObjectEnumAsObjectValue_ShouldReturnTrue()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);
            var testObject = (object)new FirstEnum(TestEnum.Value.First);

            var result = testValue.IsDefined(testObject);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsDefined_WithValidObjectEnumValue_ShouldReturnTrue()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);
            var testObject = new FirstEnum(TestEnum.Value.First);

            var result = testValue.IsDefined(testObject);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsDefined_WithDifferentEnumValueTypeAndValidUnderlyingValue_ShouldReturnFalse()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);
            var testObject = new CombinedEnum(TestEnum.Value.Unknown); ;

            var result = testValue.IsDefined(testObject);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsDefined_WithDerivedEnumValueTypeAndValidUnderlyingValue_ShouldReturnTrue()
        {
            var testValue = new CombinedEnum(TestEnum.Value.First);
            var testObject = new DerivedCombinedEnum(TestEnum.Value.First);

            var result = testValue.IsDefined(testObject);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsDefined_WithDerivedEnumValueTypeAndInvalidUnderlyingValue_ShouldReturnFalse()
        {
            var testValue = new CombinedEnum(TestEnum.Value.First);
            var testObject = new DerivedCombinedEnum(TestEnum.Value.Third); ;

            var result = testValue.IsDefined(testObject);

            Assert.IsFalse(result);
        }


        [Test]
        public void IsDefined_WitNullAsObject_ShouldReturnFalse()
        {
            var testValue = new CombinedEnum(TestEnum.Value.First);

            var result = testValue.IsDefined((object)null);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsDefined_WitNullAsEnumtValue_ShouldReturnFalse()
        {
            var testValue = new CombinedEnum(TestEnum.Value.First);

            var result = testValue.IsDefined((object)null);

            Assert.IsFalse(result);
        }

        [Test]
        public void LessThan_TwoObjectsOfSameClassWithSmallerUnderlyingValueInFirst_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new CombinedEnum(TestEnum.Value.Second);

            var result = testValue1 < testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void LessThan_TwoObjectsOfSameClassWithSmallerUnderlyingValueInSecond_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.Second);
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 < testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void LessThan_TwoObjectsOfSameClassWithSameUnderlyingValueInSecond_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 < testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void LessThanOrEqual_TwoObjectsOfSameClassWithSmallerUnderlyingValueInFirst_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new CombinedEnum(TestEnum.Value.Second);

            var result = testValue1 <= testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void LessThanOrEqual_TwoObjectsOfSameClassWithSmallerUnderlyingValueInSecond_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.Second);
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 <= testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void LessThanOrEqual_TwoObjectsOfSameClassWithSameUnderlyingValueInSecond_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 <= testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void GreaterThan_TwoObjectsOfSameClassWithSmallerUnderlyingValueInFirst_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new CombinedEnum(TestEnum.Value.Second);

            var result = testValue1 > testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void GreaterThan_TwoObjectsOfSameClassWithSmallerUnderlyingValueInSecond_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.Second);
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 > testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void GreaterThan_TwoObjectsOfSameClassWithSameUnderlyingValueInSecond_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 > testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void GreaterThanOrEqual_TwoObjectsOfSameClassWithSmallerUnderlyingValueInFirst_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new CombinedEnum(TestEnum.Value.Second);

            var result = testValue1 >= testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void GreaterThanOrEqual_TwoObjectsOfSameClassWithSmallerUnderlyingValueInSecond_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.Second);
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 >= testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void GreaterThanOrEqual_TwoObjectsOfSameClassWithSameUnderlyingValueInSecond_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 >= testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void LessThan_TwoObjectsOfDifferentClassWithSmallerUnderlyingValueInFirst_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new SecondEnum(TestEnum.Value.Second);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 < testValue2; });
        }

        [Test]
        public void LessThan_TwoObjectsOfDifferentClassWithSmallerUnderlyingValueInSecond_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new SecondEnum(TestEnum.Value.Second);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 < testValue2; });
        }

        [Test]
        public void LessThan_TwoObjectsOfDifferentClassWithSameUnderlyingValueInSecond_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 < testValue2; });
        }

        [Test]
        public void LessThanOrEqual_TwoObjectsOfDifferentClassWithSmallerUnderlyingValueInFirst_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new SecondEnum(TestEnum.Value.Second);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 <= testValue2; });
        }

        [Test]
        public void LessThanOrEqual_TwoObjectsOfDifferentClassWithSmallerUnderlyingValueInSecond_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new SecondEnum(TestEnum.Value.Second);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 <= testValue2; });
        }

        [Test]
        public void LessThanOrEqual_TwoObjectsOfDifferentClassWithSameUnderlyingValueInSecond_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 <= testValue2; });
        }

        [Test]
        public void GreaterThan_TwoObjectsOfDifferentClassWithSmallerUnderlyingValueInFirst_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new SecondEnum(TestEnum.Value.Second);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 > testValue2; });
        }

        [Test]
        public void GreaterThan_TwoObjectsOfDifferentClassWithSmallerUnderlyingValueInSecond_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new SecondEnum(TestEnum.Value.Second);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 > testValue2; });
        }

        [Test]
        public void GreaterThan_TwoObjectsOfDifferentClassWithSameUnderlyingValueInSecond_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 > testValue2; });
        }

        [Test]
        public void GreaterThanOrEqual_TwoObjectsOfDifferentClassWithSmallerUnderlyingValueInFirst_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new FirstEnum(TestEnum.Value.First);
            var testValue2 = new SecondEnum(TestEnum.Value.Second);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 >= testValue2; });
        }

        [Test]
        public void GreaterThanOrEqual_TwoObjectsOfDifferentClassWithSmallerUnderlyingValueInSecond_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new SecondEnum(TestEnum.Value.Second);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 >= testValue2; });
        }

        [Test]
        public void GreaterThanOrEqual_TwoObjectsOfDifferentClassWithSameUnderlyingValueInSecond_ShouldThrowInvalidOperationException()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = new FirstEnum(TestEnum.Value.First);

            Assert.Throws<InvalidOperationException>(() => { var result = testValue1 >= testValue2; });
        }

        [Test]
        public void LessThan_ObjectAndUnderlyingValueWithSmallerValueInFirst_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = TestEnum.Value.Second;

            var result = testValue1 < testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void LessThan_ObjectAndUnderlyingValueWithSmallerValueInSecond_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.Second);
            var testValue2 = TestEnum.Value.First;

            var result = testValue1 < testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void LessThan_ObjectAndUnderlyingValueWithSameValueInSecond_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = TestEnum.Value.First;

            var result = testValue1 < testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void LessThanOrEqual_ObjectAndUnderlyingValueWithSmallerValueInFirst_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = TestEnum.Value.Second;

            var result = testValue1 <= testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void LessThanOrEqual_ObjectAndUnderlyingValueWithSmallerValueInSecond_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.Second);
            var testValue2 = TestEnum.Value.First;

            var result = testValue1 <= testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void LessThanOrEqual_ObjectAndUnderlyingValueWithSameValueInSecond_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = TestEnum.Value.First;

            var result = testValue1 <= testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void GreaterThan_ObjectAndUnderlyingValueWithSmallerValueInFirst_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = TestEnum.Value.Second;

            var result = testValue1 > testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void GreaterThan_ObjectAndUnderlyingValueWithSmallerValueInSecond_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.Second);
            var testValue2 = TestEnum.Value.First;

            var result = testValue1 > testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void GreaterThan_ObjectAndUnderlyingValueWithSameValueInSecond_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = TestEnum.Value.First;

            var result = testValue1 > testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void GreaterThanOrEqual_ObjectAndUnderlyingValueWithSmallerValueInFirst_ShouldBeFalse()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = TestEnum.Value.Second;

            var result = testValue1 >= testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void GreaterThanOrEqual_ObjectAndUnderlyingValueWithSmallerValueInSecond_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.Second);
            var testValue2 = TestEnum.Value.First;

            var result = testValue1 >= testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void GreaterThanOrEqual_ObjectAndUnderlyingValueWithSameValueInSecond_ShouldBeTrue()
        {
            var testValue1 = new CombinedEnum(TestEnum.Value.First);
            var testValue2 = TestEnum.Value.First;

            var result = testValue1 >= testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void LessThan_UnderlyingValueAndObjectWithSmallerValueInFirst_ShouldBeTrue()
        {
            var testValue1 = TestEnum.Value.First;
            var testValue2 = new CombinedEnum(TestEnum.Value.Second);

            var result = testValue1 < testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void LessThan_UnderlyingValueAndObjectWithSmallerValueInSecond_ShouldBeFalse()
        {
            var testValue1 = TestEnum.Value.Second;
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 < testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void LessThan_UnderlyingValueAndObjectWithSameValueInSecond_ShouldBeFalse()
        {
            var testValue1 = TestEnum.Value.First;
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 < testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void LessThanOrEqual_UnderlyingValueAndObjectWithSmallerValueInFirst_ShouldBeTrue()
        {
            var testValue1 = TestEnum.Value.First;
            var testValue2 = new CombinedEnum(TestEnum.Value.Second);

            var result = testValue1 <= testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void LessThanOrEqual_UnderlyingValueAndObjectWithSmallerValueInSecond_ShouldBeFalse()
        {
            var testValue1 = TestEnum.Value.Second;
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 <= testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void LessThanOrEqual_UnderlyingValueAndObjectWithSameValueInSecond_ShouldBeTrue()
        {
            var testValue1 = TestEnum.Value.First;
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 <= testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void GreaterThan_UnderlyingValueAndObjectWithSmallerValueInFirst_ShouldBeFalse()
        {
            var testValue1 = TestEnum.Value.First;
            var testValue2 = new CombinedEnum(TestEnum.Value.Second);

            var result = testValue1 > testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void GreaterThan_UnderlyingValueAndObjectWithSmallerValueInSecond_ShouldBeTrue()
        {
            var testValue1 = TestEnum.Value.Second;
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 > testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void GreaterThan_UnderlyingValueAndObjectWithSameValueInSecond_ShouldBeFalse()
        {
            var testValue1 = TestEnum.Value.First;
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 > testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void GreaterThanOrEqual_UnderlyingValueAndObjectWithSmallerValueInFirst_ShouldBeFalse()
        {
            var testValue1 = TestEnum.Value.First;
            var testValue2 = new CombinedEnum(TestEnum.Value.Second);

            var result = testValue1 >= testValue2;

            Assert.IsFalse(result);
        }

        [Test]
        public void GreaterThanOrEqual_UnderlyingValueAndObjectWithSmallerValueInSecond_ShouldBeTrue()
        {
            var testValue1 = TestEnum.Value.Second;
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 >= testValue2;

            Assert.IsTrue(result);
        }

        [Test]
        public void GreaterThanOrEqual_UnderlyingValueAndObjectWithSameValueInSecond_ShouldBeTrue()
        {
            var testValue1 = TestEnum.Value.First;
            var testValue2 = new CombinedEnum(TestEnum.Value.First);

            var result = testValue1 >= testValue2;

            Assert.IsTrue(result);
        }
    }
}