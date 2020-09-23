using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FileImportService
{
    public partial class MainForm : Form
    {

       public NewSeviceEntryCollection NewSvcEntyInstance = null;
       public static bool StopStartServiceFlag { get; set; }
       public Button butStart
        {
            get { return btnStart; }           
        }
       public void enable_btnStart(bool truefalse)
        {
            btnStart.Enabled = truefalse;
        }
       public NewSeviceEntryCollection NewSvcEnty
        {
            get
            {
                if (NewSvcEntyInstance == null)
                    return NewSvcEntyInstance = new NewSeviceEntryCollection(this);
                else
                    return NewSvcEntyInstance;
            }
        }
       public NewServiceEntryNames NewSvcEntyNames { get; set; }
       public NewEntry newform { get; set; }
       public string EntryFormName { get; set; }
       public static string xmlfileN { get; set; }
       public string OpenFormName { get; set; }
       public string CopyEntryFormName { get; set; }
       Label lab { get; set; }

        public MainForm()
        {
            InitializeComponent();          

            // set min and max size so form cannot be resized
            this.MinimumSize = new Size(900, 620);
            this.MaximumSize = new Size(900, 620);

            // initially disable buttons
            btnCopy.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;            
          
            // Get file name
            xmlfileN = ConfigurationManager.AppSettings.Get("xmlFilePath");

            // If file exists - it may or may not have entries
            if (File.Exists(xmlfileN))
            {  
                // Call top level class to load file
                // into collection class
                NewSvcEntyNames = new NewServiceEntryNames(this);

                // Check there are entries - before attempting to load
                // (belt and braces)
                if (NewSvcEntyNames.NSC != null)
                {
                    // Check there are entries to load
                    if (NewSvcEntyNames.NSC.Count > 0)
                    {
                        // ensure stop/start button enabled
                        btnStart.Enabled = true;

                        // Loop thro collection class to add Service entries to Mainform
                        foreach (NewSeviceEntry nse in NewSvcEntyNames.NSC)
                        {
                            AddEntriesToMainForm(nse);
                        }

                        // Check the Main Service status - to set as appropriate in the 
                        // main form gui, Which effectiveley checks the value in the xml file
                        // (This value will always be in xml file by default)
                        if( NewSvcEntyNames.CheckServiceStatus().ToUpper()=="STOPPED")
                        //if (NewSvcEntyNames.CheckServiceStatus() == "false")
                        {
                            StopStart(Color.Red, "Stopped", false);                            
                        }
                        else
                        {
                            StopStart(Color.Green, "Running", true);                            
                        }
                      
                    }
                    else // if the file exists but there are no entries
                    {    // set the service to stopped by default
                        StopStart(Color.Red, "Stopped", false);
                        btnStart.Enabled = false;
                    }
                }
                else
                {
                    // File exists but no entries - so stop and disable
                    StopStart(Color.Red, "Stopped", false);
                    btnStart.Enabled = false;
                    
                }  
                
            }
            else // File does not exist - so create and set 
            {    // default service status of stopped

                // Just Load empty Windows form as no  entries              
                // Create emty file if it doesnt exist
                if (CreateEmptyFile())
                {
                    StopStart(Color.Red, "Stopped", false);
                    btnStart.Enabled = false;
                }
            }
                      
            UpdateColours(NewSvcEnty);
        }

        private bool CreateEmptyFile()
        {
            // Belt and braces - check have filepath
            // then create empty file
            if (!String.IsNullOrEmpty(xmlfileN))
            {
                // Set StopStartServiceFlag to string from bool
                // so it can be reflected in xml file
                string stopstartsvcflag = "";
                if(MainForm.StopStartServiceFlag == false)
                {
                    stopstartsvcflag = "Stopped";
                }
                else
                {
                    stopstartsvcflag = "Running";                    
                }

                // Create empty xml file
                XDocument doc =
                  new XDocument(
                    new XElement("NewSeviceEntryNames",
                        (new XElement("ServiceStatus", stopstartsvcflag))
                    )
                  );

                // save empty xml file
                doc.Save(xmlfileN);

                return true;
                
            }

            return false;
           
        }

        public void UpdateDefaultColurs()
        {
            foreach (NewSeviceEntry nse in NewSvcEnty)
            {
                //if (nse.Enabled == "True")
                    //newEntryButton.BackColor = Color.LightBlue;
            }


            foreach (Control c in flpEntryForm.Controls)
            {
                c.BackColor = Color.Gray;
            }
        }

        public void DeleteEntryAction(string DeletedEntry)
        {
            MessageBox.Show(EntryFormName.ToString());

            DeleteOpenForm(EntryFormName);
        }

        /// <summary>
        /// Fires when click Add new entry
        /// creates new NewEntry form and then displays
        /// which effectiveley gives control to the 
        /// new NewEntry form and passes Mainform instance object
        /// </summary>       
        private void btn_AddNewEntry_Click(object sender, EventArgs e)
        {
            newform = new NewEntry(this, "");
            UpdateColor();
            OpenFormName = "";
            EntryFormName = "";
            newform.ShowDialog();
            //https://www.youtube.com/watch?v=NxfYFAw0JDs
            //UpdateColours(NewSvcEnty);
        }     

        public void OpenForm(object sender)
        {
            //EntryFormName = ((Label)sender).Text;

            // Initialise NewEntry object
            newform = new NewEntry(this, sender.ToString());

            foreach(NewSeviceEntry nsec in NewSvcEnty)
            {
                if (nsec.NewSeviceEntryName == sender.ToString())
                {
                    // Enabled
                    if (nsec.Enabled.ToLower() == "true")
                       ((CheckBox)newform.Controls[2]).Checked = true;
                    else
                        ((CheckBox)newform.Controls[2]).Checked = false;

                    // Folders
                    //######
                    if (string.IsNullOrEmpty(nsec.NewSeviceEntryFolders[0].NewSeviceEntryFolderFail))
                        newform.Controls[8].Controls[1].Text = nsec.NewSeviceEntryFolders[0].NewSeviceEntryMainFolder + "\\Failed";
                    else
                        newform.Controls[8].Controls[1].Text = nsec.NewSeviceEntryFolders[0].NewSeviceEntryFolderFail;
                    //########
                    if (string.IsNullOrEmpty(nsec.NewSeviceEntryFolders[0].NewSeviceEntryBackup))
                        newform.Controls[8].Controls[2].Text = nsec.NewSeviceEntryFolders[0].NewSeviceEntryMainFolder + "\\Backup";
                    else
                        newform.Controls[8].Controls[2].Text = nsec.NewSeviceEntryFolders[0].NewSeviceEntryBackup;

                    newform.Controls[8].Controls[3].Text = nsec.NewSeviceEntryFolders[0].NewSeviceEntryMainFolder;

                    // Command
                    newform.Controls[9].Controls[0].Text = nsec.NewSeviceEntryCommands[0].NewSeviceEntrySProc;
                    newform.Controls[9].Controls[1].Text = nsec.NewSeviceEntryCommands[0].NewSeviceEntrySQL;

                    newform.Controls[11].Controls[0].Text = nsec.NewSeviceEntryDataBases[0].NewSeviceEntryDBPass;
                    newform.Controls[11].Controls[0].TabStop = false;
                    newform.Controls[11].Controls[1].Text = nsec.NewSeviceEntryDataBases[0].NewSeviceEntryDBUName;
                    newform.Controls[11].Controls[1].TabStop = false;
                    newform.Controls[11].Controls[2].Text = nsec.NewSeviceEntryDataBases[0].NewSeviceEntryDB;
                    newform.Controls[11].Controls[2].TabStop = false;
                    
                    // Name
                    if(!string.IsNullOrEmpty(CopyEntryFormName))
                        newform.Controls[6].Text = CopyEntryFormName;
                    else
                        newform.Controls[6].Text = sender.ToString();

                    // GroupBox2
                    newform.Controls[10].Controls[0].Text = "";

                    // FileTypes
                    newform.Controls[7].Controls[0].Text = "csv";

                    // Associate File Button text and backcolor                   
                    newform.Controls[0].Text = nsec.AssociateMappings ? "Disassociate File" : "Assosiate File";
                    newform.Controls[0].BackColor = nsec.AssociateMappings ? Color.Red : default(Color);
                    newform.Controls[0].ForeColor = nsec.AssociateMappings ? Color.White : Color.Black;

                    Control.ControlCollection c  = newform.Controls[1].Controls;

                    //List<NewSeviceEntry.NewSeviceEntryFileMappings> nsfm = nsec.NewSeviceEntryMap;

                    List<NewSeviceEntry.NewSeviceEntryFileAssoc> nsfm = nsec.NewSeviceEntryFileAss;
                    if (nsfm != null)
                    {
                        if (nsfm.Count > 0 )
                        {
                            newform.NewEntrydgv.Rows.Add(nsfm.Count);
                            for (int j = 0; j < nsfm.Count; j++)
                            {

                                newform.NewEntrydgv.Rows[j].Cells[0].Value = nsfm[j].ColNum;
                                newform.NewEntrydgv.Rows[j].Cells[1].Value = nsfm[j].ColName;
                                newform.NewEntrydgv.Rows[j].Cells[2].Value = nsfm[j].ColDataType;

                            }
                        }
                    }
                }                //dgvFileImpsvc

            }            
           
            newform.ShowDialog();            
        }
       
        public void DeleteOpenForm(object sender)
        {
            EntryFormName = ((Button)sender).Name;

            newform = new NewEntry(this, EntryFormName);

            newform.ShowDialog();
        }       

        public void Populate()
        {
            //NewSvcEnty = new NewSeviceEntryCollection();
            NewSvcEntyNames = new NewServiceEntryNames(this);            
        }
               
        internal void AddNewEntryForm(string str, XDocument xdoc)
        {
            Label newEntryButton = new Label();
            newEntryButton.DoubleClick += NewEntryButton_DoubleClick;
            newEntryButton.Click += NewEntryButton_Click;
            newEntryButton.BackColor = Color.Gray;
            newEntryButton.Height = 50;
            newEntryButton.Width = 710;
            newEntryButton.TextAlign = ContentAlignment.MiddleCenter;
            FontFamily family = new FontFamily("Arial");
            newEntryButton.Font = new Font(family, 10.0f, FontStyle.Bold);
            newEntryButton.BorderStyle = BorderStyle.FixedSingle;

            newEntryButton.Name = str;
            newEntryButton.Text = str;
            flpEntryForm.FlowDirection = FlowDirection.TopDown;
            flpEntryForm.WrapContents = false;
            flpEntryForm.AutoScroll = true;
            flpEntryForm.Controls.Add(newEntryButton);

            NewSvcEnty.AddNewEntry(xdoc);
        }

        public bool AlreadyExist(string nameentry)
        {
            bool returnvalue = false;
            int countEntries = 0;

            if (NewSvcEnty != null)
            {
                foreach (NewSeviceEntry nse in NewSvcEnty)
                {
                    if (nse.NewSeviceEntryName == nameentry)
                    {
                        countEntries += 1;
                        if (countEntries == 2)
                            returnvalue = true;
                        break;
                    }
                }
            }
            return returnvalue;
        }       

        private void NewEntryButton_Click(object sender, EventArgs e)
        {
            EntryFormName = ((Label)sender).Text;
            UpdateColor();
            UpdateColours(NewSvcEnty, EntryFormName);
            ((Label)sender).BackColor = Color.IndianRed;
            lab = ((Label)(sender));

            btn_AddNewEntry.Enabled = false;
            btnCopy.Enabled = true;
            btnDelete.Enabled = true;            
            btnCancel.Enabled = true;
        }

        public void UpdateColor(string EntryFormName = "")
        {            
            btnCancel.Enabled = false;

            foreach (Control c in flpEntryForm.Controls)
            {
                if (EntryFormName != "" & c.Text == EntryFormName)
                {
                    c.BackColor = Color.Gray;
                    break;
                }
                //else
                //    c.BackColor = Color.Gray;
            }
        }

        private void NewEntryButton_DoubleClick(object sender, EventArgs e)
        {
            btnCopy.Enabled = false;
            btnDelete.Enabled = false;
           // btnDelete.BackColor = Color.LightGray;
           // btnDelete.ForeColor = Color.LightGray;
            btn_AddNewEntry.Enabled = true;
            btnCancel.Enabled = false;
            
            OpenFormName = ((Label)sender).Text;
            
            OpenForm(OpenFormName);
        }

        public void UpdateColours(NewSeviceEntryCollection ns, string ignoreName = "")
        {
            if (ns != null) // check service entry collection exists
            {
                //foreach (NewSeviceEntry nse in ns) // loop thro collection
                //{
                    foreach (Control lblbut in Controls[2].Controls) // loop thro controls in each entry
                    {
                        foreach (NewSeviceEntry nsen in ns)
                        {
                            if (lblbut is Label && lblbut.Text == nsen.NewSeviceEntryName && nsen.NewSeviceEntryName != ignoreName)
                                if (nsen.Enabled.ToUpper() == "TRUE")
                                {
                                    lblbut.BackColor = Color.LightBlue;
                                    break;
                                }
                                else
                                    lblbut.BackColor = Color.LightGray;
                        }
                    }
                //}
            }
        }        

        internal void AddEntriesToMainForm(NewSeviceEntry nse = null, bool copyEntry = false)
        {
            // Each service entry is a label
            // With Double and Single click events being added
            Label newEntryButton = new Label();
            newEntryButton.DoubleClick += NewEntryButton_DoubleClick;
            newEntryButton.Click += NewEntryButton_Click;
            newEntryButton.BackColor = Color.Gray;
            newEntryButton.Height = 50;
            newEntryButton.Width = 710;
            newEntryButton.TextAlign = ContentAlignment.MiddleCenter;
           
            FontFamily family = new FontFamily("Arial");
            newEntryButton.Font = new Font(family, 10.0f, FontStyle.Bold );
            newEntryButton.BorderStyle = BorderStyle.FixedSingle;

            // Set form name (reference) and text (name on form) 
            // property of label to distinguish entry type
            if (copyEntry)
            {
                newEntryButton.Name = CopyEntryFormName;                
                newEntryButton.Text = CopyEntryFormName;              
            }
            else
            {
                newEntryButton.Name = nse.NewSeviceEntryName;
                newEntryButton.Text = nse.NewSeviceEntryName;
            }           

            flpEntryForm.FlowDirection = FlowDirection.TopDown;
            flpEntryForm.WrapContents = false;
            flpEntryForm.AutoScroll = true;    
            
            // Add label to flowlayoutpanel controls collection
            flpEntryForm.Controls.Add(newEntryButton);          
            
        }
      
        private void CloseMainForm_Click(object sender, EventArgs e)
        {           
            UpdateColor(EntryFormName);
            UpdateColours(NewSvcEnty);
            EntryFormName = "";
            btn_AddNewEntry.Enabled = true;
            btnCopy.Enabled = false;
            btnDelete.Enabled = false;
            //btnDelete.BackColor = Color.LightGray;
            //btnDelete.ForeColor = Color.LightGray;
            btnCancel.Enabled = false;

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //btnDelete.BackColor = Color.Red;
            //btnDelete.ForeColor = Color.White;
            //MessageBox.Show(EntryFormName);
            if (!string.IsNullOrEmpty(EntryFormName))
            {
                DialogResult dialogResult = MessageBox.Show("Delete: " + EntryFormName, "Delete Service Entry", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    flpEntryForm.Controls.Remove(lab);

                    NewSvcEnty.DeleteEntry(EntryFormName);                                
                }
            }           

           // lab.BackColor = Color.Gray;
            lab.BackColor = Color.LightGray;
            EntryFormName = "";

            btnCopy.Enabled = false;
            btn_AddNewEntry.Enabled = true;
            btnDelete.Enabled = false;
            //btnDelete.BackColor = Color.LightGray;
            //btnDelete.ForeColor = Color.LightGray;
            btnCancel.Enabled = false;
                        
           // If there are no more entries/services then set 
           // Service button to Stopped and disable it
           // and update xml file with status of Stopped
           if (NewSvcEntyInstance.Count == 0)
            {
                StopStart(Color.Red, "Stopped", false);
                btnStart.Enabled = false;
                NewSvcEntyNames.UpdateServiceButton("Stopped");
            }

        }

        public void DeleteServiceEntry(XDocument xdoc)
        {
            NewSvcEnty.AddNewEntry(xdoc);
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            OpenFormName = EntryFormName;
            CopyEntryFormName = EntryFormName.Trim() + " (Copy)";

            btnDelete.Enabled = false;

            foreach(NewSeviceEntry nse in NewSvcEnty)
            {
                if (nse.NewSeviceEntryName == CopyEntryFormName)
                {
                    MessageBox.Show(CopyEntryFormName + " already exists! - please rename service entry called " + CopyEntryFormName);
                    return;
                }
            }
                       
            var res = (from p in NewSvcEnty
                      where p.NewSeviceEntryName == EntryFormName
                      select p).ToList();

            List<NewSeviceEntry> ns = res;
           
            AddEntriesToMainForm(null,true);

            newform = new NewEntry(this, "");

            newform.SaveImportToXMLSettings(ns, true);

            string  xmlfile = Environment.CurrentDirectory + "\\settings2.xml";
            NewSvcEnty.AddNewEntry(XDocument.Load(xmlfile));

            CopyEntryFormName = "";

            UpdateColor();
            UpdateCopyColor(NewSvcEnty);
            EntryFormName = "";
            OpenFormName = "";
            btnCopy.Enabled = false;
        }

        private void UpdateCopyColor(List<NewSeviceEntry> ns)
        {
            foreach (Control c in flpEntryForm.Controls)
            {
                foreach (NewSeviceEntry nse in ns)
                {
                    if (c.Text == nse.NewSeviceEntryName)
                    {
                        if (nse.Enabled == "True")
                            c.BackColor = Color.LightBlue;
                        else
                            c.BackColor = Color.LightGray;
                    }
                   
                }
            }
            
              /*

            btnCancel.Enabled = false;

            string enabledFlag = ns[0].Enabled.ToString();

            foreach (NewSeviceEntry c in ns)
            {
                if (c.Enabled == "True")
                {
                    c.BackColor = Color.Gray;
                    break;
                }
                //else
                //    c.BackColor = Color.Gray;
            }
            */
        }

        internal void BtnStart_Click(object sender, EventArgs e)
        {
            //UpdateColor();

            UpdateColor(EntryFormName);
            UpdateColours(NewSvcEnty);

            btnCopy.Enabled = false;
            btnDelete.Enabled = false;         
            btnCancel.Enabled = false;

            if (btnStart.Text == "Stopped")
            {
                DialogResult dialogResult = MessageBox.Show("Would you like to Start the service: ", "Start Service", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //Start Service
                    /*
                    btnStart.BackColor = Color.Green;
                    btnStart.Text = "Running";
                    btnStart.ForeColor = Color.White;                   
                    StopStartServiceFlag = true;
                    */

                    StopStart(Color.Green, "Running", true);

                    NewSvcEntyNames.UpdateServiceButton("Running");

                }
               
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Would you like to Stop the service: ", "Start Service", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //Start Service  
                    /*
                    btnStart.BackColor = Color.Red;
                    btnStart.Text = "Stopped";
                    btnStart.ForeColor = Color.White;                 
                    StopStartServiceFlag = false;
                    */

                    StopStart(Color.Red, "Stopped", false);

                    NewSvcEntyNames.UpdateServiceButton("Stopped");

                }

            }
        }

        public void StopStart(Color c, string runstop, bool runstopFlag)
        {
            ///Start Service
            btnStart.BackColor = c;
            btnStart.Text = runstop;
            btnStart.ForeColor = Color.White;
            //NewSvcEnty[0].StopStartService = true;
            //NewServiceEntryNames.StopStartServiceFlag = true;
            StopStartServiceFlag = runstopFlag;
        }
    }
}
