using HexaBlogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexaBlogAPI.Repository
{
  public  interface IRepository<T> 
    {
        IEnumerable<T> GetAll();

        Task<T> GetById(int id);

        Task<T> AddAsync(T item);

        Task<T> Update(int id, T item);

        Task<T> Delete(int id);
    }
}
