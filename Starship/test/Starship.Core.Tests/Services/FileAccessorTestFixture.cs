using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.Practices.ObjectBuilder2;
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
        private IList<string> fileLines;

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

            fileLines = fixture.CreateMany<string>(10).ToList();


            fileSystemMock.Setup(f => f.File.Exists(It.IsAny<string>()))
                .Returns(true);
            fileSystemMock.Setup(f => f.File.AppendText(It.IsAny<string>()))
                .Returns(streamWriterMock.Object);

            fileSystemMock.Setup(f => f.File.OpenRead(It.IsAny<string>()))
                .Returns(FileLinesToStream);
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
        public void ReadSpaceObjectsFromFile_WhenFileDoesntExist_ThrowsException()
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
        public async Task ReadSpaceObjectsFromFile_ForEachLineInTheFile_TheSpaceObjectFactoryIsCalled()
        {
            // Arrange
            var fileName = fixture.Create<string>();

            var subject = fixture.Create<FileAccessor>();

            // Act
            await subject.ReadSpaceObjectFromFileAsync(fixture.Create<string>());

            // Assert
            batchSOFactoryMock.Verify(b => b.CreateFromString(It.IsAny<string>()), Times.Exactly(fileLines.Count()));
            fileLines.ForEach(l => batchSOFactoryMock.Verify(b => b.CreateFromString(l), Times.Once));
        }

        [Test]
        public async Task ReadSpaceObjectsFromFile_WhenFactoryReturnsNull_ResultIsNotAddedToResultList()
        {
            // Arrange
            batchSOFactoryMock.Setup(b => b.CreateFromString(fileLines[0]))
                .Returns(() => null);

            var subject = fixture.Create<FileAccessor>();

            // Act
            var result = await subject.ReadSpaceObjectFromFileAsync(fixture.Create<string>());

            // Assert
            Assert.AreEqual(fileLines.Count() - 1, result.Count());
        }

        private Stream FileLinesToStream()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            foreach (var line in fileLines)
            {
                writer.WriteLine(line);
            }

            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}
