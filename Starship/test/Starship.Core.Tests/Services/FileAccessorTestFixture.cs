using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models.Abstracts;
using Starship.Core.Services;

namespace Starship.Core.Tests.Services
{
    [TestFixture]
    public class FileAccessorTestFixture
    {
        private IFixture fixture;

        private Mock<IFileSystem> fileSystemMock;
        private Mock<IBatchSpaceObjectFactory> batchSOFactoryMock;

        private Mock<StreamWriter> streamWriterMock;
        private Mock<StreamReader> streamReaderMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            fileSystemMock = fixture.Freeze<Mock<IFileSystem>>();
            batchSOFactoryMock = fixture.Freeze<Mock<IBatchSpaceObjectFactory>>();
        }

        [TearDown]
        public void Teardown()
        {
            fileSystemMock.Reset();
            batchSOFactoryMock.Reset();
        }

        [SetUp]
        public void Setup()
        {
            streamWriterMock = new Mock<StreamWriter>(new MemoryStream());
            fileSystemMock.Setup(f => f.File.AppendText(It.IsAny<string>()))
                .Returns(streamWriterMock.Object);
        }

        [Test]
        public async Task WriteSpaceObjectsToFileAsync_WhenInvoked_ChecksFileExists()
        {
            // Arrange
            var fileName = fixture.Create<string>();

            var subject = fixture.Create<FileAccessor>();

            // Act
            await subject.WriteSpaceObjectsToFileAsync(fixture.CreateMany<BaseSpaceObject>(), fileName);

            // Assert
            fileSystemMock.Verify(f => f.File.Exists(fileName), Times.Once);
        }

        [Test]
        public async Task WriteSpaceObjectsToFileAsync_WhenFileAlreadyExists_DeletesTheFile()
        {
            // Arrange
            var fileName = fixture.Create<string>();
            fileSystemMock.Setup(f => f.File.Exists(fileName))
                .Returns(true);

            var subject = fixture.Create<FileAccessor>();

            // Act
            await subject.WriteSpaceObjectsToFileAsync(fixture.CreateMany<BaseSpaceObject>(), fileName);

            // Assert
            fileSystemMock.Verify(f => f.File.Delete(fileName), Times.Once);
        }

        [Test]
        public async Task WriteObjectsToFileAsync_WhenInvokedWithObjects_WritesEachObjectToFile()
        {
            // Arrange
            var objects = fixture.CreateMany<BaseSpaceObject>(10).ToList();

            var subject = fixture.Create<FileAccessor>();

            // Act
           await subject.WriteSpaceObjectsToFileAsync(objects, fixture.Create<string>());

            // Assert
            objects.ForEach(o => streamWriterMock.Verify(s => s.WriteLineAsync(o.ToString()), Times.Once));
        }

        [Test]
        public async Task ReadSpaceObjectsFromFile_WhenFileDoesntExist_ThrowsException()
        {
            // Arrange
            var fileName = fixture.Create<string>();
            fileSystemMock.Setup(f => f.File.Exists(fileName))
                .Returns(false);

            var subject = fixture.Create<FileAccessor>();

            // Act
            AsyncTestDelegate act = () => subject.ReadSpaceObjectFromFileAsync(fileName);

            // Assert
            Assert.That(act, Throws.TypeOf<FileNotFoundException>());
        }

        [Test]
        public void ReadSpaceObjectsFromFile_ForEachLineInTheFile_TheSpaceObjectFactoryIsCalled()
        {
            // Arrange
            var fileName = fixture.Create<string>();
            fileSystemMock.Setup(f => f.File.Exists(fileName))
                .Returns(false);


            // Act


            // Assert

        }
    }
}
