using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Starship.Core.Services;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Factories
{
    [TestFixture]
    public class CoordinateFactoryTestFixture
    {
        private IFixture fixture;

        private Mock<IRandomGenerator> rdnGeneratorMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            rdnGeneratorMock = fixture.Freeze<Mock<IRandomGenerator>>();
        }

        [Test]
        public void Generate_WhenInvoked_CallsRandomDoubleGeneratorFourTimes()
        {
            // Arrange
            var subject = fixture.Create<CoordinateFactory>();

            // Act
            subject.Create();

            // Assert
            rdnGeneratorMock.Verify(r => r.GenerateInt(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(4));
            rdnGeneratorMock.Verify(r => r.GenerateInt(999, It.IsAny<int>()), Times.Exactly(2));
            rdnGeneratorMock.Verify(r => r.GenerateInt(99, It.IsAny<int>()), Times.Exactly(1));
            rdnGeneratorMock.Verify(r => r.GenerateInt(9, It.IsAny<int>()), Times.Exactly(1));
        }

        [Test]
        public void Generate_WhenInvoked_UsesDifferentResultForEachCoordinate()
        {
            // Arrange
            var res1 = fixture.Create<int>();
            var res2 = fixture.Create<int>();
            var res3 = fixture.Create<int>();
            var res4 = fixture.Create<int>();

            rdnGeneratorMock.SetupSequence(r => r.GenerateInt(It.IsAny<int>(), It.IsAny<int>()))
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
    }
}
