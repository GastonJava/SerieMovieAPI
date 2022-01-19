using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SerieMovieAPI.Core.IRepositories;
using SerieMovieAPI.Data;
using SerieMovieAPI.Models;
using SerieMovieAPI.Models.DTOs.Movieseries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerieMovieAPI.Core.Repositories
{
    public class MovieserieRepository : GenericRepository<MovieserieModel>, IMovieserieRepository
    {
        public MovieserieRepository(
            SerieMovieDBContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public override async Task<IQueryable<MovieserieModel>> GetTodos() 
        {
            try
            {
                var todos = await dbset.ToListAsync();

                _logger.LogInformation("Traemos todo de pelicula o serie ${Repo}");
                return todos.AsQueryable();

            } catch (Exception ex) {
                _logger.LogError(ex, "{repo} El metodo GetTodos", typeof(MovieserieRepository));
                return new List<MovieserieModel>().AsQueryable();
            }
        }

        //metodo para traer todo desde Movieserie
        public override async Task<bool> GetByString(string titlemovie)
        {

            bool resultado = false;

            var title2 = await _context.Movieseries.FirstOrDefaultAsync(t => t.Title_Movserie == $"{titlemovie}");

            
            //var title = await dbset.FirstOrDefaultAsync(t => t.Title_Movserie == $"{titlemovie}");
       

            if (title2 != null)
            {
                var id = title2.MovieserieId;
                resultado = true;
            }

            //var get = await dbset.FirstOrDefaultAsync();

            return resultado;

        }

        
        public override async Task<IEnumerable<MovieserieModel>> GetByTitle(string titlemovie) 
        {

            var title = await _context.Movieseries.Where(t => t.Title_Movserie == $"{titlemovie}").ToListAsync();
            //var title = await _context.Movieseries.FirstOrDefaultAsync(t => t.Title_Movserie == $"{titlemovie}");
            return title;
        }
        

        /*
        public override async Task<IEnumerable<MovieseriesDTO>> GetByTitle(string titlemovie)
        {
            var title = await _context.Movieseries.FirstOrDefaultAsync(t => t.Title_Movserie == $"{titlemovie}");
            return (IEnumerable<MovieseriesDTO>)title;
        }
        */

        public override async Task<bool> Upsert(MovieserieModel entity)
        {

            try
            {

                var existingMovieserie = await dbset.Where(x => x.MovieserieId == entity.MovieserieId)
                .FirstOrDefaultAsync();

                if (existingMovieserie == null)
                {
                    _logger.LogInformation("Agregamos pelicula o serie ${Repo}");
                    return await Add(entity);
                }
                else
                {

                    _logger.LogInformation("Actualizamos pelicula o serie ${Repo}");

                    existingMovieserie.Title_Movserie = entity.Title_Movserie;
                    existingMovieserie.Image_Movserie = entity.Image_Movserie;
                    existingMovieserie.Date_Movserie = entity.Date_Movserie;
                    existingMovieserie.Rating_Movserie = entity.Rating_Movserie;
                    existingMovieserie.Characters = entity.Characters;
                    return true;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert method error", typeof(MovieserieRepository));
                return false;
            }
        }

        public override async Task<bool> Deletekey(int id)
        {
            try
            {
                var exist = await dbset.Where(x => x.MovieserieId == id)
                    .FirstOrDefaultAsync();

                if (exist != null)
                {
                    _logger.LogInformation("la pelicula existe entonces borrariamos ${Repo}");
                    dbset.Remove(exist);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete method error", typeof(MovieserieRepository));
                return false;
            }
        }

    }
}
