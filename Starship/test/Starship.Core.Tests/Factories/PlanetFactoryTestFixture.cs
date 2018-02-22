using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Starship.Core.Factories;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Factories
{
    [TestFixture]
    public class PlanetFactoryTestFixture 
    {
        private IFixture fixture;

        private Mock<IPositionGenerator> positionGenMock;
        private Mock<IRandomGenerator> randomGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            positionGenMock = fixture.Freeze<Mock<IPositionGenerator>>();
            randomGenerator = fixture.Freeze<Mock<IRandomGenerator>>();
        }

        [TearDown]
        public void Teardown()
        {
            positionGenMock.Reset();
            randomGenerator.Reset();
        }

        [SetUp]
        public void Setup()
        {
            positionGenMock.Setup(p => p.Generate())
                .Returns(fixture.Create<Position>());
        }

        [Test]
        public void Create_WhenInvoked_ReturnsAPlanetObject()
        {
            // Arrange
            var subject = fixture.Create<PlanetFactory>();

            // Act
            var result = subject.Create();

            // Assert
            Assert.AreEqual(typeof(Planet), result.GetType());
        }

        [Test]
        public void Create_WhenInvoked_CallsCoordinateGenerator()
        {
            // Arrange
            var subject = fixture.Create<PlanetFactory>();

            // Act
            subject.Create();

            // Assert
            positionGenMock.Verify(p => p.Generate(), Times.Once);
        }

        [Test]
        public void Create_WhenInvoked_CallsRandomBoolGenForIsHabitable()
        {
            // Arrange
            var isHabitable = false;
            randomGenerator.Setup(r => r.GenerateBool(It.IsAny<int>()))
                .Returns(isHabitable);
            var subject = fixture.Create<PlanetFactory>();

            // Act
            var result = subject.Create();

            // Assert
            randomGenerator.Verify(p => p.GenerateBool(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(isHabitable, result.IsHabitable);
        }

        [Test]
        public void Create_WhenInvoked_CallsRandomDoubleGenForArea()
        {
            // Arrange
            var rdnRes = fixture.Create<double>();
            var area = rdnRes * 100000000;
            randomGenerator.Setup(r => r.GenerateDouble())
                .Returns(rdnRes);
            var subject = fixture.Create<PlanetFactory>();

            // Act
            var result = subject.Create();

            // Assert
            randomGenerator.Verify(p => p.GenerateBool(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(area, result.Area);
        }
    }
}
