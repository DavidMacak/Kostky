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
        private List<int> howManyNumsThrown = new List<int>();

        public GameLogic()
        {
            CreateCanDiceBeThrownList();
            CreateHowManyNumsThrown();
        }


        public void StartRound(Player player, int roundsLeft)
        {
            BetterText.GreenText($"Právě hraje {player.Name} se skórem {player.Score} bodů.");
            dices.ThrowAllDices();
            dices.ShowDiceValues();
            CheckDices();

        }

        public void CheckDices()
        {

            for(int i = 0; i<6; i++)
            {
                howManyNumsThrown[i] = dices.diceList.FindAll(value => value.Equals(i + 1)).Count;
            }


            // DEBUG
            for (int i = 0; i < 6; i++)
            {
               BetterText.CyanText($"Číslo {i+1} padlo {howManyNumsThrown[i]}x");
            }

        }        
        private void CreateHowManyNumsThrown()
        {
            for (int i = 0; i < 6; i++)
            {
                howManyNumsThrown.Add(0);
            }
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
