
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReTechApi.Data;
using ReTechApi.Models;
using ReTechBE.UserDTO;
using System.Text;
using System.Threading.Tasks;

namespace ReTechBE
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IAuthRepo, AuthRepo>();
            builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
   {
                options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidAudience = builder.Configuration["Jwt:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
       };
   });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLiveServer", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5501") // Your Live Server URL
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            #region Seed Role Manager

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await RoleSeeder.SeedRolesAsync(roleManager);

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // Seed Admin
                var admin = await userManager.FindByEmailAsync("admin@gmail.com");
                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        EmailConfirmed = true,
                        Name = "Admin User",
                        Address = "Cairo",
                        PhoneNumber = "01000000003",
                        UserType = UserType.Admin,
                        IsActive = true
                    };
                    await userManager.CreateAsync(admin, "Demo@123");
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                // Seed Customer
                var customer = await userManager.FindByEmailAsync("customer@gmail.com");
                if (customer == null)
                {
                    customer = new ApplicationUser
                    {
                        UserName = "customer@gmail.com",
                        Email = "customer@gmail.com",
                        EmailConfirmed = true,
                        Name = "Demo Customer",
                        Address = "Cairo",
                        PhoneNumber = "01000000001",
                        UserType = UserType.Customer,
                        IsActive = true
                    };
                    await userManager.CreateAsync(customer, "Demo@123");
                    await userManager.AddToRoleAsync(customer, "Customer");
                }

                // Seed Company
                var company = await userManager.FindByEmailAsync("company@gmail.com");
                if (company == null)
                {
                    company = new ApplicationUser
                    {
                        UserName = "company@gmail.com",
                        Email = "company@gmail.com",
                        EmailConfirmed = true,
                        Name = "Demo Recycling Company",
                        Address = "Giza",
                        PhoneNumber = "01000000002",
                        UserType = UserType.RecyclingCompany,
                        IsActive = true
                    };
                    await userManager.CreateAsync(company, "Demo@123");
                    await userManager.AddToRoleAsync(company, "RecyclingCompany");
                }
            }

            #endregion
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowLiveServer");
            app.UseAuthorization();

           
            app.MapControllers();

            app.Run();
        }
    }
}
