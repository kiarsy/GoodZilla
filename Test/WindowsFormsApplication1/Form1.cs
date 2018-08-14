using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<object> Query(Type classType)
        {
            List<string> s = new List<string>();

            foreach (System.Reflection.PropertyInfo P in classType.GetProperties())
            {
                s.Add(P.Name);
            }
            List<object> SS = new List<object>();

            object obj = Activator.CreateInstance(classType);

            int c = 0;
            foreach (string st in s)
            {
                c++;
                PropertyInfo numberPropertyInfo = classType.GetProperty(st);
                numberPropertyInfo.SetValue(obj, c, null);
            }

            SS.Add(obj);


            return SS;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var aasdasd = Customer.DB().CreateCommnad()
    .Sql(@"SELECT Payment_Date,Payment_PayMoney,Sanad_Code as [Sanad.Sanad_Code],Payment_Second,Payment_Money,Payment_Wage,Payment_Code FROM tbl_Payment WHERE Payment_code=55")
    .QuerySingle<DAL.Payment.Payment>();
            
            
            var bbbb = aasdasd;
            var bb = Customer.DB().CreateCommnad().Sql(@"
SELECT 
    * 
FROM  
    tbl_ModuleLayout 
    INNER JOIN tbl_Module ON tbl_ModuleLayout.Module_Code = tbl_Module.Module_Code 
WHERE 
    (tbl_ModuleLayout.LayoutChild_Code = @1) AND 
    (tbl_ModuleLayout.ModuleLayout_Enable=1) AND
    (tbl_ModuleLayout.LayoutPosition_Code=@2)
ORDER BY 
    tbl_ModuleLayout.ModuleLayout_Index
")
                .ParameterAdd(3)
                .ParameterAdd(1)
                .Query<BLL.Module.ModuleLayout>();

            var bbb = bb;
            Class2 a = Customer.DB().CreateCommnad()
                .Sql(@"SELECT    table1.code, table1.name, Table2.T2_Family as [class3.T2_Family]
FROM         table1 INNER JOIN
                      Table2 ON table1.code = Table2.Code")
                .QuerySingle<Class2>();
            MessageBox.Show(a.code.ToString());
            return;

            //db.Class1 Class1 = new db.Class1();


            //Class1.begin();

            //Class1.Update1();
            //Class1.Update2();

            //Class1.rollback();


            //db.Class2 Class2 = new db.Class2();
            //Class2.Update1();
            //return;


            //        Customer.DB().TransactionBegin();

            //        Customer.DB().CreateCommnad()
            //            .Sql("SEARCH1")
            //            .ParameterAdd("@a", 1)
            //            .exe



            //        Customer.DB().CreateCommnad().changeCommandType(CommandType.StoredProcedure)
            //.Sql("SEARCH1")
            //.ParameterAdd("@a", 1)
            //.ExecuteNonQuery();

            //        Customer.DB().CreateCommnad().changeCommandType(CommandType.StoredProcedure)
            //.Sql("SEARCH1")
            //.ParameterAdd("@a", 1)
            //.ExecuteReturnLast();


            //        Customer.DB().TransactionCommit();

            //        return;
            //        Goodzila.GoodzilaCore db = new Goodzila.GoodzilaCore(Goodzila.DbProviders.Microsoft_Sql_Server, @"Data Source=.;Initial Catalog=Test;Integrated Security=True");

            //        //List<Class1> o = db.CreateCommnad()
            //        //    .Sql("SELECT * FROM Customer")
            //        //    .Query<Class1>();

            //       // dataGridView1.DataSource = o;

            //        db.TransactionBegin();

            //        db.CreateCommnad()
            //            .Sql("UPDATE Customer SET Name=@1 WHERE Code=@2")
            //            .changeCommandType(CommandType.Text)
            //            .ParameterAdd("Name1")
            //            .ParameterAdd("1")
            //            .ExecuteNonQuery();

            //        db.CreateCommnad()
            //            .Sql("UPDATE Customer SET Name=@1 WHERE Code=@2")
            //            .ParameterAdd("Name2")
            //            .ParameterAdd("2")
            //            .ExecuteNonQuery();

            //        //int X = db.CreateCommnad()
            //        //    .Sql("INSERT INTO Customer (Name) VALUES (@1)")
            //        //    .ParameterAdd("Name2")
            //        //    .ExecuteReturnLast();

            //        db.TransactionCommit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            List<string> DR = new List<string>();

            DR.Add("code");
            DR.Add("name");
            DR.Add("class3.T2_Family");


            object obj = Activator.CreateInstance(typeof(Class2));

            var classProperties = typeof(Class2).GetProperties()
                .Where(Pro => Pro.PropertyType.UnderlyingSystemType.Namespace.ToLower() == typeof(Class2).Namespace.ToLower())
                .Select(itm => new Goodzila.Property() { ClassObject = Activator.CreateInstance(itm.PropertyType.UnderlyingSystemType), PropertyName = itm.Name });
            List<Goodzila.Property> Property = new List<Goodzila.Property>();
            Property.AddRange(classProperties);

            foreach (string column in DR)
            {
                object value = "12";
                if (value == DBNull.Value)
                    value = null;

                if (column.IndexOf('.') == -1)
                {
                    PropertyInfo propertyInfo = typeof(Class2).GetProperty(column);
                    
                    propertyInfo.SetValue(obj, value, null);
                }
                else
                {
                    
                    string columnN = column.Split('.')[1];

                    foreach (var item in Property)
                    {
                        object Cls = item.ClassObject;
                        PropertyInfo propertyInfo = Cls.GetType().GetProperty(columnN);
                        if (propertyInfo != null)
                            propertyInfo.SetValue(Cls, value, null);
                        item.ClassObject = Cls;
                        break;
                    }

                }
            }
            foreach (var item in Property)
            {
                PropertyInfo propertyInfo = typeof(Class2).GetProperty(item.PropertyName.ToString());
                propertyInfo.SetValue(obj, item.ClassObject, null);
            }



        }
    }
}