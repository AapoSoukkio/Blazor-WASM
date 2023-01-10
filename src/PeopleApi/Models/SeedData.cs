using Microsoft.EntityFrameworkCore;
using PeopleApi.Data;
using PeopleLib;

namespace PeopleApi.Models;

    public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new PeopleContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<PeopleContext>>()))
        {
            if (context == null)
            {
                throw new ArgumentNullException("Null PeopleContext");
            }

            context.Database.EnsureCreated();

            
            if (context.People.Any())
            {
                return;   // DB has been seeded
                // To re-seed the db: delete the existing *.db file and let the app create a new one
            }

            context.People.AddRange(
                new Person {
                    Id = 1,
                    FirstName = "Jean",
                    LastName = "Riker",
                    Title = "Commander"
                },
                new Person {
                    Id = 2,
                    FirstName = "Deanna",
                    LastName = "Picard",
                    Title = "Counselor"
                },
                new Person {
                    Id = 3,
                    FirstName = "Sheldon",
                    LastName = "Young",
                    Title = "Scientist"
                },
                new Person {
                    Id = 4,
                    FirstName = "Leonard",
                    LastName = "Wolowitz",
                    Title = "Engineer"
                }
            );

            context.Addresses.AddRange(
                new Address {
                    Id = 10,
                    StreetAddress = "Boston blvd. 42",
                    PostalNumber = 45678,
                    PostalAddress = "Boston"
                },
                new Address {
                    Id = 20,
                    StreetAddress = "Starstreet 3",
                    PostalNumber = 00992,
                    PostalAddress = "New York"
                },
                new Address {
                    Id = 30,
                    StreetAddress = "Mainstreet 1",
                    PostalNumber = 12345,
                    PostalAddress = "Atlanta"
                },
                new Address {
                    Id = 40,
                    StreetAddress = "Homelane 22",
                    PostalNumber = 98989,
                    PostalAddress = "Toronto"
                }
            );

            context.ContactInfos.AddRange(
                new ContactInfo {
                    Id = 100,
                    AddressId = 10,
                    PersonId = 1,
                    Info = "Home address"
                },
                new ContactInfo {
                    Id = 200,
                    AddressId = 40,
                    PersonId = 1,
                    Info = "Office address"
                },
                new ContactInfo {
                    Id = 300,
                    AddressId = 20,
                    PersonId = 2,
                    Info = "Some address"
                },
                new ContactInfo {
                    Id = 400,
                    AddressId = 30,
                    PersonId = 3,
                    Info = "Summer cottage address"
                }
            );

            context.SaveChanges();
        }
    }
}
