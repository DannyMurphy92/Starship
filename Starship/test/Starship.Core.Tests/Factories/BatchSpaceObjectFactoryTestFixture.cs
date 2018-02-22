using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Starship.Core.Factories;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Services;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Services
{
    [TestFixture]
    public class BatchSpaceObjectFactoryTestFixture
    {
        private IFixture fixture;

        private Mock<IPlanetFactory> planetFactoryMock;
        private Mock<IMonsterFactory> monsterFactoryMock;
        private Mock<IRandomGenerator> randomGenMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture  = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            planetFactoryMock = fixture.Freeze<Mock<IPlanetFactory>>();
            monsterFactoryMock = fixture.Freeze<Mock<IMonsterFactory>>();
            randomGenMock = fixture.Freeze<Mock<IRandomGenerator>>();
        }

        [TearDown]
        public void Teardown()
        {
            planetFactoryMock.Reset();
            monsterFactoryMock.Reset();
            randomGenMock.Reset();
        }

        [Test]
        public void Generate_WhenInvoked_GeneratesTheNumberOfObjectsRequested()
        {
            // Arrange
            int amount = 500;
            var subject = fixture.Create<BatchSpaceObjectFactory>();

            // Act
            var result = subject.Generate(amount);

            // Assert
            Assert.AreEqual(amount, result.Count());
        }


        [Test]
        public void Generate_WhenInvoked_CreatesRandomBoolSuppliedAmountOfTimes()
        {
            // Arrange
            int amount = 500;
            var subject = fixture.Create<BatchSpaceObjectFactory>();

            // Act
            var result = subject.Generate(amount).ToList();

            // Assert
            randomGenMock.Verify(r => r.GenerateBool(It.IsAny<int>()), Times.Exactly(amount));
        }

        [Test]
        public void Generate_ForEachTimeTheGeneratorReturnsTrue_APlanetIsCreated()
        {
            // Arrange
            int amount = 500;
            randomGenMock.Setup(r => r.GenerateBool(It.IsAny<int>()))
                .Returns(true);

            var subject = fixture.Create<BatchSpaceObjectFactory>();

            // Act
            var result = subject.Generate(amount).ToList();


            // Assert
            planetFactoryMock.Verify(p => p.Create(),Times.Exactly(amount));
        }

        [Test]
        public void Generate_ForEachTimeTheGeneratorReturnsFalse_AMonsterIsCreated()
        {
            // Arrange
            int amount = 500;
            randomGenMock.Setup(r => r.GenerateBool(It.IsAny<int>()))
                .Returns(false);

            var subject = fixture.Create<BatchSpaceObjectFactory>();

            // Act
            var result = subject.Generate(amount).ToList();


            // Assert
            monsterFactoryMock.Verify(p => p.Create(), Times.Exactly(amount));
        }
    }
}
