using Duong_API.Data;

namespace Duong_API.IRepository
{
    public interface IDoThueRepository
    {
        Task<IEnumerable<DoThue>> GetAllAsync();
        Task<DoThue> GetByIdAsync(int id);
        Task CreateAsync(DoThue doThue);
        Task UpdateAsync(int id,DoThue doThue);
        Task DeleteAsync(int id);
    }
}
