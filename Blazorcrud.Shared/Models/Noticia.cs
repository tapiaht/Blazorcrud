using System.ComponentModel.DataAnnotations;

namespace Blazorcrud.Shared.Models
{
    public class Noticia
    {
        [Key]
        public int NoticiaId { get; set; }
        public string Titulo { get; set; } = default!;
        public string Body {get; set;} = default!;
        public Gender Gender {get; set;}
        public string IdCategoria {get; set;} = default!;
        public bool IsDeleting {get; set;} = default!;
        public List<Address> Addresses {get; set;} = default!;
    }
}