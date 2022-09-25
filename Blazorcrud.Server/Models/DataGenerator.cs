using Blazorcrud.Shared.Models;
using Bogus;

namespace Blazorcrud.Server.Models
{
    public class DataGenerator
    {
        public static void Initialize(AppDbContext appDbContext)
        {
            Randomizer.Seed = new Random(32321);
            if (!(appDbContext.Noticia.Any()))
                {
                    //Create test addresses
                    var testAddresses = new Faker<Address>()
                        .RuleFor(a => a.Street, f => f.Address.StreetAddress())
                        .RuleFor(a => a.City, f => f.Address.City())
                        .RuleFor(a => a.State, f => f.Address.State())
                        .RuleFor(a => a.ZipCode, f => f.Address.ZipCode());

                    // Create new people
                    var testPeople = new Faker<Blazorcrud.Shared.Models.Noticia>()
                        .RuleFor(p => p.Titulo, f => f.Lorem.Lines(1))
                        .RuleFor(p => p.Body, f => f.Lorem.Lines(5))
                        .RuleFor(p => p.Gender, f => f.PickRandom<Gender>())
                        .RuleFor(p => p.IdCategoria, f => f.Random.Number(15).ToString()
                        )
                        .RuleFor(p => p.Addresses, f => testAddresses.Generate(2).ToList());
                        
                    var people = testPeople.Generate(25);

                    foreach (Blazorcrud.Shared.Models.Noticia p in people)
                    {
                        appDbContext.Noticia.Add(p);
                    }
                    appDbContext.SaveChanges();
                }

            if (!(appDbContext.Uploads.Any()))
            {
                string jsonRecord = @"[{""FirstName"": ""Mundo perdido en america"",""LastName"": ""Estamos a unos minutos de ver .."",""Gender"": 1,""PhoneNumber"": ""2"",
                    ""Addresses"": [{""Street"": ""415 McKee Place"",""City"": ""Pittsburgh"",""State"": ""Pennsylvania"",""ZipCode"": ""15140""
                    },{ ""Street"": ""315 Gunpowder Road"",""City"": ""Mechanicsburg"",""State"": ""Pennsylvania"",""ZipCode"": ""17101""  }]}]";
                var testUploads = new Faker<Upload>()
                    .RuleFor(u => u.FileName, u => u.Lorem.Word()+".json")
                    .RuleFor(u => u.UploadTimestamp, u => u.Date.Past(1, DateTime.Now))
                    .RuleFor(u => u.ProcessedTimestamp, u => u.Date.Future(1, DateTime.Now))
                    .RuleFor(u => u.FileContent, Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jsonRecord)));
                var uploads = testUploads.Generate(8);

                foreach (Upload u in uploads)
                {
                    appDbContext.Uploads.Add(u);
                }
                appDbContext.SaveChanges();
            }

            if (!(appDbContext.Users.Any()))
            {
                var testUsers = new Faker<User>()
                    .RuleFor(u => u.FirstName, u => u.Name.FirstName())
                    .RuleFor(u => u.LastName, u => u.Name.LastName())
                    .RuleFor(u => u.Username, u => u.Internet.UserName())
                    .RuleFor(u => u.Password, u => u.Internet.Password());
                var users = testUsers.Generate(4);

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
                    appDbContext.Users.Add(u);
                }
                appDbContext.SaveChanges();
            }
        }
    }
}