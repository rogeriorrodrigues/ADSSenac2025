
# Hands-on Lab: Aplicação Avançada com Consumo da API Magic: The Gathering, Alertas e MVVM em .NET MAUI

## Introdução
Neste laboratório avançado, vocês irão construir uma aplicação completa utilizando o padrão MVVM em .NET MAUI. A aplicação consumirá dados detalhados da API REST do Magic: The Gathering, incluirá filtragem avançada, exibição de detalhes das cartas em alertas e uma interface aprimorada.

## Pré-requisitos
- Visual Studio 2022 com .NET MAUI.

## Etapa 1: Configuração Inicial
1. Abra o Visual Studio 2022.
2. Crie um projeto **.NET MAUI App** chamado `MagicCardsApp`.

## Etapa 2: Modelo Detalhado de Dados

```csharp
public class Card
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string ManaCost { get; set; }
    public string Text { get; set; }
    public string Rarity { get; set; }
    public string SetName { get; set; }
    public string ImageUrl { get; set; }
    public string Power { get; set; }
    public string Toughness { get; set; }
}
```

## Etapa 3: Serviço para Consumo da API

```csharp
using System.Net.Http;
using System.Text.Json;

public class MagicService
{
    private readonly HttpClient _client = new HttpClient();

    public async Task<List<Card>> GetCardsAsync(string name = "", string type = "")
    {
        var url = $"https://api.magicthegathering.io/v1/cards?name={name}&type={type}";
        var response = await _client.GetStringAsync(url);
        var json = JsonDocument.Parse(response);
        return json.RootElement.GetProperty("cards").Deserialize<List<Card>>();
    }
}
```

## Etapa 4: ViewModel com Separação Lógica Completa

```csharp
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

public class CardsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string searchName;
    public string SearchName
    {
        get => searchName;
        set
        {
            searchName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchName)));
        }
    }

    private string searchType;
    public string SearchType
    {
        get => searchType;
        set
        {
            searchType = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchType)));
        }
    }

    private ObservableCollection<Card> cards;
    public ObservableCollection<Card> Cards
    {
        get => cards;
        set
        {
            cards = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cards)));
        }
    }

    public ICommand SearchCardsCommand { get; }

    private readonly MagicService magicService;

    public CardsViewModel()
    {
        magicService = new MagicService();
        Cards = new ObservableCollection<Card>();
        SearchCardsCommand = new Command(async () => await SearchCards());
    }

    private async Task SearchCards()
    {
        var results = await magicService.GetCardsAsync(SearchName, SearchType);
        Cards.Clear();
        foreach (var card in results)
            Cards.Add(card);
    }
}
```

## Etapa 5: Interface Aprimorada

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewmodel="clr-namespace:MagicCardsApp.ViewModels"
             x:Class="MagicCardsApp.MainPage">

    <ContentPage.BindingContext>
        <viewmodel:CardsViewModel />
    </ContentPage.BindingContext>

    <StackLayout Padding="15" Spacing="10">
        <Entry Placeholder="Nome da Carta" Text="{Binding SearchName}" />
        <Entry Placeholder="Tipo da Carta" Text="{Binding SearchType}" />
        <Button Text="Buscar Cartas" Command="{Binding SearchCardsCommand}" />

        <CollectionView ItemsSource="{Binding Cards}">
            <CollectionView.ItemTemplate>
                <DataTemplate>
                    <Frame Margin="5" BorderColor="LightGray">
                        <StackLayout Orientation="Horizontal" Spacing="10">
                            <Image Source="{Binding ImageUrl}" HeightRequest="100" WidthRequest="70" />
                            <StackLayout>
                                <Label Text="{Binding Name}" FontAttributes="Bold" />
                                <Label Text="Tipo: {Binding Type}" />
                                <Label Text="Raridade: {Binding Rarity}" />
                                <Button Text="Detalhes" Clicked="OnDetailsClicked" />
                            </StackLayout>
                        </StackLayout>
                    </Frame>
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>
    </StackLayout>
</ContentPage>
```

## Etapa 6: Alertas Detalhados no Code-Behind

```csharp
private async void OnDetailsClicked(object sender, EventArgs e)
{
    var button = sender as Button;
    var card = button.BindingContext as Card;

    await DisplayAlert(card.Name, $"Mana: {card.ManaCost}\nTipo: {card.Type}\nTexto: {card.Text}\nRaridade: {card.Rarity}\nSet: {card.SetName}\nPoder/Resistência: {card.Power}/{card.Toughness}", "OK");
}
```

## Etapa 7: Testes e Ajustes
- Executem e testem busca e detalhes.

## Etapa 8: Desafio Avançado
- Adicione paginação dos resultados.
- Melhore visualmente utilizando estilos e recursos gráficos.
- Documente e publique no GitHub.
