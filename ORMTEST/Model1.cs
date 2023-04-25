using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ORMTEST
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=ModelORM")
        {
        }

        public virtual DbSet<Departements> Departements { get; set; }
        public virtual DbSet<Medecins> Medecins { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departements>()
                .Property(e => e.IdDepartement)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Departements>()
                .Property(e => e.NomDep)
                .IsUnicode(false);

            modelBuilder.Entity<Departements>()
                .Property(e => e.NomRegion)
                .IsUnicode(false);

            modelBuilder.Entity<Departements>()
                .HasMany(e => e.Medecins)
                .WithRequired(e => e.Departements)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medecins>()
                .Property(e => e.NomMed)
                .IsUnicode(false);

            modelBuilder.Entity<Medecins>()
                .Property(e => e.PrenomMed)
                .IsUnicode(false);

            modelBuilder.Entity<Medecins>()
                .Property(e => e.AdresseMed)
                .IsUnicode(false);

            modelBuilder.Entity<Medecins>()
                .Property(e => e.TelephoneMed)
                .IsUnicode(false);

            modelBuilder.Entity<Medecins>()
                .Property(e => e.IdDepartement)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Pseudo)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
