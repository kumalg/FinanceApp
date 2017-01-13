﻿using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Finanse.DataAccessLayer;
using Finanse.Models;
using System.Text.RegularExpressions;

namespace Finanse.Dialogs {
    public sealed partial class EditOperationContentDialog : ContentDialog {

        private Regex regex = NewOperation.getRegex();
        
        private bool isLoaded = false;
        private bool isUnfocused = true;

        private Operation operationToEdit = null;
        private OperationPattern operationPatternToEdit = null;

        private string acceptedCostValue = string.Empty;

        public EditOperationContentDialog(Operation operationToEdit) {

            this.InitializeComponent();
            this.operationToEdit = operationToEdit;

            DateValue.MaxDate = Settings.getMaxDate();
            setMoneyAccountComboBoxItems();

            setEditedOperationValues(operationToEdit);
        }

        public EditOperationContentDialog(OperationPattern operationPatternToEdit) {

            this.InitializeComponent();
            this.operationPatternToEdit = operationPatternToEdit;

            DateValue.Visibility = Visibility.Collapsed;
            setMoneyAccountComboBoxItems();

            setEditedOperationValues(operationPatternToEdit);
        }

        private void setMoneyAccountComboBoxItems() {
            foreach (MoneyAccount account in Dal.getAllMoneyAccounts()) {
                PayFormValue.Items.Add(new ComboBoxItem {
                    Content = account.Name,
                    Tag = account.Id
                });
            }
        }

        private void setEditedOperationValues(OperationPattern operation) {

            if (operation.isExpense)
                Expense_RadioButton.IsChecked = true;
            else Income_RadioButton.IsChecked = true;

            CostValue.Text = NewOperation.toCurrencyString(operation.Cost);
            acceptedCostValue = NewOperation.toCurrencyWithoutSymbolString(operation.Cost);
            
            NameValue.Text = operation.Title;

            if (operation is Operation && !string.IsNullOrEmpty(((Operation)operation).Date))
                DateValue.Date = DateTime.Parse(((Operation)operation).Date);

            CategoryValue.SelectedItem = CategoryValue.Items.OfType<ComboBoxItem>().SingleOrDefault(i => (int)i.Tag == operation.CategoryId);
            SubCategoryValue.SelectedItem = SubCategoryValue.Items.OfType<ComboBoxItem>().SingleOrDefault(item => (int)item.Tag == operation.SubCategoryId);
            PayFormValue.SelectedItem = PayFormValue.Items.OfType<ComboBoxItem>().SingleOrDefault(item => (int)item.Tag == operation.MoneyAccountId);

            if (!string.IsNullOrEmpty(operation.MoreInfo))
                MoreInfoValue.Text = operation.MoreInfo;

            isLoaded = true;
        }


        private void setPrimaryButtonEnabling() {
            IsPrimaryButtonEnabled = !string.IsNullOrEmpty(CostValue.Text) && isOperationEdited();
        }

        private bool isOperationEdited() {
            if (isLoaded)
                return operationPatternToEdit == null ?
                    !operationToEdit.Equals(editedOperation()) :
                    !operationPatternToEdit.Equals(editedOperationPattern());
            return false;
        }

        public Operation editedOperation() {
            return new Operation {
                Id = operationToEdit.Id,
                Date = getDate(),
                Title = NameValue.Text,
                Cost = decimal.Parse(acceptedCostValue, Settings.getActualCultureInfo()),
                isExpense = (bool)Expense_RadioButton.IsChecked,
                CategoryId = getCategoryId(),
                SubCategoryId = getSubCategoryId(),
                MoreInfo = MoreInfoValue.Text,
                MoneyAccountId = (int)((ComboBoxItem)PayFormValue.SelectedItem).Tag
            };
        }

        public OperationPattern editedOperationPattern() {
            return new OperationPattern {
                Id = operationPatternToEdit.Id,
                Title = NameValue.Text,
                Cost = decimal.Parse(acceptedCostValue, Settings.getActualCultureInfo()),
                isExpense = (bool)Expense_RadioButton.IsChecked,
                CategoryId = getCategoryId(),
                SubCategoryId = getSubCategoryId(),
                MoreInfo = MoreInfoValue.Text,
                MoneyAccountId = (int)((ComboBoxItem)PayFormValue.SelectedItem).Tag
            };
        }

        private string getDate() {
            return DateValue.Date == null ?
                string.Empty :
                DateValue.Date.Value.ToString("yyyy.MM.dd");
        }

        private int getCategoryId() {
            return CategoryValue.SelectedIndex != -1 ?
                (int)((ComboBoxItem)CategoryValue.SelectedItem).Tag :
                1;
        }

        private int getSubCategoryId() {
            return SubCategoryValue.SelectedIndex != -1 ?
                (int)((ComboBoxItem)SubCategoryValue.SelectedItem).Tag :
                -1;
        }

        private void NowaOperacja_DodajClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {

             if (operationPatternToEdit == null)
                Dal.saveOperation(editedOperation());
             else
                Dal.saveOperationPattern(editedOperationPattern());
        }

