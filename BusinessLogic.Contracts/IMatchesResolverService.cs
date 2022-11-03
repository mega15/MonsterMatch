using BusinessLogic.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
{
    public interface IMatchesResolverService
    {
        Fighter ResolveMatch(Fighter fighter1, Fighter fighter2);
    }
}
