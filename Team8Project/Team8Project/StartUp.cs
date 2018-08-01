using System;
using System.Collections.Generic;
using Team8Project.Contracts;
using Team8Project.Models;
using Team8Project.Models.Magic;
using Team8Project.Providers;

namespace Team8Project
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //TODO:MOVE TO ENGINE/FACTORY

            //factory
            //create heroes
            Hero hero1 = new Hero("Pesho", 400, 10, 20);
            Hero hero2 = new Hero("Gesho", 500, 15, 25);
            hero1.Opponent = hero2;
            hero2.Opponent = hero1;

            var heroes = new List<Hero>() { hero1, hero2 };

            //create spells
            DamagingAbility fireball = new DamagingAbility("fireball", 2, 50);
            DamagingAbility sinisterStrike = new DamagingAbility("sinister strike", 2, 40);

            //todo: method for adding abilities depending on a class
            hero1.AddAbility(fireball);
            fireball.Caster = hero1;

            hero2.AddAbility(sinisterStrike);
            sinisterStrike.Caster = hero2;


            //factory Turns
            Turn turn = new Turn();

            SetWhoIsFirst(turn, hero1);
            Hero activeHero = heroes.Find(h => h.HasTurn == true);

            //Engine
            //StartGame
            while (true)
            {
                activeHero = heroes.Find(h => h.HasTurn == true);

                Console.WriteLine($" Turn: {turn.NumberOfTurn}. {activeHero.Name} is active");

                //cast
                activeHero.UseAbility(activeHero.Abilities[0]);
                Console.WriteLine($"{activeHero.Name} uses {activeHero.Abilities[0].Name} and {activeHero.Abilities[0].ToString()}. {activeHero.Opponent.Name} is left with {activeHero.Opponent.HealthPoints} HP");

                if (activeHero.Opponent.HealthPoints < 0)
                {
                    Console.WriteLine($"{activeHero.Name.ToUpper()} WON! ");
                    break;
                }

                SwapTurnHolder(activeHero);

                turn = new Turn();
            }
        }
        public static void SetWhoIsFirst(Turn turn, IHero hero1)
        {

            var res = RandomProvider.Generate(1, 3);

            if (res == 1)
            {
                turn.ActiveHero = hero1;
                hero1.HasTurn = true;
                hero1.Opponent.HasTurn = false;
            }
            else
            {
                turn.ActiveHero = hero1.Opponent;
                hero1.Opponent.HasTurn = true;
                hero1.HasTurn = false;
            }

        }

        private static void SwapTurnHolder(IHero active)
        {
            active.HasTurn = false;
            active.Opponent.HasTurn = true;
        }
    }
}