// <copyright file="MATDBContext.cs" company="Carl Zeiss Vision International GmbH">
//     Copyright (c) Carl Zeiss Vision International GmbH.  All rights reserved.
//     THIS IS UNPUBLISHED PROPRIETARY SOURCE CODE OF Carl Zeiss Vision International GmbH.
//     The copyright notice does not evidence any actual or intended publication.
// </copyright>


using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ReproDB.Entities.Entities;

namespace ReproDB.Entities
{
    public partial class MATDBContext : DbContext
    {
        public virtual DbSet<MATLanguage> Languages { get; set; } = null!;
        public virtual DbSet<MATLanguage1> Languages1 { get; set; } = null!;

        public MATDBContext(DbContextOptions<MATDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=ReproDB;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("mat");

            modelBuilder.Entity<MATLanguage>(entity =>
            {
                entity.Property(e => e.Language).ValueGeneratedNever();

                entity.Property(e => e.Rfc1766).IsFixedLength();
            });

            modelBuilder.Entity<MATLanguage1>(entity =>
            {
                entity.Property(e => e.Language).ValueGeneratedNever();

                entity.Property(e => e.Rfc1766).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
