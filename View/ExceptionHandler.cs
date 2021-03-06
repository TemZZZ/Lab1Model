﻿using System;
using System.Collections.Generic;


namespace View
{
	/// <summary>
	/// Класс обработки исключений при вызове функций
	/// </summary>
	public class ExceptionHandler
	{
		/// <summary>
		/// Вызывает функцию и обрабатывает возможные исключения
		/// </summary>
		/// <typeparam name="T">Тип входного параметра</typeparam>
		/// <typeparam name="TResult">Возвращаемый тип</typeparam>
		/// <param name="function">Функция</param>
		/// <param name="parameter">Входной параметр функции</param>
		/// <param name="exceptionTypeToMessageMap">
		/// Перечислитель "тип исключения-сообщение при исключении"</param>
		/// <param name="errorMessager">Делегат для вывода сообщений при
		/// возникновении исключения</param>
		/// <returns>Объект типа TResult или default(TResult)</returns>
		public static TResult CallFunction<T, TResult>(
			Func<T, TResult> function, T parameter,
			IEnumerable<KeyValuePair<Type, string>>
				exceptionTypeToMessageMap,
			Action<string> errorMessager = null)
		{
			try
			{
				return function(parameter);
			}
			catch (Exception e)
			{
				foreach (var exceptionTypeToMessage in
					exceptionTypeToMessageMap)
				{
					if (exceptionTypeToMessage.Key != e.GetType())
						continue;

					errorMessager?.Invoke(exceptionTypeToMessage.Value);
					return default;
				}
				throw;
			}
		}
	}
}
