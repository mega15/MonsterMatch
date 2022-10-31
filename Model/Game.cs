using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public bool IsClosed { get; set; }
        public virtual ICollection<PlayersByGame>? Players { get; set; }
    }
}
