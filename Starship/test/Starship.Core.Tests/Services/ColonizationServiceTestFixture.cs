﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Starship.Core.Models;
using Starship.Core.Models.Abstracts;
using Starship.Core.Services;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Services
{
    [TestFixture]
    public class ColonizationServiceTestFixture
    {
        private IFixture fixture;

        private Mock<ITravelService> travelServiceMock;

        private IList<Planet> planets;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            travelServiceMock = fixture.Freeze<Mock<ITravelService>>();
        }

        [SetUp]
        public void Setup()
        {
            SetUpPlanets();

            travelServiceMock.SetupSequence(
                    t => t.FindNearestObject(It.IsAny<Position>(), It.IsAny<IList<Planet>>()))
                 .Returns(planets[0])
                .Returns(planets[1])
                .Returns(planets[2]);
        }

        [TearDown]
        public void Teardown()
        {
            travelServiceMock.Reset();
        }


        [Test]
        public void ConquerTheUniverseSecs_WhenTimeLimitOnlyAllowsForConqueringSomePlanets_OnlyConquersSomePlanets()
        {
            // Arrange
            var travelTime = 20;
            var clonizeRate = 0.6;
            var pcColonize = .3;
            var timeLimit = planets[0].Area * pcColonize * clonizeRate;
            timeLimit += planets[1].Area * pcColonize * clonizeRate;
            timeLimit += travelTime * 60 *2;

            var subject = new ColonizationService(travelServiceMock.Object, travelTime, clonizeRate, pcColonize);

            // Act
            var result = subject.ConquerTheUniverseSecs(fixture.Create<Position>(), planets, timeLimit).ToList();

            // Assert
            Assert.AreEqual(2, result.Count());
            travelServiceMock.Verify(t => t.FindNearestObject(It.IsAny<Position>(), It.IsAny<IList<Planet>>()), Times.Exactly(3));
        }
        
        [Test]
        public void ConquerTheUniverseSecs_WhenTimeLimitOnlyAllowsForConqueringOfAPlanet_DoesNotConquerPlanet()
        {
            // Arrange
            var timeLimit = planets[0].Area * .5 * .4;

            var subject = fixture.Create<ColonizationService>();

            // Act
            var result = subject.ConquerTheUniverseSecs(fixture.Create<Position>(), planets, timeLimit).ToList();

            // Assert
            Assert.AreEqual(0, result.Count());
            travelServiceMock.Verify(t => t.FindNearestObject(It.IsAny<Position>(), It.IsAny<IList<Planet>>()), Times.Once);
        }
        
        [Test]
        public void ConquerTheUniverseSecs_WhenInvoked_MustUsePositionOfDestinationAsStartPosForNextIteration()
        {
            // Arrange
            var travelTime = 20;
            var clonizeRate = 0.6;
            var pcColonize = .3;
            var timeLimit = planets[0].Area * pcColonize * clonizeRate;
            timeLimit += planets[1].Area * pcColonize * clonizeRate;
            timeLimit += travelTime * 60 * 2;
            var startPos = fixture.Create<Position>();
            var pos1 = planets[0].Position;
            var pos2 = planets[1].Position;

            var subject = new ColonizationService(travelServiceMock.Object, travelTime, clonizeRate, pcColonize);

            // Act
            var result = subject.ConquerTheUniverseSecs(startPos, planets, timeLimit).ToList();

            // Assert
            travelServiceMock.Verify(t => t.FindNearestObject(startPos, It.IsAny<IList<Planet>>()), Times.Once);
            travelServiceMock.Verify(t => t.FindNearestObject(pos1, It.IsAny<IList<Planet>>()), Times.Once);
            travelServiceMock.Verify(t => t.FindNearestObject(pos2, It.IsAny<IList<Planet>>()), Times.Once);
        }

        private void SetUpPlanets()
        {
            planets = new List<Planet>();
            planets.Add(new Planet(fixture.Create<Position>(), true, 100000));
            planets.Add(new Planet(fixture.Create<Position>(), true, 100000));
            planets.Add(new Planet(fixture.Create<Position>(), true, 100000));
        }
    }
}
