using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightBehaviour.Classes
{
    class Player : Character
    {
        public Player(string name)
        {
            this.Name = name;
            this.Stats = new CharacterStats(200, 50, 15, 30, 20, 10, 7, 0, 0, 0, 0);
        }
    }
}
