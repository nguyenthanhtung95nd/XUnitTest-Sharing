using FluentAssertions;
using Moq;
using PracticeUnitTest.Services.Implementations;
using PracticeUnitTest.Utilities;
using System.Net;
using Xunit;

namespace PracticeUnitTest.Tests.Services
{
    public class InstallAppServiceTests
    {
        private readonly Mock<IFileDownloader> _fileDownloader;
        private string _setupDestinationFile;

        private readonly InstallAppService _installAppService;

        public InstallAppServiceTests()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _setupDestinationFile = "C://download";

            _installAppService = new InstallAppService(_fileDownloader.Object, _setupDestinationFile);
        }

        [Fact]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            // Arrange
            _fileDownloader.Setup(fd =>
                    fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            // Act
            var actual = _installAppService.DownloadInstaller("customer", "installer");

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void DownloadInstaller_DownloadCompletes_ReturnTrue()
        {
            // Acts
            var actual = _installAppService.DownloadInstaller("customer", "installer");

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void DownloadInstaller_DownloadCompletes_VerifyPathDownload()
        {
            // Arrange
            string actualPathDownload = null;
            _fileDownloader.Setup(fd =>
                    fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((x, y) => actualPathDownload = y);

            // Acts
            _installAppService.DownloadInstaller("customer", "installer");

            // Assert
            actualPathDownload.Should().Be(_setupDestinationFile);
        }
    }
}