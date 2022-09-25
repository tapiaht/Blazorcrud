using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services
{
    public interface INoticiaService
    {
        Task<PagedResult<Noticia>> GetPeople(string? name, string page);
        Task<Noticia> GetPerson(int id);

        Task DeletePerson(int id);

        Task AddPerson(Noticia person);

        Task UpdatePerson(Noticia person);
    }
}