﻿using System;

using ConsoleLoaderModel;

using Lab1Model;


public class MainApp
{
	public static void Main()
	{
		Console.WriteLine("Программа для вычисления\n" +
			"комплексного сопротивления радиокомпонентов");

		// Основной цикл программы
		while (true)
		{
			Console.Write("\nДля выхода из программы введите Q (q)\n" +
				"или введите тип радиокомпонента R (r), L (l) или C (c): ");

			// Хранит введенную с консоли строку
			string inputStr = Console.ReadLine();

			// Если введена "Q" или "q" - выйти из программы
			if (inputStr.ToUpper() == "Q") { return; }

			// На основании введенной строки,
			// создать объект радиокомпонента
			ComponentBase cmp = ConsoleLoader.GetComponent(
				inputStr, Console.WriteLine);

			// Если не удалось вернуть объект радиокомпонента,
			// перейти к следующей итерации основного цикла программы
			if (cmp == null) { continue; }

			// Запрашивает у пользователя значение физической величины
			// радиокомпонента и считывает введенную строку

			ConsoleLoader.AskComponentValue(in cmp, Console.Write);
			inputStr = Console.ReadLine();

			// Преобразует строку со значением физической величины
			// радиокомпонента в вещественное число
			double value = ConsoleLoader.StringToDouble(
				inputStr, Console.WriteLine);

			// Если значение физической величины - не число
			// или значение физической величины меньше нуля,
			// то вывести пользователю сообщение об ошибке
			// и перейти к следующей итерации основного цикла программы
			if ((double.IsNaN(value)) ||
				(!ConsoleLoader.IsPositiveDouble(value,
				"Значение физической величины не может быть отрицательным",
				Console.WriteLine))) { continue; }

			// Запрашивает у пользователя значение частоты
			// и считывает введенную строку

			Console.Write("Введите частоту в герцах: ");
			inputStr = Console.ReadLine();

			// Преобразует строку со значением частоты
			// в вещественное число
			double freq = ConsoleLoader.StringToDouble(
				inputStr, Console.WriteLine);

			// Если значение частоты - не число
			// или значение частоты меньше нуля,
			// то вывести пользователю сообщение об ошибке
			// и перейти к следующей итерации основного цикла программы
			if ((double.IsNaN(freq)) ||
				(!ConsoleLoader.IsPositiveDouble(freq,
				"Значение частоты не может быть отрицательным",
				Console.WriteLine))) { continue; }

			// Присвоить радиокомпоненту значение физической величины
			cmp.Value = value;

			// Напечатать комплексный импеданс радиокомпонента
			ConsoleLoader.PrintComplex(
				cmp.GetImpedance(freq), Console.WriteLine);
		}
	}
}