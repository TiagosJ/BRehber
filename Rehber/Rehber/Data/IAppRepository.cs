using Rehber.Models;
using System.Collections.Generic;

namespace Rehber.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity);
        void Update<T>(T entity);   
        void Delete<T>(T entity);
        bool SaveAll();

        List<City> GetCities();
        List<Photo> GetPhotosByCity(int cityId);
        City GetCityById(int cityId);
        Photo GetPhoto(int id);
    }
}
