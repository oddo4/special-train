using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightBehaviour.Behaviors;

namespace FightBehaviour.Classes
{
    class Monster : Character
    {
        public Monster(string name)
        {
            this.Name = name;
            this.Stats = new CharacterStats(200, 100, 10, 10, 5, 5, 10, 50, 0, 0, 0);
        }

        public void LivesCheck()
        {
            if (Stats.HP > 25)
            {
                AttackBehavior = new ScorpionAttackBehavior();
            }
        }
    }
}
