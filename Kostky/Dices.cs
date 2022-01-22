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

        public void ThrowDices(List<bool> canDiceBeThrown)
        {
            Console.WriteLine("Házím kostky...");
            for (int i = 0; i < 6; i++) 
            {
                if (canDiceBeThrown[i])
                {
                    diceList[i] = GetNumber();
                }
            }
        }

        public void ShowDiceValues(List<bool> canDiceBeThrown)
        {
            for (int i = 0; i < 6; i++)
            {
                if (canDiceBeThrown[i])
                {
                    Console.Write($"{diceList[i]} ");
                }
            }
            Console.WriteLine();
        }

        private int GetNumber()
        {
            return rint.Next(1, 7);
        }

    }
}
