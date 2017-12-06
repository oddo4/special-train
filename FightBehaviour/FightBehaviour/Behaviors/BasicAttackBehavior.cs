using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightBehaviour.Behaviors
{
    class BasicAttackBehavior : Interfaces.IAttackBehavior
    {
        public void Attack(Classes.Character attacker, Classes.Character target)
        {
            target.Lives -= attacker.AttackStrenght * 1;
        }
    }
}
