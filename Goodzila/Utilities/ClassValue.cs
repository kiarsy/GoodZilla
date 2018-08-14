using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Goodzila.Utilities
{
    internal class ClassValue
    {
        public T SetClassValues<T>(DbDataReader Dr)
        {
            #region List Of Class Refrence Column Name

            //Get List Of Refrenced Properties
            //(Has Attribute [Goodzila.Attributes.RefrenceType])
            //(Has Same Base Class)

            var ClassRefrenceColumns = typeof(T).GetProperties()
                .Where(itm =>
                    itm.GetCustomAttributes(typeof(Goodzila.Attributes.ForeignKey), false).Length > 0  //(Has Attribute [Goodzila.Attributes.RefrenceType])

                    ||

                    itm.PropertyType.BaseType == typeof(T).BaseType //(Has Same Base Class)
                    )
                .Select(itm => new Goodzila.Struct.Property()
                {
                    ClassObject = Activator.CreateInstance(itm.PropertyType),
                    PropertyName = itm.Name
                });

            //-----
            List<Goodzila.Struct.Property> ListClassProperty = new List<Goodzila.Struct.Property>();
            ListClassProperty.AddRange(ClassRefrenceColumns);
            #endregion

            #region List OF Query Column Name
            var QueryColumnNames = new List<string>();

            for (int i = 0; i < Dr.FieldCount; i++)
            {
                QueryColumnNames.Add(Dr.GetName(i));
            }

            #endregion

            T ClassRecord = (T)Activator.CreateInstance(typeof(T));

            foreach (string column in QueryColumnNames)
            {
                //Read Current Column Value
                object CurrentValue = Dr[column];

                //Get Current Property
                PropertyInfo property = GetProperty<T>(column);
                string ColumnSelectClass = "";
                string ColumnSelectClassField = "";
                if (property == null)
                    if (column.IndexOf('.') != -1)
                    {
                        ColumnSelectClass = column.Split('.')[0];
                        ColumnSelectClassField = column.Split('.')[1];
                        property = GetProperty<T>(ColumnSelectClass);
                    }
                    else
                        continue;

                //Set Default Value if Value Is Null
                if (CurrentValue == DBNull.Value)
                    CurrentValue = GetDefault(property.PropertyType);

                

                if (column.IndexOf('.')!=-1)
                {
                    ColumnSelectClass = column.Split('.')[0];
                }

                //Set VALUE
                if (ClassRefrenceColumns.Count(itm => itm.PropertyName.ToLower() == column.ToLower()) > 0 // Is Refrence Property
                    ||

                    (ColumnSelectClass != "" && ClassRefrenceColumns.Count(itm => itm.PropertyName.ToLower() == ColumnSelectClass.ToLower()) > 0)
                    )
                {
                    #region Set Refrenced Type Property

                    //Get Refrence Properies
                    Struct.Property refClassProperty = ClassRefrenceColumns.First(itm => itm.PropertyName.ToLower() == column.ToLower() || itm.PropertyName.ToLower() == ColumnSelectClass.ToLower());

                    //Get Refrence Class
                    object RefrenceClass = refClassProperty.ClassObject;

                    //Get Refrence Class Value Property
                    PropertyInfo RefrenceClassValueProperty = RefrenceClass.GetType().GetProperty(refClassProperty.ColumnName);


                    object tmpRefrenceClass = property.GetValue(ClassRecord, null);
                    //Set Value

                    if (ColumnSelectClass != "")
                    {
                        RefrenceClassValueProperty = RefrenceClass.GetType().GetProperty(ColumnSelectClassField);
                        RefrenceClassValueProperty.SetValue(tmpRefrenceClass, changeType(CurrentValue, RefrenceClassValueProperty.PropertyType), null);
                        //Set Refrence Class to T Property
                        property.SetValue(ClassRecord, tmpRefrenceClass, null);
                    }
                    else
                    {
                        RefrenceClassValueProperty.SetValue(RefrenceClass, changeType(CurrentValue, RefrenceClassValueProperty.PropertyType), null);
                        //Set Refrence Class to T Property
                        property.SetValue(ClassRecord, RefrenceClass, null);
                    }
                    



                    

                    #endregion
                }
                else
                {
                    #region Set Value Type Property

                    //Get Property
                    //Set Value To Record
                    if (property != null)
                        if (CurrentValue != null)
                            property.SetValue(ClassRecord, changeType(CurrentValue, property.PropertyType), null);

                    #endregion
                }
            }

            //Add To OutPut Records
            return ClassRecord;
        }



        private PropertyInfo GetProperty<T>(string ColumnName)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(ColumnName);
            if (propertyInfo==null)
            {
                propertyInfo = typeof(T).GetProperties().Where(property => property.Name.ToLower() == ColumnName.ToLower()).FirstOrDefault();
            }

            return propertyInfo;
        }

        private object GetDefault(Type type)
        {

            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        private object changeType(object value, Type t)
        {
            t = Nullable.GetUnderlyingType(t) ?? t;
            return Convert.ChangeType(value, t);
        }

    }
}