using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ORM_PPE_SLAM
{
    public partial class OrmNba : DbContext
    {
        public OrmNba()
            : base("name=OrmNba")
        {
        }

        public virtual DbSet<Equipe> Equipes { get; set; }
        public virtual DbSet<Joueur> Joueurs { get; set; }
        public virtual DbSet<Match_joueur> Match_joueur { get; set; }
        public virtual DbSet<Match> Matchs { get; set; }
        public virtual DbSet<Match_Equipes> Match_Equipes { get; set; }

        public virtual DbSet<MatchPageModel> MatchPageModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipe>()
                .Property(e => e.NOM_Equipe)
                .IsUnicode(false);

            modelBuilder.Entity<Equipe>()
                .Property(e => e.LIB_Equipe)
                .IsUnicode(false);

            modelBuilder.Entity<Equipe>()
                .Property(e => e.CONFERENCE_Equipe)
                .IsUnicode(false);

            modelBuilder.Entity<Equipe>()
                .Property(e => e.DIVISION_Equipe)
                .IsUnicode(false);

            modelBuilder.Entity<Equipe>()
                .HasMany(e => e.Joueurs)
                .WithRequired(e => e.Equipe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipe>()
                .HasMany(e => e.Matchs)
                .WithOptional(e => e.Equipe1)
                .HasForeignKey(e => e.EQUIPE_Domicile);

            modelBuilder.Entity<Equipe>()
                .HasMany(e => e.Matchs1)
                .WithOptional(e => e.Equipe2)
                .HasForeignKey(e => e.EQUIPE_Exterieur);

            modelBuilder.Entity<Joueur>()
                .Property(e => e.NOM_Joueur)
                .IsUnicode(false);

            modelBuilder.Entity<Joueur>()
                .Property(e => e.PRENOM_Joueur)
                .IsUnicode(false);

            modelBuilder.Entity<Joueur>()
                .Property(e => e.POSTE_Joueur)
                .IsUnicode(false);

            modelBuilder.Entity<Joueur>()
                .HasMany(e => e.Match_joueur)
                .WithRequired(e => e.Joueur)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                .Property(e => e.LIEU_Match)
                .IsUnicode(false);

            modelBuilder.Entity<Match>()
                .HasMany(e => e.Match_joueur)
                .WithRequired(e => e.Match)
                .WillCascadeOnDelete(false);
        }
    }
}
