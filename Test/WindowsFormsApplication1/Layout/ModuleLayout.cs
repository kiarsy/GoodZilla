using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Module
{
    public class ModuleLayout 
    {
        #region Fields

        public int LayoutPosition_Code
        {
            get;
            set;
        }
        public int LayoutChild_Code
        {
            get;
            set;
        }
        public int Module_Code
        {
            get;
            set;
        }
        public int ModuleLayout_Code
        {
            get;
            set;
        }

        public int ModuleLayout_Index
        {
            get;
            set;
        }
        public string ModuleLayout_Title
        {
            get;
            set;
        }
        public bool ModuleLayout_Enable
        {
            get;
            set;
        }

        public string ModuleLayout_Config
        {
            get;
            set;
        }
        public string ModuleLayout_Condition
        {
            get;
            set;
        }

        public string ModuleLayout_WebPanel
        {
            get;
            set;
        }

        
        public string Module_Name
        {
            set;
            get;
        }
        #endregion

    }
}
