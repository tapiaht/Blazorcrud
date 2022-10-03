using Blazorcrud.Shared.Models;
using Bogus;
using System.Diagnostics;

namespace Blazorcrud.Server.Models
{
    public class DataGenerator
    {
        public static void Initialize(AppDbContext appDbContext)
        {
            
            Randomizer.Seed = new Random(32321);
            Debug.Print("Aqui pasa algo");
            if (!(appDbContext.Noticia.Any()))
                {


                // Create new people
                var testPeople = new Faker<Blazorcrud.Shared.Models.Noticia>()
                    .RuleFor(p => p.Titulo, f => f.Lorem.Lines(1))
                    .RuleFor(p => p.Body, f => f.Lorem.Lines(5))
                    .RuleFor(p => p.IdCategoria, f => f.Random.Number(15));
                        
                    var people = testPeople.Generate(255);

                    foreach (Blazorcrud.Shared.Models.Noticia p in people)
                    {
                        appDbContext.Noticia.Add(p);
                    }
                    appDbContext.SaveChanges();
                }

            if (!(appDbContext.Categoria.Any()))
            {
                string jsonRecord = @"[{""FirstName"": ""Mundo perdido en america"",""LastName"": ""Estamos a unos minutos de ver .."",""Gender"": 1,""PhoneNumber"": ""2"",
                    ""Addresses"": [{""Street"": ""415 McKee Place"",""City"": ""Pittsburgh"",""State"": ""Pennsylvania"",""ZipCode"": ""15140""
                    },{ ""Street"": ""315 Gunpowder Road"",""City"": ""Mechanicsburg"",""State"": ""Pennsylvania"",""ZipCode"": ""17101""  }]}]";
                var testUploads = new Faker<Categoria>()
                    .RuleFor(u => u.Nombre, u => u.Commerce.Product())
                    .RuleFor(u => u.Foto, u => u.Commerce.ProductDescription());
                var uploads = testUploads.Generate(20);

                foreach (Categoria u in uploads)
                {
                    appDbContext.Categoria.Add(u);
                }
                appDbContext.SaveChanges();
            }

            if (!(appDbContext.User.Any()))
            {
                var testUsers = new Faker<User>()
                    .RuleFor(u => u.FirstName, u => u.Name.FirstName())
                    .RuleFor(u => u.LastName, u => u.Name.LastName())
                    .RuleFor(u => u.Username, u => u.Internet.UserName())
                    .RuleFor(u => u.Password, u => u.Internet.Password());
                var users = testUsers.Generate(100);

                User customUser = new User(){
                    FirstName = "Terry",
                    LastName = "Smith",
                    Username = "admin",
                    Password = "admin"
                };

                users.Add(customUser);

                foreach (User u in users)
                {
                    u.PasswordHash = BCrypt.Net.BCrypt.HashPassword(u.Password);
                    u.Password = "**********";
                    appDbContext.User.Add(u);
                }
                appDbContext.SaveChanges();
            }
        }
    }
}