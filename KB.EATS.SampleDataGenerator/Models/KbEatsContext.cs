using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KB.EATS.SampleDataGenerator.Models;

public partial class KbEatsContext : DbContext
{
    public KbEatsContext()
    {
    }

    public KbEatsContext(DbContextOptions<KbEatsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ShiftData> ShiftDatas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserShift> UserShifts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=KB-EATS;User Id=TheRoslyn;Password=1q2w3e4r5.T!;Encrypt=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShiftData>(entity =>
        {
            entity.HasKey(e => e.ShiftDataId).HasName("PK_ShiftData");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.FullDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.UserShift).WithMany(p => p.ShiftData)
                .HasForeignKey(d => d.UserShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShiftData_UserShifts");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.RegisterDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserShift>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("smalldatetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserShifts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserShifts_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
