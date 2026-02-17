namespace Mission06_Hoopes.Models
{
    public class Category
    {
        public int CategoryId { get; set; }              // numeric id for the category
        public string CategoryName { get; set; } = "";   // the visible name (e.g., "Comedy")
        
        // navigation property: all movies that belong to this category
        public List<Movie>? Movies { get; set; }
    }
}