using Microsoft.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using PVC_Server.Core.Entities.Identity;
using PVC_Server.Infrastructure.Constants;

namespace PVC_Server.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,int,UserClaim,UserRole,UserLogin,RoleClaim,UserToken> {

        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration) {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users",InfrastructureConstant.Schema.Identity);
            builder.Entity<Role>().ToTable("Roles", InfrastructureConstant.Schema.Identity);
            builder.Entity<UserRole>().ToTable("UserRoles", InfrastructureConstant.Schema.Identity);
            builder.Entity<RoleClaim>().ToTable("RoleClaims", InfrastructureConstant.Schema.Identity);
            builder.Entity<UserClaim>().ToTable("UserClaims", InfrastructureConstant.Schema.Identity);
            builder.Entity<UserLogin>().ToTable("UserLogins", InfrastructureConstant.Schema.Identity);
            builder.Entity<UserToken>().ToTable("UserTokens", InfrastructureConstant.Schema.Identity);

            builder.Entity<User>()
                .Ignore(u => u.PhoneNumber)
                .Ignore(u => u.PhoneNumberConfirmed);
        }
    }
}
