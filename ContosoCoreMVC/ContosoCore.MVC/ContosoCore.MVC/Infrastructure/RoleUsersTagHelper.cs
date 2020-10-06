using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoCore.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ContosoCore.MVC.Infrastructure
{
    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public RoleUsersTagHelper(UserManager<AppUser> manager, RoleManager<IdentityRole> role)
        {
            userManager = manager;
            roleManager = role;
        }

        [HtmlAttributeName("identity-role")]
        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();

            IdentityRole role = await roleManager.FindByIdAsync(Role);

            if (role != null)
            {
                foreach (var item in userManager.Users)
                {
                    if (item != null && await userManager.IsInRoleAsync(item, role.Name))
                    {
                        names.Add(item.UserName);
                    }
                }
            }
            output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(",", names)); //mandamos el listado de usuarios que contiene el rol
        }
    }
}
