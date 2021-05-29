using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ExamCorrectionBackend.Domain.Entities;

#nullable disable

namespace ExamCorrectionBackend.Persistence
{
    public partial class ExamCorrectionContext : DbContext
    {
        public ExamCorrectionContext()
        {
        }

        public ExamCorrectionContext(DbContextOptions<ExamCorrectionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Dataset> Datasets { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamTask> ExamTasks { get; set; }
        public virtual DbSet<StudentSolution> StudentSolutions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings.ExamCorrection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.OwnerId)
                    .IsRequired()
                    .HasColumnName("ownerId");
            });

            modelBuilder.Entity<Dataset>(entity =>
            {
                entity.ToTable("Dataset");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.IsSimilar).HasColumnName("isSimilar");

                entity.Property(e => e.Score)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("score");

                entity.Property(e => e.Sentence1)
                    .IsRequired()
                    .HasColumnName("sentence1");

                entity.Property(e => e.Sentence2)
                    .IsRequired()
                    .HasColumnName("sentence2");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ScoreThreshold)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("scoreThreshold");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exams_Course");
            });

            modelBuilder.Entity<ExamTask>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.ExamId).HasColumnName("examId");

                entity.Property(e => e.Solution)
                    .IsRequired()
                    .HasColumnName("solution");

                entity.Property(e => e.TaskOrder).HasColumnName("taskOrder");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamTasks)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamTasks_Exam");
            });

            modelBuilder.Entity<StudentSolution>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnName("answer");

                entity.Property(e => e.Score)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("score");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasColumnName("studentId");

                entity.Property(e => e.TaskId).HasColumnName("taskId");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.StudentSolutions)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentSolutions_ExamTask");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
