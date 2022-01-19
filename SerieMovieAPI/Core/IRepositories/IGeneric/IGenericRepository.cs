using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerieMovieAPI.Core
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();

        Task<IQueryable<T>> GetTodos();
        /* Task<IActionResult> GetTodos(); */

        // este metodo busca y trae sin GUID
        Task<T> GetbyIdkey(int id);

        //metodo por id GUID del USER
        Task<T> GetbyId(Guid id);

        //get by title STRING
        Task<bool> GetByString(string title);

        Task<IEnumerable<T>> GetByTitle(string title);

        Task<IEnumerable<T>> GetbyNames(string name);

        Task<IEnumerable<T>> GetbyAges(int age);

        Task<IEnumerable<T>> GetcharacterByIdmovie(int idmovie);

        Task<bool> Add(T entity);

        Task<bool> AddDTO(T entity);

        //este metodo borra por ID sin GUID
        Task<bool> Deletekey(int id);

        //metodo por id GUID del USER
        Task<bool> Delete(Guid id);

        Task<bool> Upsert(T entity);

    }
}
