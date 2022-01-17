using System;
using System.Collections.Generic;

namespace Kostky
{
    public class Dices
    {
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

        public void ThrowAllDices()
        {
            Console.WriteLine("Házím kostky...");
            for (int i = 0; i < 6; i++) 
            {
                diceList[i] = GetNumber();
            }
        }

        public void ThrowSomeDices()
        {
            BetterText.CyanText("Házím kostky...");
            for (int i = 0; i < 6; i++)
            {
                diceList[i] = GetNumber();
            }
        }

        public void ShowDiceValues()
        {
            foreach (int dice in diceList)
            {
                Console.Write($"{dice} ");
            }
            Console.WriteLine();
        }

        private int GetNumber()
        {
            return rint.Next(1, 7);
        }

    }
}
