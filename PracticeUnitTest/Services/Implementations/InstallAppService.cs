using PracticeUnitTest.Utilities;
using System.Net;

namespace PracticeUnitTest.Services.Implementations
{
    public class InstallAppService
    {
        private readonly IFileDownloader _fileDownloader;
        private string _setupDestinationFile;

        public InstallAppService(IFileDownloader fileDownloader, string setupDestinationFile)
        {
            _fileDownloader = fileDownloader;
            _setupDestinationFile = setupDestinationFile;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _fileDownloader.DownloadFile(
                    string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName),
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}