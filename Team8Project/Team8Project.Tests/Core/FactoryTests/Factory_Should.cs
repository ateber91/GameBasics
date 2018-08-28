using Microsoft.VisualStudio.TestTools.UnitTesting;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core;
using Team8Project.Models.Characters;

namespace Team8Project.Tests.Core.FactoryTests
{
    [TestClass]
    public class Factory_Should
    {
        [TestMethod]
        public void ReturnHero_WhenCreateWarriorIsCalled()
        {
            // Arrange
            var factory = new Factory() ;
            // Act
            var hero = factory.CreateWarrior("Hero", HeroClass.Warrior, 200, 10, 15);

            // Assert
            Assert.IsInstanceOfType(hero, typeof(IHero));
        }

        [TestMethod]
        public void ReturnHero_WhenCreateAssasinIsCalled()
        {
            // Arrange
            var factory = new Factory();
            // Act
            var hero = factory.CreateAssasin("Hero", HeroClass.Assasin, 200, 10, 15);

            // Assert
            Assert.IsInstanceOfType(hero, typeof(IHero));
        }

        [TestMethod]
        public void ReturnHero_WhenCreateMageIsCalled()
        {
            // Arrange
            var factory = new Factory();
            // Act
            var hero = factory.CreateMage("Hero", HeroClass.Mage, 200, 10, 15);

            // Assert
            Assert.IsInstanceOfType(hero, typeof(IHero));
        }
        [TestMethod]
        public void ReturnHero_WhenCreateClericIsCalled()
        {
            // Arrange
            var factory = new Factory();
            // Act
            var hero = factory.CreateCleric("Hero", HeroClass.Cleric, 200, 10, 15);

            // Assert
            Assert.IsInstanceOfType(hero, typeof(IHero));
        }
    }
}
