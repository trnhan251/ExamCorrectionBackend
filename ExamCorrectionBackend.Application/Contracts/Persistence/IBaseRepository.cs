using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamCorrectionBackend.Application.Contracts.Persistence
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByGuIdAsync(Guid id);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByLongIdAsync(long id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size);
    }
}