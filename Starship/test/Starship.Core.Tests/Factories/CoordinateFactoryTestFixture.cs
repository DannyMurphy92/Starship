using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Starship.Core.Factories;
using Starship.Core.Models;
using Starship.Core.Services;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Factories
{
    [TestFixture]
    public class CoordinateFactoryTestFixture
    {
        private IFixture fixture;

        private Mock<IRandomGenerator> rdnGeneratorMock;
        private string stringInput;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            rdnGeneratorMock = fixture.Freeze<Mock<IRandomGenerator>>();
        }

        [SetUp]
        public void Setup()
        {
            stringInput = "123.123.12.1";
        }

        [TearDown]
        public void Teardown()
        {
            rdnGeneratorMock.Reset();
        }

        [Test]
        public void Create_WhenInvoked_CallsRandomDoubleGeneratorFourTimes()
        {
            // Arrange
            var subject = fixture.Create<CoordinateFactory>();

            // Act
            subject.Create();

            // Assert
            rdnGeneratorMock.Verify(r => r.GenerateInt(It.IsAny<int>()), Times.Exactly(4));
            rdnGeneratorMock.Verify(r => r.GenerateInt(999), Times.Exactly(2));
            rdnGeneratorMock.Verify(r => r.GenerateInt(99), Times.Exactly(1));
            rdnGeneratorMock.Verify(r => r.GenerateInt(9), Times.Exactly(1));
        }

        [Test]
        public void Create_WhenInvoked_UsesDifferentResultForEachCoordinate()
        {
            // Arrange
            var res1 = fixture.Create<int>();
            var res2 = fixture.Create<int>();
            var res3 = fixture.Create<int>();
            var res4 = fixture.Create<int>();

            rdnGeneratorMock.SetupSequence(r => r.GenerateInt(It.IsAny<int>()))
                .Returns(res1)
                .Returns(res2)
                .Returns(res3)
                .Returns(res4);

            var subject = fixture.Create<CoordinateFactory>();

            // Act
            var result = subject.Create();

            // Assert
            Assert.AreEqual(res1, result.Area1);
            Assert.AreEqual(res2, result.Area2);
            Assert.AreEqual(res3, result.Area3);
            Assert.AreEqual(res4, result.Area4);
        }

        [Test]
        public void CreateFromString_WhenInvalidString_ThrowsException()
        {
            // Arrange
            stringInput = "not a valid coordinate";
            var subject = fixture.Create<CoordinateFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(stringInput);

            // Assert
            Assert.That(act, Throws.ArgumentException);
        }

        [Test]
        public void CreateFromString_WhenArea1Invalid_ThrowsException()
        {
            // Arrange
            var args = stringInput.Split('.');
            args[0] = "fff";
            stringInput = string.Join(".", args);
            var subject = fixture.Create<CoordinateFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(stringInput);

            // Assert
            Assert.That(act, Throws.ArgumentException);
        }

        [Test]
        public void CreateFromString_WhenArea2Invalid_ThrowsException()
        {
            // Arrange
            var args = stringInput.Split('.');
            args[1] = "fff";
            stringInput = string.Join(".", args);
            var subject = fixture.Create<CoordinateFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(stringInput);

            // Assert
            Assert.That(act, Throws.ArgumentException);
        }

        [Test]
        public void CreateFromString_WhenArea3Invalid_ThrowsException()
        {
            // Arrange
            var args = stringInput.Split('.');
            args[2] = "fff";
            stringInput = string.Join(".", args);
            var subject = fixture.Create<CoordinateFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(stringInput);

            // Assert
            Assert.That(act, Throws.ArgumentException);
        }

        [Test]
        public void CreateFromString_WhenArea4Invalid_ThrowsException()
        {
            // Arrange
            var args = stringInput.Split('.');
            args[3] = "fff";
            stringInput = string.Join(".", args);
            var subject = fixture.Create<CoordinateFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(stringInput);

            // Assert
            Assert.That(act, Throws.ArgumentException);
        }

        [Test]
        public void CreateFromString_WhenInvokedWithValidArgument_ReturnsCorrectValue()
        {
            // Arrange
            var expected = fixture.Create<Coordinate>();
            stringInput = expected.ToString();
            var subject = fixture.Create<CoordinateFactory>();

            // Act
            var result = subject.CreateFromString(stringInput);

            // Assert
            Assert.AreEqual(expected.Area1, result.Area1);
            Assert.AreEqual(expected.Area2, result.Area2);
            Assert.AreEqual(expected.Area3, result.Area3);
            Assert.AreEqual(expected.Area4, result.Area4);
        }
    }
}
