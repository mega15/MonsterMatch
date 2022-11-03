using BusinessLogic.Model;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLogic
{
    public static class FigtherFactory
    {
        public static Fighter Create(Character character, Weapon weapon)
        {
            Fighter fighter = null ;
            if (character.CharacterType == CharacterType.Monster)
                fighter = new Monster(character, weapon);
            else
                fighter = new Robot(character, weapon);

            return fighter;
        }
    }
}
