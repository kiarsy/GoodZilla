using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL.Layout
{
    public class Layout 
    {
        public int Layout_Code
        {
            get;
            set;
        }
        public int Layout_Column
        {
            get;
            set;
        }
        public int Layout_Index
        {
            get;
            set;
        }


        public Position position
        {
            get;
            set;
        }
        public Place place
        {
            get;
            set;
        }
        public Domain domain
        {
            get;
            set;
        }
        public LayoutPosition layoutPosition
        {
            get;
            set;
        }

        
    }
}