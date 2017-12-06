using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightBehaviour.Interfaces;
using FightBehaviour.Behaviors;

namespace FightBehaviour.Classes
{
    class Character
    {
        public string Name;
        public CharacterStats Stats;
        private IAttackBehavior _attackBehavior = new BasicAttackBehavior();

        public IAttackBehavior AttackBehavior
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
