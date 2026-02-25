using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Game:BaseEntity
    {
        
        [Required]
        [MaxLength(3000)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [MaxLength(500)]
        public string Cover {  get; set; } 
        //foriengh key
        public int CategoryId {  get; set; }
        //navigation property
        public Category Category { get; set; }
        //navigation property
        public ICollection<GameDevice> Devices { get; set; }=new List<GameDevice>();
    }
}
