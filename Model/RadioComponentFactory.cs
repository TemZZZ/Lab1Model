﻿using System;
using System.Collections.Generic;
using Model.PassiveComponents;


namespace Model
{
	/// <summary>
	/// Класс фабрики радиокомпонентов
	/// </summary>
	public class RadioComponentFactory
	{
		/// <summary>
		/// Словарь соответствий типа <see cref="Type"/> радиокомпонента и
		/// элемента из перечисления <see cref="RadioComponentType"/>
		/// </summary>
		private readonly Dictionary<Type, RadioComponentType>
			_typeToRadioComponentTypeMap
			= new Dictionary<Type, RadioComponentType>
			{
				[typeof(Resistor)] = RadioComponentType.Resistor,
				[typeof(Inductor)] = RadioComponentType.Inductor,
				[typeof(Capacitor)] = RadioComponentType.Capacitor,
			};

		/// <summary>
		/// Возвращает словарь пар значений
		/// "тип радиокомпонента-радиокомпонент"
		/// </summary>
		/// <param name="radioComponentValue">Значение физической величины
		/// радиокомпонента</param>
		/// <returns>Словарь пар значений
		/// "тип радиокомпонента-радиокомпонент"
		/// </returns>
		private Dictionary<RadioComponentType, RadioComponentBase>
				GetTypeToRadioComponentMap(double radioComponentValue)
		{
			return new Dictionary<RadioComponentType, RadioComponentBase>
			{
				[RadioComponentType.Resistor]
					= new Resistor(radioComponentValue),
				[RadioComponentType.Inductor]
					= new Inductor(radioComponentValue),
				[RadioComponentType.Capacitor]
					= new Capacitor(radioComponentValue)
			};
		}

		/// <summary>
		/// Возвращает радиокомпонент определенного типа с требуемым
		/// значением физической величины
		/// </summary>
		/// <param name="radioComponentType">Тип радиокомпонента</param>
		/// <param name="radioComponentValue">Значение физической величины
		/// радиокомпонента</param>
		/// <returns>Объект класса-наследника
		/// <see cref="RadioComponentBase"/></returns>
		/// <exception cref="KeyNotFoundException"/>
		public RadioComponentBase CreateRadioComponent(
			RadioComponentType radioComponentType,
			double radioComponentValue)
		{
			return GetTypeToRadioComponentMap(radioComponentValue)
				[radioComponentType];
		}

		/// <summary>
		/// Возвращает тип <see cref="RadioComponentType"/> переданного
		/// радиокомпонента
		/// </summary>
		/// <param name="radioComponent">Радиокомпонент</param>
		/// <returns>Тип радиокомпонента</returns>
		public RadioComponentType GetRadioComponentType(
			IRadioComponent radioComponent)
		{
			return _typeToRadioComponentTypeMap[radioComponent.GetType()];
		}
	}
}