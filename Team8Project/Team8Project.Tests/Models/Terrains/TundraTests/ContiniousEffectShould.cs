using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Models.Terrains;

namespace Team8Project.Tests.Models.Terrains.TundraTests
{
    [TestClass]
    public class ContiniousEffectShould
    {
        [TestMethod]
        public void ReturnSuccessMessageIfItIsDay()
        {
            //Arrange
            var tundra = new Tundra();
            //Setup
            var hotMock = new Mock<IEffect>();
            var heroMock = new Mock<IHero>();
            var appliedEffects = new List<IEffect>();

            appliedEffects.Add(hotMock.Object);

            hotMock.SetupGet(hot => hot.Type).Returns(EffectType.Incapacitated);

            heroMock
            .SetupGet(hero => hero.AppliedEffects)
            .Returns(appliedEffects);

            heroMock
                .SetupGet(hero => hero.Name)
                .Returns("Gosho");

            string result = tundra.ContinuousEffect(heroMock.Object);

            Assert.AreEqual("Gosho's duration of all applied incapacitating effects increased by 1", result);
        }

        [TestMethod]
        public void ReturnSuccessMessageIfItIsNight()
        {
            //Arrange
            var tundra = new Tundra();
            tundra.IsDay = false;
            //Setup
            var abilityMock = new Mock<IAbility>();
            var heroMock = new Mock<IHero>();
            var abilities = new List<IAbility>();

            abilities.Add(abilityMock.Object);

            heroMock
            .SetupGet(hero => hero.Abilities)
            .Returns(abilities);

            heroMock
                .SetupGet(hero => hero.Name)
                .Returns("Gosho");

            string result = tundra.ContinuousEffect(heroMock.Object);

            Assert.AreEqual("Gosho's available abilities are on cool down", result);
        }
    }
}
