using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace onlineCinema.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        // Basic info.
        [Required(ErrorMessage = "Please specify the movie title")]
        [StringLength(200)]
        [Display(Name = "Title")]
        public string Title { get; set; } = null!;

        [StringLength(1000)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [StringLength(200)]
        [Display(Name = "Director")]
        public string? Director { get; set; }

        [StringLength(500)]
        [Display(Name = "Actors")]
        public string? Actors { get; set; }

        [StringLength(100)]
        [Display(Name = "Genre")]
        public string? Genre { get; set; }

        // Duration.
        [Required]
        [Display(Name = "Duration (minutes)")]
        public int DurationMinutes { get; set; }

        // Media.
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        // Rental period.
        [Display(Name = "Rental start date")]
        public DateTime? RentalStartDate { get; set; }

        [Display(Name = "Rental end date")]
        public DateTime? RentalEndDate { get; set; }

        // Navigation properties.
        [Display(Name = "Sessions")]
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
