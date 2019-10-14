using System.ValueEnum.Tests.Models;
using NUnit.Framework;

namespace System.ValueEnum.Tests
{
    public class ValueEnumTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Initialize_WithNonEnumTypeParameter_ShouldThrowTypeInitializationException()
        {
            Assert.Throws(
                typeof(TypeInitializationException),
                () =>
                {
                    var a = new InvalidEnum(1);
                });
        }

        [Test]
        public void Initialize_WithOuOfRangeEnumValueArgument_ShouldThrowTypeInitializationException()
        {
            Assert.Throws(
                typeof(TypeInitializationException),
                () =>
                {
                    var a = new FirstEnum(TestEnum.Value.Second);
                });
        }

        [Test]
        public void Cast_ToUnderlyingEnum_ShouldReturnInstanceOfUnderlyingEnum()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = (TestEnum.Value) testValue;

            Assert.IsInstanceOf<TestEnum.Value>(result);
        }

        [Test]
        public void Cast_ToUnderlyingEnum_EnumValuesShouldBeEqual()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = (TestEnum.Value) testValue;

            Assert.AreEqual(TestEnum.Value.First, result);
        }

        [Test]
        public void Cast_ToInteger_ShouldReturnInstanceOfInt()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = (int) testValue;

            Assert.IsInstanceOf<int>(result);
        }

        [Test]
        public void Cast_ToInteger_IntegerValuesShouldBeEqual()
        {
            var testValue = new FirstEnum(TestEnum.Value.First);

            var result = (int) testValue;

            Assert.AreEqual((int) TestEnum.Value.First, result);
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
        public void Parse_WithValidName_ShouldReturnValueEnumWithCorrespondingValue()
        {
            var testString = TestEnum.Value.First.ToString();

            var result = (TestEnum.Value) FirstEnum.Parse<FirstEnum>(testString);

            Assert.AreEqual(
                TestEnum.Value.First,
                result
            );
        }

        [Test]
        public void Parse_WithInvalidName_ShouldThrowFormatException()
        {
            Assert.Throws(
                typeof(FormatException),
                () =>
                {
                    var result = FirstEnum.Parse<FirstEnum>("SomeString");
                }
            );
        }

        [Test]
        public void Parse_WithValidNameButUndefined_ShouldThrowInvalidOperationException()
        {
            Assert.Throws(
                typeof(InvalidOperationException),
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
        public void TryParse_WithValidName_ShouldSetOutValueEnumWithCorrespondingValue()
        {
            var testString = TestEnum.Value.First.ToString();

            FirstEnum.TryParse<FirstEnum>(testString, out var result);

            Assert.AreEqual(
                TestEnum.Value.First,
                (TestEnum.Value) result
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
        public void TryParse_WithInvalidName_ShouldSetOutValueEnumWithDefault()
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
        public void TryParse_WithValidNameButUndefined_ShouldSetOutValueEnumWithDefault()
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
    }
}