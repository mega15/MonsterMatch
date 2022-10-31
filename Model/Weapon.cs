using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Weapon
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Character Character { get; set; }
        public int Attack { get; set; }
        public ElementType ElementType { get; set; }
    }
}
