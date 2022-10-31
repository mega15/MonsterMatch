using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PlayersByGame
    {
        public Game Game { get; set; }
        public Guid GameId { get; set; }
        public Player Player { get; set; }
        public Guid PlayerId { get; set; }
        public Character? Character { get; set; }
        public int Money { get; set; }
        public int Points { get; set; }
        [NotMapped]
        public int PointsPosition { get; set; }
        [NotMapped]
        public int MoneyPosition { get; set; }
    }
}
