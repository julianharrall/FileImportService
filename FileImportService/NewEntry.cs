using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;

namespace FileImportService
{
    public partial class NewEntry : Form
    {
        //public bool StopStartService { get; set; }
        public string lblNewEntryTitle { get; set; }
        public string xmlfile { get; set; }
        public XDocument xml { get; set; }
        public static List<string> wholeFile { get; set; }

        public DataGridView NewEntrydgv
        {
            get {return dgvFileImpsvc; }
        }

        public delegate void FireOnDelete(String str);
        public FireOnDelete deleteParam;
        MainForm MF { get; set; }
        public NewEntry(MainForm mf, string ac)
        {
            InitializeComponent();

            dgvFileImpsvc.AllowUserToAddRows = false;
           

            this.MinimumSize = new Size(800, 520);
            this.MaximumSize = new Size(800, 780); 
            
            this.Size = new Size(800, 520); 

            //xmlfile = Environment.CurrentDirectory + "\\settings2.xml";
            xmlfile = ConfigurationManager.AppSettings.Get("xmlFilePath");

            MF = mf;

            deleteParam += new FireOnDelete(MF.DeleteEntryAction);
        }

        /// <summary>
        /// Saves both a New entry and any changes to an existing entry.
        /// Calls SaveImportToXMLSettings() function
        /// and then uses the Mainform instance object passed in,
        /// to call the MainForms function AddNewEntryForm, 
        /// passing in title text of NewEntry form as string
        /// </summary>        
        private void BtnSaveNewEntry_Click(object sender, EventArgs e)
        {
            // Save New entry (MF.OpenFormName is empty)            
            if (string.IsNullOrEmpty(MF.OpenFormName.Trim()) )
            {

                ValidateEntries();
               
                SaveImportToXMLSettings();

                // Add new entry to form
                MF.AddNewEntryForm(lblNewEntryTitle, xml);
                
                if (MF.NewSvcEntyInstance.Count > 0)
                    MF.enable_btnStart(true);
            }
            // Save changes to Existing entry - (MF.OpenFormName is not empty)
            else
            {
                ValidateEntries();

                SaveXMLToFile();                    
            }

            MF.UpdateColours(MF.NewSvcEnty);
            MF.Controls[0].Visible = true;
            MF.EntryFormName = "";
            MF.OpenFormName = "";
            this.Close();
        }

        public void ValidateEntries()
        {
            lblNewEntryTitle = lblNewEntryTtitle.Text.Trim();
            if (String.IsNullOrEmpty(lblNewEntryTitle))
            {
                MessageBox.Show("New Entry must have a Title!!");
                MF.UpdateColours(MF.NewSvcEnty);
                this.Close();
                return;
            }

            if (MF.AlreadyExist(lblNewEntryTitle))
            {
                MessageBox.Show("This entry title already exists!!");
                return;
            }

            if (!CheckForBlankEntries(this.Controls))
                return;
        }
        
