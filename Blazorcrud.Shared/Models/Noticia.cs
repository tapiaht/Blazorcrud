using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazorcrud.Shared.Models
{
    public class Noticia
    {
        [Key]
        public int NoticiaId { get; set; }
        public string Titulo { get; set; } = default!;
        public string Body {get; set;} = default!;
        [ForeignKey("IdCategoria")]
        public string IdCategoria {get; set;} = default!;
        public bool IsDeleting {get; set;} = default!;
        
        public virtual Categoria IdCategoriaNavigation { get; set; }


    }
}