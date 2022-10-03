using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blazorcrud.Server.Models
{
    public class NoticiaRepository:INoticiaRepository
    {
        private readonly AppDbContext _appDbContext;

        public NoticiaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Noticia> AddNoticia(Noticia noticia)
        {
            var result = await _appDbContext.Noticia.AddAsync(noticia);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Noticia?> DeleteNoticia(int noticiaId)
        {
            var result = await _appDbContext.Noticia.FirstOrDefaultAsync(p => p.NoticiaId == noticiaId);
            if (result!=null)
            {
                _appDbContext.Noticia.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("noticia not found");
            }
            return result;
        }

        public async Task<Noticia?> GetNoticia(int noticiaId)
        {
            var result = await _appDbContext.Noticia
                //.Select(t => new Noticia
                //{
                //    NoticiaId = t.NoticiaId,
                //    Titulo = t.Titulo,
                //    Body = t.Body,
                //    IdCategoria = t.IdCategoria
                //})
                .FirstOrDefaultAsync(p => p.NoticiaId == noticiaId);
            if (result != null)
            {
                //Debug.Print("EStoy aqui pero no ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZz");
                return result;
            }
            else
            {
                throw new KeyNotFoundException("noticia not found");
            }
        }

        public PagedResult<Noticia> GetNoticia(string? name, int page)
        {
            int pageSize = 5;

            if (name != null)
            {
                Debug.Print("estoy aqui");

                return _appDbContext.Noticia
                    .Where(p => p.Titulo.Contains(name, StringComparison.CurrentCultureIgnoreCase) ||
                        p.Body.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(p => p.NoticiaId)
                    .GetPaged(page, pageSize);
            }
            else
            {
                Debug.Print("por else xxxxxxxxxxxxxxxxxxxxxxzXXXXXXXXXXXXXXXXXXXXXx");

                return _appDbContext.Noticia
                       .Select(t => new Noticia
                       {
                           NoticiaId = t.NoticiaId,
                           Titulo = t.Titulo,
                           Body = t.Body,
                           IdCategoria = t.IdCategoria
                       })
                     .OrderBy(p => p.NoticiaId)
                    .GetPaged(page, pageSize);
             }   
        }

        public async Task<Noticia?> UpdateNoticia(Noticia noticia)
        {
            var result = await _appDbContext.Noticia.FirstOrDefaultAsync(u => u.NoticiaId== noticia.NoticiaId);
            //var result = await _appDbContext.Noticia
                               
                //.Include("Categoria").FirstOrDefaultAsync(p => p.NoticiaId == noticia.NoticiaId);
            if (result!=null)
            {
                var currency1 = _appDbContext.Noticia.Find(noticia.NoticiaId);
                currency1.Titulo = noticia.Titulo;
                currency1.Body = noticia.Body;
                Debug.Print("Al parecer hay datos     " + currency1.Titulo);
                // Update existing noticia
                _appDbContext.Entry(result).CurrentValues.SetValues(noticia);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("noticia not found");
            }
            return result;
        }
    }
}