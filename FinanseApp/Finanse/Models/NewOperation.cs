﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Finanse.Models {
    class NewOperation {
        public static string toCurrencyString(string value) {
            return decimal.Parse(value, Settings.getActualCultureInfo()).ToString("C", Settings.getActualCultureInfo());
        }

        public static string toCurrencyString(decimal value) {
            return value.ToString("C", Settings.getActualCultureInfo());
        }

        public static string toCurrencyWithoutSymbolString(decimal value) {
            return value.ToString(Settings.getActualCultureInfo());
        }

        public static Regex getRegex() {
            NumberFormatInfo numberFormatInfo = Settings.getActualCultureInfo().NumberFormat;
            int decimalDigits = numberFormatInfo.CurrencyDecimalDigits;
            char decimalSeparator = numberFormatInfo.CurrencyDecimalSeparator[0];

            string format = decimalDigits == 0 ?
                @"\d+" :
                @"\d+\" + decimalSeparator + @"\d{0," + decimalDigits + @"}|\d+|\" + decimalSeparator + @"\d{0," + decimalDigits + @"}";

            return new Regex(format);
        }

        public static Regex getSignedRegex() {
            NumberFormatInfo numberFormatInfo = Settings.getActualCultureInfo().NumberFormat;
            int decimalDigits = numberFormatInfo.CurrencyDecimalDigits;
            char decimalSeparator = numberFormatInfo.CurrencyDecimalSeparator[0];

            string format = decimalDigits == 0 ?
                @"[+-]?\d+|[+-]" :
                @"[+-]?(\d+(\" + decimalSeparator + @"\d{0," + decimalDigits + @"})?)?";
            /*
             * \((\d+\,\d{0,2}|\d+|\,\d{0,2})\)?|\(|(\d+\,\d{0,2}|\d+|\,\d{0,2})
             * 
            string format2 = decimalDigits == 0 ?
                @"[+-]?\d+|[+-]" :
                @"\(?(\d+\" + decimalSeparator + @"\d{0," + decimalDigits + @"}|\d+|\" + decimalSeparator + @"\d{0," + decimalDigits + @"})|[+-]";
                */
            return new Regex(format);
        }
    }
}
