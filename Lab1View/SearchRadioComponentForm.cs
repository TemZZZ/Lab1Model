﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using Lab1Model;


namespace Lab1View
{
	/// <summary>
	/// Класс формы поиска радиокомпонентов
	/// </summary>
	public partial class SearchRadioComponentForm : Form
	{
		/// <summary>
		/// Список радиокомпонентов, по которым осуществляется поиск
		/// </summary>
		private SortableBindingList<RadioComponentBase> RadioComponents
		{ get; } = new SortableBindingList<RadioComponentBase>();

		const string allTypesText = "<Все>";
		const string resistorTypeText = "Резистор";
		const string inductorTypeText = "Катушка индуктивности";
		const string capacitorTypeText = "Конденсатор";

		/// <summary>
		/// Создает форму поиска радиокомпонентов
		/// </summary>
		public SearchRadioComponentForm(
			SortableBindingList<RadioComponentBase> radioComponents)
		{
			RadioComponents = radioComponents;

			InitializeComponent();

			// Заполняет radioComponentTypeComboBox типами радиокомпонентов
			radioComponentTypeComboBox.DataSource =
				GetRadioComponentTypeComboBoxItems();

			SetupOnSearchOptionsChanged();

			RadioComponents.ListChanged += OnRadioComponentsChanged;
		}

		/// <summary>
		/// Возвращает названия типов радиокомпонентов для заполнения
		/// <see cref="radioComponentTypeComboBox"/>
		/// </summary>
		/// <returns>Массив строк</returns>
		private string[] GetRadioComponentTypeComboBoxItems()
		{
			string[] radioComponentTypeComboBoxItems =
			{
				allTypesText,
				resistorTypeText,
				inductorTypeText,
				capacitorTypeText
			};

			return radioComponentTypeComboBoxItems;
		}

		/// <summary>
		/// Добавляет обработчик <see cref="OnSearchOptionsChanged"/>
		/// к событиям изменения состояния элементов в форме поиска
		/// </summary>
		private void SetupOnSearchOptionsChanged()
		{
			Control[] controlsWithTextChangedEvent =
			{
				radioComponentTypeComboBox,
				lessThanPositiveDoubleTextBox,
				moreThanPositiveDoubleTextBox,
				equalPositiveDoubleTextBox
			};

			CheckBox[] checkBoxes =
			{
				lessThanCheckBox,
				moreThanCheckBox,
				equalCheckBox
			};

			foreach (var control in controlsWithTextChangedEvent)
			{
				control.TextChanged += OnSearchOptionsChanged;
			}

			foreach (var checkBox in checkBoxes)
			{
				checkBox.CheckedChanged += OnSearchOptionsChanged;
			}
		}

		/// <summary>
		/// Закрывает форму
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Сообщает пользователю в <see cref="searchStatusLabel"/>
		/// о том, что фильтр поиска был изменен и активирует кнопку
		/// <see cref="searchRadioComponentsButton"/>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSearchOptionsChanged(object sender, EventArgs e)
		{
			const string searchOptionsChangedText
				= "Параметры поиска изменены. Нажмите \"Найти\"";

			searchStatusLabel.Text = searchOptionsChangedText;
			searchRadioComponentsButton.Enabled = true;
		}

		/// <summary>
		/// Производит поиск радиокомпонентов в соответствие с
		/// фильтрами поиска, сообщает пользователю в
		/// <see cref="searchStatusLabel"/> статус поиска
		/// и деактивирует кнопку
		/// <see cref="searchRadioComponentsButton"/>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchRadioComponentsButton_Click(
			object sender, EventArgs e)
		{
			const string searchFinishedText = "Поиск завершен.\n";
			const string foundText =
				"Найденные радиокомпоненты подсвечены.\n";
			const string notFoundText = "Ничего не найдено.\n";
			const string changeSearchParametersText =
				"Измените параметры для нового поиска.";

			//searchStatusLabel.Text = searchFinishedText + foundText +
				//changeSearchParametersText;
			//searchRadioComponentsButton.Enabled = false;
		}

		/// <summary>
		/// При изменениях в списке радиокомпонентов
		/// <see cref="RadioComponents"/> сообщает пользователю в
		/// <see cref="searchStatusLabel"/> об изменениях и
		/// активирует кнопку <see cref="searchRadioComponentsButton"/>
		/// для возобновления поиска
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OnRadioComponentsChanged(
			object sender, ListChangedEventArgs e)
		{
			const string radioComponentsChangedText =
				"Список радиокомпонентов изменился.\n" +
				"Можно возобновить поиск.";
			searchStatusLabel.Text = radioComponentsChangedText;

			searchRadioComponentsButton.Enabled = true;
		}
	}
}