using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Starship.Core.Factories;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Factories
{
    [TestFixture]
    public class SpaceObjectFactoryFactoryTestFixture
    {
        private IFixture fixture;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
        }

        [Test]
        public void CreateFactory_WhenPassedMonsterFlag_ReturnsMonsterFactory()
        {
            // Arrange
            var subject = fixture.Create<SpaceObjectFactoryFactory>();

            // Act
            var result = subject.CreateFactory(ObjectsEnum.Monster);

            // Assert
            Assert.AreEqual(typeof(MonsterFactory), result.GetType());
        }


        [Test]
        public void CreateFactory_WhenPassedPlanetFlag_ReturnsMonsterFactory()
        {
            // Arrange
            var subject = fixture.Create<SpaceObjectFactoryFactory>();

            // Act
            var result = subject.CreateFactory(ObjectsEnum.Planet);

            // Assert
            Assert.AreEqual(typeof(PlanetFactory), result.GetType());
        }
    }
}
