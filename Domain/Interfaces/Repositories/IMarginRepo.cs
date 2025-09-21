using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IMarginRepo
    {
        Task<Margin> GetByIdAsync(int id);
        Task<List<Margin>> GetAllAsync();
        Task<Margin> AddAsync(Margin entity);
        Task<Margin> UpdateAsync(Margin entity);
        Task<bool> RemoveAsync(Margin entity);
    }
}
