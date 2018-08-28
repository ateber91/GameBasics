using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Team8Project.Contracts;
using Team8Project.Core;
using Team8Project.Core.Commands.SelectAbility;
using Team8Project.Core.Contracts;
using Team8Project.Data;

namespace Team8Project.Tests.Commands.SelectAbilityCommandsTests
{
    [TestClass]
    public class SelectBasicAbilityCommand_Should
    {
        [TestMethod]
        public void SaveInData_ActiveHeroFirstAbility_WhenExecuted()
        {
            //Arrange
            var data = new DataContainer();
            var factoryMock = new Mock<IFactory>();
            var turnMock = new Mock<ITurnProcessor>();

            var heroMock = new Mock<IHero>();
            var newAbilityMock = new Mock<IAbility>();

            var listAbilities = new List<IAbility>() { newAbilityMock.Object };
            heroMock.Setup(x => x.Abilities).Returns(listAbilities);
            turnMock.Setup(x => x.ActiveHero).Returns(heroMock.Object);
            var command = new SelectBasicAbilityCommand(factoryMock.Object, data, turnMock.Object);
            //Act

            command.Execute();
            //Assert
            Assert.AreEqual(newAbilityMock.Object, data.SelectedAbility);
        }

        [TestMethod]
        public void SaveInData_ActiveHeroSecondAbility_WhenExecuted()
        {
            //Arrange
            var data = new DataContainer();
            var factoryMock = new Mock<IFactory>();
            var turnMock = new Mock<ITurnProcessor>();

            var heroMock = new Mock<IHero>();
            var firstAbilityMock = new Mock<IAbility>();
            var secondAbilityMock = new Mock<IAbility>();

            var listAbilities = new List<IAbility>() { firstAbilityMock.Object, secondAbilityMock.Object };
            heroMock.Setup(x => x.Abilities).Returns(listAbilities);
            turnMock.Setup(x => x.ActiveHero).Returns(heroMock.Object);
            var command = new SelectDamageAbilityCommand(factoryMock.Object, data, turnMock.Object);
            //Act

            command.Execute();
            //Assert
            Assert.AreEqual(secondAbilityMock.Object, data.SelectedAbility);
        }


        [TestMethod]
        public void SaveInData_ActiveHeroThirdAbility_WhenExecuted()
        {
            //Arrange
            var data = new DataContainer();
            var factoryMock = new Mock<IFactory>();
            var turnMock = new Mock<ITurnProcessor>();

            var heroMock = new Mock<IHero>();
            var firstAbilityMock = new Mock<IAbility>();
            var secondAbilityMock = new Mock<IAbility>();
            var thirdAbilityMock = new Mock<IAbility>();

            var listAbilities = new List<IAbility>() {
                firstAbilityMock.Object,
                secondAbilityMock.Object,
                thirdAbilityMock.Object };

            heroMock.Setup(x => x.Abilities).Returns(listAbilities);
            turnMock.Setup(x => x.ActiveHero).Returns(heroMock.Object);
            var command = new SelectEffectAbilityCommand(factoryMock.Object, data, turnMock.Object);
            //Act

            command.Execute();
            //Assert
            Assert.AreEqual(thirdAbilityMock.Object, data.SelectedAbility);
        }
    }
}
