using System.ComponentModel.DataAnnotations;

namespace api.TransferModels;

public class CreateCategoryRequestDto
{
    [Required]

    public string Name { get; set; }
    [Required]

    public string Description { get; set; }
}