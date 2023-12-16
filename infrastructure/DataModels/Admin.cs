namespace infrastructure.DataModels;
using System.ComponentModel.DataAnnotations;

public class Admin
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public int FailedLoginAttempts { get; set; }
}