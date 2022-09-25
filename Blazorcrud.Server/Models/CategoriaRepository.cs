using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Server.Models
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoriaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Categoria> AddUpload(Categoria upload)
        {
            var result = await _appDbContext.Categoria.AddAsync(upload);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Categoria?> DeleteUpload(int Id)
        {
            var result = await _appDbContext.Categoria.FirstOrDefaultAsync(u => u.Id==Id);
            if (result!=null)
            {
                _appDbContext.Categoria.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Upload not found");
            }
            return result;
        }

        public async Task<Categoria?> GetUpload(int Id)
        {
            var result = await _appDbContext.Categoria.FirstOrDefaultAsync(u => u.Id==Id);
            if(result != null)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException("Upload not found");
            }
        }

        public PagedResult<Categoria> GetUploads(string? name, int page)
        {
            int pageSize = 5;

            if (name != null)
            {
                return _appDbContext.Categoria
                    .Where(u => u.Nombre.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(u => u.Foto)
                    .GetPaged(page, pageSize);
            }
            else
            {
                return _appDbContext.Categoria
                    .OrderBy(u => u.Foto)
                    .GetPaged(page, pageSize);
            }
        }

        public async Task<Categoria?> UpdateUpload(Categoria upload)
        {
            var result = await _appDbContext.Categoria.FirstOrDefaultAsync(u => u.Id==upload.Id);
            if (result!=null)
            {
                // Update existing upload
                _appDbContext.Entry(result).CurrentValues.SetValues(upload);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Upload not found");
            }
            return result;
        }
    }
}