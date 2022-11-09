using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class Robot : Fighter
    {
        public Robot(Character character, Weapon weapon)
        {
            Id = character.Id;
            Name = character.Name;
            Attack = character.Attack;
            Life = character.Life;
            ElementType = character.ElementType;
            CharacterType = character.CharacterType;
            Weapon = weapon;
        }
        public override double DoAttack()
        {
            return (Attack * 1.1) + Weapon.Attack;
        }
    }
}
