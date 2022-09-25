using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models
{
    public interface INoticiaRepository
    {
        PagedResult<Noticia> GetPeople(string? name, int page);
        Task<Noticia?> GetPerson(int noticiaId);
        Task<Noticia> AddPerson(Noticia person);
        Task<Noticia?> UpdatePerson(Noticia person);
        Task<Noticia?> DeletePerson(int noticiaId);
    }
}