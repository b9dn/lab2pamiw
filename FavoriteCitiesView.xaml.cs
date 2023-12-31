﻿using P04WeatherForecastAPI.Client.Services;
using P04WeatherForecastAPI.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace P04WeatherForecastAPI.Client
{
    /// <summary>
    /// Interaction logic for FavoriteCitiesView.xaml
    /// </summary>
    public partial class FavoriteCitiesView : Window
    {
        public FavoriteCitiesView(FavoriteCityViewModel favoriteCityViewModel)
        {
            DataContext = favoriteCityViewModel;
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e) {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }
    }
}
