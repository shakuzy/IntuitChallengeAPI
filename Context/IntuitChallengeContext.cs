using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using IntuitChallengeAPI.Models;

namespace IntuitChallengeAPI.Context
{
    public partial class IntuitChallengeContext : DbContext
    {
        public IntuitChallengeContext()
        {
        }

        public IntuitChallengeContext(DbContextOptions<IntuitChallengeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                  .AddJsonFile("appsettings.json")
                                                  .Build();
                optionsBuilder.UseSqlServer($"Server={IntuitChallengeAPI.Clases.StaticFunctions.Desencriptar(configuration.GetConnectionString("Server"))};Database={IntuitChallengeAPI.Clases.StaticFunctions.Desencriptar(configuration.GetConnectionString("Database"))};User Id={IntuitChallengeAPI.Clases.StaticFunctions.Desencriptar(configuration.GetConnectionString("User_Id"))};password={IntuitChallengeAPI.Clases.StaticFunctions.Desencriptar(configuration.GetConnectionString("Password"))};Trusted_Connection=False;MultipleActiveResultSets=true;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellidos).IsUnicode(false);

                entity.Property(e => e.Cuit)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("CUIT");

                entity.Property(e => e.Domicilio).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombres).IsUnicode(false);

                entity.Property(e => e.TelefonoCelular).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
