using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MIE.APP.Modules.Main
{
   internal class CreateXML
    {
       ConnectDatabase condb = new ConnectDatabase();


       internal void CreateXMLToFile(string maTheBHYT, long maVienPhi) //Xuat tung ma Vienphi mot
       {
           try
           {
               string ten_fileXML = maTheBHYT + "_" + maVienPhi + ".xml"; //ten file

               XmlTextWriter writer = new XmlTextWriter(ten_fileXML, System.Text.Encoding.UTF8);
               writer.WriteStartDocument(true);
               writer.Formatting = Formatting.Indented;
               writer.Indentation = 2;
               writer.WriteStartElement("BangKe");
               writer.WriteAttributeString("PhienBan", "2.0"); //tieu de
               createNode_Bang1(maVienPhi, writer);
               //createNode_Bang1("1", "Product 1", "1000", writer);
               //createNode_Bang1("2", "Product 2", "2000", writer);
               //createNode_Bang1("3", "Product 3", "3000", writer);
               //createNode_Bang1("4", "Product 4", "4000", writer);
               writer.WriteEndElement();
               writer.WriteEndDocument();
               writer.Close();
               MessageBox.Show("XML File created ! ");
           }
           catch (Exception)
           {              
               throw;
           }
       }

       //private void createNode_Bang1(string pID, string pName, string pPrice, XmlTextWriter writer)
       //{
       //    writer.WriteStartElement("Product");
       //    writer.WriteStartElement("Product_id");
       //    writer.WriteString(pID);
       //    writer.WriteEndElement();
       //    writer.WriteStartElement("Product_name");
       //    writer.WriteString(pName);
       //    writer.WriteEndElement();
       //    writer.WriteStartElement("Product_price");
       //    writer.WriteString(pPrice);
       //    writer.WriteEndElement();
       //    writer.WriteEndElement();
       //}

       //tao bang 1

       private void createNode_Bang1(long maVienPhi, XmlTextWriter writer)
       {
           try
           {
               //Lay thong tin ve benh nhan
               string sql_ttbang1 = "";
               DataView dv_bang1 = new DataView(condb.getDataTable(sql_ttbang1));
               if (dv_bang1 != null && dv_bang1.Count > 0)
               {
 
               }


               writer.WriteStartElement("ma_lk");
               writer.WriteString(maVienPhi.ToString());
               writer.WriteEndElement();
               writer.WriteStartElement("stt");
               writer.WriteString(stt.ToString());
               writer.WriteEndElement();
               writer.WriteStartElement("ma_bn");
               writer.WriteString(ma_bn);
               writer.WriteEndElement();
           }
           catch (Exception)
           {
               
               throw;
           }
       }





    }
}
