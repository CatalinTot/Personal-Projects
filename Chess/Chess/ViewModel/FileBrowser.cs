using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;

namespace Chess.ViewModel
{
    public class FileBrowser : IFileBrowser
    {
        public FileBrowser()
        {
        }

        public string ReadFromFile(out IEnumerable<string> content)
        {
            OpenFileDialog dialog = new ();
            if (dialog.ShowDialog() != true)
            {
                content = null;
                return null;
            }

            content = File.ReadAllLines(dialog.FileName);
            return dialog.FileName;
        }
    }
}
