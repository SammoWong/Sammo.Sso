using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sammo.Sso.Domain.Entities;
using Sammo.Sso.Infrastructure.Data.Configurations;
using System;

namespace Sammo.Sso.Infrastructure.Data.Context
{
    public class SsoDbContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        public SsoDbContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Button> Buttons { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new ButtonConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var connString = _serviceProvider.GetService<IConfiguration>().GetSection("ConnectionStrings:MySql").Value;
            optionsBuilder.UseMySql(connString);
        }
    }
}
