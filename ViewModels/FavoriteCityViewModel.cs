using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class FavoriteCityViewModel : ObservableObject, INotifyPropertyChanged {

        private IFavoriteCityService _favoriteCityService;

        public ObservableCollection<FavoriteCity> FavoriteCities { get; set; }


        private FavoriteCity _selectedCity;


        public FavoriteCityViewModel(IFavoriteCityService favoriteCityService)
        {
            _favoriteCityService = favoriteCityService;
            FavoriteCities = new ObservableCollection<FavoriteCity>();
            foreach (FavoriteCity city in _favoriteCityService.GetAllFavoriteCities()) {
                FavoriteCities.Add(city);
            }
        }

        [RelayCommand]
        public void ClearFavoriteCities() {
            FavoriteCities.Clear();
            _favoriteCityService.RemoveAll();
        }

        [RelayCommand]
        public void removeCity(String Name) {
            FavoriteCity toRemove = new FavoriteCity { Name = Name };
            FavoriteCities.Remove(toRemove);
            _favoriteCityService.RemoveFavoriteCity(Name);
        }

    }
}
