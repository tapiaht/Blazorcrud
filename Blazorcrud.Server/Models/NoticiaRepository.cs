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

        public async Task<Noticia> AddPerson(Noticia person)
        {
            var result = await _appDbContext.Noticia.AddAsync(person);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Noticia?> DeletePerson(int noticiaId)
        {
            var result = await _appDbContext.Noticia.FirstOrDefaultAsync(p => p.NoticiaId == noticiaId);
            if (result!=null)
            {
                _appDbContext.Noticia.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Person not found");
            }
            return result;
        }

        public async Task<Noticia?> GetPerson(int noticiaId)
        {
            var result = await _appDbContext.Noticia
                .Select(t => new Noticia
                {
                    NoticiaId = t.NoticiaId,
                    Titulo = t.Titulo,
                    Body = t.Body,
                    IdCategoria = t.IdCategoria
                })
                .FirstOrDefaultAsync(p => p.NoticiaId == noticiaId);
            if (result != null)
            {
                //Debug.Print("EStoy aqui pero no ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZz");
                return result;
            }
            else
            {
                throw new KeyNotFoundException("Person not found");
            }
        }

        public PagedResult<Noticia> GetPeople(string? name, int page)
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

        public async Task<Noticia?> UpdatePerson(Noticia person)
        {
            //var result = await _appDbContext.User.FirstOrDefaultAsync(u => u.Id == user.Id);
            var result = await _appDbContext.Noticia
                               
                .Include("Categoria").FirstOrDefaultAsync(p => p.NoticiaId == person.NoticiaId);
            if (result!=null)
            {
                // Update existing person
                _appDbContext.Entry(result).CurrentValues.SetValues(person);
                
                
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Person not found");
            }
            return result;
        }
    }
}