using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Team8Project.Common.Enums;
using Team8Project.Models.Characters;

namespace Team8Project.Tests.Models.HeroTests
{
    [TestClass]
    public class Name_Should
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsArgumentException_WhenInvalidValueIsPassed()
        {
            // Arrange
            var invalidName = "Mega-giga-super-duper-hooper-gooper-shooper-epicHeroNameFromSpace123123123123123123123123";
            var hero = new Warrior(invalidName, HeroClass.Warrior, 200, 10, 15);
        }

        [TestMethod]
        public void ReturnTheProperValue_WhenGetMethodIsCalled()
        {
            var invalidName = "Pesho";
            // Arrange
            var hero = new Warrior(invalidName, HeroClass.Warrior, 200, 10, 15);

            // Act
            var result = hero.Name;

            // Assert
            Assert.AreEqual("Pesho", result);
        }

    }
}
