using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuizMart.Models.DomainModels;

namespace QuizMart.Context;

public partial class QuizMartDbContext : DbContext
{
    public QuizMartDbContext()
    {
    }

    public QuizMartDbContext(DbContextOptions<QuizMartDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Choice> Choices { get; set; }

    public virtual DbSet<Deck> Decks { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=QuizMartConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Choice>(entity =>
        {
            entity.HasKey(e => e.ChoiceId).HasName("PK__Choices__76F516868F5D5FFB");

            entity.Property(e => e.ChoiceId)
                .ValueGeneratedNever()
                .HasColumnName("ChoiceID");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.QuizId).HasColumnName("QuizID");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Choices)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Choices__QuizID__4316F928");
        });

        modelBuilder.Entity<Deck>(entity =>
        {
            entity.HasKey(e => e.DeckId).HasName("PK__Decks__76B5444CB77F3F94");

            entity.Property(e => e.DeckId)
                .ValueGeneratedNever()
                .HasColumnName("DeckID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EndsAt).HasColumnType("datetime");
            entity.Property(e => e.HostId).HasColumnName("HostID");
            entity.Property(e => e.ModeratorId).HasColumnName("ModeratorID");
            entity.Property(e => e.PublishedAt).HasColumnType("datetime");
            entity.Property(e => e.StartsAt).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Host).WithMany(p => p.DeckHosts)
                .HasForeignKey(d => d.HostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Decks__HostID__3C69FB99");

            entity.HasOne(d => d.Moderator).WithMany(p => p.DeckModerators)
                .HasForeignKey(d => d.ModeratorId)
                .HasConstraintName("FK__Decks__Moderator__3D5E1FD2");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.QuizId).HasName("PK__Quizzes__8B42AE6E939DF2CF");

            entity.Property(e => e.QuizId)
                .ValueGeneratedNever()
                .HasColumnName("QuizID");
            entity.Property(e => e.DeckId).HasColumnName("DeckID");
            entity.Property(e => e.QuestionText).HasColumnType("text");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Deck).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.DeckId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quizzes__DeckID__403A8C7D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A679599CC");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC2B3F406C");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
