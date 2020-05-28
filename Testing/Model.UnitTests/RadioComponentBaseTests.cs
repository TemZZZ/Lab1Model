﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;


namespace Model.UnitTests
{
	/// <summary>
	/// Набор тестов, общих для классов, производных от
	/// <see cref="RadioComponentBase"/>
	/// </summary>
	/// <typeparam name="T">Класс, производный от
	/// <see cref="RadioComponentBase"/></typeparam>
	[TestFixture]
	public abstract class RadioComponentBaseTests<T>
		where T : RadioComponentBase, new()
	{
		protected const double MinRadioComponentValue = 0;
		protected const double MinFrequency = 0;

		private static readonly double[] _goodDoubles
			= { 0, 1, double.MaxValue };

		private static readonly
			Dictionary<double, Type> _badDoubleToExpectedExceptionTypeMap
				= new Dictionary<double, Type>
				{
					[double.NegativeInfinity]
						= typeof(ArgumentOutOfRangeException),
					[-1] = typeof(ArgumentOutOfRangeException),
					[double.PositiveInfinity]
						= typeof(ArgumentOutOfRangeException),
					[double.NaN] = typeof(ArgumentException)
				};

		protected static double[] GoodRadioComponentValues => _goodDoubles;
		protected static double[] GoodFrequencies => _goodDoubles;

		protected static
			IEnumerable<TestCaseData> ValuePropertyGoodValuesTestCases()
		{
			foreach (var radioComponentValue in GoodRadioComponentValues)
			{
				yield return new TestCaseData(radioComponentValue)
					.SetName($"Когда свойству Value присваивается " +
					$"значение {radioComponentValue}, то свойство Value " +
					$"должно стать равным {radioComponentValue}.");
			}
		}

		protected static
			IEnumerable<TestCaseData> ValuePropertyBadValuesTestCases()
		{
			foreach (var doubleToExpectedExceptionType
				in _badDoubleToExpectedExceptionTypeMap)
			{
				yield return new TestCaseData(doubleToExpectedExceptionType)
					.SetName($"Когда свойству Value присваивается " +
					$"значение {doubleToExpectedExceptionType.Key}, " +
					$"то должно выбрасываться исключение " +
					$"{doubleToExpectedExceptionType.Value}.");
			}
		}

		[Test, TestCaseSource("ValuePropertyGoodValuesTestCases")]
		public void ValueProperty_AssignedGoodValues_IsAssigned(double value)
		{
			// Setup
			var expected = value;

			// Act
			var radioComponent = new T
			{
				Value = value
			};
			var actual = radioComponent.Value;

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test, TestCaseSource("ValuePropertyBadValuesTestCases")]
		public void ValueProperty_AssignedBadValues_ThrowsExpectedException(
			KeyValuePair<double, Type> doubleToExpectedExceptionType)
		{
			// Setup
			var radioComponent = new T();

			// Assert
			_ = Assert.Throws(doubleToExpectedExceptionType.Value,
				() => radioComponent.Value
					= doubleToExpectedExceptionType.Key);
		}

		[Test]
		[TestCase(double.NegativeInfinity,
			typeof(ArgumentOutOfRangeException),
			TestName = "Когда в метод GetImpedance передается значение " +
			"частоты NegativeInfinity, то должно выбрасываться исключение " +
			"ArgumentOutOfRangeException.")]
		[TestCase(MinFrequency - 1, typeof(ArgumentOutOfRangeException),
			TestName = "Когда в метод GetImpedance передается значение " +
			"частоты (-1), то должно выбрасываться исключение " +
			"ArgumentOutOfRangeException.")]
		[TestCase(double.PositiveInfinity,
			typeof(ArgumentOutOfRangeException),
			TestName = "Когда в метод GetImpedance передается значение " +
			"частоты PositiveInfinity, то должно выбрасываться исключение " +
			"ArgumentOutOfRangeException.")]
		[TestCase(double.NaN, typeof(ArgumentException),
			TestName = "Когда в метод GetImpedance передается значение " +
			"частоты NaN, то должно выбрасываться исключение " +
			"ArgumentException.")]
		public void GetImpedance_BadFrequencies_ThrowsExpectedException(
			double frequency, Type expectedException)
		{
			// Setup
			var radioComponent = new T();

			// Assert
			_ = Assert.Throws(expectedException,
				() => radioComponent.GetImpedance(frequency));
		}

		public abstract
			void GetImpedance_GoodValuesAndFrequencies_ReturnsValue(
				double value, double frequency, Complex expectedImpedance);
	}
}
