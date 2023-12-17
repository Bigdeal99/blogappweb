using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;

namespace api.TransferModels;

public class UpdateBlogRequestDto
{

            public string Title { get; set; }
            public string Summary { get; set; }
            public string Content { get; set; }
            public DateTime PublicationDate { get; set; }
            [Required]
            public int CategoryId { get; set; }
     
            public string FeaturedImage { get; set; }
    
}