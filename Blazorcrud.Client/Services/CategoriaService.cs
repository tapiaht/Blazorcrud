using Blazorcrud.Client.Shared;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services
{
    public class CategoriaService: ICategoriaService
    {
        private readonly IHttpService _httpService;

        public CategoriaService(IHttpService httpService)
        {
            _httpService=httpService;
        }

        public async Task<PagedResult<Categoria>> GetUploads(string? name, string page)
        {
            return await _httpService.Get<PagedResult<Categoria>>("api/categoria" + "?page=" + page + "&name=" + name);
        }

        public async Task<Categoria> GetUpload(int id)
        {
            return await _httpService.Get<Categoria>($"api/categoria/{id}");
        }

        public async Task DeleteUpload(int id)
        {
            await _httpService.Delete($"api/categoria/{id}");
        }

        public async Task AddUpload(Categoria upload)
        {
            await _httpService.Post($"api/categoria", upload);
        }

        public async Task UpdateUpload(Categoria upload)
        {
            await _httpService.Put($"api/categoria", upload);
        }
    }
}