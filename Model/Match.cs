using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Match
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Game Game { get; set; }
        public Character? Character1 { get; set; }
        public Weapon? WeaponCharacter1 { get; set; }
        public Character? Character2 { get; set; }
        public Weapon? WeaponCharacter2 { get; set; }
        public Character? Winner { get; set; }
        [NotMapped]
        public Guid WeaponSelectedId { get; set; }
        [NotMapped]
        public int BidAmonunt { get; set; }
        [NotMapped]
        public BidClass BidClass { get; set; }
    }
}
