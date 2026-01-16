using onlineCinema.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onlineCinema.Models
{
    public class Hall
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int RowsCount { get; set; }
        public int SeatsPerRow { get; set; }
        public int HallTypeId { get; set; }
        [ForeignKey("HallTypeId")]
        public HallType HallType { get; set; }
    }
}