        //public void SaveXMLToFile(List<NewSeviceEntry> nser)
        /// <summary>
        /// Saves any changes to an existing FileImport service entry
        /// </summary>
        public void SaveXMLToFile()
        {
            List<NewSeviceEntry> nser = null;

            IEnumerable<NewSeviceEntry> results = MF.NewSvcEnty.Where(s => s.NewSeviceEntryName == MF.OpenFormName);
            nser = results.ToList();

            xml = XDocument.Load(xmlfile);

            XElement xEmp = XElement.Load(xmlfile);

            string NewcurrentEntryName = lblNewEntryTtitle.Text;
            string CurrentOpenFormName = MF.OpenFormName;
            if (NewcurrentEntryName != CurrentOpenFormName)
            {
                foreach (NewSeviceEntry c in MF.NewSvcEnty)
                {
                    if (c.NewSeviceEntryName == lblNewEntryTtitle.Text)
                    {
                        MessageBox.Show("This entry title already exists!!");
                        return;
                    }
                }
            }

            var xmlDetails = from emps in xEmp.Elements("SeviceEntry")
                             where emps.Element("ID").Element("Name").Value.Equals(MF.OpenFormName)
                             select emps;

            // Check if name of service entry has changed and needs updating in xml
            if (lblNewEntryTtitle.Text != nser[0].NewSeviceEntryName.ToString())
            {
                nser[0].NewSeviceEntryName = lblNewEntryTtitle.Text;

               
                xmlDetails.First().Element("ID").Element("Name").Value = lblNewEntryTtitle.Text;
                //
                xEmp.Save(xmlfile);

                string originalName = MF.OpenFormName;

                MF.OpenFormName = lblNewEntryTtitle.Text;

                // foreach (Control c in MF.Controls[4].Controls) -- Errored saying out of range
                foreach (Control c in MF.Controls[2].Controls)
                {
                   // MessageBox.Show(c.Text);
                    //if (c.Text == "File Import Service")
                    //    string x = MF.Controls[3].Text;
                    if (c.Text == originalName)
                        c.Text = lblNewEntryTtitle.Text;
                }
                
            }

            if (chkBxEnabled.Checked.ToString() != nser[0].Enabled.ToString().ToLower())
            {
                nser[0].Enabled = chkBxEnabled.Checked.ToString();
                                
                xmlDetails.First().Element("Enabled").Value = chkBxEnabled.Checked.ToString();
                //
                xEmp.Save(xmlfile);

            }

            if (txtDatabase.Text != nser[0].NewSeviceEntryDataBases[0].NewSeviceEntryDB.ToString())
            {
                nser[0].NewSeviceEntryDataBases[0].NewSeviceEntryDB = txtDatabase.Text;
                               
                xmlDetails.First().Element("Database").Element("Name").Value = txtDatabase.Text;
                //
                xEmp.Save(xmlfile);

            }
            if (txtDBUName.Text != nser[0].NewSeviceEntryDataBases[0].NewSeviceEntryDBUName.ToString())
            {
                nser[0].NewSeviceEntryDataBases[0].NewSeviceEntryDBUName = txtDBUName.Text;
                                
                xmlDetails.First().Element("Database").Element("Username").Value = txtDBUName.Text;
                //
                xEmp.Save(xmlfile);
            }
            if (txtDBPass.Text != nser[0].NewSeviceEntryDataBases[0].NewSeviceEntryDBPass.ToString())
            {
                nser[0].NewSeviceEntryDataBases[0].NewSeviceEntryDBPass = txtDBPass.Text;
                                
                xmlDetails.First().Element("Database").Element("Password").Value = txtDBPass.Text;
                //
                xEmp.Save(xmlfile);

            }
            if (txtFolder.Text != nser[0].NewSeviceEntryFolders[0].NewSeviceEntryMainFolder.ToString())
            {
                nser[0].NewSeviceEntryFolders[0].NewSeviceEntryMainFolder = txtFolder.Text;

                xmlDetails.First().Element("Folders").Element("Name").Value = txtFolder.Text;
                //
                xEmp.Save(xmlfile);
            }
            if (string.IsNullOrEmpty(txtFolderBU.Text) || txtFolderBU.Text != nser[0].NewSeviceEntryFolders[0].NewSeviceEntryBackup.ToString())
            {
                nser[0].NewSeviceEntryFolders[0].NewSeviceEntryBackup = txtFolderBU.Text;

                if (string.IsNullOrEmpty(txtFolderBU.Text))
                {
                    txtFolderBU.Text = txtFolder.Text + "\\Backup";
                }

                xmlDetails.First().Element("Folders").Element("Backup").Value = txtFolderBU.Text;
                //
                xEmp.Save(xmlfile);
            }
            if (string.IsNullOrEmpty(txtFolderBU.Text) || txtFolderFail.Text != nser[0].NewSeviceEntryFolders[0].NewSeviceEntryFolderFail.ToString())
            {
                nser[0].NewSeviceEntryFolders[0].NewSeviceEntryFolderFail = txtFolderFail.Text;


                if (string.IsNullOrEmpty(txtFolderBU.Text))
                {
                    txtFolderFail.Text = txtFolder.Text + "\\Failed";
                }

                xmlDetails.First().Element("Folders").Element("Failed").Value = txtFolderFail.Text;
                //
                xEmp.Save(xmlfile);
            }
            if (txtSproc.Text != nser[0].NewSeviceEntryCommands[0].NewSeviceEntrySProc.ToString())
            {
                nser[0].NewSeviceEntryCommands[0].NewSeviceEntrySProc = txtSproc.Text;

                xmlDetails.First().Element("Command").Element("StoredProcedure").Value = txtSproc.Text;
                //
                xEmp.Save(xmlfile);
            }
            if (txtSQL.Text != nser[0].NewSeviceEntryCommands[0].NewSeviceEntrySQL.ToString())
            {
                nser[0].NewSeviceEntryCommands[0].NewSeviceEntrySQL = txtSQL.Text;

                xmlDetails.First().Element("Command").Element("SQL").Value = txtSQL.Text;
                //
                xEmp.Save(xmlfile);
            }

            // ##################### Need to update xml entry for datagridview if it has changed in any way #####################
            if (dgvFileImpsvc.RowCount > 0)
            {
                foreach(DataGridViewRow dr in dgvFileImpsvc.Rows)
                {
                    foreach(DataGridViewCell dc in dr.Cells)
                    {
                        //nser[0].NewSeviceEntryFileAss[]
                        
                    }
                    
                }

            }

            //List<NewSeviceEntry.NewSeviceEntryFileAssoc> nsfm = nser.NewSeviceEntryFileAss;
            //dgvFileImpsvc.ColumnCount = 3;

            /*              
                if (dgvFileImpsvc.RowCount > 0)
                {
                    for (int dgv = 0; dgv < dgvFileImpsvc.RowCount; dgv++)
                    {
                        //dgvFileImpsvc.Rows.Add();                        
                        ns[0].NewSeviceEntryFileAss[dgv].ColNum = (int)dgvFileImpsvc.Rows[dgv].Cells[0].Value;
                        ns[0].NewSeviceEntryFileAss[dgv].ColName = dgvFileImpsvc.Rows[dgv].Cells[1].Value.ToString();
                        ns[0].NewSeviceEntryFileAss[dgv].ColDataType = dgvFileImpsvc.Rows[dgv].Cells[2].Value.ToString();

                    }
                }
             */
            //dgvFileImpsvc.ColumnCount = 3;
            //nser[0].NewSeviceEntryFileAss.Capacity = 3;
            
            if (dgvFileImpsvc.RowCount > 0) // errors as says NewSeviceEntryFileAss is empty -- Think columns need to be added??################################
            {
                for(int dgv = 0; dgv < dgvFileImpsvc.RowCount; dgv++)
                {
                    nser[0].NewSeviceEntryFileAss[dgv].ColNum = (int)dgvFileImpsvc.Rows[dgv].Cells[0].Value;
                    nser[0].NewSeviceEntryFileAss[dgv].ColName = dgvFileImpsvc.Rows[dgv].Cells[1].Value.ToString();
                    nser[0].NewSeviceEntryFileAss[dgv].ColDataType = dgvFileImpsvc.Rows[dgv].Cells[2].Value.ToString();

                }
            }
            // check a file was imported (before clearing all the file details)
            else if(nser[0].NewSeviceEntryFileAss != null)
            {
                
                nser[0].NewSeviceEntryFileAss.Clear();
                
                //xmlDetails.First().Element("FileAssociation").Element("RowDetails").Element("ColNum").Value = "";
                //xmlDetails.First().Element("FileAssociation").Element("RowDetails").Element("ColName").Value = "";
                //xmlDetails.First().Element("FileAssociation").Element("RowDetails").Element("ColDataType").Value = "";
                nser[0].AssociateMappings = false;
                xmlDetails.First().Element("FileAssociation").RemoveAll();
                xmlDetails.First().Element("RowDetails").RemoveAll();
                xmlDetails.First().Element("Association").Value = "False";
                AssociateFileButton();
                xEmp.Save(xmlfile);
            }

        }
              
