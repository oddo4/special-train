using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightBehaviour.Classes
{
    class Character
    {
        public string Name;
        public CharacterStats Stats;
        private Interfaces.IAttackBehavior _attackBehavior = new Behaviors.BasicAttackBehavior();

        public Interfaces.IAttackBehavior AttackBehavior
        {
            get { return _attackBehavior; }
            set { _attackBehavior = value; }
        }

        public void AttackCommand(Character attacker, Character target)
        {
            AttackBehavior.Attack(attacker, target);
        }

    }
}
