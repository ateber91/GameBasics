using System;
using System.Collections.Generic;
using Team8Project.Contracts;
using Team8Project.Models;
using Team8Project.Models.Magic;
using Team8Project.Core;
namespace Team8Project
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            GameEngine.Instance.Run();
        }
    }
}