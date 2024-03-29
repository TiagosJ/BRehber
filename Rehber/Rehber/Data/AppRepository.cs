﻿using Microsoft.EntityFrameworkCore;
using Rehber.Models;
using System.Collections.Generic;
using System.Linq;

namespace Rehber.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context;

        public AppRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity)
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity)
        {
            _context.Remove(entity);
        }

        public List<City> GetCities()
        {
            var cities = _context.Cities.Include(c => c.Photos).Include(z => z.User).ToList();
            return cities;
        }

        public City GetCityById(int cityId)
        {
            var city = _context.Cities.Include(c => c.Photos).FirstOrDefault(c => c.Id == cityId);
            return city;

        }

        public Photo GetPhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(c => c.Id == id);
            return photo;

        }

        public List<Photo> GetPhotosByCity(int cityId)
        {
            var photos = _context.Photos.Where(p => p.CityId == cityId).ToList();
            return photos;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;

        }

        public void Update<T>(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
