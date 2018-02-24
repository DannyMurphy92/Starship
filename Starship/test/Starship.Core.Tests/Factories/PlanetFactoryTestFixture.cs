using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Starship.Core.Factories;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Factories
{
    [TestFixture]
    public class PlanetFactoryTestFixture 
    {
        private IFixture fixture;

        private Mock<IPositionFactory> positionGenMock;
        private Mock<IRandomGenerator> randomGenerator;


        private Planet planet;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            positionGenMock = fixture.Freeze<Mock<IPositionFactory>>();
            randomGenerator = fixture.Freeze<Mock<IRandomGenerator>>();
        }

        [SetUp]
        public void Setup()
        {
            planet = fixture.Create<Planet>();

            positionGenMock.Setup(p => p.Create())
                .Returns(fixture.Create<Position>());

            positionGenMock.Setup(p => p.CreateFromString(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => planet.Position);
        }

        [TearDown]
        public void Teardown()
        {
            positionGenMock.Reset();
            randomGenerator.Reset();
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
            positionGenMock.Verify(p => p.Create(), Times.Once);
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
            var area = rdnRes * 100;
            randomGenerator.Setup(r => r.GenerateDouble())
                .Returns(rdnRes);
            var subject = fixture.Create<PlanetFactory>();

            // Act
            var result = subject.Create();

            // Assert
            randomGenerator.Verify(p => p.GenerateBool(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(area, result.Area);
        }

        [Test]
        public void CreateFromString_WhenNotPassedCommaListOf6Items_ThrowsException()
        {
            // Arrange
            var invalidStr = "This is not a valid planet string";
            var subject = fixture.Create<PlanetFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(invalidStr);

            // Assert 
            Assert.That(act, Throws.ArgumentException
                .With.Property("Message").EqualTo("Input is not a valid argument"));
        }

        [Test]
        public void CreateFromString_WhenIsValidPlanetString_ReturnsMatchingMonsterObject()
        {
            // Arrange
            var planetString = planet.ToString();
            var subject = fixture.Create<PlanetFactory>();

            // Act
            var result = subject.CreateFromString(planetString);

            // Assert
            var pPos = planet.Position;
            var rPos = result.Position;
            Assert.AreEqual(planet.Area, result.Area);
            Assert.AreEqual(planet.IsHabitable, result.IsHabitable);
            Assert.IsTrue(pPos.X == rPos.X && pPos.Y == rPos.Y && pPos.Z == rPos.Z);
        }

        [Test]
        public void CreateFromString_WhenIsHabitableDoesNotParseToBool_ThrowsException()
        {
            // Arrange
            var planet = fixture.Create<Planet>();
            var planetString = planet.ToString();
            var planetArgs = planetString.Split(',');
            planetArgs[4] = "ffff";
            planetString = string.Join(",", planetArgs);
            var subject = fixture.Create<PlanetFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(planetString);

            // Assert 
            Assert.That(act, Throws.ArgumentException
                .With.Property("Message").EqualTo("Input is not a valid argument"));
        }

        [Test]
        public void CreateFromString_WhenAreaDoesNotParseToDouble_ThrowsException()
        {
            // Arrange
            var planet = fixture.Create<Planet>();
            var planetString = planet.ToString();
            var planetArgs = planetString.Split(',');
            planetArgs[5] = "ffff";
            planetString = string.Join(",", planetArgs);
            var subject = fixture.Create<PlanetFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(planetString);

            // Assert 
            Assert.That(act, Throws.ArgumentException
                .With.Property("Message").EqualTo("Input is not a valid argument"));
        }
    }
}
