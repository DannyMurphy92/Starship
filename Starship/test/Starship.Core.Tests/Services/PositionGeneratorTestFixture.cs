using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Starship.Core.Services;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Services
{
    [TestFixture]
    public class PositionGeneratorTestFixture
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
            var subject = fixture.Create<PositionGenerator>();

            // Act
            subject.Generate();

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

            var subject = fixture.Create<PositionGenerator>();

            // Act
            var result = subject.Generate();

            // Assert
            Assert.AreEqual(res1*999, result.XCoor);
            Assert.AreEqual(res2*999, result.YCoor);
            Assert.AreEqual(res3*999, result.ZCoor);
        }
    }
}
