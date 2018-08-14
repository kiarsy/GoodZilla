using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Request 
    {
        #region Properties
      
        public int Request_Code
        {
            get;
            set;
        }

        public string Request_Date
        {
            get;
            set;
        }

        public Int64 Request_Money
        {
            get;
            set;
        }

        public bool Request_Payment
        {
            get;
            set;
        }
        
        public Int16 Request_Installment
        {
            get;
            set;
        }

        public Int16 Request_Rest
        {
            get;
            set;
        }

        public Int64 Request_IstallmentMoney
        {
            get;
            set;
        }

        public Int64 Request_IstallmentLastMoney
        {
            get;
            set;
        }

        public bool Request_Accept
        {
            get;
            set;
        }
        
        public bool Request_Reject
        {
            get;
            set;
        }

        public string Request_DeliberationDate
        {
            get;
            set;
        }


        public SubScribe SubScribe
        {
            get;
            set;
        }


        public SubScribe Request_Guarantor1
        {
            get;
            set;
        }



        public SubScribe Request_Guarantor2
        {
            get;
            set;
        }

        #endregion

       

    }
}
