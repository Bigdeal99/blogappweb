using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;
using infrastructure.DataModels;

namespace api.TransferModels;

public class CreateBlogRequestDto
{
    [Required]
    public string Title { get; set; } = string.Empty;



    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime PublicationDate { get; set; }
    

}