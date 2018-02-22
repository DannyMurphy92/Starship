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

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            positionGenMock = fixture.Freeze<Mock<IPositionGenerator>>();
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
    }
}
