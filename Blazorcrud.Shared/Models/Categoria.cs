using System.ComponentModel.DataAnnotations;

namespace Blazorcrud.Shared.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Foto {get; set;} = default!;
        public virtual ICollection<Noticia> Noticias { get; set; }
 
    }
}