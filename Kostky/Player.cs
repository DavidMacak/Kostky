using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kostky
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        private bool isWinner;

        public bool IsWinner
        {
            get { return isWinner; }
            set { isWinner = false; }
        }


    }
}
