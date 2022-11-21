using Ecommerce.Domain.Common;
using Ecommerce.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Sql.DataAccess
{
    public partial class DataContext
    {
        public string UserId;
        public string UserName;
        public void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>().Property(prop => prop.CreatedBy)
                .HasMaxLength(256);

            builder.Entity<T>().Property(prop => prop.LastModifiedBy)
                .HasMaxLength(256);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            #region Identity Config
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "IdentityUsers");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).HasMaxLength(256);
                entity.Property(e => e.LastName).HasMaxLength(256);
                entity.Property(e => e.Gender).HasMaxLength(50);
                entity.Property(e => e.Address).HasMaxLength(500);
                entity.Property(e => e.CreatedBy).HasMaxLength(256);
                entity.Property(e => e.LastModifiedBy).HasMaxLength(256);
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "IdentityRoles");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("IdentityUserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("IdentityUserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("IdentityUserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("IdentityRoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("IdentityUserTokens");
            });
            #endregion


        }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public async Task<int> SaveChangesAsync()
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return base.Database.BeginTransaction();
        }

        ////When you expect a model back (async)
        //public async Task<IList<T>> ExecWithStoreProcedureAsync<T>(string query, params object[] parameters)
        //{
        //    return await base.Database.ExecuteSqlRawAsync<T>(query, parameters).ToListAsync();
        //}

        ////When you expect a model back
        //public IEnumerable<T> ExecWithStoreProcedure<T>(string query)
        //{
        //    return base.Database.ExecuteSqlRaw<T>(query);
        //}

        //public Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
        //{
        //    return base.Database.ExecuteSqlRawAsync(sql, parameters);
        //}


        //// Fire and forget (async)
        //public async Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters)
        //{
        //    await base.Database.ExecuteSqlRawAsync(query, parameters);
        //}

        //// Fire and forget
        //public void ExecuteWithStoreProcedure(string query, params object[] parameters)
        //{
        //    base.Database.ExecuteSqlRaw(query, parameters);
        //}

        private void OnBeforeSaving()
        {
            UserId = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UserName = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {

                if (entry.Entity is BaseEntity baseEntity)
                {
                    var now = DateTime.Now;
                    //var user = GetCurrentUser();
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            baseEntity.LastModifiedDate = now;
                            baseEntity.LastModifiedBy = UserName;
                            break;

                        case EntityState.Added:
                            baseEntity.CreatedDate = now;
                            baseEntity.CreatedBy = UserName;
                            baseEntity.LastModifiedDate = now;
                            baseEntity.LastModifiedBy = UserName;
                            break;
                    }
                }
            }
        }

        private string GetCurrentUser()
        {
            return "admin";
        }
    }
}
