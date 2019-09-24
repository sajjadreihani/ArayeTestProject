﻿// <auto-generated />
using ArayeTestProject.Api.Presistences.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArayeTestProject.Api.Migrations {
    [DbContext (typeof (AppDbContext))]
    [Migration ("20190920095748_initial2")]
    partial class initial2 {
        protected override void BuildTargetModel (ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation ("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation ("Relational:MaxIdentifierLength", 128)
                .HasAnnotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity ("ArayeTestProject.Api.Application.Models.Domain.City", b => {
                b.Property<long> ("Id")
                    .ValueGeneratedOnAdd ()
                    .HasAnnotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string> ("Name")
                    .HasMaxLength (50);

                b.HasKey ("Id");

                b.ToTable ("Cities");
            });

            modelBuilder.Entity ("ArayeTestProject.Api.Application.Models.Domain.Sale", b => {
                b.Property<long> ("Id")
                    .ValueGeneratedOnAdd ()
                    .HasAnnotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<long> ("CityId");

                b.Property<long> ("Price");

                b.Property<int> ("ProductId");

                b.Property<string> ("ProductName")
                    .HasMaxLength (50);

                b.Property<string> ("UserName")
                    .HasMaxLength (50);

                b.HasKey ("Id");

                b.HasIndex ("CityId");

                b.ToTable ("Sales");
            });

            modelBuilder.Entity ("ArayeTestProject.Api.Application.Models.Domain.Sale", b => {
                b.HasOne ("ArayeTestProject.Api.Application.Models.Domain.City", "City")
                    .WithMany ()
                    .HasForeignKey ("CityId")
                    .OnDelete (DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}