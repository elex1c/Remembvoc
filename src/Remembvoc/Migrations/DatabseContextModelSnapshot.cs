﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Remembvoc;

#nullable disable

namespace Remembvoc.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-rc.1.24451.1");

            modelBuilder.Entity("Remembvoc.Models.ApplicationModels.Languages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShortForm")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ShortForm")
                        .IsUnique();

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Remembvoc.Models.ApplicationModels.Words", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LanguageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Phrase")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("Phrase")
                        .IsUnique();

                    b.ToTable("Words");
                });

            modelBuilder.Entity("Remembvoc.Models.ApplicationModels.Words", b =>
                {
                    b.HasOne("Remembvoc.Models.ApplicationModels.Languages", "Language")
                        .WithMany("Words")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Remembvoc.Models.ApplicationModels.Languages", b =>
                {
                    b.Navigation("Words");
                });
#pragma warning restore 612, 618
        }
    }
}