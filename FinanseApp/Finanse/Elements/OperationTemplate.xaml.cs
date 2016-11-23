﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Finanse.Pages;
using System.Reflection;
using System.Globalization;
using Finanse.DataAccessLayer;
using Finanse.Models;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Finanse.Elements {

    public sealed partial class OperationTemplate : UserControl {

        private Models.OperationPattern Operation {

            get {
                return this.DataContext as Models.OperationPattern;
            }
        }

        public OperationTemplate() {

            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }

        public void Operation_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args) {

            /* BO PIERDOLI ŻE NULL WCHODZI */
            if (Operation == null)
                return;

            if (Settings.GetAccountEllipseVisibility()) {

                MoneyAccount moneyAccount = Dal.GetMoneyAccountById(Operation.MoneyAccountId);

                if (moneyAccount != null)
                    if (!string.IsNullOrEmpty(moneyAccount.Color)) {
                        MoneyAccountEllipse.Visibility = Visibility.Visible;
                        MoneyAccountEllipse.Fill = Functions.GetSolidColorBrush(moneyAccount.Color);
                    }
            }

            OperationCategory cat = Dal.GetOperationCategoryById(Operation.CategoryId);
            OperationSubCategory subCat = Dal.GetOperationSubCategoryById(Operation.SubCategoryId);
            SubCategoryNameStackPanel.Visibility = Visibility.Collapsed;

            /* WCHODZI IKONKA KATEGORII */
            //Icon_OperationTemplate.FontFamily = new FontFamily(Settings.GetActualIconStyle());
            Icon_OperationTemplate.Opacity = 1;
            Title_OperationTemplate.Foreground = (SolidColorBrush)Application.Current.Resources["Text"];

            if (cat != null && subCat == null) {
                Ellipse_OperationTemplate.Stroke = new SolidColorBrush(Functions.GetSolidColorBrush(cat.Color).Color);
                Icon_OperationTemplate.Foreground = new SolidColorBrush(Functions.GetSolidColorBrush(cat.Color).Color);
                Icon_OperationTemplate.Glyph = cat.Icon;

                if (String.IsNullOrEmpty(Operation.Title)) {
                    //Title_OperationTemplate.Foreground = (SolidColorBrush)Application.Current.Resources["Text-1"];
                    Title_OperationTemplate.Text = cat.Name;
                }
            }

            else if (cat != null && subCat != null) {
                Ellipse_OperationTemplate.Stroke = new SolidColorBrush(Functions.GetSolidColorBrush(subCat.Color).Color);
                Icon_OperationTemplate.Foreground = new SolidColorBrush(Functions.GetSolidColorBrush(subCat.Color).Color);
                Icon_OperationTemplate.Glyph = subCat.Icon;

                if (String.IsNullOrEmpty(Operation.Title)) {
                    //Title_OperationTemplate.Foreground = (SolidColorBrush)Application.Current.Resources["Text-1"];
                    Title_OperationTemplate.Text = subCat.Name;
                }
            }

           // ((ResourceDictionary)Application.Current.Resources["IconBase"]).Item
            
            else {
                Ellipse_OperationTemplate.Stroke = (SolidColorBrush)Application.Current.Resources["DefaultEllipseColor"];
                Icon_OperationTemplate.Foreground = (SolidColorBrush)Application.Current.Resources["DefaultEllipseColor"];
                Icon_OperationTemplate.Glyph = ((FontIcon)Application.Current.Resources["DefaultEllipseIcon"]).Glyph;
                Icon_OperationTemplate.Opacity = 0.2;
            }

            /* WYGLĄD KOSZTU (CZERWONY Z MINUSEM CZY ZIELONY Z PLUSEM) */
            if (Operation.isExpense) {

                Cost_OperationTemplate.Text = (-Operation.Cost).ToString("C", Settings.GetActualCurrency());
                Cost_OperationTemplate.Foreground = (SolidColorBrush)Application.Current.Resources["RedColorStyle"];
            }

            else {

                Cost_OperationTemplate.Text = Operation.Cost.ToString("C", Settings.GetActualCurrency());
                Cost_OperationTemplate.Foreground = (SolidColorBrush)Application.Current.Resources["GreenColorStyle"];
            }

            SubCategory_OperationTemplate.Text = "";
            Category_OperationTemplate.Text = "";

            /* WYGLĄD NAZWY KATEGORII */
            if (Settings.GetCategoryNameVisibility()) {
                CategoryNameStackPanel.Visibility = Visibility.Visible;
                Category_OperationTemplate.Text = "Nie znaleziono kategorii";
                if (cat != null)
                    Category_OperationTemplate.Text = cat.Name;

                if (subCat != null) {
                    SubCategoryNameStackPanel.Visibility = Visibility.Visible;
                    SubCategory_OperationTemplate.Text = subCat.Name;
                }
            }
        }
    }
}
