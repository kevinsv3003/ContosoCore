using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ContosoCore.MVC.Models
{
    public class AppUser : IdentityUser
    {

    }

    public class CreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }


    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }

        public IEnumerable<AppUser> Members { get; set; }

        public IEnumerable<AppUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }

        public string RoleId { get; set; }

        public string[] IdsToAdd { get; set; } //Todos los id que se agregaran a un rol

        public string[] IdsToDelete { get; set; } // Todos los id que se eliminaran de un rol
    }
}

