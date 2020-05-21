using System;
using Drums.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Drums.Data
{
    public class DrumsDbContext: DbContext
    {
        public DrumsDbContext(DbContextOptions<DrumsDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }
        
        public DbSet<ReportCard> ReportCards { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //     //This will singularize all table names dotnet core 2
        //     // foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        //     // {
        //     //     entityType.Relational().TableName = entityType.DisplayName();
        //     // }
        //     foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //     {
        //         // Use the entity name instead of the Context.DbSet<T> name
        //         // refs https://docs.microsoft.com/en-us/ef/core/modeling/relational/tables#conventions
        //         modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
        //     }
        // }
    }
}