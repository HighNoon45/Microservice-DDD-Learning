using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ICostRepo
    {
        Task<Cost> GetByIdAsync(int id);
        Task<List<Cost>> GetAllAsync();
        Task<Cost> AddAsync(Cost entity);
        Task<Cost> UpdateAsync(Cost entity);
        Task<bool> RemoveAsync(Cost entity);
    }
}
