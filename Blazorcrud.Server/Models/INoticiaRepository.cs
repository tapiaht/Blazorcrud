using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models
{
    public interface INoticiaRepository
    {
        PagedResult<Noticia> GetNoticia(string? name, int page);
        Task<Noticia?> GetNoticia(int noticiaId);
        Task<Noticia> AddNoticia(Noticia noticia);
        Task<Noticia?> UpdateNoticia(Noticia noticia);
        Task<Noticia?> DeleteNoticia(int noticiaId);
    }
}