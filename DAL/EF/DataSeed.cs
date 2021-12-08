using DAL.Entities;
using Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class DataSeed
    {
        public static async Task Seed(DataContext db)
        {
            if (!db.Orders.Any())
            {
                var cities = new List<City>
                {
                    new City
                    {
                        Name = "Киев"
                    },
                    new City
                    {
                        Name = "Харьков"
                    },
                    new City
                    {
                        Name = "Полтава"
                    }
                };

                foreach (var item in cities)
                    await db.Cities.AddAsync(item);

                var orders = new List<Order>
                {
                    new Order
                    {
                        FirstName ="Руслан",
                        LastName ="Демитов",
                        CityId = cities[0].Id,
                        Comment ="",
                        Weight = 68.5,
                        SendType = SendType.OversizedCargo
                    },
                    new Order
                    {
                        FirstName ="Владимир",
                        LastName ="Кузнецов",
                        CityId = cities[1].Id,
                        Comment = "не очень длинынй комментарий",
                        Weight = 8.5,
                        SendType = SendType.OverallCargo
                    },
                    new Order
                    {
                        FirstName ="Евгений",
                        LastName ="Шаповалов",
                        CityId = cities[2].Id,
                        Comment = "какой то очень длинный комментарий",
                        Weight = 0.1,
                        SendType = SendType.Letter
                    }
                };

                foreach (var item in orders)
                    await db.Orders.AddAsync(item);

                await db.SaveChangesAsync();
            }
        }
    }
}
