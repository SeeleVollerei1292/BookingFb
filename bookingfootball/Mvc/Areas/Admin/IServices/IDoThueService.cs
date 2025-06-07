using Duong_API.Data;

namespace DuongPia.Areas.Admin.IServices
{
    public interface IDoThueService
    {
        Task<IEnumerable<DoThue>> GetAllAsync();
        Task<DoThue> GetByIdAsync(int id);
        Task CreateAsync(DoThue doThue);
        Task UpdateAsyncs(int id, DoThue doThue);
        Task DeleteAsync(int id);
    }
}
