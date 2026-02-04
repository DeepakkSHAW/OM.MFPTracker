using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OM.MFPTracker.Data;
using OM.MFPTracker.Data.Models;
using OM.MFPTracker.Data.Services;
using System;

namespace ConsoleApp.Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=========Console App to test API's============");

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<MFPTrackerDbContext>(options =>
                        options.UseSqlite(context.Configuration.GetConnectionString("Sqlite")));

                    services.AddScoped<IPersonRepo, PersonRepo>();
                })
                .Build();

            //PersonRepo : IPersonRepo

            using (var scope = host.Services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IPersonRepo>();

                var db = scope.ServiceProvider.GetRequiredService<MFPTrackerDbContext>();

                //Alternative ways to do migration and seeding data
                //db.Database.Migrate();

                //if (!db.People.Any())
                //{
                //    db.People.Add(new Person
                //    {
                //        FirstName = "Rupam",
                //        LastName = "Shaw",
                //        DateOfBirth = new DateTime(1990, 1, 1)
                //    });

                //    db.SaveChanges();
                //}

                //var people = db.People.ToList();

                //foreach (var person in people)
                //{
                //    Console.WriteLine($"{person.Id}: {person.FirstName} {person.LastName}");
                //}
                var people = await repo.GetAllAsync();

                foreach (var p in people)
                {
                    Console.WriteLine($"{p.Id}: {p.FirstName} {p.LastName}");
                }

                // CREATE
                var person = await repo.AddAsync(new Person
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1992, 5, 10)
                });

                // READ
                var allPeople = await repo.GetAllAsync();
                Console.WriteLine($"Total people: {allPeople.Count}");

                // UPDATE
                person.LastName = "Smith";
                await repo.UpdateAsync(person);

                // DELETE
                await repo.DeleteAsync(person.Id);
            }
        }

    }
}
