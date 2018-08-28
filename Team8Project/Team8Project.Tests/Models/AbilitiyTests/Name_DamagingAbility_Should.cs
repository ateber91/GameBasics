using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Models.Magic;

namespace Team8Project.Tests.Models.HeroTests
{
    [TestClass]
    public class Name_DamagingAbility_Should
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsArgumentException_WhenInvalidValueIsPassed()
        {
            // Arrange
            var ability = new DamagingAbility("", 2, HeroClass.Assasin, EffectType.Damage, 2);
        }

        [TestMethod]
        public void ReturnTheProperValue_WhenGetMethodIsCalled()
        {
            // Arrange
            var ability = new DamagingAbility("Pesho", 2, HeroClass.Assasin, EffectType.Damage, 2);

            // Act
            var result = ability.Name;

            // Assert
            Assert.AreEqual("Pesho", result);
        }

    }
}
