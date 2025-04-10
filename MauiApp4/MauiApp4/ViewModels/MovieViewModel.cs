using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MauiApp4.Models;
using MauiApp4.Services;

namespace MauiApp4.ViewModels
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string searchTitle;
        public string SearchTitle
        {
            get => searchTitle;
            set
            {
                searchTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchTitle)));
            }
        }

        private Movie movie;
        public Movie Movie
        {
            get => movie;
            set
            {
                movie = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Movie)));
            }
        }

        public ICommand SearchMovieCommand { get; }

        private readonly MovieService movieService;

        public MovieViewModel()
        {
            movieService = new MovieService();
            SearchMovieCommand = new Command(async () => await SearchMovie());
        }

        private async Task SearchMovie()
        {
            Movie = await movieService.GetMovieAsync(SearchTitle);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
