using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeUnitTest.Utilities
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string path);
    }
}
