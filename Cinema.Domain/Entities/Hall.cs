using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Domain.Entities
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
    }
}