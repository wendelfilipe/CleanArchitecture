using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public SeedUserRoleInitial(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager
        )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void SeedRoles()
        {
            if(userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = userManager.CreateAsync(user, "Padrao#2021").Result;

                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
            if(userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = userManager.CreateAsync(user, "Padrao#2021").Result;

                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public void SeedUsers()
        {
            if(!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "USER";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
             if(!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}