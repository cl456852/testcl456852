using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace UI1
{
    public class OpenFolderDialog : FolderNameEditor, IDisposable
    {
        FolderNameEditor.FolderBrowser fDialog = new FolderNameEditor.FolderBrowser();

        public OpenFolderDialog()
        {
        }

        public DialogResult ShowDialog()
        {
            return ShowDialog("Select a folder:");
        }

        public DialogResult ShowDialog(string description)
        {
            fDialog.Description = description;
            return fDialog.ShowDialog();
        }

        public string Path
        {
            get
            {
                return fDialog.DirectoryPath;
            }
        }

        public void Dispose()
        {
            fDialog.Dispose();
        }
    }
}