using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IMarkupRepo
    {
        Task<Markup> GetByIdAsync(int id);
        Task<List<Markup>> GetAllAsync();
        Task<Markup> AddAsync(Markup entity);
        Task<Markup> UpdateAsync(Markup entity);
        Task<bool> RemoveAsync(Markup entity);
    }
}
