using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services
{
    public interface ICategoriaService
    {
        Task<PagedResult<Categoria>> GetUploads(string? name, string page);
        Task<Categoria> GetUpload(int id);

        Task DeleteUpload(int id);

        Task AddUpload(Categoria upload);

        Task UpdateUpload(Categoria upload);
    }
}