using SoltimateLib.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoltimateWeb.Data
{

    /// <summary>
    /// Create databasecontext with the classes that we want to use.
    /// </summary>
    public class SoltimateContext : IdentityDbContext<
        SoltimateUser,
        SoltimateRole,
        string,
        SoltimateUserClaim,
        SoltimateUserRole,
        SoltimateUserLogin,
        SoltimateRoleClaim,
        SoltimateUserToken
    >
    {
        /// <summary>
        /// Create new instance of the DbContext.
        /// </summary>
        /// <param name="options"></param>
        public SoltimateContext(DbContextOptions<SoltimateContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Set details for model creation.
        /// </summary>
        /// <param name="builder">Instance of the modelbuilder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //First call base model create.
            base.OnModelCreating(builder);

            /*builder.Entity()
.HasQueryFilter(p => !p.IsDisabled &&
p.TenantId == this.TenantId);*/

            //Override the tables here because the class related Table settings arent being set on the migrations.
            builder.Entity<SoltimateRole>().ToTable("Role");
            builder.Entity<SoltimateRoleClaim>().ToTable("RoleClaim");
            builder.Entity<SoltimateUser>().ToTable("User");
            builder.Entity<SoltimateUserClaim>().ToTable("UserClaim");
            builder.Entity<SoltimateUserLogin>().ToTable("UserLogin");
            builder.Entity<SoltimateUserRole>().ToTable("UserRole");
            builder.Entity<SoltimateUserToken>().ToTable("UserToken");
        }

        /// <summary>
        /// Bla into context.
        /// </summary>
        //public DbSet<Bla> Bla { get; set; }
    }
}
