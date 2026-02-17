using System.ComponentModel.DataAnnotations;

namespace Mission06_Hoopes.Models;

public class Movie
{
    [Key]
    public int MovieId { get; set; }

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int Year { get; set; }

    [Required]
    public string Director { get; set; } = string.Empty;

    [Required]
    public string Rating { get; set; } = string.Empty; // we'll make this a dropdown next

    // Optional
    public bool? Edited { get; set; }

    // Optional
    public string? LentTo { get; set; }

    // Optional, max 25 chars
    [StringLength(25)]
    public string? Notes { get; set; }
}