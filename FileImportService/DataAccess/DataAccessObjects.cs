using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FileImportService.DataAccess
{
  public class DataAccessObjects
    {
        string file ;

        XDocument xdoc ;

        public void ReadRecursiveley()
        {
            file = Environment.CurrentDirectory + "\\settings2.xml";
            xdoc = XDocument.Load(file);
            Recursive(xdoc.Elements());
        }


        public void ReadSettings()
        {
            file = Environment.CurrentDirectory + "\\settings2.xml";
            XmlDocument xdc = new XmlDocument();
            xdc.Load(file);
            XmlNodeList xnl = xdc.DocumentElement.SelectNodes("/NewSeviceEntryNames/SeviceEntry");
            foreach (XmlNode xn in xnl)
            {
                System.Windows.Forms.MessageBox.Show(xn.Attributes["Name"].Value);

            xnl = xdc.DocumentElement.SelectNodes("/NewSeviceEntryNames/SeviceEntry/Database"); 
            foreach(XmlNode xn2 in xnl)
            {         
                
                System.Windows.Forms.MessageBox.Show(xn2["Name"].InnerText);
                System.Windows.Forms.MessageBox.Show(xn2["Username"].InnerText);
                System.Windows.Forms.MessageBox.Show(xn2["Password"].InnerText);
               
                
            }
            }
           
            
        }


        public void Recursive(IEnumerable<XElement> elements)
        {
            foreach (XElement n in elements)
            {
                Console.WriteLine(n.Name);
                Console.WriteLine("--");
                if (n.Descendants().Any())
                {
                    //System.Windows.Forms.MessageBox.Show(n.Value.ToString());
                   
                    //System.Windows.Forms.MessageBox.Show(n.Attribute);
                    Recursive(n.Elements());
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(n.Name.ToString() + "=" + n.Value.ToString());// End of node (leaf)
                }
            }
        }
        //https://www.youtube.com/watch?v=OzwqpFfifoQ
        public void ReadXMLSettings()
        {
            string file = Environment.CurrentDirectory + "\\settings2.xml";

            XmlTextReader xtr = new XmlTextReader(file);

            while (xtr.Read())                
            {

                xtr.NodeType.GetType();
                //System.Windows.Forms.MessageBox.Show(xtr.NodeType.GetType().ToString());
                xtr.NodeType.GetType();
                xtr.NodeType.GetType();
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "NewSeviceEntryNames")
                {
                    //System.Windows.Forms.MessageBox.Show(xtr.ReadElementString());
                    //}

                    

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "SeviceEntry")
                    {
                       
                        //System.Windows.Forms.MessageBox.Show(xtr.ReadElementString().ToString());
                        if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "DataBase")
                        {
                            xtr.MoveToElement();
                            if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Name")
                            {
                                System.Windows.Forms.MessageBox.Show(xtr.ReadElementString().ToString());
                            }
                            xtr.MoveToElement();
                            if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "UserName")
                            {
                                System.Windows.Forms.MessageBox.Show(xtr.ReadElementString().ToString());
                            }
                            xtr.MoveToElement();
                            if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Password")
                            {
                                System.Windows.Forms.MessageBox.Show(xtr.ReadElementString().ToString());
                            }
                        }

                        if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Folders")
                        {
                            System.Windows.Forms.MessageBox.Show(xtr.ReadElementString().ToString());
                            if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Name")
                            {
                                System.Windows.Forms.MessageBox.Show(xtr.ReadElementString().ToString());
                            }
                            if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Backup")
                            {
                                System.Windows.Forms.MessageBox.Show(xtr.ReadElementString().ToString());
                            }
                            if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Failed")
                            {
                                System.Windows.Forms.MessageBox.Show(xtr.ReadElementString().ToString());
                            }
                        }

                        if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Command")
                        {
                            System.Windows.Forms.MessageBox.Show(xtr.ReadElementString().ToString());
                            if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "StoredProcedure")
                            {
                                System.Windows.Forms.MessageBox.Show(xtr.ReadElementString().ToString());
                            }
                            if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "SQL")
                            {
                                System.Windows.Forms.MessageBox.Show(xtr.ReadElementString().ToString());
                            }

                        }
                    }

                }
              
            }
        }
    }

}
    