        private bool CheckForBlankEntries(Control.ControlCollection cntrls)
        {
            /*
            string x = cntrls[0].Text; // enabled
             x = cntrls[1].Text; // cancel
             x = cntrls[2].Text; // save
             x = cntrls[3].Text; // name
             x = cntrls[4].Text; // fn
             x = cntrls[5].Text; // filetypes
             x = cntrls[6].Text; // folders
            x = cntrls[6].Controls[0].Text; // folders
            x = cntrls[6].Controls[1].Text; // folders
            x = cntrls[6].Controls[2].Text; // folders
            x = cntrls[6].Controls[3].Text; // folders
            x = cntrls[7].Text; // command
             x = cntrls[8].Text; // groupbox2
              x = cntrls[9].Text; // groupbox2
              */
            


            if (string.IsNullOrEmpty(cntrls[8].Controls[3].Text))          
            {
                MessageBox.Show("Main Folder IS EMPTY!!!!");
                return false;
            }
           

            if (string.IsNullOrEmpty(cntrls[9].Controls[0].Text) && string.IsNullOrEmpty(cntrls[9].Controls[1].Text))
            {
                MessageBox.Show("Stored procedure and Sql IS EMPTY!!!!");
                return false;
            }
            else if (!string.IsNullOrEmpty(cntrls[9].Controls[0].Text) && !string.IsNullOrEmpty(cntrls[9].Controls[1].Text))
            {
                MessageBox.Show("Stored procedure and Sql are both populated \n You can only have one entry!!!!");
                return false;
            }

            if (string.IsNullOrEmpty(cntrls[11].Controls[0].Text))
            {
                MessageBox.Show("Password IS EMPTY!!!!");
                return false;
            }
            if (string.IsNullOrEmpty(cntrls[11].Controls[1].Text))
            {
                MessageBox.Show("Username IS EMPTY!!!!");
                return false;
            }
            if (string.IsNullOrEmpty(cntrls[11].Controls[2].Text))
            {
                MessageBox.Show("Database IS EMPTY!!!!");
                return false;
            }
                     

            return true;
        }

