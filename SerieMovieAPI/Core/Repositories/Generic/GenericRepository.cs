using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SerieMovieAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerieMovieAPI.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected SerieMovieDBContext _context;

        protected DbSet<T> dbset;

        protected readonly ILogger _logger;

        public GenericRepository(SerieMovieDBContext context, ILogger logger) 
        {
         
            _context = context;
            _logger = logger;
           this.dbset = context.Set<T>(); 
        }


        public virtual async Task<bool> Add(T entity)
        {
            await dbset.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> AddDTO(T entity) 
        {
            await dbset.AddAsync(entity);
            return true;
        }

        public virtual async Task<IQueryable<T>> GetTodos()
        {
            return dbset.AsNoTracking();
        }


        public virtual async Task<IEnumerable<T>> All()
        {
           return await dbset.ToListAsync();
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetbyId(Guid id)
        {
            return await dbset.FindAsync(id);
        }

        public virtual Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetbyIdkey(int id)
        {
            return await dbset.FindAsync(id);
        }

        public virtual Task<bool> Deletekey(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> GetByString(string title)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> GetByTitle(string titlemovie) 
        {
            throw new NotImplementedException();

        }

        public virtual Task<IEnumerable<T>> GetbyNames(string name)
        {
            throw new NotImplementedException();

        }

        public virtual Task<IEnumerable<T>> GetbyAges(int age) 
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> GetcharacterByIdmovie(int idmovie) 
        {
            throw new NotImplementedException();
        }
    }
}
