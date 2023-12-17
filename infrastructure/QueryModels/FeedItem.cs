using infrastructure.DataModels;
using System.ComponentModel.DataAnnotations;


namespace infrastructure.QueryModels
{
    public class BlogFeedQuery
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Summary { get; set; }
        // Consider excluding or including based on the use case
        public string? Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public int CategoryId { get; set; }

        // Uncomment and use DTOs if needed
        // public CategoryDto? Category { get; set; }
        // public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();

        public string? FeaturedImage { get; set; }
    }
}
