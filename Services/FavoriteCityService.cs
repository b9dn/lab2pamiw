using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    internal class FavoriteCityService : IFavoriteCityService
    {
        private List<FavoriteCity> _favoriteCities;

        public FavoriteCityService() {
            _favoriteCities = new List<FavoriteCity>();
        }

        public List<FavoriteCity> GetAllFavoriteCities() {
            return _favoriteCities;
        }

        public void AddFavoriteCity(FavoriteCity city) {
            if (!_favoriteCities.Any(x => x.Name == city.Name)) {
                _favoriteCities.Add(city);
            }
        }

        public void RemoveAll() {
            _favoriteCities.Clear();
        }

        public void RemoveFavoriteCity(String Name) {
            var cityToRemove = _favoriteCities.SingleOrDefault(city => city.Name == Name);
            if (cityToRemove != null) {
                _favoriteCities.Remove(cityToRemove);
            }
        }
    }
}
