﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TestCacheAPI.Models;

namespace TestCacheAPI.Context
{
    public partial class TestdbContext : DbContext
    {
        public TestdbContext()
        {
        }

        public TestdbContext(DbContextOptions<TestdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Table1> Table1 { get; set; }
        public virtual DbSet<Table2> Table2 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table1>(entity =>
            {
                entity.HasKey(e => e.IdTable);

                entity.Property(e => e.Field1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Field2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Field3)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Field4)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Field5)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Table2>(entity =>
            {
                entity.HasKey(e => e.IdTable);

                entity.Property(e => e.Field1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Field2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Field3)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Field4)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Field5)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
