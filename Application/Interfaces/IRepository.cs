using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<OperationResult<T>> GetByIdAsync(string id);
        Task<OperationResult<IEnumerable<T>>> GetAllAsync();
        Task<OperationResult<T>> AddAsync(T entity);
        Task<OperationResult<T>> UpdateAsync(T entity);
        Task<OperationResult<bool>> DeleteAsync(string id);
    }
}
