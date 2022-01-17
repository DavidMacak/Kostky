using System;
using System.Collections.Generic;

namespace Kostky
{
    public class GameLogic
    {

        Dices dices = new Dices();
        public List<bool> canDiceBeThrown = new List<bool>();
        private List<int> howManyNumsThrown = new List<int>();
        private int roundsLeft = 0;
        private int roundScore = 0;

        public GameLogic()
        {
            CreateCanDiceBeThrownList();
            CreateHowManyNumsThrown();
        }

        public void StartRound(Player player, int maxRounds)
        {
            roundsLeft = maxRounds;
            roundScore = 0;

            Console.WriteLine();
            BetterText.GreenText($"Právě hraje {player.Name} se skórem {player.Score} bodů.");

            while(roundsLeft > 0)
            {
                roundsLeft--;
                dices.ThrowAllDices();
                dices.ShowDiceValues();
                CheckDices();
            }

            if (roundScore >= 350 && roundsLeft == 0)
            {
                player.Score += roundScore;
            }
        }

        private void IgnoreOnesFives(bool ones, bool fives, int indexNumber)
        {
            if (indexNumber == 0)
            {
                ones = true;
            }
            else if (indexNumber == 4)
            {
                fives = true;
            }
        }
        private bool IgnoreOnesFives(int indexNumber, int count)
        {
            if (indexNumber == 0 || indexNumber ==4 && count == 2)
            {
               return true;
            }
            return false;
        }

        public void CheckDices()
        {
            int triplets = 0;
            int pairs = 0;
            int postupka = 6;

            bool ones = false;
            bool fives = false;
            bool ignoreOnes = false;
            bool ignoreFives = false;
            bool ignoreLastCheck = false;

            //List s počtem kolikrát se hodilo nějaké číslo
            for (int i = 0; i<6; i++)
            {
                howManyNumsThrown[i] = dices.diceList.FindAll(value => value.Equals(i + 1)).Count;
            }

            for (int i = 0; i < 6; i++)
            {
                if(howManyNumsThrown[i] == 6)
                {
                    BetterText.MagentaText("6 stejných! +3000");
                    roundScore += 3000;
                    roundsLeft = 3;
                    return;
                }

                if (howManyNumsThrown[i] == 5)
                {
                    BetterText.MagentaText("5 stejných! +2000");
                    roundScore += 2000;
                    IgnoreOnesFives(ignoreOnes, ignoreFives, i);
                    break;
                }

                if (howManyNumsThrown[i] == 4)
                {
                    BetterText.MagentaText("4 stejné! +1000");
                    roundScore += 1000;
                    IgnoreOnesFives(ignoreOnes, ignoreFives, i);
                    break;
                }

                if (howManyNumsThrown[i] == 3)
                {
                    triplets++;
                    if(i == 0)
                    {
                        BetterText.MagentaText("3 stejné jedničky! +1000");
                        ignoreOnes = true;
                        roundScore += 1000;
                        ones = true;
                    }
                    else
                    {
                        int score = (i + 1) * 100;
                        BetterText.MagentaText("3 stejné! +" + score);
                        if(i == 4)
                        {
                            fives = true;
                            ignoreFives = true;
                        }
                        roundScore += score;
                        ignoreLastCheck = true;
                    }

                    if (triplets == 2)
                    {
                        BetterText.MagentaText("Dvě trojice!");
                        roundsLeft = 3;
                        return;

                    }
                }

                if (howManyNumsThrown[i] == 2)
                {
                    pairs++;

                    if (pairs == 3)
                    {
                        BetterText.MagentaText("Dvojičky! +1500");
                        roundScore += 1500;
                        roundsLeft = 3;
                        return;

                    }
                }

                if(howManyNumsThrown[i] == 1)
                {
                    postupka -= 1;

                    if(postupka == 0)
                    {
                        BetterText.MagentaText("Postupka! +2500");
                        roundScore += 2500;
                        roundsLeft = 3;
                        ignoreOnes = true;
                        ignoreFives = true;
                        return;
                    }
                }
            }

            if (howManyNumsThrown[0] > 0 && howManyNumsThrown[0] <= 2 && !ignoreOnes)
            {
                ones = true;
                int score = howManyNumsThrown[0] * 100;
                BetterText.MagentaText("Jednička! +" + score);
                roundScore += score;
            }
            if (howManyNumsThrown[4] > 0 && howManyNumsThrown[4] <= 2 && !ignoreFives)
            {
                fives = true;
                int score = howManyNumsThrown[4] * 50;
                BetterText.MagentaText("Pětka! +" + score);
                roundScore += score;
            }
            if(!ones && !fives && !ignoreLastCheck)
            {
                roundsLeft = 0;
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
