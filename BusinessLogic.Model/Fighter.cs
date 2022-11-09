using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public abstract class Fighter : Character
    {
        protected abstract double DoAttack();
        public Weapon Weapon { get; set; }
    }
}