        /// <summary>
        /// Creates new xml file, if doesnt exist and adds the new 
        /// xml entry and saves the file
        /// Then uses the Mainform instance object passed in,
        /// to call the MainForms function AddNewEntryInCollObject,
        /// passing in the xml document, with new entry, as a string
        /// </summary>
        //public void SaveImportToXMLSettings(List<NewSeviceEntry> ns = null, bool copyEntry = false)
        //{
        //    // Get imported file data/row details (if it has been imported)
        //    List<string> ls = NewEntry.wholeFile;

        //    // Check a file was imported
        //    // if it was, remove empty entry in list (caused by columns row)
        //    if (ls != null)
        //    {
        //        ls.Remove(""); 
        //    }
        //    else
        //    {
        //        // File not imported 
        //        // so intialise imported file to empty List<string>
        //        ls = new List<string>();
        //    }           

        //    String AssocFileYN = "";

        //    // check and set file association flag
        //    if (btnAssocFile.Text == "Associate File")
        //    {
        //        AssocFileYN = "False";
        //    }
        //    else
        //    {
        //        AssocFileYN = "True";
        //    }

        //    //https://stackoverflow.com/questions/22050070/auto-increment-id-value-in-xml-file

        //    // declare and initialise count variable
        //    // used to enumerate the imported file row entries 
        //    int count = 0;            

        //    if (!File.Exists(xmlfile)) // File doesnt exists 
        //    {
        //        if (txtFolderBU.Text == "")
        //            txtFolderBU.Text = txtFolder.Text + "\\Backup";
        //        if (txtFolderFail.Text == "")
        //            txtFolderFail.Text = txtFolder.Text + "\\Failed";

        //        if (chkBxEnabled.Checked)                
        //            chkBxEnabled.Text = "True";
        //        else
        //            chkBxEnabled.Text = "False";

        //        if (btnAssocFile.Text == "Associate File")
        //            AssocFileYN = "False";

        //            xml = new XDocument(
        //        new XDeclaration("1.0", "utf8", "yes"),
        //        new XComment("Settings file for FileImportService"),
        //        new XElement("NewSeviceEntryNames", "",
        //        new XElement("SeviceEntry",
        //          new XElement("ID",
        //             new XElement("Name", lblNewEntryTtitle.Text)),
        //         new XElement("Enabled", chkBxEnabled.Text),
        //         new XElement("Database",
        //             new XElement("Name", txtDatabase.Text),
        //             new XElement("Username", txtDBUName.Text),
        //             new XElement("Password", txtDBPass.Text)),
        //          new XElement("Folders",
        //             new XElement("Name", txtFolder.Text),
        //             new XElement("Backup", txtFolderBU.Text),
        //             new XElement("Failed", txtFolderFail.Text)),
        //          new XElement("Command",
        //             new XElement("StoredProcedure", txtSproc.Text),
        //             new XElement("SQL", txtSQL.Text)),


        //           new XElement("Association", AssocFileYN),

        //           new XElement("FileAssociation",
        //                    from colDetails in dgvFileImpsvc.Rows.Cast<DataGridViewRow>()
        //                    select new XElement("RowDetails",
        //                                   new XElement("ColNum", colDetails.Cells[0].Value),
        //                                   new XElement("ColName", colDetails.Cells[1].Value),
        //                                   new XElement("ColDataType", colDetails.Cells[2].Value)
        //                               )),                 

        //          //https://stackoverflow.com/questions/30040519/how-does-one-increment-update-integer-value-of-an-xml-attribute-in-c-sharp-xatt
        //          new XElement("RowDetails",
        //                    from rowDetail in ls.Select(i => new XElement("Row", i))
        //                    select new XElement("Row", new XAttribute("Num", 1), rowDetail.Value
        //                               ))


        //             )));


        //        IEnumerable<XAttribute> listOfAttributes =
        //           from att in xml.Root.Element("SeviceEntry").Element("RowDetails").Elements("Row").Attributes()
        //           select att;
        //        foreach (XAttribute a in listOfAttributes)
        //            a.SetValue(++count);


        //    }
        //    else if(ns == null) // New entry being added to existing file
        //    {

        //        String impFileName = lblNewEntryTtitle.Text;

        //        if (!string.IsNullOrEmpty(MF.CopyEntryFormName))
        //            impFileName = MF.CopyEntryFormName;

