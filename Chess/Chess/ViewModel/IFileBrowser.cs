using System.Collections.Generic;

namespace Chess.ViewModel
{
    public interface IFileBrowser
    {
        public string ReadFromFile(out IEnumerable<string> content);
    }
}