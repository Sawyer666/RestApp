using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApp.Repositories.Interfaces
{
    public interface IRegionRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> CreateRegion(T region);
        Task<T> DeleteRegion(int id);
        Task<T> GetRegion(int id);
        Task<T> UpdateRegion(T region);
    }
}