        //        xml = XDocument.Load(xmlfile);

        //        if (txtFolderBU.Text == "")
        //            txtFolderBU.Text = txtFolder.Text + "\\Backup";
        //        if (txtFolderFail.Text == "")
        //            txtFolderFail.Text = txtFolder.Text + "\\Failed";

        //        if (!string.IsNullOrEmpty(MF.CopyEntryFormName))
        //            lblNewEntryTtitle.Text = MF.CopyEntryFormName;

        //        if (chkBxEnabled.Checked)
        //            chkBxEnabled.Text = "True";
        //        else
        //            chkBxEnabled.Text = "False";              

        //        XElement x = new XElement("SeviceEntry", 
        //         new XElement("ID",
        //             new XElement("Name", impFileName)),
        //         new XElement("Enabled", chkBxEnabled.Text),
        //         new XElement("Database",
        //             new XElement("Name", txtDatabase.Text),
        //             new XElement("Username", txtDBUName.Text),
        //             new XElement("Password", txtDBPass.Text)),
        //          new XElement("Folders",
        //             new XElement("Name", txtFolder.Text),
        //             new XElement("Backup", txtFolderBU.Text),
        //             new XElement("Failed", txtFolderFail.Text)),
        //          new XElement("Command",
        //             new XElement("StoredProcedure", txtSproc.Text),
        //             new XElement("SQL", txtSQL.Text)),

        //        new XElement("Association", AssocFileYN),

        //        new XElement("FileAssociation",
        //                    from colDetails in dgvFileImpsvc.Rows.Cast<DataGridViewRow>()
        //                    select new XElement("RowDetails",
        //                                   new XElement("ColNum", colDetails.Cells[0].Value),
        //                                   new XElement("ColName", colDetails.Cells[1].Value),
        //                                   new XElement("ColDataType", colDetails.Cells[2].Value)
        //                               )),            


        //        new XElement("RowDetails",
        //                   from rowDetail in ls.Select(i => new XElement("Row", i))
        //                   select new XElement("Row", new XAttribute("Num", 1), rowDetail.Value
        //                               ))

        //             );


        //        if (x.HasElements.Equals("RowDetails"))
        //        {

        //            // increment row attributes row nummbers
        //            IEnumerable<XAttribute> listOfAttributes =
        //                 from att in xml.Root.Element("SeviceEntry").Element("RowDetails").Elements("Row").Attributes()
        //                 select att;

        //            foreach (XAttribute a in listOfAttributes)
        //                a.SetValue(++count);
        //        }

        //        //##################################### ADD INCREMENT ################################## 07/05/2020
        //        xml.Root.Add(x);

        //    }
        //    else // File exists and has entries
        //    {
        //        //List<NewSeviceEntry> ns = new List<NewSeviceEntry>();

        //        xml = XDocument.Load(xmlfile);

        //        //if (copyEntry)
        //        //{
        //        //    ns[0].Enabled = "False";
        //        //}

        //        //if(MF.CopyEntryFormName.Contains("(Copy)"))
        //        //    ns[0].Enabled = "False";
        //        string fileassoc = "";
        //        if (ns[0].AssociateMappings == true)
        //        {
        //            fileassoc = "True";
        //        }
        //        else
        //        {
        //            fileassoc = "False";
        //        }


        //        if (dgvFileImpsvc.RowCount > 0)
        //        {
        //            for (int dgv = 0; dgv < dgvFileImpsvc.RowCount; dgv++)
        //            {
        //                //dgvFileImpsvc.Rows.Add();                        
        //                ns[0].NewSeviceEntryFileAss[dgv].ColNum = (int)dgvFileImpsvc.Rows[dgv].Cells[0].Value;
        //                ns[0].NewSeviceEntryFileAss[dgv].ColName = dgvFileImpsvc.Rows[dgv].Cells[1].Value.ToString();
        //                ns[0].NewSeviceEntryFileAss[dgv].ColDataType = dgvFileImpsvc.Rows[dgv].Cells[2].Value.ToString();

        //            }
        //        }
        //        else
        //        {
        //            //ns[0].NewSeviceEntryFileAss.Count = 0;
        //        }


