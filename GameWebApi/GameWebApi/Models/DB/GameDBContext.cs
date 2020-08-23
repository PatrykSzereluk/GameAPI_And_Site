using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GameWebApi.Models.DB
{
    public partial class GameDBContext : DbContext
    {
        public GameDBContext()
        {
        }

        public GameDBContext(DbContextOptions<GameDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClanMembers> ClanMembers { get; set; }
        public virtual DbSet<Clans> Clans { get; set; }
        public virtual DbSet<PlayerDates> PlayerDates { get; set; }
        public virtual DbSet<PlayerIdentity> PlayerIdentity { get; set; }
        public virtual DbSet<PlayerSalt> PlayerSalt { get; set; }
        public virtual DbSet<PlayerStatistics> PlayerStatistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=GameDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClanMembers>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ClanMembers", "Common");

                entity.Property(e => e.DateOfJoin).HasColumnType("date");

                entity.HasOne(d => d.Clan)
                    .WithMany()
                    .HasForeignKey(d => d.ClanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClanMembers_Clans");
            });

            modelBuilder.Entity<Clans>(entity =>
            {
                entity.ToTable("Clans", "Common");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Acronym)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AvatarUrl)
                    .HasColumnName("AvatarURL")
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlayerDates>(entity =>
            {
                entity.HasKey(e => e.PlayerId);

                entity.ToTable("PlayerDates", "Common");

                entity.Property(e => e.PlayerId).ValueGeneratedNever();

                entity.Property(e => e.BanDate).HasColumnType("date");

                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.Property(e => e.LastPasswordChangeDate).HasColumnType("date");

                entity.Property(e => e.ModificationDate).HasColumnType("date");

                entity.HasOne(d => d.Player)
                    .WithOne(p => p.PlayerDates)
                    .HasForeignKey<PlayerDates>(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayerDates_PlayerIdentity");
            });

            modelBuilder.Entity<PlayerIdentity>(entity =>
            {
                entity.ToTable("PlayerIdentity", "Common");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.GameToken)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Nick)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlayerSalt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PlayerSalt", "Common");

                entity.Property(e => e.Salt).IsRequired();

                entity.HasOne(d => d.Player)
                    .WithMany()
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salt_PlayerIdentity1");
            });

            modelBuilder.Entity<PlayerStatistics>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PlayerStatistics", "Common");

                entity.HasOne(d => d.Player)
                    .WithMany()
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayerStatistics_PlayerIdentity");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
