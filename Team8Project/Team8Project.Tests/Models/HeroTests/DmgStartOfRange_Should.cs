using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Models.Characters;

namespace Team8Project.Tests.Models.HeroTests
{
    [TestClass]
    public class DmgStartOfRange_Should
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsArgumentException_WhenInvalidValueIsPassed()
        {
            var hero = new Warrior("Pessho", HeroClass.Warrior, 100, -4, 15);
        }

        [TestMethod]
        public void ReturnTheProperValue_WhenGetMethodIsCalled()
        {
            // Arrange
            var hero = new Warrior("Pesho", HeroClass.Warrior, 200, 10, 15);

            // Act
            var result = hero.DmgStartOfRange;

            // Assert
            Assert.IsTrue(result >= 1 && result <= 500);
        }

    }
}
