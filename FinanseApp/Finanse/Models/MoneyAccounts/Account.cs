﻿using Finanse.DataAccessLayer;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Finanse.Models.MoneyAccounts {
    abstract public class Account : ICloneable {

        private int id = -1;

        [PrimaryKey]
        public int Id {
            get {
                if (id == -1)
                    id = AccountsDal.getHighestIdOfAccounts() + 1;
                return id;
            }
            set {
                id = value;
            }
        }
        public string Name { get; set; }
        public string ColorKey { get; set; }

        public SolidColorBrush SolidColorBrush {
            get {
                return string.IsNullOrEmpty(ColorKey) ?
                    (SolidColorBrush)Application.Current.Resources["DefaultEllipseColor"] :
                    (SolidColorBrush)(((ResourceDictionary)Application.Current.Resources["ColorBase"]).FirstOrDefault(i => i.Key.Equals(ColorKey)).Value);
            }
        }

        public abstract string getActualMoneyValue();

        public override string ToString() {
            return Name;
        }

        public Account Clone() {
            return (Account)MemberwiseClone();
        }

        public override int GetHashCode() {
            return Name.GetHashCode() * Id;
        }
        public override bool Equals(object o) {
            if (o == null || !(o is Account))
                return false;

            Account secondAccount = (Account)o;

            return
                secondAccount.Id == Id &&
                secondAccount.Name == Name &&
                secondAccount.ColorKey == ColorKey;
        }
    }
}
