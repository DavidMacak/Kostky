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
        bool additionalThrow = false;

        public GameLogic()
        {
            CreateCanDiceBeThrownList();
            CreateHowManyNumsThrown();
        }

        public void StartRound(Player player, int maxRounds)
        {
            roundsLeft = maxRounds;
            roundScore = 0;
            additionalThrow = false;

            ResetCanDiceBeThrownList();

            Console.WriteLine();
            BetterText.GreenText($"Právě hraje {player.Name} se skórem {player.Score} bodů.");

            while(roundsLeft > 0)
            {
                roundsLeft--;
                dices.ThrowDices(canDiceBeThrown);
                dices.ShowDiceValues(canDiceBeThrown); // ukáže jenom nově hozené kostky (canDiceBeThrown[dice] = true)
                CheckDices();
            }

            if (roundScore >= 350 && roundsLeft == 0)
            {
                player.Score += roundScore;
            }
        }


//--------------------------------------------------------------------------------

        public void CheckDices()
        {
            HowManyNumsThrown();
            

            // Nejvyšší možné hody pokud dohazuji
            if (SixDiceThrow() || additionalThrow)
            {
                if (Straight())
                    return;
                if (SixOfAKind())
                    return;
            }

            // Nejvyšší možné hody
            if (SixDiceThrow())
            {
                if (Straight())
                    return;
                if (SixOfAKind())
                    return;
                if (ThreePairs())
                    return;
                if (TwoTriples())
                    return;
                if (FourOfAKindPair())
                    return;
            }

            // Střední hody
            if (FiveOfAKind())
                return;
            if (FourOfAKind())
                return;
            if (Triples())
                return;
            if (Ones())
                return;
            if (Fives())
                return;

            // Nejmenší hody
            if (One())
                return;
            if (Five())
                return;

            // Smůla
            roundsLeft = 0;
            roundScore = 0;

        }

