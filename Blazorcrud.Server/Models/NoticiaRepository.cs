using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;

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
                .Include(p => p.Addresses)
                .FirstOrDefaultAsync(p => p.NoticiaId == noticiaId);
            if (result != null)
            {
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
                return _appDbContext.Noticia
                    .Where(p => p.Titulo.Contains(name, StringComparison.CurrentCultureIgnoreCase) ||
                        p.Body.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(p => p.NoticiaId)
                    .Include(p => p.Addresses)
                    .GetPaged(page, pageSize);
            }
            else
            {
                return _appDbContext.Noticia
                    .OrderBy(p => p.NoticiaId)
                    .Include(p => p.Addresses)
                    .GetPaged(page, pageSize);
            }
        }

        public async Task<Noticia?> UpdatePerson(Noticia person)
        {
            var result = await _appDbContext.Noticia.Include("Addresses").FirstOrDefaultAsync(p => p.NoticiaId == person.NoticiaId);
            if (result!=null)
            {
                // Update existing person
                _appDbContext.Entry(result).CurrentValues.SetValues(person);
                
                // Remove deleted addresses
                foreach (var existingAddress in result.Addresses.ToList())
                {
                   if(!person.Addresses.Any(o => o.AddressId == existingAddress.AddressId))
                     _appDbContext.Addresses.Remove(existingAddress);
                }

                // Update and Insert Addresses
                 foreach (var addressModel in person.Addresses)
                 {
                    var existingAddress = result.Addresses
                        .Where(a => a.AddressId == addressModel.AddressId)
                        .SingleOrDefault();
                    if (existingAddress != null)
                        _appDbContext.Entry(existingAddress).CurrentValues.SetValues(addressModel);
                    else
                    {
                        var newAddress = new Address
                        {
                            AddressId = addressModel.AddressId,
                            Street = addressModel.Street,
                            City = addressModel.City,
                            State = addressModel.State,
                            ZipCode = addressModel.ZipCode
                        };
                        result.Addresses.Add(newAddress);
                    }
                }
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