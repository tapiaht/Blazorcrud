using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models
{
    public interface ICategoriaRepository
    {
        PagedResult<Categoria> GetUploads(string? name, int page);
        Task<Categoria?> GetUpload(int Id);
        Task<Categoria> AddUpload(Categoria upload);
        Task<Categoria?> UpdateUpload(Categoria upload);
        Task<Categoria?> DeleteUpload(int id);
    }
}