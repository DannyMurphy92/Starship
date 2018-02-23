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
    public class MonsterTestFixture
    {
        private IFixture fixture;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
        }

        [Test]
        public void ToString_WhenInvoked_ReturnsObjectAsFormattedString()
        {
            // Arrange
            var subject = fixture.Create<Monster>();
            var expected = $"{ObjectsEnum.Monster}, {subject.Position.X}, {subject.Position.Y}, {subject.Position.Z}";

            // Act
            var result = subject.ToString();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
