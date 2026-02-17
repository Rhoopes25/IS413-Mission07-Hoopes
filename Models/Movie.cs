using System.ComponentModel.DataAnnotations;

namespace Mission06_Hoopes.Models;

public class Movie
{
    [Key]
    public int MovieId { get; set; }

    // Instead of a free-text Category string, we store the numeric foreign key:
    [Required]
    public int CategoryId { get; set; }

    // Navigation property so EF can give us the Category object if we need it:
    public Category? Category { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Range(1888, 9999, ErrorMessage = "Year must be 1888 or later")]
    public int Year { get; set; }

    [Required]
    public string Director { get; set; } = string.Empty;

    [Required]
    public string Rating { get; set; } = string.Empty;

    // Make Edited non-nullable bool so the app always knows true/false
    [Required]
    public bool Edited { get; set; }

    // Optional
    public string? LentTo { get; set; }

    // Optional, max 25 chars
    [StringLength(25)]
    public string? Notes { get; set; }

    // DB expects
    [Required]
    public bool CopiedToPlex { get; set; }
}