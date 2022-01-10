using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kostky
{
    public class GameLogic
    {
        Dices dices = new Dices();
        public List<bool> canDiceBeThrown = new List<bool>();

        private bool is1inList = false;
        private bool is2inList = false;
        private bool is3inList = false;
        private bool is4inList = false;
        private bool is5inList = false;
        private bool is6inList = false;


        public GameLogic()
        {
            CreateCanDiceBeThrownList();
        }


        public void StartRound(Player player)
        {
            Console.WriteLine($"Právě hraje {player.Name} se skórem {player.Score} bodů.");
            dices.ThrowAllDices();
            dices.ShowDiceValues();
            CheckDices();
        }

        public void CheckDices()
        {
            is1inList = dices.diceList.Contains(1);
            



        }        
        
        private void CreateCanDiceBeThrownList()
        {
            for(int i = 0; i < 6; i++)
            {
                canDiceBeThrown.Add(true);
            }
        }
    }
}
