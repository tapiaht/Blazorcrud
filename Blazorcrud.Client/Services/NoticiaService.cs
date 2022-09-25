using Blazorcrud.Client.Shared;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Blazorcrud.Client.Services
{
    public class NoticiaService: INoticiaService
    {
        private IHttpService _httpService;

        public NoticiaService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<PagedResult<Noticia>> GetPeople(string? name, string page)
        {
            return await _httpService.Get<PagedResult<Noticia>>("api/noticia" + "?page=" + page + "&name=" + name);
        }

        public async Task<Noticia> GetPerson(int id)
        {
            return await _httpService.Get<Noticia>($"api/noticia/{id}");
        }

        public async Task DeletePerson(int id)
        {
            await _httpService.Delete($"api/noticia/{id}");
        }

        public async Task AddPerson(Noticia person)
        {
            await _httpService.Post($"api/noticia", person);
        }

        public async Task UpdatePerson(Noticia person)
        {
            await _httpService.Put($"api/noticia", person);
        }
    }
}