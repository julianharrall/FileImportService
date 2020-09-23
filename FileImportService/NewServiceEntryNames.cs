using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace FileImportService
{
    /// <summary>
    /// This class is a top level class and contains a reference to the 
    /// object collection class NewSeviceEntryCollection
    /// It takes a parameter of the Mainform object class as this
    /// parameter is used to pass to NewSeviceEntryCollection
    /// as this is used to access the Stop/Start Button on the Mainform
    /// </summary>
    public class NewServiceEntryNames
    {
        public bool StopStartServiceFlagClass { get; set; }
        public string xmlfileSvcEntry { get; set; }
        public NewSeviceEntryCollection NSC { get; set; }
        public XDocument xmlDocSvcEntry { get; set; }
        public string xmlFilePathConfig { get; set; }
        public MainForm main_form { get; set; }

        /// <summary>
        /// Loads xml file that may or may not have entries
        /// and creates NewSeviceEntryCollection object collection
        /// </summary>
        public NewServiceEntryNames(MainForm mf)
        {
            // create class level reference to Mainform
            main_form = mf;           
         
            // Set local copy of xml file
            xmlfileSvcEntry = MainForm.xmlfileN;

            // Load file into XDocument
            xmlDocSvcEntry = XDocument.Load(xmlfileSvcEntry);

            // Load and create all entries into collection
            NSC = new NewSeviceEntryCollection(mf);

            if (NSC.Count() > 0)
            {                   
                foreach (NewSeviceEntry nse in NSC)
                {

                    //if (nse.Enabled == "True")
                    //newEntryButton.BackColor = Color.LightBlue;
                }
            }      
            
        }     

        /// <summary>
        /// Checks and returns the service status, 
        /// whether service is Stopped or Started
        /// </summary>
        /// <returns></returns>
        public string CheckServiceStatus()
        {
           
            return xmlDocSvcEntry.Element("NewSeviceEntryNames").Element("ServiceStatus").Value;
        }

        public void SetStopStartFlag()
        {
          
            if (File.Exists(xmlfileSvcEntry))
            {
                NSC = new NewSeviceEntryCollection(main_form);
                NSC.GetAllEntries();                
            }
        }

        internal void UpdateServiceButton(string statusstring)
        {
            //throw new NotImplementedException();
            //XDocument xdoc = XDocument.Load(xmlFilePathConfig);
            /*
            var element = xmlDocSvcEntry.Element("NewSeviceEntryNames").Element("ServiceStatus");
            element.Value = statusstring;            
            xmlDocSvcEntry.Save(xmlfileSvcEntry);
            */

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlfileSvcEntry);
            XmlNode node = xmlDoc.SelectSingleNode("/NewSeviceEntryNames/ServiceStatus");
            node.InnerText = statusstring;
            xmlDoc.Save(xmlfileSvcEntry);


        }        
       
    }
}
