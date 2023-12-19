using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;

namespace api.TransferModels;

public class UpdateCommentRequestDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }
    public DateTime PublicationDate { get; set; }

    public int BlogId { get; set; }
    public virtual Blog Blog { get; set; }
}