using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;
using Starship.Core.Models;

namespace Starship.Core.Tests.Models
{
    [TestFixture]
    public class CoordinateTestFixture
    {
        private IFixture fixture;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
        }

        [Test]
        public void ToString_WhenInvoked_ReturnsObjectAsString()
        {
            // Arrange
            var subject = fixture.Create<Coordinate>();
            var expected = $"{subject.Area1}.{subject.Area2}.{subject.Area3}.{subject.Area4}";

            // Act
            var result = subject.ToString();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
