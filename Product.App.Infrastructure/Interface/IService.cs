using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.App.Infrastructure.Interface
{
    public interface IService<T>
    {
        List<T> Get();
        Task<T> GetAsync(Guid id);
        Task<T> CreateAsync(T model);
        Task<T> UpdateAsync(T model);
        Task<bool> DeleteAsync(Guid id);
    }
}
