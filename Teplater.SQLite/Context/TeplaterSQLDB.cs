using Microsoft.EntityFrameworkCore;
using Templator.DTO.DTOModels;

namespace Teplater.SQLite.Context
{
    public class TeplaterSQLDB : DbContext
    {
        public DbSet<Document> Documents { get; set; }

        public DbSet<Template> Templates { get; set; }

        public TeplaterSQLDB(DbContextOptions<TeplaterSQLDB> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source=TemplaterDB.db;Cache=Shared")
                ;

            base.OnConfiguring(optionsBuilder);
        }
    }
}
