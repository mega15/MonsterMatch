using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    internal class ElementComparer
    {
        public double Resolve(ElementType fighter1WeaponElement, ElementType fighter2WeaponElement)
        {
            Random random = new Random();
            switch (fighter1WeaponElement)
            {
                case ElementType.Air:
                    if (fighter2WeaponElement == ElementType.Air)
                        return 0;
                    else if (fighter2WeaponElement == ElementType.Electric)
                        return -.6;
                    else if (fighter2WeaponElement == ElementType.Fire)
                        return -.2;
                    else if (fighter2WeaponElement == ElementType.Water)
                        return .2;
                    else if (fighter2WeaponElement == ElementType.Earth)
                        return .6;
                    break;
                case ElementType.Fire:
                    if (fighter2WeaponElement == ElementType.Air)
                        return .2;
                    else if (fighter2WeaponElement == ElementType.Electric)
                        return - random.Next(0, 20) * .1;
                    else if (fighter2WeaponElement == ElementType.Fire)
                        return 0;
                    else if (fighter2WeaponElement == ElementType.Water)
                        return -.6;
                    else if (fighter2WeaponElement == ElementType.Earth)
                        return .6;
                    break;
                case ElementType.Electric:
                    if (fighter2WeaponElement == ElementType.Air)
                        return .6;
                    else if (fighter2WeaponElement == ElementType.Electric)
                        return 0;
                    else if (fighter2WeaponElement == ElementType.Fire)
                        return random.Next(0, 20) * .1;
                    else if (fighter2WeaponElement == ElementType.Water)
                        return random.Next(0, 20) * .1;
                    else if (fighter2WeaponElement == ElementType.Earth)
                        return -.6;
                    break;
                case ElementType.Earth:
                    if (fighter2WeaponElement == ElementType.Air)
                        return -.6;
                    else if (fighter2WeaponElement == ElementType.Electric)
                        return .6;
                    else if (fighter2WeaponElement == ElementType.Fire)
                        return .2;
                    else if (fighter2WeaponElement == ElementType.Water)
                        return -.2;
                    else if (fighter2WeaponElement == ElementType.Earth)
                        return 0;
                    break;
                case ElementType.Water:
                    if (fighter2WeaponElement == ElementType.Air)
                        return .2;
                    else if (fighter2WeaponElement == ElementType.Electric)
                        return -random.Next(0, 20) * .1;
                    else if (fighter2WeaponElement == ElementType.Fire)
                        return .6;
                    else if (fighter2WeaponElement == ElementType.Water)
                        return 0;
                    else if (fighter2WeaponElement == ElementType.Earth)
                        return -.6;
                    break;
                default:
                    return 0;
            }

            return 0;
        }
    }
}
