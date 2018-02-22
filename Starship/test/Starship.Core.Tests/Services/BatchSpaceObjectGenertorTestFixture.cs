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
using Starship.Core.Factories.Interfaces;
using Starship.Core.Services;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Services
{
    [TestFixture]
    public class BatchSpaceObjectGenertorTestFixture
    {
        private IFixture fixture;

        private Mock<IPlanetFactory> planetFactoryMock;
        private Mock<IMonsterFactory> monsterFactoryMock;
        private Mock<IRandomGenerator> randomGenMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture  = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            planetFactoryMock = fixture.Create<Mock<IPlanetFactory>>();
            monsterFactoryMock = fixture.Create<Mock<IMonsterFactory>>();
            randomGenMock = fixture.Create<Mock<IRandomGenerator>>();
        }


        [Test]
        public void Generate_WhenInvoked_GeneratesTheNumberOfObjectsRequested()
        {
            // Arrange
            int amount = 500;
            var subject = fixture.Create<BatchSpaceObjectGenertor>();

            // Act
            var result = subject.Generate(amount);

            // Assert
            Assert.AreEqual(amount, result.Count());
        }
    }
}
