using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileImportService
{
    public class NewSeviceEntry
    {
        public string Enabled { get; set; }
        public string NewSeviceEntryName { get; set; }
        public List<NewSeviceEntryFileAssoc> NewSeviceEntryFileAss { get; set; } //https://www.technothirsty.com/convert-list-xml-csharp/
        public NewSeviceEntryDataBase[] NewSeviceEntryDataBases { get; set; }
        public NewSeviceEntryCommand[] NewSeviceEntryCommands { get; set; }
        public NewSeviceEntryFolder[] NewSeviceEntryFolders { get; set; }
        public List<NewSeviceEntryFileMappings> NewSeviceEntryMap { get; set; }
        //public bool StopStartService { get; set; }
        public bool AssociateMappings { get; set; }
        //public strin NewSeviceEntryFileMappings { get; set; }

        public class NewSeviceEntryDataBase
        {
            public string NewSeviceEntryDB { get; set; }
            public string NewSeviceEntryDBPass { get; set; }
            public string NewSeviceEntryDBUName { get; set; }
        }
        
        public class NewSeviceEntryCommand
        {
            public string NewSeviceEntrySProc { get; set; }
            public string NewSeviceEntrySQL { get; set; }
        }

        public class NewSeviceEntryFolder
        {
            public string NewSeviceEntryMainFolder { get; set; }
            public string NewSeviceEntryBackup { get; set; }
            public string NewSeviceEntryFolderFail { get; set; }
            //public string NewSeviceEntryMainFolderSelection { get; set; }
        }

        public class NewSeviceEntryFileAssoc
        {
            public int ColNum { get; set; }
            public string ColName { get; set; }
            public string ColDataType { get; set; }

        }

        public class NewSeviceEntryFileMappings
        {
            public string RowVal { get; set; }
        }

    }

        
      
    
}