        //        XElement x = new XElement("SeviceEntry",
        //         new XElement("ID",
        //             new XElement("Name", MF.CopyEntryFormName)),
        //         new XElement("Enabled","False"),
        //         new XElement("Database",
        //             new XElement("Name", ns[0].NewSeviceEntryDataBases[0].NewSeviceEntryDB),
        //             new XElement("Username", ns[0].NewSeviceEntryDataBases[0].NewSeviceEntryDBUName),
        //             new XElement("Password", ns[0].NewSeviceEntryDataBases[0].NewSeviceEntryDBPass)),
        //          new XElement("Folders",
        //             new XElement("Name", ns[0].NewSeviceEntryFolders[0].NewSeviceEntryMainFolder),
        //             new XElement("Backup", ns[0].NewSeviceEntryFolders[0].NewSeviceEntryBackup),
        //             new XElement("Failed", ns[0].NewSeviceEntryFolders[0].NewSeviceEntryFolderFail)),
        //          new XElement("Command",
        //             new XElement("StoredProcedure", ns[0].NewSeviceEntryCommands[0].NewSeviceEntrySProc),
        //             new XElement("SQL", ns[0].NewSeviceEntryCommands[0].NewSeviceEntrySQL)),


        //          new XElement("Association", fileassoc),

        //          new XElement("FileAssociation",
        //                    from colDetails in dgvFileImpsvc.Rows.Cast<DataGridViewRow>()
        //                    select new XElement("RowDetails",
        //                                   new XElement("ColNum", colDetails.Cells[0].Value),
        //                                   new XElement("ColName", colDetails.Cells[1].Value),
        //                                   new XElement("ColDataType", colDetails.Cells[2].Value)
        //                               )),      


        //          new XElement("RowDetails",
        //                    from rowDetail in ls.Select(i => new XElement("Row", i))
        //                    select new XElement("Row", new XAttribute("Num", 1), rowDetail.Value
        //                               ))

        //          );

        //        xml.Root.Add(x);

        //    }

        //    // try putting servervice entry name in here #########################################
        //    IEnumerable<XAttribute> listOfAttributes2 =
        //             from att in xml.Root.Element("SeviceEntry").Element("RowDetails").Elements("Row").Attributes()
        //             //where xml.Element("SeviceEntry").Element("ID").Elements("Name").Equals(lblNewEntryTtitle.Text)
        //             select att;
        //    foreach (XAttribute a in listOfAttributes2)
        //        a.SetValue(++count);

        //    xml.Save(xmlfile);
        //}

