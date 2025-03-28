
# Hands-on Lab: Aplicação Intermediária MVVM com Lista de Tarefas em .NET MAUI 

## Introdução
Neste laboratório, vocês irão criar uma aplicação intermediária utilizando o padrão MVVM em .NET MAUI para gerenciar uma lista de tarefas. Este projeto enfatizará conceitos como Binding, comandos, gerenciamento de estado e persistência básica.

## Pré-requisitos
- Visual Studio 2022 com .NET MAUI.

## Etapa 1: Configuração Inicial
1. Abra o Visual Studio 2022.
2. Crie um projeto **.NET MAUI App** chamado `ListaTarefasApp`.

## Etapa 2: Modelo de Dados
Crie a pasta Models e adicione o modelo Tarefa:

```csharp
public class Tarefa
{
    public string Nome { get; set; }
    public bool Concluida { get; set; }
}
```

## Etapa 3: ViewModel
Crie a pasta ViewModels e adicione TarefaViewModel:

```csharp
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

public class TarefaViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string nomeTarefa;
    public string NomeTarefa
    {
        get => nomeTarefa;
        set
        {
            nomeTarefa = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomeTarefa)));
        }
    }

    public ObservableCollection<Tarefa> Tarefas { get; set; }

    public ICommand AdicionarTarefaCommand { get; }
    public ICommand RemoverTarefaCommand { get; }

    public TarefaViewModel()
    {
        Tarefas = new ObservableCollection<Tarefa>();

        AdicionarTarefaCommand = new Command(AdicionarTarefa);
        RemoverTarefaCommand = new Command<Tarefa>(RemoverTarefa);
    }

    private void AdicionarTarefa()
    {
        if (!string.IsNullOrWhiteSpace(NomeTarefa))
        {
            Tarefas.Add(new Tarefa { Nome = NomeTarefa, Concluida = false });
            NomeTarefa = string.Empty;
        }
    }

    private void RemoverTarefa(Tarefa tarefa)
    {
        Tarefas.Remove(tarefa);
    }
}
```

## Etapa 4: Interface Gráfica
Edite MainPage.xaml:

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewmodel="clr-namespace:ListaTarefasApp.ViewModels"
             x:Class="ListaTarefasApp.MainPage">

    <ContentPage.BindingContext>
        <viewmodel:TarefaViewModel />
    </ContentPage.BindingContext>

    <StackLayout Padding="20">
        <Entry Placeholder="Nova Tarefa" Text="{Binding NomeTarefa}" />
        <Button Text="Adicionar" Command="{Binding AdicionarTarefaCommand}" />

        <CollectionView ItemsSource="{Binding Tarefas}">
            <CollectionView.ItemTemplate>
                <DataTemplate>
                    <SwipeView>
                        <SwipeView.RightItems>
                            <SwipeItems>
                                <SwipeItem Text="Excluir" BackgroundColor="Red" Command="{Binding Source={RelativeSource AncestorType={x:Type viewmodel:TarefaViewModel}}, Path=RemoverTarefaCommand}" CommandParameter="{Binding .}" />
                            </SwipeItems>
                        </SwipeView.RightItems>
                        <StackLayout Orientation="Horizontal">
                            <CheckBox IsChecked="{Binding Concluida}" />
                            <Label Text="{Binding Nome}" VerticalOptions="Center" />
                        </StackLayout>
                    </SwipeView>
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>
    </StackLayout>
</ContentPage>
```

## Etapa 5: Execução e Testes
- Execute o aplicativo.
- Adicione, marque e remova tarefas.

## Etapa 6: Desafio Intermediário
- Implemente a persistência das tarefas usando preferências locais.
- Adicione opções para ordenar e filtrar tarefas.
- Documente o projeto e publique-o no GitHub.
