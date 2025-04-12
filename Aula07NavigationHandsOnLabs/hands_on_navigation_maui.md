
## Hands-On Lab: Navegação com .NET MAUI Shell

### Objetivo
Implementar uma aplicação básica utilizando a navegação do .NET MAUI Shell. O app terá duas páginas principais: uma página inicial com uma lista simples e uma página de detalhes que mostrará informações adicionais ao selecionar um item.

---

### Passo 1 – Criar o Projeto

Abra o Visual Studio e crie um projeto novo:

- Escolha **.NET MAUI App**.
- Nomeie como `NavigationApp`.
- Clique em **Criar**.

---

### Passo 2 – Configurar o Shell

Abra `AppShell.xaml` e defina a estrutura básica para navegação:

```xml
<Shell xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
       xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
       xmlns:local="clr-namespace:NavigationApp"
       x:Class="NavigationApp.AppShell">

    <ShellContent Title="Página Inicial" 
                  ContentTemplate="{DataTemplate local:MainPage}" />
</Shell>
```

---

### Passo 3 – Criar ViewModel e Modelo

Crie uma pasta chamada `ViewModels` e adicione uma classe chamada `ItemViewModel.cs`:

```csharp
public class ItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

Em seguida, adicione `MainPageViewModel.cs`:

```csharp
public class MainPageViewModel
{
    public ObservableCollection<ItemViewModel> Items { get; set; }

    public MainPageViewModel()
    {
        Items = new ObservableCollection<ItemViewModel>
        {
            new ItemViewModel { Id = 1, Name = "Item 1" },
            new ItemViewModel { Id = 2, Name = "Item 2" },
            new ItemViewModel { Id = 3, Name = "Item 3" }
        };
    }
}
```

---

### Passo 4 – Implementar MainPage.xaml

Edite `MainPage.xaml` para exibir uma lista e configurar navegação:

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="NavigationApp.MainPage"
             Title="Itens">

    <CollectionView ItemsSource="{Binding Items}"
                    SelectionMode="Single"
                    SelectionChanged="OnSelectionChanged">
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <StackLayout Padding="10">
                    <Label Text="{Binding Name}" FontSize="20" />
                </StackLayout>
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>

</ContentPage>
```

No arquivo `MainPage.xaml.cs`, configure a navegação:

```csharp
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as ItemViewModel;
        if (item == null)
            return;

        await Shell.Current.GoToAsync($"itemdetail?id={item.Id}&name={item.Name}");

        ((CollectionView)sender).SelectedItem = null;
    }
}
```

---

### Passo 5 – Criar a Página de Detalhes

Adicione uma nova página chamada `ItemDetailPage.xaml`:

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="NavigationApp.ItemDetailPage"
             Title="Detalhes do Item">

    <StackLayout Padding="20">
        <Label x:Name="ItemLabel" FontSize="24" FontAttributes="Bold" />
        <Button Text="Voltar" Clicked="OnBackButtonClicked" />
    </StackLayout>
</ContentPage>
```

Em `ItemDetailPage.xaml.cs` adicione:

```csharp
[QueryProperty(nameof(ItemId), "id")]
[QueryProperty(nameof(ItemName), "name")]
public partial class ItemDetailPage : ContentPage
{
    public string ItemId { get; set; }
    public string ItemName { get; set; }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ItemLabel.Text = $"Item: {ItemName} (ID: {ItemId})";
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
```

---

### Passo 6 – Registrar a Rota no Shell

Em `AppShell.xaml.cs`, registre a rota para `ItemDetailPage`:

```csharp
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("itemdetail", typeof(ItemDetailPage));
    }
}
```

---

### Passo 7 – Executar o App

- Compile e execute o aplicativo.
- Toque em um item da lista para navegar à página de detalhes.
- Utilize o botão **Voltar** para retornar à página inicial.

---

### Conclusão
Neste Hands-on Lab você implementou a navegação básica usando o .NET MAUI Shell, incluindo passagem de parâmetros entre páginas e retorno à página anterior. Agora você pode expandir essa solução com mais páginas e funcionalidades complexas.
