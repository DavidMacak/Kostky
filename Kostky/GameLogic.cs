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
        private int roundsLeft = 0;

        public GameLogic()
        {
            CreateCanDiceBeThrownList();
            CreateHowManyNumsThrown();
        }


        public void StartRound(Player player, int maxRounds)
        {
            roundsLeft = maxRounds;
            Console.WriteLine();
            BetterText.GreenText($"Právě hraje {player.Name} se skórem {player.Score} bodů.");

            while(roundsLeft > 0)
            {
                //Console.WriteLine("Zbývá kol: " + roundsLeft);
                dices.ThrowAllDices();
                dices.ShowDiceValues();
                CheckDices(player);
            }

        }

        public void CheckDices(Player player)
        {
            roundsLeft--;
            int triplets = 0;
            int pairs = 0;
            int postupka = 6;

            //List s počtem kolikrát se hodilo nějaké číslo
            for (int i = 0; i<6; i++)
            {
                howManyNumsThrown[i] = dices.diceList.FindAll(value => value.Equals(i + 1)).Count;
            }

            //// DEBUG
            //for (int i = 0; i < 6; i++)
            //{
            //    BetterText.CyanText($"Číslo {i + 1} padlo {howManyNumsThrown[i]}x");
            //}

            for (int i = 0; i < 6; i++)
            {
                if(howManyNumsThrown[i] == 6)
                {
                    BetterText.MagentaText("6 stejných! +3000");
                    player.Score += 3000;
                    roundsLeft = 3;
                    break;
                }

                if (howManyNumsThrown[i] == 5)
                {
                    BetterText.MagentaText("5 stejných! +2000");
                    player.Score += 2000;
                    break;
                }

                if (howManyNumsThrown[i] == 4)
                {
                    BetterText.MagentaText("4 stejné! +1000");
                    player.Score += 1000;
                    break;
                }

                if (howManyNumsThrown[i] == 3)
                {
                    triplets++;
                    if(i == 0)
                    {
                        BetterText.MagentaText("3 stejné jedničky! +1000");
                        player.Score += 1000;
                    }
                    else
                    {
                        int score = (i + 1) * 100;
                        BetterText.MagentaText("3 stejné! +" + score);
                        player.Score += score;
                    }

                    if(triplets == 2)
                    {

                        break;

                    }
                }

                if (howManyNumsThrown[i] == 2)
                {
                    pairs++;

                    if (pairs == 3)
                    {
                        BetterText.MagentaText("Dvojičky! +1500");
                        player.Score += 1500;
                        break;

                    }
                }

                if(howManyNumsThrown[i] == 1)
                {
                    postupka -= 1;

                    if(postupka == 0)
                    {
                        BetterText.MagentaText("Postupka! +2500");
                        player.Score += 2500;
                        break;
                    }

                }
                
            }

            if (howManyNumsThrown[0] > 0 && howManyNumsThrown[0] <= 2)
            {
                int score = howManyNumsThrown[0] * 100;
                BetterText.MagentaText("Jednička! +" + score);
                player.Score += score;
            }
            else if (howManyNumsThrown[4] > 0 && howManyNumsThrown[4] <= 2)
            {
                int score = howManyNumsThrown[4] * 50;
                BetterText.MagentaText("Pětka! +" + score);
                player.Score += score;
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