        private void TypeOfOperationRadioButton_Checked(object sender, RoutedEventArgs e) {

            if (CategoryValue.SelectedIndex != -1) {
                int idOfSelectedCategory = (int)((ComboBoxItem)CategoryValue.SelectedItem).Tag;
                int idOfSelectedSubCategory = -1;

                if (SubCategoryValue.SelectedIndex != -1)
                    idOfSelectedSubCategory = (int)((ComboBoxItem)SubCategoryValue.SelectedItem).Tag;

                setCategoryComboBoxItems((bool)Expense_RadioButton.IsChecked, (bool)Income_RadioButton.IsChecked);

                if (CategoryValue.Items.OfType<ComboBoxItem>().Any(i => (int)i.Tag == idOfSelectedCategory))
                    CategoryValue.SelectedItem = CategoryValue.Items.OfType<ComboBoxItem>().Single(i => (int)i.Tag == idOfSelectedCategory);

                else
                    SubCategoryValue.IsEnabled = false;

                if (idOfSelectedSubCategory != -1) {
                    if (SubCategoryValue.Items.OfType<OperationSubCategory>().Any(i => i.Id == idOfSelectedSubCategory)) {
                        OperationSubCategory subCatItem = Dal.getOperationSubCategoryById(idOfSelectedSubCategory);
                        SubCategoryValue.SelectedItem = SubCategoryValue.Items.OfType<ComboBoxItem>().Single(ri => ri.Content.ToString() == subCatItem.Name);
                    }
                }
            }

            else {
                setCategoryComboBoxItems((bool)Expense_RadioButton.IsChecked, (bool)Income_RadioButton.IsChecked);
                SubCategoryValue.IsEnabled = false;
            }

            setPrimaryButtonEnabling();
        }

        private void setCategoryComboBoxItems(bool inExpenses, bool inIncomes) {

            CategoryValue.Items.Clear();

            foreach (OperationCategory catItem in Dal.getAllCategories()) {

                if ((catItem.VisibleInExpenses && inExpenses) 
                    || (catItem.VisibleInIncomes && inIncomes)) {

                    CategoryValue.Items.Add(new ComboBoxItem {
                        Content = catItem.Name,
                        Tag = catItem.Id
                    });
                }
            }
        }

        private void setSubCategoryComboBoxItems(bool inExpenses, bool inIncomes) {

            SubCategoryValue.Items.Clear();

            if (CategoryValue.SelectedIndex != -1) {

                foreach (OperationSubCategory subCatItem in Dal.getOperationSubCategoriesByBossId((int)((ComboBoxItem)CategoryValue.SelectedItem).Tag)) {

                    if ((subCatItem.VisibleInExpenses && inExpenses)
                        || (subCatItem.VisibleInIncomes && inIncomes)) {

                        if (SubCategoryValue.Items.Count == 0)
                            SubCategoryValue.Items.Add(new ComboBoxItem {
                                Content = "Brak",
                                Tag = -1,
                            });
                        SubCategoryValue.Items.Add(new ComboBoxItem {
                            Content = subCatItem.Name,
                            Tag = subCatItem.Id
                        });
                    }
                }

                SubCategoryValue.IsEnabled = !(SubCategoryValue.Items.Count == 0);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            setSubCategoryComboBoxItems((bool)Expense_RadioButton.IsChecked, (bool)Income_RadioButton.IsChecked);
            setPrimaryButtonEnabling();
        }

        private void SubCategoryValue_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (SubCategoryValue.SelectedIndex == 0)
                SubCategoryValue.SelectedIndex--;

            setPrimaryButtonEnabling();
        }

        private void CostValue_GotFocus(object sender, RoutedEventArgs e) {
           isUnfocused = false;

            if (!string.IsNullOrEmpty(CostValue.Text)) {
                CostValue.Text = acceptedCostValue;
                CostValue.SelectionStart = CostValue.Text.Length;
            }
        }

        private void CostValue_LostFocus(object sender, RoutedEventArgs e) {
           isUnfocused = true;

            if (!string.IsNullOrEmpty(CostValue.Text))
                CostValue.Text = NewOperation.toCurrencyString(CostValue.Text);
        }

        private void CostValue_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args) {

            if (string.IsNullOrEmpty(CostValue.Text)) {
                acceptedCostValue = string.Empty;
                return;
            }

            else if (isUnfocused)
                return;

            else if (regex.Match(CostValue.Text).Value != CostValue.Text) {
                int whereIsSelection = CostValue.SelectionStart;
                CostValue.Text = acceptedCostValue;
                CostValue.SelectionStart = whereIsSelection - 1;
            }
            
            acceptedCostValue = CostValue.Text;
        }

        private void CostValue_TextChanged(object sender, TextChangedEventArgs e) {
            setPrimaryButtonEnabling();
        }

        private void NameValue_TextChanged(object sender, TextChangedEventArgs e) {
            setPrimaryButtonEnabling();
        }

        private void DateValue_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args) {
            setPrimaryButtonEnabling();
        }

        private void PayFormValue_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            setPrimaryButtonEnabling();
        }

        private void MoreInfoValue_TextChanged(object sender, TextChangedEventArgs e) {
            setPrimaryButtonEnabling();
        }
    }
}