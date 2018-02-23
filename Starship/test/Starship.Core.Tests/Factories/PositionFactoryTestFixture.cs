using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Starship.Core.Factories;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Services;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Factories
{
    [TestFixture]
    public class PositionFactoryTestFixture
    {
        private IFixture fixture;

        private Mock<ICoordinateFactory> coorFactoryMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            coorFactoryMock = fixture.Freeze<Mock<ICoordinateFactory>>();
        }

        [Test]
        public void Generate_WhenInvoked_CallsRandomDoubleGeneratorThreeTimes()
        {
            // Arrange
            var subject = fixture.Create<PositionFactory>();

            // Act
            subject.Create();

            // Assert
            coorFactoryMock.Verify(r => r.Create(), Times.Exactly(3));
        }

        [Test]
        public void Generate_WhenInvoked_UsesDifferentResultForEachCoordinate()
        {
            // Arrange
            var res1 = fixture.Create<Coordinate>();
            var res2 = fixture.Create<Coordinate>();
            var res3 = fixture.Create<Coordinate>();

            coorFactoryMock.SetupSequence(r => r.Create())
                .Returns(res1)
                .Returns(res2)
                .Returns(res3);

            var subject = fixture.Create<PositionFactory>();

            // Act
            var result = subject.Create();

            // Assert
            Assert.AreEqual(res1, result.X);
            Assert.AreEqual(res2, result.Y);
            Assert.AreEqual(res3, result.Z);
        }
    }
}
