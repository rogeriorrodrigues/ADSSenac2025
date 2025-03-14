namespace MauiApp3;

using MauiApp3.ViewModels;

public partial class ListViewPage : ContentPage
{
    public ListViewPage()
    {
        InitializeComponent();
        BindingContext = new ListViewPageViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ListViewPageViewModel viewModel)
        {
            viewModel.LoadCardsCommand.Execute(null);
        }
    }
}