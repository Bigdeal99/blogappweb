using infrastructure.DataModels;
using System.ComponentModel.DataAnnotations;

namespace infrastructure.QueryModels;

public class BlogFeedQuery
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Summary { get; set; }
    public string? Content { get; set; }

    public DateTime PublicationDate { get; set; }

    public int CategoryId { get; set; }

    // If you need category details, consider a DTO instead of an EF entity
    //public CategoryDto? Category { get; set; } 

    // Consider whether comments are needed here, if so, use a DTO
    //public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();

    public string? FeaturedImage { get; set; }

    public class CommentFeedQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime PublicationDate { get; set; }
        public int BlogPostId { get; set; }
        public virtual Blog Blog { get; set; }
        
    }
}