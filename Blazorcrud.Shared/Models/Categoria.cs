using System.ComponentModel.DataAnnotations;

namespace Blazorcrud.Shared.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Noticia = new HashSet<Noticia>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Foto {get; set;} = default!;
        public virtual ICollection<Noticia> Noticia { get; set; }
 
    }
}