//--------------------------------------------------------------------------------

        private bool Straight()
        {
            int straight = 0;

            for (int i = 0; i < 6; i++)
            {
                if (howManyNumsThrown[i] == 1)
                {
                    straight++;
                }
            }

            if (straight == 6)
            {
                BetterText.MagentaText("Postupka! +1500");
                roundScore += 1500;
                ResetRounds();
                return true;
            }

            return false;
        }

        private bool SixOfAKind()
        {
            for (int i = 0; i < 6; i++)
            {
                if (howManyNumsThrown[i] == 6)
                {
                    BetterText.MagentaText("6 stejných! +3000");
                    roundScore += 3000;
                    ResetRounds();
                    return true;
                }
            }

            return false;
        }

        private bool ThreePairs()
        {
            int pairs = 0;

            for (int i = 0; i < 6; i++)
            {
                if(howManyNumsThrown[i] == 2)
                {
                    pairs++;
                }
            }

            if (pairs == 3)
            {
                BetterText.MagentaText("3 páry! +1500");
                roundScore += 1500;
                ResetRounds();
                return true;
            }

            return false;
        }

        private bool TwoTriples ()
        {
            int triples = 0;

            for (int i = 0; i < 6; i++)
            {
                if (howManyNumsThrown[i] == 3)
                {
                    triples++;
                }
            }

            if (triples == 3)
            {
                BetterText.MagentaText("2 trojice! +2500");
                roundScore += 2500;
                ResetRounds();
                return true;
            }

            return false;
        }

        private bool FourOfAKindPair()
        {

            for(int i = 0;i < 6; i++)
            {
                if (howManyNumsThrown[i] == 4)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (howManyNumsThrown[j] == 2 && j != i)
                        {
                            BetterText.MagentaText("4 stejné a pár! +1500");
                            roundScore += 1500;
                            ResetRounds();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //--------------------------------------------------------------------------------

        private bool FiveOfAKind()
        {
            for(int i =0;i < 6; i++)
            {
                if(howManyNumsThrown[i] == 5)
                {
                    BetterText.MagentaText("5 stejných! +2000");
                    BetterText.MagentaText("Chceš dohodit? Ano - zmáčkní enter, Ne - něco napiš a zmáčkni enter");

                    if (!GetInput.IsEmpty())
                    {
                        roundScore += 2000;
                        EndRound();
                        return true;
                    }
                    else
                    {
                        // zjistím kterou kostku můžu hodit
                        for (int j = 0;j < 6; j++)
                        {
                            if(dices.diceList[j] == i)
                            {
                                canDiceBeThrown[j] = false;
                            }
                        }

                        additionalThrow = true;
                        
                    }

                    
                }
            }
            return false;
        }

        private bool FourOfAKind()
        {
            return false;
        }

        private bool Triples()
        {
            return false;
        }

        private bool Ones()
        {
            return false;
        }

        private bool Fives()
        {
            return false;
        }

        private bool One()
        {
            return false;
        }
        private bool Five()
        {
            return false;
        }

        //--------------------------------------------------------------------------------

        /// <summary>
        /// Zjisti jestli se hazelo všemi 6 kostkami
        /// </summary>
        /// <returns></returns>
        private bool SixDiceThrow()
        {
            for (int i = 0; i < 6; i++)
            {
                if (canDiceBeThrown[i] == false)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Kolikrát padlo určité číslo
        /// </summary>
        private void HowManyNumsThrown()
        {
            for (int i = 0; i < 6; i++)
            {
                howManyNumsThrown[i] = dices.diceList.FindAll(value => value.Equals(i + 1)).Count;
            }

        }

        /// <summary>
        /// Můžeme hodit všechny kostky a máme opět 3 kola
        /// </summary>
        private void ResetRounds()
        {
            additionalThrow = false;
            roundsLeft = 3;
            ResetCanDiceBeThrownList();
        }

        private void EndRound()
        {
            roundsLeft = 0;
        }

        private void ResetCanDiceBeThrownList()
        {
            for(int i = 0; i < 6; i++)
            {
                canDiceBeThrown[i] = true;
            }
        }

        //--------------------------------------------------------------------------------

        private void CreateHowManyNumsThrown()
        {
            for (int i = 0; i < 6; i++)
            {
                howManyNumsThrown.Add(0);
            }
        }

        private void CreateCanDiceBeThrownList()
        {
            for (int i = 0; i < 6; i++)
            {
                canDiceBeThrown.Add(true);
            }
        }

        //--------------------------------------------------------------------------------

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
            if (indexNumber == 0 || indexNumber == 4 && count == 2)
            {
                return true;
            }
            return false;
        }
    }
}

//int triplets = 0;
//int pairs = 0;
//int postupka = 6;

//bool ones = false;
//bool fives = false;
//bool ignoreOnes = false;
//bool ignoreFives = false;
//bool ignoreLastCheck = false;

////List s počtem kolikrát se hodilo nějaké číslo
//for (int i = 0; i < 6; i++)
//{
//    howManyNumsThrown[i] = dices.diceList.FindAll(value => value.Equals(i + 1)).Count;
//}

//for (int i = 0; i < 6; i++)
//{
//    if (howManyNumsThrown[i] == 6 && canDiceBeThrown[i])
//    {
//        BetterText.MagentaText("6 stejných! +3000");
//        roundScore += 3000;
//        roundsLeft = 3;
//        ResetCanDiceBeThrownList();
//        return;
//    }

//    if (howManyNumsThrown[i] == 5 && canDiceBeThrown[i])
//    {
//        BetterText.MagentaText("5 stejných! +2000");
//        roundScore += 2000;
//        IgnoreOnesFives(ignoreOnes, ignoreFives, i);
//        ignoreLastCheck = true;
//        //TODO bud hodí jednu kostku a bude 6 stejných nebo ukončí kolo a hraje další hráč


//        BetterText.CyanText("Dohodit 1 kostku nebo ukončit kolo?");
//        if (GetInput.IsEmpty())
//        {
//            for (int j = 0; j < 6; j++)
//            {
//                if (dices.diceList[j] == i)
//                {
//                    canDiceBeThrown[j] = false;
//                }
//            }
//            break;
//        }
//        else
//        {
//            return;
//        }
//    }

//    if (howManyNumsThrown[i] == 4)
//    {
//        int canContinue = 0;

//        for (int j = 0; j < 6; j++)
//        {
//            if (canDiceBeThrown[j] && dices.diceList[j] == i)
//            {
//                canContinue++;
//            }
//        }

//        if (canContinue == 4)
//        {
//            BetterText.MagentaText("4 stejné! +1000");
//            roundScore += 1000;
//            IgnoreOnesFives(ignoreOnes, ignoreFives, i);
//            ignoreLastCheck = true;

//            BetterText.CyanText("Dohodit 2 kostky nebo ukončit kolo?");
//            if (GetInput.IsEmpty())
//            {
//                for (int j = 0; j < 6; j++)
//                {
//                    if (dices.diceList[j] == i)
//                    {
//                        canDiceBeThrown[j] = false;
//                    }
//                }
//                break;
//            }
//            else
//            {
//                return;
//            }
//        }


//    }

//    if (howManyNumsThrown[i] == 3 && canDiceBeThrown[i])
//    {
//        triplets++;
//        if (i == 0)
//        {
//            BetterText.MagentaText("3 stejné jedničky! +1000");
//            ignoreOnes = true;
//            roundScore += 1000;
//            ones = true;

//            for (int j = 0; j < 6; j++)
//            {
//                if (dices.diceList[j] == 1)
//                {
//                    canDiceBeThrown[j] = false;
//                }
//            }

//            //TODO hodí 3 kostky nebo konec
//        }
//        else
//        {
//            int score = (i + 1) * 100;
//            BetterText.MagentaText("3 stejné! +" + score);
//            if (i == 4)
//            {
//                fives = true;
//                ignoreFives = true;
//            }
//            roundScore += score;
//            ignoreLastCheck = true;
//            //TODO hodí 3 kostky nebo konec
//        }

//        if (triplets == 2)
//        {
//            BetterText.MagentaText("Dvě trojice!");
//            roundsLeft = 3;
//            return;

//        }
//    }

//    if (howManyNumsThrown[i] == 2 && canDiceBeThrown[i])
//    {
//        pairs++;

//        if (pairs == 3)
//        {
//            BetterText.MagentaText("Dvojičky! +1500");
//            roundScore += 1500;
//            roundsLeft = 3;
//            return;

//        }
//    }

//    if (howManyNumsThrown[i] == 1 && canDiceBeThrown[i])
//    {
//        postupka -= 1;

//        if (postupka == 0)
//        {
//            BetterText.MagentaText("Postupka! +2500");
//            roundScore += 2500;
//            roundsLeft = 3;
//            ignoreOnes = true;
//            ignoreFives = true;
//            return;
//        }
//    }
//}

//// Zjistí jestli byla hozena jednička
//if (howManyNumsThrown[0] > 0 && howManyNumsThrown[0] <= 2 && !ignoreOnes)
//{


//    for (int i = 0; i < 6; i++)
//    {
//        if (dices.diceList[i] == 1 && canDiceBeThrown[i])
//        {
//            ones = true;
//            int score = howManyNumsThrown[0] * 100;
//            BetterText.MagentaText("Jednička! +" + score);
//            roundScore += score;
//            canDiceBeThrown[i] = false;
//        }
//    }
//}

//// Zjistí jestli byla hozena pětka
//if (howManyNumsThrown[4] > 0 && howManyNumsThrown[4] <= 2 && !ignoreFives)
//{

//    for (int i = 0; i < 6; i++)
//    {
//        if (dices.diceList[i] == 5 && canDiceBeThrown[i])
//        {
//            fives = true;
//            int score = howManyNumsThrown[4] * 50;
//            BetterText.MagentaText("Pětka! +" + score);
//            roundScore += score;
//            canDiceBeThrown[i] = false;

//        }
//    }
//}

//// Pokud nebylo hozeno vůbec nic tak konec kola
//if (!ones && !fives && !ignoreLastCheck)
//{
//    roundScore = 0;
//    roundsLeft = 0;
//}
