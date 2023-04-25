using ModelGSB;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ORMGSB
{
    public partial class GsbOrm : DbContext
    {
        public GsbOrm()
            : base("name=GsbOrm")
        {
        }

        public virtual DbSet<Departement> Departements { get; set; }
        public virtual DbSet<Medecin> Medecins { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departement>()
                .Property(e => e.IdDepartement)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Departement>()
                .Property(e => e.NomDep)
                .IsUnicode(false);

            modelBuilder.Entity<Departement>()
                .Property(e => e.NomRegion)
                .IsUnicode(false);

            modelBuilder.Entity<Departement>()
                .HasMany(e => e.Medecins)
                .WithRequired(e => e.Departement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.NomMed)
                .IsUnicode(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.PrenomMed)
                .IsUnicode(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.AdresseMed)
                .IsUnicode(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.TelephoneMed)
                .IsUnicode(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.IdDepartement)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
