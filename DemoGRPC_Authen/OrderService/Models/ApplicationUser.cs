using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        override public string Id { get; set; }
    }
}
