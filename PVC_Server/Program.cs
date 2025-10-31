using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Microsoft.IdentityModel.Tokens;
using PVC_Server.Application.Interfaces;
using PVC_Server.Application.Options;
using PVC_Server.Core.Entities.Identity;
using PVC_Server.Infrastructure.Data;
using PVC_Server.Infrastructure.Services;
using System.Text;

namespace PVC_Server {
	public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

		
			// Add services to the container.

			builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("EmailSettings"));

			JwtOption jwtOption = new JwtOption() {
				Issuer = builder.Configuration["Jwt:Issuer"] ?? "Unknown",
				Audience = builder.Configuration["Jwt:Audience"] ?? "Unknown",
				LifeTimeMin = int.Parse(builder.Configuration["Jwt:LifeTimeMin"] ?? "30"),
				Key = builder.Configuration["Jwt:Key"] ?? "Unknown"
			};
			builder.Services.AddSingleton(jwtOption);

			builder.Services.AddScoped<IEmailService, EmailService>();

			#region Bearer Authentication
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters {
					ValidateIssuer = true,
					ValidIssuer = jwtOption.Issuer,

					ValidateAudience = true,
					ValidAudience = jwtOption.Audience,

					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Key))
				};
			});
			#endregion

			#region Add Identity with Configuration
			builder.Services.AddIdentity<User, Role>(options => {
				options.User.RequireUniqueEmail = true;

				options.Password.RequiredLength = 8;
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;

				options.SignIn.RequireConfirmedEmail = true;

				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();
			#endregion

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			builder.Services.AddScoped<ApplicationDbContext>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
