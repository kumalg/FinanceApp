﻿using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using SQLite.Net.Attributes;

namespace Finanse.Models.Categories {

    public class Category {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 
        public string Name  { get; set; }
        public string ColorKey { get; set; }
        public string IconKey { get; set; }
        public bool VisibleInIncomes { get; set; } 
        public bool VisibleInExpenses { get; set; }
        public bool CantDelete { get; set; }

        public SolidColorBrush Brush {
            get {
                return string.IsNullOrEmpty(ColorKey) ?
                    (SolidColorBrush)Application.Current.Resources["DefaultEllipseColor"] :
                    (SolidColorBrush)(((ResourceDictionary)Application.Current.Resources["ColorBase"]).FirstOrDefault(i => i.Key.Equals(ColorKey)).Value);
            }
        }
        public FontIcon Icon {
            get {
                return string.IsNullOrEmpty(IconKey) ?
                    (FontIcon)Application.Current.Resources["DefaultEllipseIcon"] :
                    (FontIcon)(((ResourceDictionary)Application.Current.Resources["IconBase"]).FirstOrDefault(i => i.Key.Equals(IconKey)).Value);
            }
        }
        public Category() {
        }

        public Category(Category previousCategory) {
            Id = previousCategory.Id;
            IconKey = previousCategory.IconKey;
            ColorKey = previousCategory.ColorKey;
            Name = previousCategory.Name;
            VisibleInExpenses = previousCategory.VisibleInExpenses;
            VisibleInIncomes = previousCategory.VisibleInIncomes;
        }

        public override int GetHashCode() {
            return Name.GetHashCode() * Id;
        }
        public override bool Equals(object o) {
            if (!(o is Category))
                return false;

            Category secondCategory = o as Category;

            return
                ColorKey == secondCategory.ColorKey &&
                IconKey == secondCategory.IconKey &&
                Name == secondCategory.Name &&
                VisibleInExpenses == secondCategory.VisibleInExpenses &&
                VisibleInIncomes == secondCategory.VisibleInIncomes;
        }
    }
}
