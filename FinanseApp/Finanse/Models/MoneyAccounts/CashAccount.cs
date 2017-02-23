﻿using Finanse.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanse.Models.MoneyAccounts {
    class CashAccount : Account {
        public override string GetActualMoneyValue() => AccountsDal.CashAccountBalanceById(Id).ToString("C", Settings.GetActualCultureInfo());
    }
}
