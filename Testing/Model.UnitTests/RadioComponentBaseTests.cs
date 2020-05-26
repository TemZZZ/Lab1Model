﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
		const double MinRadioComponentValue = 0;
		protected const double MinFrequency = 0;

		/// <summary>
		/// Когда свойству <see cref="RadioComponentBase.Value"/>
		/// присваивается значение value, то свойство
		/// <see cref="RadioComponentBase.Value"/> должно стать равно value
		/// </summary>
		/// <param name="value">Значение свойства
		/// <see cref="RadioComponentBase.Value"/></param>
		[Test]
		[TestCase(MinRadioComponentValue,
			TestName = "Когда свойству Value присваивается значение 0, " +
			"то свойство Value должно стать равно 0.")]
		[TestCase(MinRadioComponentValue + 1,
			TestName = "Когда свойству Value присваивается значение 1, " +
			"то свойство Value должно стать равно 1.")]
		[TestCase(double.MaxValue - 1,
			TestName = "Когда свойству Value присваивается значение " +
			"MaxValue - 1, то свойство Value должно стать равно " +
			"MaxValue - 1.")]
		[TestCase(double.MaxValue,
			TestName = "Когда свойству Value присваивается значение " +
			"MaxValue, то свойство Value должно стать равно MaxValue.")]
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

		/// <summary>
		/// Когда свойству <see cref="RadioComponentBase.Value"/>
		/// присваивается значение value, то должно выбрасываться
		/// исключение expectedException
		/// </summary>
		/// <param name="value">Значение свойства
		/// <see cref="RadioComponentBase.Value"/></param>
		/// <param name="expectedException">Требуемый тип выбрасываемого
		/// исключения</param>
		[Test]
		[TestCase(double.NegativeInfinity,
			typeof(ArgumentOutOfRangeException),
			TestName = "Когда свойству Value присваивается значение " +
			"NegativeInfinity, то должно выбрасываться исключение " +
			"ArgumentOutOfRangeException.")]
		[TestCase(MinRadioComponentValue - 1,
			typeof(ArgumentOutOfRangeException),
			TestName = "Когда свойству Value присваивается значение (-1), " +
			"то должно выбрасываться исключение " +
			"ArgumentOutOfRangeException.")]
		[TestCase(double.PositiveInfinity,
			typeof(ArgumentOutOfRangeException),
			TestName = "Когда свойству Value присваивается значение " +
			"PositiveInfinity, то должно выбрасываться исключение " +
			"ArgumentOutOfRangeException.")]
		[TestCase(double.NaN, typeof(ArgumentException),
			TestName = "Когда свойству Value присваивается значение NaN, " +
			"то должно выбрасываться исключение ArgumentException.")]
		public void ValueProperty_AssignedBadValues_ThrowsExpectedException(
			double value, Type expectedException)
		{
			// Setup
			var radioComponent = new T();

			// Assert
			_ = Assert.Throws(expectedException,
				() => radioComponent.Value = value);
		}

		/// <summary>
		/// Когда в метод
		/// <see cref="RadioComponentBase.GetImpedance(double)"/> передается
		/// значение частоты frequency, то должно выбрасываться исключение
		/// expectedException
		/// </summary>
		/// <param name="frequency">Значение частоты</param>
		/// <param name="expectedException">Требуемый тип выбрасываемого
		/// исключения</param>
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

		/// <summary>
		/// Когда в метод
		/// <see cref="RadioComponentBase.GetImpedance(double)"/>
		/// радиокомпонента со значением свойства (Value == value) передается
		/// значение частоты frequency, то метод
		/// <see cref="RadioComponentBase.GetImpedance(double)"/> должен
		/// вернуть значение expectedImpedance
		/// </summary>
		/// <param name="value">Значение свойства Value</param>
		/// <param name="frequency">Значение частоты</param>
		/// <param name="expectedImpedance">Требуемый комплексный импеданс
		/// </param>
		[Test]
		public abstract
			void GetImpedance_GoodValuesAndFrequencies_ReturnsValue(
				double value, double frequency, Complex expectedImpedance);
	}
}
