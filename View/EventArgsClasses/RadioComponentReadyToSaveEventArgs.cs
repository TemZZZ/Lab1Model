﻿using System;


namespace View
{
	/// <summary>
	/// Параметр сохранения радиокомпонентов
	/// </summary>
	public enum RadioComponentSaveOption
	{
		SaveAll,
		SaveSelected
	}

	/// <summary>
	/// Класс данных события подтверждения выбора
	/// параметра сохранения радиокомпонента
	/// </summary>
	public class RadioComponentReadyToSaveEventArgs : EventArgs
	{
		/// <summary>
		/// Создает объект класса
		/// <see cref="RadioComponentReadyToSaveEventArgs"/>
		/// </summary>
		/// <param name="radioComponentSaveOption">
		/// Параметр сохранения радиокомпонентов</param>
		public RadioComponentReadyToSaveEventArgs(
			RadioComponentSaveOption radioComponentSaveOption)
		{
			RadioComponentSaveOption = radioComponentSaveOption;
		}

		/// <summary>
		/// Позволяет получить параметр сохранения радиокомпонентов
		/// </summary>
		public RadioComponentSaveOption RadioComponentSaveOption { get; }
	}
}
