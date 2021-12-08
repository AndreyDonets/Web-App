using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext db;
        private bool disposed = false;
        private IMapper Mapper => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Order, OrderDTO>().ReverseMap();
            cfg.CreateMap<City, CityDTO>();
        }).CreateMapper();

        public OrderService(DataContext db) => this.db = db;

        public IEnumerable<OrderDTO> GetAll() => Mapper.Map<IEnumerable<Order>, List<OrderDTO>>(db.Orders.Include(x => x.City));
        public OrderDTO Get(Guid id) => Mapper.Map<Order, OrderDTO>(db.Orders.Include(x => x.City).FirstOrDefault(x => x.Id == id));
        public void Create(OrderDTO item)
        {
            if (db.Orders.Find(item.Id) == null)
            {
                item.Price = CalcPrice(item);
                db.Orders.Add(Mapper.Map<OrderDTO, Order>(item));
            }
        }
        public void Update(OrderDTO item)
        {
            var order = db.Orders.Find(item.Id);
            if (order != null)
            {
                item.Price = CalcPrice(item);
                order.FirstName = item.FirstName;
                order.LastName = item.LastName;
                order.Weight = item.Weight;
                order.CityId = item.CityId;
                order.SendType = item.SendType;
                order.Comment = item.Comment;
                order.Price = item.Price;
                db.Orders.Update(order);
            }
        }
        public void Delete(Guid id)
        {
            var order = db.Orders.Find(id);
            if (order != null)
                db.Orders.Remove(order);
        }
        public void Save() => db.SaveChanges();
        public async Task<IEnumerable<OrderDTO>> GetAllAsync() => Mapper.Map<IEnumerable<Order>, List<OrderDTO>>(await db.Orders.Include(x => x.City).ToListAsync());
        public async Task<OrderDTO> GetAsync(Guid id) => Mapper.Map<Order, OrderDTO>(await db.Orders.Include(x => x.City).FirstOrDefaultAsync(x => x.Id == id));
        public async Task CreateAsync(OrderDTO item)
        {
            if (db.Orders.Find(item.Id) == null)
            {
                item.Price = CalcPrice(item);
                db.Orders.Add(Mapper.Map<OrderDTO, Order>(item));
                await db.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(OrderDTO item)
        {
            var order = await db.Orders.FindAsync(item.Id);
            if (order != null)
            {
                order.FirstName = item.FirstName;
                order.LastName = item.LastName;
                order.Weight = item.Weight;
                order.CityId = item.CityId;
                order.SendType = item.SendType;
                order.Comment = item.Comment;
                order.Price = CalcPrice(item);
                db.Orders.Update(order);
                await db.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            var order = await db.Orders.FindAsync(id);
            if (order != null)
            {
                db.Orders.Remove(order);
                await db.SaveChangesAsync();
            }
        }

        private decimal CalcPrice(OrderDTO order)
        {
            var city = db.Cities.Find(order.CityId).Name;
            decimal costPerKg = 30;
            decimal deliveryCostToCity = 0;
            switch (city)
            {
                case "Киев":
                    deliveryCostToCity = 60;
                    break;
                case "Харьков":
                    deliveryCostToCity = 40;
                    break;
                case "Полтава":
                    deliveryCostToCity = 20;
                    break;
            }
            return deliveryCostToCity + (decimal)order.Weight * costPerKg;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                db.Dispose();
                disposed = true;
            }
        }
    }
}
