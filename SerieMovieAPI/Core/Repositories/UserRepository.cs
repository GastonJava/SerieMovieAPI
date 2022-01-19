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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SerieMovieDBContext context, ILogger logger) :
            base(context, logger)
        {
        }

        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                return await dbset.ToListAsync();
            }
            catch (Exception ex) 
            { 
              _logger.LogError(ex, "{Repo} All method error", typeof(UserRepository));
              return new List<User>();
            }
        }

        public override async Task<bool> Upsert(User entity) 
        {
            try
            {
                var existingUser = await dbset.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    return await Add(entity);
                }
                else
                {
                    existingUser.FirstName = entity.FirstName;
                    existingUser.LastName = entity.LastName;
                    existingUser.Email = entity.Email;
                    return true;
                }

            }
            catch (Exception ex) {
                _logger.LogError(ex, "{Repo} Upsert method error", typeof(UserRepository));
                return false;
            }
        }

        // eliminar usuario
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbset.Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if(exist != null)
                {
                  dbset.Remove(exist);
                  return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete method error", typeof(UserRepository));
                return false;
            }
        }

       
    }
}