using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kostky
{
    public class Dices
    {
        //public class Dice
        //{
        //    public int Number { get; set; }
        //}

        Random rint = new Random();
        public List<int> diceList = new List<int>();

        public Dices()
        {
            CreateDices();
        }

        private void CreateDices()
        {
            for (int i = 0; i < 6; i++)
            {
                diceList.Add(1);
            }
        }

        /// <summary>
        /// Throws all dices
        /// </summary>
        public void ThrowAllDices()
        {
            Console.WriteLine("Házím kostky...");
            for (int i = 0; i < 6; i++) 
            {
                diceList[i] = GetNumber();
            }

            //foreach(int dice in diceList)
            //{
            //    dice = GetNumber();
            //}
        }

        /// <summary>
        /// Throws x dices
        /// </summary>
        public void ThrowSomeDices()
        {
            BetterText.CyanText("Házím kostky...");
            for (int i = 0; i < 6; i++)
            {
                diceList[i] = GetNumber();
            }
        }

        /// <summary>
        /// Throw specific dice
        /// </summary>
        /// <param name="diceOrder"></param>
        //public void ThrowDice(int diceOrder)
        //{
        //    diceList[diceOrder].Number = GetNumber();
        //}

        public void ShowDiceValues()
        {
            int i = 1;
            foreach (int dice in diceList)
            {
                Console.WriteLine($"{i++}. kostka má hodnotu: {dice}");
            }
        }

        private int GetNumber()
        {
            return rint.Next(1, 7);
        }

    }
}
