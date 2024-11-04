using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CandidateInfoAPI.Models;

public partial class CandidateDataContext : DbContext
{
    public CandidateDataContext()
    {
    }

    public CandidateDataContext(DbContextOptions<CandidateDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CandidateInformation> CandidateInformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=Conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CandidateInformation>(entity =>
        {
            entity.HasKey(e => e.CandidateId).HasName("PK__Candidat__DF539B9C51DE22B4");

            entity.ToTable("CandidateInformation");

            entity.Property(e => e.CandidateId).ValueGeneratedNever();
            entity.Property(e => e.EmailAddress).HasMaxLength(40);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.GitHubProfileUrl).HasColumnName("GitHubProfileURL");
            entity.Property(e => e.LastName).HasMaxLength(40);
            entity.Property(e => e.LinkedInProfileUrl).HasColumnName("LinkedInProfileURL");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.PreferredTime).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
