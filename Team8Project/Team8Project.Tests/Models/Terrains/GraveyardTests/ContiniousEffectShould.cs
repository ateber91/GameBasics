using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Models.Characters;
using Team8Project.Models.Magic.EffectAbilities;
using Team8Project.Models.Terrains;

namespace Team8Project.Tests.Models.Terrains
{
    [TestClass]
    public class ContiniousEffectShould
    {

            [TestMethod]
            public void ReturnSuccessMessageIfItIsDay()
            {
                //Arrange
                var graveyard = new Graveyard();
                graveyard.IsDay = true;
                //Setup
                var hotMock = new Mock<IEffect>();
                var heroMock = new Mock<IHero>();
                var appliedEffects = new List<IEffect>();

                appliedEffects.Add(hotMock.Object);

                hotMock.SetupGet(hot => hot.Type).Returns(EffectType.HOT);

                heroMock
                .SetupGet(hero => hero.AppliedEffects)
                .Returns(appliedEffects);

                heroMock
                    .SetupGet(hero => hero.Name)
                    .Returns("Gosho");
                
                string result = graveyard.ContinuousEffect(heroMock.Object);

                Assert.AreEqual("Gosho's duration of all applied HOT effects decreased by 1", result);
            }

        [TestMethod]
        public void ReturnSuccessMessageIfItIsNight()
        {
            //Arrange
            var graveyard = new Graveyard();
            graveyard.IsDay = false;
            //Setup
            var dotMock = new Mock<IEffect>();
            var heroMock = new Mock<IHero>();
            var appliedEffects = new List<IEffect>();

            appliedEffects.Add(dotMock.Object);

            dotMock.SetupGet(dot => dot.Type).Returns(EffectType.DOT);

            heroMock
            .SetupGet(hero => hero.AppliedEffects)
            .Returns(appliedEffects);

            heroMock
                .SetupGet(hero => hero.Name)
                .Returns("Gosho");

            string result = graveyard.ContinuousEffect(heroMock.Object);

            Assert.AreEqual("Gosho's duration of all applied DOT effects increased by 1", result);
        }

        //[TestMethod]
        //public void IncreaseDotEffectStacksIfItIsNight()
        //{
           
        //    var graveyard = new Graveyard();
        //    var dot = new Dot("Burn", 2, HeroClass.Mage, EffectType.DOT, 2, 9);
        //    dot.CurrentStacks = 2;
        //    var heroMock = new Mock<IHero>();
        //    var appliedEffects = new List<IEffect>();

        //    appliedEffects.Add(dot);

        //    heroMock
        //    .SetupGet(hero => hero.AppliedEffects)
        //    .Returns(appliedEffects);

        //    var result = graveyard.ContinuousEffect(heroMock.Object);

        //    Assert.AreEqual(3, dot.CurrentStacks);
        //}
    }
}
