using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PlayerMatchBid
    {
        [Key]
        public Guid Id { get; set; }
        public Match Match { get; set; }
        public Character Character { get; set; }
        public int BidAmount { get; set; }
        public BidClass BidClass { get; set; }
    }
}
