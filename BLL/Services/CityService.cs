using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CityService : ICityService
    {
        private readonly DataContext db;
        private bool disposed = false;
        private IMapper Mapper => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<City, CityDTO>().ReverseMap();
            cfg.CreateMap<Order, OrderDTO>();
        }).CreateMapper();

        public CityService(DataContext db) => this.db = db;

        public IEnumerable<CityDTO> GetAll() => Mapper.Map<IEnumerable<City>, List<CityDTO>>(db.Cities);
        public CityDTO Get(Guid id) => Mapper.Map<City, CityDTO>(db.Cities.Find(id));
        public void Create(CityDTO item)
        {
            if (db.Cities.Find(item.Id) == null)
                db.Cities.Add(Mapper.Map<CityDTO, City>(item));
        }
        public void Update(CityDTO item)
        {
            var city = db.Cities.Find(item.Id);
            if (city != null)
            {
                city.Name = item.Name;
                db.Cities.Update(city);
            }
        }
        public void Delete(Guid id)
        {
            var city = db.Cities.Find(id);
            if (city != null)
                db.Cities.Remove(city);
        }
        public void Save() => db.SaveChanges();
        public async Task<IEnumerable<CityDTO>> GetAllAsync() => Mapper.Map<IEnumerable<City>, List<CityDTO>>(await db.Cities.ToListAsync());
        public async Task<CityDTO> GetAsync(Guid id) => Mapper.Map<City, CityDTO>(await db.Cities.FindAsync(id));
        public async Task CreateAsync(CityDTO item)
        {
            if (db.Cities.Find(item.Id) == null)
            {
                db.Cities.Add(Mapper.Map<CityDTO, City>(item));
                await db.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(CityDTO item)
        {
            var city = await db.Cities.FindAsync(item.Id);
            if (city != null)
            {
                city.Name = item.Name;
                db.Cities.Update(city);
                await db.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            var city = await db.Cities.FindAsync(id);
            if (city != null)
            {
                db.Cities.Remove(city);
                await db.SaveChangesAsync();
            }
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
