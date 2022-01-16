using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kostky
{
    class Game
    {

        private List<Player> players = new List<Player>();
        GameLogic gl = new GameLogic();
        private int playersCount;
        private int maximumScore;
        private int maxRounds = 3;
        private bool nextRound = true;
        private int winnerIndex;
        private int highestScore;


        public void Start()
        {
            AddPlayers();
            SetMaximumScore();
            GameLoop();

        }

        private void GameLoop()
        {
            BetterText.CyanText("Hra začíná!");
            while (nextRound)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    gl.StartRound(players[i], maxRounds);

                    if(players[i].Score > highestScore)
                    {
                        highestScore = players[i].Score;
                        winnerIndex = i;
                    }
                    
                }
                if(highestScore >= maximumScore)
                {
                    nextRound = false;
                    break;

                }

            }

            BetterText.GreenText($"Vítězem je {players[winnerIndex].Name} s {players[winnerIndex].Score} skóre! Gratuluji!");
        }

        private void SetMaximumScore()
        {
            Console.WriteLine("Nastav maximální skóre:");
            maximumScore = GetInput.IntType();
        }

        private void AddPlayers()
        {
            playersCount = HowManyPlayers();

            for (int i = 0; i < playersCount; i++)
            {
                Player player = new Player();
                player.Name = NamingPlayer(i);

                players.Add(player);
            }
        }

        private int HowManyPlayers()
        {
            int input;
            Console.WriteLine("Kolik hráčů bude hrát? ");
            input = GetInput.IntType(2);

            return input;
        }

        private string NamingPlayer(int playerNum)
        {
            Console.WriteLine($"Jméno pro hráče číslo {playerNum + 1}:");
            string name; 
            name = Console.ReadLine();
            return name;
        }

        public void GetAllPlayers()
        {
            foreach (Player player in players)
            {
                Console.WriteLine($"{player.Name} has {player.Score} score");
            }
        }


    }
}
