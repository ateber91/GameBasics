using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Models.Characters;
using Team8Project.Models.Magic;

namespace Team8Project.Tests.Models.AbilitiyTests
{
    [TestClass]
    public class DamagingAbility_Should
    {
        [TestMethod]
        public void RemoveHealthFromOpponentHp_WhenApplied()
        {
            //Arrange 
            var casterMock= new Mock<IHero>();
            var opponentMock = new Mock<IHero>();
            var dmgAbility =
                new DamagingAbility("Blade Storm", 1, HeroClass.Warrior, EffectType.Damage, 18);

            casterMock.Setup(x => x.DmgStartOfRange).Returns(10);
            casterMock.Setup(x => x.DmgEndOfRange).Returns(20);

            opponentMock.Setup(x => x.HealthPoints).Returns(100);
            opponentMock.Setup(x => x.HasRessistance).Returns(false);

            var opponent =  new Warrior("Hero", HeroClass.Warrior, 200, 10, 15);

            casterMock.Setup(x => x.Opponent).Returns(opponent);

            dmgAbility.Caster = casterMock.Object;

            //Act
            dmgAbility.Apply();
            //Assert
            Assert.AreNotEqual(100, opponent.HealthPoints);
        }
    }
}
