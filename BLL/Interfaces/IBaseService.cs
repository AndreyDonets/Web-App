using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBaseService<T> : IDisposable
         where T: BaseModelDTO
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T item);
        Task CreateAsync(T item);
        Task DeleteAsync(Guid id);

        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Update(T item);
        void Create(T item);
        void Delete(Guid id);
    }
}
