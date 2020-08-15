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

        public virtual DbSet<PlayerDates> PlayerDates { get; set; }
        public virtual DbSet<PlayerIdentity> PlayerIdentity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                entity.Property(e => e.GameToken)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsFixedLength();

                entity.Property(e => e.Nick)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsFixedLength();

                entity.Property(e => e.Password).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
