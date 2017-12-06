using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightBehaviour.Classes
{
    class Monster : Character
    {
        public Monster(string name, int strenght, int defense)
        {
            this.Name = name;
            this.Stats = new CharacterStats(1000,100,20,10,5,5,10,50,0,0,0);
        }

        public void LivesCheck()
        {
            if (Stats.HP > 25)
            {
                AttackBehavior = new Behaviors.ScorpionAttackBehavior();
            }
        }
    }
}
