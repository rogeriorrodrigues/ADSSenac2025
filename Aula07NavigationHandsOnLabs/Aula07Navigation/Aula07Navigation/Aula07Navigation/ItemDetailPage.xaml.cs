namespace Aula07Navigation;

[QueryProperty(nameof(ItemId), "id")]
[QueryProperty(nameof(ItemName), "name")]
public partial class ItemDetailPage : ContentPage
{
	public ItemDetailPage()
	{
		InitializeComponent();
	}

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