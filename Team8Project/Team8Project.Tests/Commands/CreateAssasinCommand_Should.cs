using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core;
using Team8Project.Core.Commands.CreateHero;
using Team8Project.Data;

namespace Team8Project.Tests.Commands
{
    [TestClass]
    public class CreateAssasinCommand_Should
    {
        [TestMethod]
        public void AddToList_WhenValidaInput()
        {
            var factoryMock = new Mock<IFactory>();
            var dataMock = new Mock<IDataContainer>();
            var hero = new Mock<IHero>();
            
            var predefinedList = new List<IHero>() { };

            dataMock
                .SetupGet(data => data.ListHeros)
                .Returns(predefinedList);

            var command = new CreateAssasinCommand(factoryMock.Object, dataMock.Object);
            
            command.Execute();
            
            Assert.IsTrue(dataMock.Object.ListHeros.Count >= 1);
        }

        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //[TestMethod]
        //public void ThrowException_WhenInvalidParamsPassed()
        //{
        //    // Arrange
        //    var factoryMock = new Mock<IFactory>();
        //    var dataMock = new Mock<IDataContainer>();
        //    var hero = new Mock<IHero>();

        //    hero.SetupGet(t => t.HealthPoints).Returns(1000000);

        //    factoryMock.Setup(t => t.CreateAssasin("", HeroClass.Assasin, 0, 0, 0)).Returns(hero.Object);

        //    dataMock
        //        .SetupGet(data => data.ListHeros)
        //        .Returns(new List<IHero>());

        //    var command = new CreateAssasinCommand(factoryMock.Object, dataMock.Object);

        //    // Act
        //    command.Execute();

        //}
    }
}
