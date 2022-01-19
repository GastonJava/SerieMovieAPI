﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SerieMovieAPI.Data;

namespace SerieMovieAPI.Migrations
{
    [DbContext(typeof(SerieMovieDBContext))]
    partial class SerieMovieDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GenreModelMovieserieModel", b =>
                {
                    b.Property<int>("GenresGenreId")
                        .HasColumnType("int");

                    b.Property<int>("MovieseriesMovieserieId")
                        .HasColumnType("int");

                    b.HasKey("GenresGenreId", "MovieseriesMovieserieId");

                    b.HasIndex("MovieseriesMovieserieId");

                    b.ToTable("GenreModelMovieserieModel");
                });

            modelBuilder.Entity("SerieMovieAPI.Models.CharacterModel", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age_Character")
                        .HasColumnType("int");

                    b.Property<string>("History_Character")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieserieId")
                        .HasColumnType("int");

                    b.Property<string>("Name_Character")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weight_Character")
                        .HasColumnType("float");

                    b.HasKey("CharacterId");

                    b.HasIndex("MovieserieId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("SerieMovieAPI.Models.GenreModel", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageFK")
                        .HasColumnType("int");

                    b.Property<string>("Name_Genre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.HasIndex("ImageFK")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("SerieMovieAPI.Models.ImageModel", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Data_Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name_Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title_Image")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.HasIndex("CharacterId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("SerieMovieAPI.Models.MovieserieModel", b =>
                {
                    b.Property<int>("MovieserieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date_Movserie")
                        .HasColumnType("datetime2");

                    b.Property<int>("ImageFK")
                        .HasColumnType("int");

                    b.Property<int>("Rating_Movserie")
                        .HasColumnType("int");

                    b.Property<string>("Title_Movserie")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MovieserieId");

                    b.HasIndex("ImageFK")
                        .IsUnique();

                    b.ToTable("Movieseries");
                });

            modelBuilder.Entity("SerieMovieAPI.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GenreModelMovieserieModel", b =>
                {
                    b.HasOne("SerieMovieAPI.Models.GenreModel", null)
                        .WithMany()
                        .HasForeignKey("GenresGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SerieMovieAPI.Models.MovieserieModel", null)
                        .WithMany()
                        .HasForeignKey("MovieseriesMovieserieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SerieMovieAPI.Models.CharacterModel", b =>
                {
                    b.HasOne("SerieMovieAPI.Models.MovieserieModel", "Movieserie")
                        .WithMany("Characters")
                        .HasForeignKey("MovieserieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movieserie");
                });

            modelBuilder.Entity("SerieMovieAPI.Models.GenreModel", b =>
                {
                    b.HasOne("SerieMovieAPI.Models.ImageModel", "Image")
                        .WithOne("Genre")
                        .HasForeignKey("SerieMovieAPI.Models.GenreModel", "ImageFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("SerieMovieAPI.Models.ImageModel", b =>
                {
                    b.HasOne("SerieMovieAPI.Models.CharacterModel", "Character")
                        .WithMany("Images")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("SerieMovieAPI.Models.MovieserieModel", b =>
                {
                    b.HasOne("SerieMovieAPI.Models.ImageModel", "Image")
                        .WithOne("Movieserie")
                        .HasForeignKey("SerieMovieAPI.Models.MovieserieModel", "ImageFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("SerieMovieAPI.Models.CharacterModel", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("SerieMovieAPI.Models.ImageModel", b =>
                {
                    b.Navigation("Genre");

                    b.Navigation("Movieserie");
                });

            modelBuilder.Entity("SerieMovieAPI.Models.MovieserieModel", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
