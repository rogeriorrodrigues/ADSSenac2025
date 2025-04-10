
# Hands-on Lab: Aplicação Avançada com Consumo da OMDb API (Filmes), Alertas e MVVM em .NET MAUI (3 horas)

## Introdução
Neste laboratório, vocês irão construir uma aplicação robusta usando .NET MAUI com o padrão MVVM. A aplicação consumirá dados detalhados da OMDb API, incluirá pesquisa avançada de filmes, exibição detalhada em alertas e uma interface interativa.

## Pré-requisitos
- Visual Studio 2022 com .NET MAUI.
- API Key gratuita obtida em [http://www.omdbapi.com/apikey.aspx](http://www.omdbapi.com/apikey.aspx)

## Etapa 1: Configuração Inicial
Crie um projeto **.NET MAUI App** chamado `FilmesApp`.

## Etapa 2: Modelo Detalhado de Dados;
Crie a pasta Models e adicione o modelo Movie:

```csharp
public class Movie
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string Genre { get; set; }
    public string Plot { get; set; }
    public string Director { get; set; }
    public string Actors { get; set; }
    public string Poster { get; set; }
    public string imdbRating { get; set; }
}
```

## Etapa 3: Serviço para Consumo da API
Crie a pasta Services e adicione a classe MovieService:

```csharp
using System.Net.Http;
using System.Text.Json;

public class MovieService
{
    private readonly HttpClient _client = new HttpClient();
    private const string apiKey = "966c4f4f";

    public async Task<Movie> GetMovieAsync(string title)
    {
        var response = await _client.GetStringAsync($"http://www.omdbapi.com/?t={title}&apikey={apiKey}");
        return JsonSerializer.Deserialize<Movie>(response);
    }
}
```

## Etapa 4: ViewModel com MVVM
Crie a pasta ViewModels e adicione MovieViewModel:


```csharp
using System.ComponentModel;
using System.Windows.Input;

public class MovieViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

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
}
```

## Etapa 5: Interface Gráfica Completa
Altere a MainPage para o layout abaixo. Lembre se que se for copiar todo o XAML, atente-se ao nome da aplicação que vocês criaram.

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewmodel="clr-namespace:FilmesApp.ViewModels"
             x:Class="FilmesApp.MainPage">

    <ContentPage.BindingContext>
        <viewmodel:MovieViewModel />
    </ContentPage.BindingContext>

    <StackLayout Padding="20" Spacing="15">
        <Entry Placeholder="Título do filme" Text="{Binding SearchTitle}" />
        <Button Text="Buscar Filme" Command="{Binding SearchMovieCommand}" />

        <Frame BorderColor="LightGray" Padding="10" IsVisible="{Binding Movie, Converter={StaticResource NullToVisibilityConverter}}">
            <StackLayout>
                <Image Source="{Binding Movie.Poster}" HeightRequest="200" />
                <Label Text="{Binding Movie.Title}" FontAttributes="Bold" FontSize="Medium" />
                <Label Text="Ano: {Binding Movie.Year}" />
                <Label Text="Gênero: {Binding Movie.Genre}" />
                <Label Text="Avaliação IMDB: {Binding Movie.imdbRating}" />
                <Button Text="Mais Detalhes" Clicked="OnDetailsClicked" />
            </StackLayout>
        </Frame>
    </StackLayout>
</ContentPage>
```

## Etapa 6: Alertas Detalhados no Code-Behind
No mesmo arquivo MainPage.xaml adicione um método para servir de alerta

```csharp
private async void OnDetailsClicked(object sender, EventArgs e)
{
    var vm = BindingContext as MovieViewModel;
    var movie = vm.Movie;

    await DisplayAlert(movie.Title, $"Diretor: {movie.Director}\nAtores: {movie.Actors}\nSinopse: {movie.Plot}", "OK");
}
```

## Etapa 7: Execução e Testes
Execute a aplicação, faça buscas e visualize detalhes.

## Etapa 8: Desafio Avançado
- Implemente armazenamento local dos filmes pesquisados.
- Adicione navegação entre múltiplas telas.
- Documente e publique no GitHub.
