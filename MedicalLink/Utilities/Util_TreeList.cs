using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.Utilities
{
    public static class Util_TreeList
    {
        public static List<T> TreeListToList<T>(this TreeList treelist) where T : class, new()
        {
            try
            {
                T tempT = new T();
                var tType = tempT.GetType();
                List<T> list = new List<T>();
                foreach (TreeListNode item in treelist.Nodes)
                {
                    T returnValue = default(T);
                    object _dataNode = treelist.GetDataRecordByNode(item);
                    foreach (var value in _dataNode)
                    {
                    }
                    //T returnValue_1 = (new[] { _dataNode }).Cast<T>().Single();




                    //returnValue = (T)Convert.ChangeType(_dataNode, typeof(T));
                    list.Add(returnValue);
                }

                //
                //T obj = new T();
                //foreach (var prop in obj.GetType().GetProperties())
                //{
                //    //var propertyInfo = tType.GetProperty(prop.Name);
                //    //var rowValue = row[prop.Name];
                //    //var t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                //    //try
                //    //{
                //    //    object safeValue = (rowValue == null || DBNull.Value.Equals(rowValue)) ? null : Convert.ChangeType(rowValue, t);
                //    //    propertyInfo.SetValue(obj, safeValue, null);

                //    //}
                //    //catch (Exception ex)
                //    //{//this write exception to my logger
                //    //    continue;
                //    //}
                //}
                //list.Add(obj);

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
