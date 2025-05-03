using API.Common.Domain.Roles;
using API.Common.Domain.Users;
using EKSystemApp.Persistence.DbInitiliazers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Persistence.Context;
using System.Security.Claims;


namespace Persistence.DbInitializerService
{
    public class DBInitiliazerContext : IDbInitiliazerContext
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly ApplicationDbContext context;

        public DBInitiliazerContext(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public async Task Initialize(ApplicationDbContext context, IConfiguration configurations)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            await this.roleManager.CreateAsync(new AppRole { Name = "SystemAdministrators" });
            await this.roleManager.CreateAsync(new AppRole { Name = "Customer" });

            var roleId = this.roleManager.Roles.Where(p => p.Name == "SystemAdministrators").Select(p => p.Id).FirstOrDefault();
            var user = new AppUser
            {
                AccessFailedCount = 0,
                Email = configurations.GetSection("Application:defaultSystemAdministratorEmail").Value!,
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = "case@case.com",
                NormalizedUserName = configurations.GetSection("Application:defaultSystemAdministratorUserName").Value,
                TwoFactorEnabled = false,
                IdentityNumber = "111111111111",
                IdentityType = "T.C",
                UserName = configurations.GetSection("Application:defaultSystemAdministratorUserName").Value!,
                FirstName = configurations.GetSection("Application:defaultSystemAdministratorFirstName").Value!,
                LastName = configurations.GetSection("Application:defaultSystemAdministratorLastName").Value!,
                RoleId = roleId,
            };
            IdentityResult result = this.userManager.CreateAsync(user, (configurations.GetSection("Application:defaultSystemAdministratorPassword").Value)!).Result;
            if (result.Succeeded)
            {
                var adminUser = (await this.userManager.FindByNameAsync(user!.UserName!))!;
                var data = await this.userManager.AddToRoleAsync(adminUser, "SystemAdministrators");
                var claims = new List<Claim> {
                    new Claim("name", "SystemAdministrators")

                };
                await this.userManager.AddClaimsAsync(adminUser, claims);
            }
            await context.SaveChangesAsync();
        }
    }

}
