using System.ComponentModel.DataAnnotations.Schema;
using ApplicationDev.Models;
using Microsoft.AspNetCore.Identity;
public class ApplicationUser : IdentityUser
{
    public int? StoreId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? UsernameChangeLimit { get; set; } = 10;
    public byte[]? ProfilePicture { get; set; }
    public virtual Store Store { get; set; }
}