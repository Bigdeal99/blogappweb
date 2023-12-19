using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class Comment
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }
    public DateTime PublicationDate { get; set; }
    public int BlogId { get; set; }
    public virtual Blog Blog { get; set; }
}