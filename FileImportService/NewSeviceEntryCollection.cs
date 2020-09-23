using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace FileImportService
{
    public class NewSeviceEntryCollection : List<NewSeviceEntry>
    {
        private Button btnStopStart;
        //private Action<object, EventArgs> btnStart_Click;

        public string xmlfile { get; set; }
        List<NewSeviceEntry> ne { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mn"></param>
        public NewSeviceEntryCollection(MainForm mn)
        {
            //btnStopStart = btnMF;
            btnStopStart = mn.butStart;
            // Get and set filename
            xmlfile = MainForm.xmlfileN;                

            // If file exists - Get all entries
            if (File.Exists(xmlfile))
            {
                GetAllEntries();
            }
            else // File should exist but incase been deleted/moved - set to Stopped with no entries
            { 
                btnStopStart.Enabled = false;
                btnStopStart.BackColor = Color.LightGray;
                btnStopStart.Font = new Font(btnStopStart.Font.Name, btnStopStart.Font.Size, FontStyle.Regular);
                btnStopStart.Text = "Stopped";
                btnStopStart.ForeColor = Color.White;
            }
        }

        /*
        public NewSeviceEntryCollection(Action<object, EventArgs> btnStart_Click)
        {
            this.btnStart_Click = btnStart_Click;            
        }
        */

        public void GetAllEntries() // ############################################################################################
        {         
            ne = (from e in XDocument.Load(xmlfile).Root.Elements("SeviceEntry")
                  select new NewSeviceEntry
                  {
                      NewSeviceEntryName = (string)e.Element("ID"),

                      Enabled = (string)e.Element("Enabled"),

                      NewSeviceEntryDataBases =
                      (from d in e.Elements("Database")
                       select new NewSeviceEntry.NewSeviceEntryDataBase
                       {
                           NewSeviceEntryDB = (string)d.Element("Name"),
                           NewSeviceEntryDBUName = (string)d.Element("Username"),
                           NewSeviceEntryDBPass = (string)d.Element("Password"),
                       }
                       ).ToArray(),

                      NewSeviceEntryFolders =
                      (from f in e.Elements("Folders")
                       select new NewSeviceEntry.NewSeviceEntryFolder
                       {
                           NewSeviceEntryMainFolder = (string)f.Element("Name"),
                           NewSeviceEntryBackup = (string)f.Element("Backup"),
                           NewSeviceEntryFolderFail = (string)f.Element("Failed")
                       }
                       ).ToArray(),

                      NewSeviceEntryCommands =
                      (from c in e.Elements("Command")
                       select new NewSeviceEntry.NewSeviceEntryCommand
                       {
                           NewSeviceEntrySProc = (string)c.Element("StoredProcedure"),
                           NewSeviceEntrySQL = (string)c.Element("SQL")
                       }
                       ).ToArray(),

                      /*
                      NewSeviceEntryFileMappings =
                      (from c in e.Elements("FilaMappings")
                       select new NewSeviceEntry.NewSeviceEntryFileAssoc
                       {
                           ColNum = (string)c.Element("StoredProcedure"),
                           ColName = (string)c.Element("SQL"),
                           ColDataType = (string)c.Element("SQL")
                       }
                       ).ToArray(),
                     */

                      AssociateMappings = (bool)e.Element("Association"),

                      NewSeviceEntryFileAss =
                      (from d in e.Elements("FileAssociation").Elements("RowDetails")
                       select new NewSeviceEntry.NewSeviceEntryFileAssoc
                       {
                           ColNum = (int)d.Element("ColNum"),
                           ColName = (string)d.Element("ColName"),
                           ColDataType = (string)d.Element("ColDataType"),
                       }
                       ).ToList(),

                      NewSeviceEntryMap =
                      (from d in e.Elements("RowDetails")//.Elements("Row")
                       select new NewSeviceEntry.NewSeviceEntryFileMappings
                       {
                           RowVal = (string)d.Element("Row"),
                          
                       }
                       ).ToList(),


                  }).ToList();

            foreach (NewSeviceEntry dr in ne)
            {
                this.Add(dr);
            }
        }

        public void GetEntry(string name)
        {
            //this.Contains(new SeviceEntry(name));
        }

        public void AddNewEntry(XDocument xdoc)
        {// need to add only the new entry at the moment we are reading both entries - I think!!
            List<NewSeviceEntry> ne;
            ne = (from e in xdoc.Root.Elements("SeviceEntry")
                  select new NewSeviceEntry
                  {
                      NewSeviceEntryName = (string)e.Element("ID"),

                      Enabled = ((string)e.Element("Enabled")).ToLower(),

                      NewSeviceEntryDataBases =
                      (from d in e.Elements("Database")
                       select new NewSeviceEntry.NewSeviceEntryDataBase
                       {
                           NewSeviceEntryDB = (string)d.Element("Name"),
                           NewSeviceEntryDBUName = (string)d.Element("Username"),
                           NewSeviceEntryDBPass = (string)d.Element("Password"),
                       }
                       ).ToArray(),

                      NewSeviceEntryFolders =
                      (from f in e.Elements("Folders")
                       select new NewSeviceEntry.NewSeviceEntryFolder
                       {
                           NewSeviceEntryMainFolder = (string)f.Element("Name"),
                           NewSeviceEntryBackup = (string)f.Element("Backup"),
                           NewSeviceEntryFolderFail = (string)f.Element("Failed")
                       }
                       ).ToArray(),

                      NewSeviceEntryCommands =
                      (from c in e.Elements("Command")
                       select new NewSeviceEntry.NewSeviceEntryCommand
                       {
                           NewSeviceEntrySProc = (string)c.Element("StoredProcedure"),
                           NewSeviceEntrySQL = (string)c.Element("SQL")
                       }
                       ).ToArray(),

                      /*
                      NewSeviceEntryFileMappings =
                      (from c in e.Elements("FilaMappings")
                       select new NewSeviceEntry.NewSeviceEntryFileAssoc
                       {
                           ColNum = (string)c.Element("StoredProcedure"),
                           ColName = (string)c.Element("SQL"),
                           ColDataType = (string)c.Element("SQL")
                       }
                       ).ToArray(),
                     */

                  }).ToList();
                        

            this.Add(ne[ne.Count-1]);
        }


        public void DeleteEntry(string name)
        {
            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/0e00125b-1198-482f-9b65-9bbfa167f995/c-how-to-delete-element-in-xml?forum=csharpgeneral

            /*
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlfile);
            
            XElement root = XElement.Load(xmlfile);
            IEnumerable<XElement> ServiceEntryElement =
                from el in root.Elements()
                where (string)el.Element("SeviceEntry").Element("ID").Element("Name") == name
                select el;

            ServiceEntryElement.Remove();
            */

            //https://stackoverflow.com/questions/8382834/how-to-remove-an-xml-element-from-file        
            XDocument doc = XDocument.Load(xmlfile);
                var q = from node in doc.Descendants("SeviceEntry")                    
                    where (string)node.Element("ID").Element("Name") == name
                        select node;
                q.ToList().ForEach(x => x.Remove());
                doc.Save(xmlfile);
            

            // Delete entry from NewSeviceEntryCollection collection i.e. this
            for (int x =0;x<this.Count;x++)
            {
                if (this[x].NewSeviceEntryName == name)
                {
                    this.Remove(this[x]);
                }
            }

            //root.Save(xmlfile);
        }
      
    }
    
    
}
