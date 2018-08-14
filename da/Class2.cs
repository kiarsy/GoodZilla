using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace da
{
    public abstract class Class2
    {
        public void commit()
        {
            DB.gDb().TransactionCommit();
        }

        public void rollback()
        {
            DB.gDb().TransactionRollBack();
        }

        public void begintrans()
        {
            DB.gDb().TransactionBegin();

        }



    }
}
