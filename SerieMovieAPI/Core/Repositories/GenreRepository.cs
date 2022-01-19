using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SerieMovieAPI.Core.IRepositories;
using SerieMovieAPI.Data;
using SerieMovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerieMovieAPI.Core.Repositories
{
    public class GenreRepository : GenericRepository<GenreModel>, IGenreRepository
    {
        public GenreRepository(SerieMovieDBContext context, ILogger logger) :
           base(context, logger)
        {
        }

        public override async Task<IEnumerable<GenreModel>> All()
        {
            try
            {
                return await dbset.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(GenreRepository));
                return new List<GenreModel>();
            }
        }

        public override async Task<bool> Upsert(GenreModel entity)
        {
            try
            {
                var existingGenre = await dbset.Where(x => x.GenreId == entity.GenreId)
                .FirstOrDefaultAsync();

                if (existingGenre == null)
                {
                    return await Add(entity);
                }
                else
                {
                    existingGenre.Image_Genre = entity.Image_Genre;
                    existingGenre.Name_Genre = entity.Name_Genre;
                    existingGenre.Movieserie = entity.Movieserie;   
                    return true;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert method error", typeof(GenreRepository));
                return false;
            }
        }
    }
}