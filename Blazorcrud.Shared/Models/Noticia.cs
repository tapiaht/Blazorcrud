using System.ComponentModel.DataAnnotations;

namespace Blazorcrud.Shared.Models
{
    public class Noticia
    {
        [Key]
        public int PersonId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName {get; set;} = default!;
        public Gender Gender {get; set;}
        public string PhoneNumber {get; set;} = default!;
        public bool IsDeleting {get; set;} = default!;
        public List<Address> Addresses {get; set;} = default!;
    }
}