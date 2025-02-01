using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.App.Infrastructure.Interface
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAsync();
        Task<T> GetAsync(Guid id);
        Task<T> CreateAsync(T model);
        Task<T> UpdateAsync(T model);
        Task<bool> DeleteAsync(Guid id);
    }
}
