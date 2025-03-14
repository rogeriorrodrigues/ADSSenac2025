using MauiApp3.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;

namespace MauiApp3.ViewModels
{
    public class ListViewPageViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private ObservableCollection<Card> _cards;
        private bool _isLoading;

        public ObservableCollection<Card> Cards
        {
            get => _cards;
            set
            {
                _cards = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadCardsCommand { get; }

        public ListViewPageViewModel()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.magicthegathering.io/v1/")
            };
            Cards = new ObservableCollection<Card>();
            LoadCardsCommand = new Command(async () => await LoadCardsAsync());
        }

        private async Task LoadCardsAsync()
        {
            try
            {
                IsLoading = true;
                var response = await _httpClient.GetAsync("cards");
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var cardDictionary = JsonSerializer.Deserialize<Dictionary<string, List<Card>>>(responseString);

                if (cardDictionary != null && cardDictionary.TryGetValue("cards", out var cards))
                {
                    var filteredCards = cards.Where(c => !string.IsNullOrEmpty(c.imageUrl)).ToList();
                    Cards = new ObservableCollection<Card>(filteredCards);
                }
                else
                {
                    // Handle no cards found
                }
            }
            catch (Exception ex)
            {
                // Handle error
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
