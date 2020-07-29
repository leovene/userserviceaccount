using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using UserServiceAccount.Data.Configs;
using UserServiceAccount.Data.Mappings;
using UserServiceAccount.Domain.Entities;

namespace UserServiceAccount.Data.Contexts
{
    public class SqlServerContext : DbContext
    {
        private readonly IHostEnvironment _env;
        public SqlServerContext(IHostEnvironment env) => _env = env ?? throw new ArgumentException(nameof(env));
        public DbSet<UserEntity> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(_env.ContentRootPath)
                 .AddJsonFile("appsettings.Json")
                 .AddJsonFile("appsettings.Development.Json", true)
                 .AddEnvironmentVariables();

            if (_env.IsDevelopment())
            {
                builder.AddUserSecrets<ConnectionString>();
            }

            var config = builder.Build();

            if (!optionsBuilder.IsConfigured)
                _ = optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Configuration(modelBuilder);

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void Configuration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<UserEntity>(u =>
            {
                u.HasData(new UserEntity
                {
                    Id = Guid.Parse("45BCE24A-BE4B-4285-8286-2ED46CC5E9B7"),
                    UserName = "leo.ochoalopes@gmail.com",
                    Password = "c9faab11c5ed20b95f555bac440f7b1a"
                });
            });
            #endregion
        }
    }
}
