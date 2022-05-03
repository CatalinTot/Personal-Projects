using Chess.ViewModel;
using System.Collections.Generic;

namespace ChessTests.Tests.Stubs
{
    public class StubFileBrowser : IFileBrowser
    {
        public string ReadFromFile(out IEnumerable<string> content)
        {
            content = new List<string>() { "e4 e6" };
            return "TestFilePath";
        }
    }
}
