using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SerieMovieAPI.Models;
using SerieMovieAPI.Models.DTOs.User;
using System.IO;

namespace SerieMovieAPI.Data
{
    public class SerieMovieDBContext : IdentityDbContext<CustomUserIdentity>
    {
        public virtual DbSet<User> Users {  get; set; }

        public virtual DbSet<MovieserieModel> Movieseries { get; set; }

        public virtual DbSet<CharacterModel> Characters { get; set; }

        public virtual DbSet<GenreModel> Genres {  get; set; }

        /* public virtual DbSet<ImageModel> Images {  get; set; } */

        public SerieMovieDBContext(DbContextOptions<SerieMovieDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /* OneToManyRelationshipConfig(modelBuilder); */
            /* ManytoManyRelationshipConfig(modelBuilder); */
            /* OneToOneRelationshipConfiguration(modelBuilder); */
            /* Seed(modelBuilder); */
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        private void OneToManyRelationshipConfig(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<GenreModel>()
                .HasMany(g => g.Movieseries)
                .WithOne(c => c.GenreModel)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            */
            /** ULTIMO CAMBIO DATABASE 
            modelBuilder.Entity<MovieserieModel>()
                .HasMany(m => m.Characters)
                .WithOne(c => c.Movieserie)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(); */
        }

        private void ManytoManyRelationshipConfig(ModelBuilder modelBuilder) 
        {
         /*   
            modelBuilder.Entity<CharacterMovieserie>()
                .HasKey(cm => new { cm.CharacterId, cm.MovieserieId }); 
            
            modelBuilder.Entity<CharacterMovieserie>()
               .HasOne(cm => cm.Character)
               .WithMany(c => c.CharacterMovieseries)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey(cm => cm.CharacterId)
               .IsRequired();

            modelBuilder.Entity<CharacterMovieserie>()
               .HasOne(cm => cm.Movieserie)
               .WithMany(m => m.CharacterMovieseries)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey(cm => cm.MovieserieId)
               .IsRequired(); 
         */
        }

        private void OneToOneRelationshipConfig(ModelBuilder modelBuilder)
        { 
            //configuramos la clave foranea de 1 to 1 Imagen - Genre
            /*modelBuilder.Entity<ImageModel>()
                .HasOne(i => i.Genre)
                .WithOne(g => g.Image)
                .HasForeignKey<GenreModel>(g => g.ImageFK)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(); */

            //configuramos la clave foranea de 1 to 1 Imagen - Movieserie
           /* modelBuilder.Entity<ImageModel>()
                .HasOne(i => i.Movieserie)
                .WithOne(m => m.Image)
                .HasForeignKey<MovieserieModel>(m => m.ImageFK)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(); */
        }

         public static void Seed(ModelBuilder modelBuilder)
         {

            modelBuilder.Entity<MovieserieModel>()
             .HasData(
               new MovieserieModel
               {
                   /*MovieserieId = 1, */
                 
               }

              );

            //character
            modelBuilder.Entity<CharacterModel>()
                .HasData(
                  new CharacterModel
                  {
                      /* CharacterId = 1, */
                      
                  }

                 );

            /*
            modelBuilder.Entity<ImageModel>()
                 .HasData(
                  new ImageModel {
                    ImageId = 1,    
                    Title_Image = "Random titulo",
                    Name_Image = "random", 
                    Data_Image = ReadFile("StaticFiles/images/badbunny.jpg"),

                  }
                
                 );
            */
         } 

        public static byte[] ReadFile(string path)
        {
            byte[] data = null;

            FileInfo flinfo = new FileInfo(path);
            long numBytes = flinfo.Length;

            FileStream fstream = new FileStream(path, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fstream);

            data = br.ReadBytes((int)numBytes);

            return data;
        }
    }
}
