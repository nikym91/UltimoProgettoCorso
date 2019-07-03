using System;
using System.Linq;
using EF_DB_Layer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsClubModel;

namespace SportsClubWeb.Infrastructure
{
    public static class IWebHostExtensions
    {
        public static IWebHost EnsureMigrationsandPopulate(this IWebHost host)
        {
            using (var scope = host.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                    if (context.Fields.Any())
                    {
                        foreach (var item in context.Fields)
                        {
                            var f = item.Name;
                        }
                        return host;
                    }

                    var res = new Reservation()
                    {
                        Date = DateTime.Now,
                        IsChallenge = false,
                        IsDouble = false,
                        Price = 30.00m,
                        Sport = "Tennis",
                        TimeStart = DateTime.Now,
                        TimeEnd = DateTime.Now.AddHours(1),
                        User = new User()
                        {
                            FirstName = "Francesca",
                            LastName = "Sabbi",
                            BirthDate = DateTime.Now.AddYears(-20),
                            DateOfRegistration = DateTime.Now,
                            Address = "Via alla piazza della salita 26R",
                            Email = "ciccio@gmail.com",
                            PhoneNumber = "010-123456"
                        },
                        Field = new TennisCourt()
                        {
                            Name = "Wimbledon",
                            Surface = Surfaces.Grass,
                            Price = 30.00m,
                            Players = 2
                        }
                    };

                    var res1 = new Reservation()
                    {
                        Date = DateTime.Now,
                        IsChallenge = false,
                        IsDouble = true,
                        Price = 25.00m,
                        Sport = "Tennis",
                        TimeStart = DateTime.Now,
                        TimeEnd = DateTime.Now.AddHours(1),
                        User = new User()
                        {
                            FirstName = "Nicoletto",
                            LastName = "Magi",
                            BirthDate = DateTime.Now.AddYears(-27),
                            DateOfRegistration = DateTime.Now,
                            Address = "Piazza della salita della via 27",
                            Email = "ciccio@gmail.com",
                            PhoneNumber = "010-123456"
                        },
                        Field = new TennisCourt()
                        {
                            Name = "Roland Garros",
                            Surface = Surfaces.Clay,
                            Price = 25.00m,
                            Players = 2
                        }
                    };

                    var res2 = new Reservation()
                    {
                        Date = DateTime.Now,
                        IsChallenge = false,
                        IsDouble = false,
                        Price = 20.00m,
                        Sport = "Paddle",
                        TimeStart = DateTime.Now,
                        TimeEnd = DateTime.Now.AddHours(1),
                        User = new User()
                        {
                            FirstName = "Valeria",
                            LastName = "Passerini",
                            BirthDate = DateTime.Now.AddYears(-27),
                            DateOfRegistration = DateTime.Now,
                            Address = "Piazza della salita della via 27",
                            Email = "ciccio@gmail.com",
                            PhoneNumber = "010-123456"
                        },
                        Field = new PaddleCourt()
                        {
                            Name = "Oslo",
                            Surface = Surfaces.Concrete,
                            Price = 20.00m,
                            Players = 2
                        },
                    };

                    var res3 = new Reservation()
                    {
                        Date = DateTime.Now,
                        IsChallenge = false,
                        IsDouble = false,
                        Price = 20.00m,
                        Sport = "Paddle",
                        TimeStart = DateTime.Now,
                        TimeEnd = DateTime.Now.AddHours(1),
                        User = new User()
                        {
                            FirstName = "Stefania",
                            LastName = "Beltrami",
                            BirthDate = DateTime.Now.AddYears(-22),
                            DateOfRegistration = DateTime.Now,
                            Address = "Piazza della piazza nella piazza 21",
                            Email = "ciccio@gmail.com",
                            PhoneNumber = "010-123456"
                        },
                        Field = new PaddleCourt()
                        {
                            Name = "Dubai",
                            Surface = Surfaces.Concrete,
                            Price = 20.00m,
                            Players = 2
                        }
                    };

                    var res4 = new Reservation()
                    {
                        Date = DateTime.Now,
                        IsChallenge = false,
                        IsDouble = false,
                        Price = 50.00m,
                        Sport = "Soccer",
                        TimeStart = DateTime.Now,
                        TimeEnd = DateTime.Now.AddHours(1),
                        User = new User()
                        {
                            FirstName = "Cristina",
                            LastName = "Del Pizzo",
                            BirthDate = DateTime.Now.AddYears(-22),
                            DateOfRegistration = DateTime.Now,
                            Address = "Piazza della piazza nella piazza 21",
                            Email = "ciccio@gmail.com",
                            PhoneNumber = "010-123456"
                        },
                        Field = new SoccerField()
                        {
                            Name = "Old Trafford",
                            Surface = Surfaces.Grass,
                            Price = 50.00m,
                            Players = 7,
                            IsSeven = true
                        },
                    };

                    var res5 = new Reservation()
                    {
                        Date = DateTime.Now,
                        IsChallenge = false,
                        IsDouble = false,
                        Price = 75.00m,
                        Sport = "Soccer",
                        TimeStart = DateTime.Now,
                        TimeEnd = DateTime.Now.AddHours(1),
                        User = new User()
                        {
                            FirstName = "Federica",
                            LastName = "Pessina",
                            BirthDate = DateTime.Now.AddYears(-26),
                            DateOfRegistration = DateTime.Now,
                            Address = "Piazza della piazza nella piazza 21",
                            Email = "ciccio@gmail.com",
                            PhoneNumber = "010-123456"
                        },
                        Field = new SoccerField()
                        {
                            Name = "Fra Strabbi",
                            Surface = Surfaces.Grass,
                            Price = 75.00m,
                            Players = 5,
                            IsSeven = false
                        }
                    };

                    context.Reservations.Add(res);
                    context.Reservations.Add(res1);
                    context.Reservations.Add(res2);
                    context.Reservations.Add(res3);
                    context.Reservations.Add(res4);
                    context.Reservations.Add(res5);
                    context.SaveChanges();
                }
            }
            return host;
        }
    }
}