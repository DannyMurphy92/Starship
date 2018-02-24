using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        [SetUp]
        public void Setup()
        {
            planetFactoryMock.Setup(p => p.Create())
                .Returns(fixture.Create<Planet>());
            planetFactoryMock.Setup(p => p.CreateFromString(It.IsAny<string>()))
                .Returns(fixture.Create<Planet>());
            monsterFactoryMock.Setup(p => p.Create())
                .Returns(fixture.Create<Monster>());
            monsterFactoryMock.Setup(p => p.CreateFromString(It.IsAny<string>()))
                .Returns(fixture.Create<Monster>());
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
            var result = subject.Create(amount);

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
            var result = subject.Create(amount).ToList();

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
            var result = subject.Create(amount).ToList();


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
            var result = subject.Create(amount).ToList();


            // Assert
            monsterFactoryMock.Verify(p => p.Create(), Times.Exactly(amount));
        }

        [Test]
        public void CreateFromString_WhenIsMonsterString_CallMonsterFactory()
        {
            // Arrange
            var monster = fixture.Create<Monster>();
            var monsterStr = monster.ToString();

            var subject = fixture.Create<BatchSpaceObjectFactory>();

            // Act
            subject.CreateFromString(monsterStr);

            // Assert
            monsterFactoryMock.Verify(m => m.CreateFromString(monsterStr), Times.Once);
        }

        [Test]
        public void CreateFromString_WhenIsPlanetString_CallPlanetFactory()
        {
            // Arrange
            var planet = fixture.Create<Planet>();
            var planetSr = planet.ToString();

            var subject = fixture.Create<BatchSpaceObjectFactory>();

            // Act
            subject.CreateFromString(planetSr);

            // Assert
            planetFactoryMock.Verify(m => m.CreateFromString(planetSr), Times.Once);
        }

        [Test]
        public void CreateFromString_WhenIsNotAValidSpaceObjectString_ReturnsNull()
        {
            // Arrange
            var input = fixture.Create<string>();

            var subject = fixture.Create<BatchSpaceObjectFactory>();

            // Act
            var result = subject.CreateFromString(input);

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CreateFromString_WhenPlanetFactoryThrowsException_ReturnsNull()
        {
            // Arrange
            planetFactoryMock.Setup(p => p.CreateFromString(It.IsAny<string>()))
                .Throws(new Exception());

            var planet = fixture.Create<Planet>();
            var planetSr = planet.ToString();

            var subject = fixture.Create<BatchSpaceObjectFactory>();

            // Act
            var result = subject.CreateFromString(planetSr);

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CreateFromString_WhenMonsterFactoryThrowsException_ReturnsNull()
        {
            // Arrange
            monsterFactoryMock.Setup(p => p.CreateFromString(It.IsAny<string>()))
                .Throws(new Exception());

            var monster = fixture.Create<Monster>();
            var monsterStr = monster.ToString();

            var subject = fixture.Create<BatchSpaceObjectFactory>();

            // Act
            var result = subject.CreateFromString(monsterStr);

            // Assert
            Assert.AreEqual(null, result);
        }
    }
}