        /// <summary>
        /// Adds both New and Copied Xml entries
        /// and Saves the Xml file      
        /// </summary>
        public void SaveImportToXMLSettings(List<NewSeviceEntry> ns = null, bool copyEntry = false)
        {

            // Get imported file data/row details (if it has been imported)
            List<string> ls = NewEntry.wholeFile;

            // Check a file was imported
            // if it was, remove empty entry in list (caused by columns row)
            if (ls != null)
            {
                ls.Remove("");
            }
            else
            {
                // File not imported 
                // so intialise imported file to empty List<string>
                ls = new List<string>();
            }

            String AssocFileYN = "";

            // check and set file association flag
            if (btnAssocFile.Text == "Associate File")
            {
                AssocFileYN = "False";
            }
            else
            {
                AssocFileYN = "True";
            }

            //https://stackoverflow.com/questions/22050070/auto-increment-id-value-in-xml-file

            // declare and initialise count variable
            // used to enumerate the imported file row entries 
            int count = 0;

            if (ns == null) // New entry being added 
            {
                // FileImport Service name
                String impFileName = lblNewEntryTtitle.Text;               

                // Load xml file into xml object
                xml = XDocument.Load(xmlfile); 

                // Check if folder failed and backup entries are blank
                // If they are blank, default the values
                if (txtFolderBU.Text == "")
                    txtFolderBU.Text = txtFolder.Text + "\\Backup";
                if (txtFolderFail.Text == "")
                    txtFolderFail.Text = txtFolder.Text + "\\Failed";               

                // Set chkBxEnabled Text value
                if (chkBxEnabled.Checked)
                    chkBxEnabled.Text = "True";
                else
                    chkBxEnabled.Text = "False";

                // Initialise and add xml elements to XElement object
                XElement x = new XElement("SeviceEntry",
                 new XElement("ID",
                     new XElement("Name", impFileName)),
                 new XElement("Enabled", chkBxEnabled.Text),
                 new XElement("Database",
                     new XElement("Name", txtDatabase.Text),
                     new XElement("Username", txtDBUName.Text),
                     new XElement("Password", txtDBPass.Text)),
                  new XElement("Folders",
                     new XElement("Name", txtFolder.Text),
                     new XElement("Backup", txtFolderBU.Text),
                     new XElement("Failed", txtFolderFail.Text)),
                  new XElement("Command",
                     new XElement("StoredProcedure", txtSproc.Text),
                     new XElement("SQL", txtSQL.Text)),

                new XElement("Association", AssocFileYN),

                new XElement("FileAssociation",
                            from colDetails in dgvFileImpsvc.Rows.Cast<DataGridViewRow>()
                            select new XElement("RowDetails",
                                           new XElement("ColNum", colDetails.Cells[0].Value),
                                           new XElement("ColName", colDetails.Cells[1].Value),
                                           new XElement("ColDataType", colDetails.Cells[2].Value)
                                       )),
                
                new XElement("RowDetails",
                           from rowDetail in ls.Select(i => new XElement("Row", i))
                           select new XElement("Row", new XAttribute("Num", 1), rowDetail.Value
                                       ))

                     );
                
                if (x.HasElements.Equals("RowDetails"))
                {
                    // increment row attributes row nummbers
                    IEnumerable<XAttribute> listOfAttributes =
                         from att in xml.Root.Element("SeviceEntry").Element("RowDetails").Elements("Row").Attributes()
                         select att;

                    foreach (XAttribute a in listOfAttributes)
                        a.SetValue(++count);
                }

                //##################################### ADD INCREMENT ################################## 07/05/2020
                xml.Root.Add(x);

            }
            else // Copying an existing entry
            {        

                xml = XDocument.Load(xmlfile);

                string fileassoc = "";
                if (ns[0].AssociateMappings == true)
                {
                    fileassoc = "True";
                }
                else
                {
                    fileassoc = "False";
                }
                
                if (dgvFileImpsvc.RowCount > 0)
                {
                    for (int dgv = 0; dgv < dgvFileImpsvc.RowCount; dgv++)
                    {
                        //dgvFileImpsvc.Rows.Add();                        
                        ns[0].NewSeviceEntryFileAss[dgv].ColNum = (int)dgvFileImpsvc.Rows[dgv].Cells[0].Value;
                        ns[0].NewSeviceEntryFileAss[dgv].ColName = dgvFileImpsvc.Rows[dgv].Cells[1].Value.ToString();
                        ns[0].NewSeviceEntryFileAss[dgv].ColDataType = dgvFileImpsvc.Rows[dgv].Cells[2].Value.ToString();
                    }
                }
                else
                {
                    //ns[0].NewSeviceEntryFileAss.Count = 0;
                }
                
                XElement x = new XElement("SeviceEntry",
                 new XElement("ID",
                     new XElement("Name", MF.CopyEntryFormName)),
                 new XElement("Enabled", "False"),
                 new XElement("Database",
                     new XElement("Name", ns[0].NewSeviceEntryDataBases[0].NewSeviceEntryDB),
                     new XElement("Username", ns[0].NewSeviceEntryDataBases[0].NewSeviceEntryDBUName),
                     new XElement("Password", ns[0].NewSeviceEntryDataBases[0].NewSeviceEntryDBPass)),
                  new XElement("Folders",
                     new XElement("Name", ns[0].NewSeviceEntryFolders[0].NewSeviceEntryMainFolder),
                     new XElement("Backup", ns[0].NewSeviceEntryFolders[0].NewSeviceEntryBackup),
                     new XElement("Failed", ns[0].NewSeviceEntryFolders[0].NewSeviceEntryFolderFail)),
                  new XElement("Command",
                     new XElement("StoredProcedure", ns[0].NewSeviceEntryCommands[0].NewSeviceEntrySProc),
                     new XElement("SQL", ns[0].NewSeviceEntryCommands[0].NewSeviceEntrySQL)),

                  new XElement("Association", fileassoc),

                  new XElement("FileAssociation",
                            from colDetails in dgvFileImpsvc.Rows.Cast<DataGridViewRow>()
                            select new XElement("RowDetails",
                                           new XElement("ColNum", colDetails.Cells[0].Value),
                                           new XElement("ColName", colDetails.Cells[1].Value),
                                           new XElement("ColDataType", colDetails.Cells[2].Value)
                                       )),

                  new XElement("RowDetails",
                            from rowDetail in ls.Select(i => new XElement("Row", i))
                            select new XElement("Row", new XAttribute("Num", 1), rowDetail.Value
                                       ))

                  );

                xml.Root.Add(x);

            }

            // try putting servervice entry name in here #########################################
            IEnumerable<XAttribute> listOfAttributes2 =
                     from att in xml.Root.Element("SeviceEntry").Element("RowDetails").Elements("Row").Attributes()
                         //where xml.Element("SeviceEntry").Element("ID").Elements("Name").Equals(lblNewEntryTtitle.Text)
                     select att;
            foreach (XAttribute a in listOfAttributes2)
                a.SetValue(++count);

            xml.Save(xmlfile);
        }

