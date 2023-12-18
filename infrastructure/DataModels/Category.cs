using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class Category
{
    [Key]

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Blog> Blog { get; set; }
}