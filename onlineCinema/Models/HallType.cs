using onlineCinema.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onlineCinema.Models 
{
    public class HallType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)] 
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostMultiplier { get; set; }
        public ICollection<Hall> Halls { get; set; } = new List<Hall>();
    }
}