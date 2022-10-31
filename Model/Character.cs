using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Character
    {
        [Key]
        public Guid Id { get; set; }
        public Player Player { get; set; }
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Life { get; set; }
        [NotMapped]
        public string ImageUrl { get; set; }
        public ElementType ElementType { get; set; }
        public CharacterType CharacterType { get; set; }
        public virtual ICollection<Weapon>? Weapons { get; set; }
    }
}