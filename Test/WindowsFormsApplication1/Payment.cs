using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Payment
{
    public class Payment 
    {
        #region Properties
        public int Payment_Code
        {
            get;
            set;
        }

        public string Payment_Date
        {
            get;
            set;
        }

        public Decimal Payment_Money
        {
            get;
            set;
        }

        public Decimal Payment_Wage
        {
            get;
            set;
        }

        public Decimal Payment_PayMoney
        {
            get;
            set;
        }

        public Decimal Payment_Second
        {
            get;
            set;
        }

        public Sanad Sanad
        {
            get;
            set;
        }
        #endregion


    }
}
