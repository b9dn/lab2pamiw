using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using P04WeatherForecastAPI.Client.Commands;
using P04WeatherForecastAPI.Client.DataSeeders;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using P04WeatherForecastAPI.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    // przekazywanie wartosci do innego formularza 
    public partial class MainViewModelV4 : ObservableObject, INotifyPropertyChanged
    {
        private Weather _weather;
        private readonly IAccuWeatherService _accuWeatherService;
        private readonly IFavoriteCityService _favoriteCityService;
        private readonly FavoriteCityViewModel _favoriteCityViewModel;
        private FavoriteCitiesView _favoriteCitiesView;
        //public ICommand LoadCitiesCommand { get;  }


        public MainViewModelV4(IAccuWeatherService accuWeatherService, IFavoriteCityService favoriteCityService, FavoriteCityViewModel favoriteCityViewModel, FavoriteCitiesView favoriteCitiesView) {
            _favoriteCitiesView = favoriteCitiesView;
            _favoriteCityViewModel = favoriteCityViewModel;
            _accuWeatherService = accuWeatherService;
            _favoriteCityService = favoriteCityService;
            Cities = new ObservableCollection<CityViewModel>();
        }

        //[ObservableProperty]
        //private WeatherViewModel weatherView;
        //public WeatherViewModel WeatherView { 
        //    get { return weatherView; } 
        //    set { 
        //        weatherView = value;
        //        OnPropertyChanged();
        //    }
        //}


        private WeatherViewModel weatherView;
        public WeatherViewModel WeatherView {
            get { return weatherView; }
            set {
                weatherView = value;
                OnPropertyChanged();
            }
        }


        private CityViewModel _selectedCity;
        public CityViewModel SelectedCity {
            get => _selectedCity;
            set {
                _selectedCity = value;
                OnPropertyChanged();
                LoadWeather();
            }
        }


        private async void LoadWeather()
        {
            if(SelectedCity != null)
            {
                _weather = await _accuWeatherService.GetCurrentConditions(SelectedCity.Key); 
                WeatherView = new WeatherViewModel(_weather);
            }
        }


        // podajście nr 2 do przechowywania kolekcji obiektów:
        public ObservableCollection<CityViewModel> Cities { get; set; }

        [RelayCommand]
        public async void LoadCities(string locationName)
        {
            // podejście nr 2:
            var cities = await _accuWeatherService.GetLocations(locationName);
            Cities.Clear();
            foreach (var city in cities) 
                Cities.Add(new CityViewModel(city));
        }

        [RelayCommand]
        public void OpenFavotireCities() {
            _favoriteCitiesView = App.serviceProvider.GetService<FavoriteCitiesView>();
            _favoriteCitiesView.Show();
        }

        [RelayCommand]
        public void AddToFavorites() {
            _favoriteCityService.AddFavoriteCity(new FavoriteCity() { Name = _selectedCity.LocalizedName });
        }
    }
}
