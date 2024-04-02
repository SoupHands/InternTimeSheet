using Last_Try.Data;
using Last_Try.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.DependencyInjection;

public class Startup(IConfiguration configuration)
{



    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddControllersWithViews();
        services.AddRazorPages();

        services.AddIdentity<ApplicationUser, IdentityRole>(config =>
        {
            config.SignIn.RequireConfirmedEmail = false;
        });

        services.AddControllersWithViews(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole("Intern")
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });
      

        static async Task CreateUser(UserManager<IdentityUser> userManager, string username, string password)
        {
            var user = new IdentityUser { UserName = username, Email = username };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                throw new Exception("Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        async Task ConfigureAsync(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            string[] roleNames = { "Intern", "Supervisor" };
            foreach (var roleName in roleNames)
            {
                var roleExist = roleManager.RoleExistsAsync(roleName).Result;
                if (!roleExist)
                {
                    roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
                }
            }
            var user = new ApplicationUser { UserName = "APuleo", Email = "tpuleo@yahoo.com" };
            var result = await userManager.CreateAsync(user, "TestPass123!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Intern");
            }

        }

    }


}