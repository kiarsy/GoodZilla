using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Sanad 
    {
        #region Properties
        public Int64 Sanad_Code
        {
            get;
            set;
        }

        public string Sanad_Date
        {
            get;
            set;
        }

        public string Sanad_CreateUser
        {
            get;
            set;
        }

        public bool Sanad_Sys
        {
            get;
            set;
        }

        public int SanadType_Code
        {
            get;
            set;
        }

        public string Sanad_Description
        {
            get;
            set;
        }

        public decimal Sanad_Bed
        {
            get;
            set;
        }

        public decimal Sanad_Bes
        {
            get;
            set;
        }

        public Payment.Payment Payment
        {
            get;
            set;
        }

        #endregion

        //public List<Sanad> List()
        //{
        //    return DbCommad().Sql("SELECT City_Code,City_Name FROM tbl_City")
        //        .Query<Sanad>();
        //}

    }
}
