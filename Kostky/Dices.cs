using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kostky
{
    public class Dices
    {
        public class Dice
        {
            public int Number { get; set; }
        }

        Random rint = new Random();
        public List<Dice> diceList = new List<Dice>();

        public Dices()
        {
            CreateDices();
        }

        private void CreateDices()
        {
            for (int i = 0; i < 6; i++)
            {
                diceList.Add(new Dice());
            }
        }

        /// <summary>
        /// Throws all dices
        /// </summary>
        public void ThrowAllDices()
        {
            Console.WriteLine("Házím kostky...");
            foreach(Dice dice in diceList)
            {
                dice.Number = GetNumber();
            }
        }

        /// <summary>
        /// Throws x dices
        /// </summary>
        public void ThrowSomeDices()
        {
            Console.WriteLine("Házím kostky...");
            foreach (Dice dice in diceList)
            {
                dice.Number = GetNumber();
            }
        }

        /// <summary>
        /// Throw specific dice
        /// </summary>
        /// <param name="diceOrder"></param>
        public void ThrowDice(int diceOrder)
        {
            diceList[diceOrder].Number = GetNumber();
        }

        public void ShowDiceValues()
        {
            int i = 1;
            foreach (Dice dice in diceList)
            {
                Console.WriteLine($"{i++}. má hodnotu: {dice.Number}");
            }
        }

        private int GetNumber()
        {
            return rint.Next(1, 7);
        }

    }
}