        public void DeleteServiceEntry(string serviceEntryName)
        {           
            serviceEntryName = "nebosh";
            XDocument xdoc = XDocument.Load(xmlfile);                                 
            
            xdoc.Descendants("SeviceEntry")
                .Where(x => (string)x.Element("ID").Element("Name") == MF.EntryFormName)
                .Remove();

            MF.DeleteServiceEntry(xdoc);

            xdoc.Save(xmlfile);            

            this.Close();

            OnThresholdReached(MF.EntryFormName);
        }
               
        private void BtnCancelNewEntry_Click(object sender, EventArgs e)
        {
            MF.UpdateColours(MF.NewSvcEnty);
            //MF.UpdateColor("Cancel");
            //MF.UpdateColours(MF.NewSvcEnty, MF.EntryFormName);
            MF.OpenFormName = "";
            MF.EntryFormName = "";            
            this.Close();
        }      

        private void BtnDeleteEntry_Click(object sender, EventArgs e)
        { 
            Button btn = (Button)sender;
            string entryName = this.lblNewEntryTitle;
            //OnThresholdReached(btn);
            DeleteServiceEntry(entryName);
            MF.OpenFormName = "";
        }

        protected virtual void OnThresholdReached(object e)
        {           
           string entryName = this.lblNewEntryTitle;
            deleteParam(entryName);
        }

        private void NewEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            Button txt = ActiveControl as Button;
            
            MF.UpdateColor(txt.Text.ToString());
        }
        
        public void AssociateFileButton()
        {
            btnAssocFile.Text = "Associate File";
            btnAssocFile.BackColor = default(Color);
            btnAssocFile.ForeColor = Color.Black;
        }

        private void BtnAssocFile_Click(object sender, EventArgs e)
        {
            Button butname = (Button)sender;
            string buttonName = butname.Text;

            // Check if file already associated
            if(buttonName == "Disassociate File")
            {
                this.dgvFileImpsvc.Rows.Clear();
                AssociateFileButton();
                //wholeFile.Clear();
                return;
            }

            // Open FileDialog form
            string FileNameSelected = "";
            OpenFileDialog ofd = new OpenFileDialog();           
            ofd.Filter = "csv files (*.csv)|*.csv";
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            //https://www.c-sharpcorner.com/UploadFile/mahesh/openfiledialog-in-C-Sharp/

            // Set file name
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                FileNameSelected = ofd.FileName;
            }

            // If filename selected - continue
            if (!string.IsNullOrEmpty(FileNameSelected))
            {
                int counter = 0;
                string line;
                // reader to read file
                StreamReader readFileCols = new StreamReader(FileNameSelected);
                // read first line i.e. columns
                string readingColLine = readFileCols.ReadLine();
                // count number of columns
                int numOfCols = readingColLine.Count(c => c == ',') + 1;
               
                // read whole file
                string inLineWholeFile = readFileCols.ReadToEnd();

                // store whole file into a list of strings
                wholeFile = inLineWholeFile.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();

                // Store columns in a list of strings
                List<string> cols = new List<string>();
                while ((line = readFileCols.ReadLine()) != null)
                //while ((line = readingColLine.ReadLine()) != null)
                {
                    cols = readingColLine.Split(',').ToList<string>();
                    counter++;
                }

                // Close StreamReader
                readFileCols.Close();               

                cols = readingColLine.Split(',').ToList<string>();
                int col = 0, row = 0, colnum = 1;
                              
                foreach (string colname in cols)
                {
                    this.dgvFileImpsvc.Rows.Add();
                    this.dgvFileImpsvc.Rows[row].Cells[col].Value = colnum;
                    this.dgvFileImpsvc.Rows[row].Cells[col + 1].Value = colname;
                    this.dgvFileImpsvc.Rows[row].Cells[2].Value = "Varchar";
                    row++;
                    colnum++;
                }
                   
                int cntdg = dgvFileImpsvc.RowCount;
                                  

                if (row > 0)
                {
                    btnAssocFile.Text = "Disassociate File";
                    btnAssocFile.BackColor = Color.Red;
                    btnAssocFile.ForeColor = Color.White;
                    //MF.NewSvcEnty[8].
                }
            }
            
        }
        
        private void Button1_Click(object sender, EventArgs e)
        {
            string selected_Folder = "";

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if(result == DialogResult.OK & !string.IsNullOrEmpty(fbd.SelectedPath))
                {
                    selected_Folder = fbd.SelectedPath;
                    //txtFolder.Text = selected_Folder;

                    // set forms control collection for the text box
                    Control.ControlCollection cntrls = this.Controls;
                    cntrls[8].Controls[3].Text = selected_Folder;
                }
            }
           
        }
    }
}
