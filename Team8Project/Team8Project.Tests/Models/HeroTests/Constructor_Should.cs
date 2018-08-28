using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Models.Characters;

namespace Team8Project.Tests.Models.HeroTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void SetProperName_WhenTheObjectIsConstructed()
        {
            // Arrange & Act
            var hero = new Warrior("Valid name", HeroClass.Warrior, 200, 10, 15);

            // Assert
            Assert.AreEqual("Valid name", hero.Name);
        }

        [TestMethod]
        public void InitilizeAbilitiesCollection_WhenTheObjectIsConstructed()
        {
            // Arrange & Act
            var hero = new Warrior("Valid name", HeroClass.Warrior, 200, 10, 15);

            // Assert
            Assert.IsNotNull(hero.Abilities);
        }

        [TestMethod]
        public void InitilizeAppliedEffectsCollection_WhenTheObjectIsConstructed()
        {
            // Arrange & Act
            var hero = new Warrior("Valid name", HeroClass.Warrior, 200, 10, 15);

            // Assert
            Assert.IsNotNull(hero.AppliedEffects);
        }
    }
}
