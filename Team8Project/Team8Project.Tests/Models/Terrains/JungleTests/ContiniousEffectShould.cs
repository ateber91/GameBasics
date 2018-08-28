using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Team8Project.Contracts;
using Team8Project.Models.Terrains;

namespace Team8Project.Tests.Models.Terrains.JungleTests
{
    [TestClass]
    public class ContiniousEffectShould
    {
        [TestMethod]
        public void ReturnSuccessMessageIfItIsDay()
        {
            //Arrange
            var jungle = new Jungle();
            jungle.IsDay = true;
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

            string result = jungle.ContinuousEffect(heroMock.Object);

            Assert.AreEqual("Gosho's damaging abilities power increased by 5", result);
        }

        [TestMethod]
        public void ReturnSuccessMessageIfItIsNight()
        {
            //Arrange
            var jungle = new Jungle();
            jungle.IsDay = false;
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

            string result = jungle.ContinuousEffect(heroMock.Object);

            Assert.AreEqual("Gosho's damaging abilities power decreased by 2", result);
        }
    }
}
