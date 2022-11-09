using BusinessLogic.Contracts;
using BusinessLogic.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class MatchesResolverService : IMatchesResolverService
    {
        public Fighter ResolveMatch(Fighter fighter1, Fighter fighter2)
        {
            Random random = new Random();

            Fighter attacker = fighter1;
            Fighter defender = fighter2;

            do
            {
                double attack = attacker.DoAttack();
                attack = attack * random.Next(0, 1);

                if (attack > 0)
                {
                    ElementComparer comparer = new ElementComparer();
                    double porcentage = comparer.Resolve(attacker.Weapon.ElementType, defender.Weapon.ElementType);
                    attack = (attack * porcentage) + attack;
                    defender.Life -= (int)Math.Round(attack);
                }

                if (attacker == fighter1)
                {
                    attacker = fighter2;
                    defender = fighter1;
                }
                else
                {                    
                    attacker = fighter1;
                    defender = fighter2;
                }
            } while (defender.Life > 0);

            return attacker;
        }
    }
}
