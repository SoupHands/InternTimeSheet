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

        services.AddControllersWithViews(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole("Intern")
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });
        services.AddTransient<IEmailSender, EmailSender>(i =>
    new EmailSender(
        Configuration["EmailSettings:Host"],
        Configuration["EmailSettings:Port"],
        Configuration["EmailSettings:Username"],
        Configuration["EmailSettings:Password"]));

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
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailSender : IEmailSender
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public EmailSender(string host, int port, string username, string password)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
        }

        public EmailSender(string? v1, string? v2, string? v3, string? v4)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
            V4 = v4;
        }

        public string? V1 { get; }
        public string? V2 { get; }
        public string? V3 { get; }
        public string? V4 { get; }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = true,
            };
            await client.SendMailAsync(
                new MailMessage(_username, email, subject, message) { IsBodyHtml = true });
        }
    }

}