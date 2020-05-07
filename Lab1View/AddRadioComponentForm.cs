﻿#define TEST

using System;
using System.Windows.Forms;


namespace Lab1View
{
    /// <summary>
    /// Форма добавления новых радиокомпонентов
    /// </summary>
    public partial class AddRadioComponentForm : Form
    {
        //const string doublePattern =
        //    @"^[-+]?([0-9]+[\.\,]?[0-9]*([eE]?[-+]?[0-9]*))?$";
        /// <summary>
        /// Шаблон регулярного выражения положительных вещественных чисел
        /// </summary>
        const string positiveDoublePattern =
            @"^([0-9]+[\.\,]?[0-9]*([eE]?[-+]?[0-9]*))?$";

        private readonly Random randomIntGenerator = new Random();

        /// <summary>
        /// Создает новый форму добавления радиокомпонентов
        /// </summary>
        public AddRadioComponentForm()
        {
            InitializeComponent();
#if !TEST
            generateRandomDataButton.Visible = false;
#endif
            // Регистрируются обработчики событий
            // изменения состояния радиокнопок

            resistorRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            inductorRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            capacitorRadioButton.CheckedChanged += RadioButton_CheckedChanged;

            // По умолчанию выбрана радиокнопка резистора
            resistorRadioButton.Checked = true;

            valueRegexTextBox.Pattern = positiveDoublePattern;
        }

        /// <summary>
        /// Преобразует строку в вещественное число
        /// и сообщает о результате преобразования
        /// </summary>
        /// <param name="text">Исходная строка</param>
        /// <param name="isPositiveDouble">Результат преобразования</param>
        /// <param name="messager">Делегат для сообщений об ошибках</param>
        /// <returns>Преобразованное число</returns>
        double ToPositiveDouble(string text, out bool isPositiveDouble,
            Action<string> messager)
        {
            const string emptyTextCaution = "Поле не может быть пустым";
            const string notNumberCaution =
                "Введенное значение не является числом";
            const string notPositiveNumberCaution =
                "Число не может быть отрицательным";

            isPositiveDouble = false;

            if (string.IsNullOrEmpty(text.Replace('.', ',')))
            {
                messager(emptyTextCaution);
                const double zero = 0;
                return zero;
            }

            bool isDouble = double.TryParse(
                text.Replace('.', ','), out double doubleValue);

            if (!isDouble)
            {
                messager(notNumberCaution);
                return doubleValue;
            }

            if (doubleValue < 0)
            {
                messager(notPositiveNumberCaution);
                return doubleValue;
            }

            isPositiveDouble = true;
            return doubleValue;
        }

        private void Messager(string message)
        {
            const string messageBoxHeader = "Предупреждение";
            MessageBox.Show(message, messageBoxHeader,
                MessageBoxButtons.OK);
        }

        private void RadioButton_CheckedChanged(
            object sender, EventArgs e)
        {
            if (!(sender is RadioButton selectedRadioButton)) { return; }

            const string resistorValueUnitText = "Сопротивление, Ом";
            const string inductorValueUnitText = "Индуктивность, Гн";
            const string capacitorValueUnitText = "Емкость, Ф";

            if (selectedRadioButton == resistorRadioButton)
            {
                valueUnitLabel.Text = resistorValueUnitText;
            }
            else if (selectedRadioButton == inductorRadioButton)
            {
                valueUnitLabel.Text = inductorValueUnitText;
            }
            else if (selectedRadioButton == capacitorRadioButton)
            {
                valueUnitLabel.Text = capacitorValueUnitText;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GenerateRandomDataButton_Click(
            object sender, EventArgs e)
        {
            const int maxRadioButtonNumber = 3;

            const double resistorDivisor = 1e6;
            const double inductorDivisor = 1e12;
            const double capacitorDivisor = 1e15;

            double value = randomIntGenerator.Next();

            switch (randomIntGenerator.Next(maxRadioButtonNumber))
            {
                case 0:
                    resistorRadioButton.Checked = true;
                    value /= resistorDivisor;
                    break;
                case 1:
                    inductorRadioButton.Checked = true;
                    value /= inductorDivisor;
                    break;
                case 2:
                    capacitorRadioButton.Checked = true;
                    value /= capacitorDivisor;
                    break;
            }

            valueRegexTextBox.Text = Convert.ToString(value);
        }
    }
}
