using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Starship.Core.Services;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Factories
{
    [TestFixture]
    public class PositionFactoryTestFixture
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
        public void Generate_WhenInvoked_CallsRandomDoubleGeneratorThreeTimes()
        {
            // Arrange
            var subject = fixture.Create<PositionFactory>();

            // Act
            subject.Create();

            // Assert
            rdnGeneratorMock.Verify(r => r.GenerateDouble(), Times.Exactly(3));
        }

        [Test]
        public void Generate_WhenInvoked_UsesDifferentResultForEachCoordinate()
        {
            // Arrange
            var res1 = fixture.Create<double>();
            var res2 = fixture.Create<double>();
            var res3 = fixture.Create<double>();

            rdnGeneratorMock.SetupSequence(r => r.GenerateDouble())
                .Returns(res1)
                .Returns(res2)
                .Returns(res3);

            var subject = fixture.Create<PositionFactory>();

            // Act
            var result = subject.Create();

            // Assert
            Assert.AreEqual(res1*999, result.X);
            Assert.AreEqual(res2*999, result.Y);
            Assert.AreEqual(res3*999, result.Z);
        }
    }
}
