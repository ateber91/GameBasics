using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Models.Magic.EffectAbilities;

namespace Team8Project.Tests.Models.AbilitiyTests
{
    [TestClass]
    public class EffectAbility_Should
    {
        [TestMethod]
        public void ApplyEffectOnTarget_WhenAbilityIsUsenOnOpponent()
        {
            //Arrange 
            var casterMock = new Mock<IHero>();
            var buff = new Buff("Critical", 2, HeroClass.Assasin, EffectType.Buff, 1, 20);
            buff.Caster = casterMock.Object;
            var casterAppliedEffects = new List<IEffect>();
            casterMock.Setup(x => x.AppliedEffects).Returns(casterAppliedEffects);
            //Act
            buff.Apply();
            //Assert
            Assert.AreEqual(buff, casterMock.Object.AppliedEffects.FirstOrDefault());
        }

        [TestMethod]
        public void ApplyEffectOnTarget_WhenAbilityIsUsedOnOpponent()
        {
            //Arrange 
            var casterMock = new Mock<IHero>();
            var opponentMock = new Mock<IHero>();
            var debuff = new Debuff("Curse", 2, HeroClass.Cleric, EffectType.Debuff, 2, 10);
            debuff.Caster = casterMock.Object;
            var opponentAppliedEffects = new List<IEffect>();
            opponentMock.Setup(x => x.AppliedEffects).Returns(opponentAppliedEffects);
            casterMock.Setup(x => x.Opponent).Returns(opponentMock.Object);
            //Act
            debuff.Apply();
            //Assert
            Assert.AreEqual(debuff, opponentMock.Object.AppliedEffects.FirstOrDefault());
        }
    }
}
