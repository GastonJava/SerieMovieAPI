using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SerieMovieAPI.Core.IConfiguration;
using SerieMovieAPI.Core.IRepositories;
using SerieMovieAPI.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace SerieMovieAPI.Data
{
   
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SerieMovieDBContext _context;

        private readonly ILogger _logger;

        private readonly IWebHostEnvironment env;


        public IUserRepository Users { get; private set; }

        public ICharacterRepository Characters {  get; private set; }

        public IGenreRepository Genres { get; private set; }

        public IMovieserieRepository Movieseries {  get; private set; }

        public IImageRepository Images {  get; private set; }   

        public UnitOfWork(
          SerieMovieDBContext context,
          ILoggerFactory loggerFactory,
          IWebHostEnvironment enviroment

        )
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            env = enviroment;
            

            Users = new UserRepository(_context, _logger);
            Characters = new CharacterRepository(_context, _logger);
            Genres = new GenreRepository(_context, _logger);
            Movieseries = new MovieserieRepository(_context, _logger);
            Images = new ImageRepository(env);    
           
        }

        public async Task CompleteAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
