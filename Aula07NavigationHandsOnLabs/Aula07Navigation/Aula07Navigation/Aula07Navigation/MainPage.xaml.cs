using Aula07Navigation.Viewmodel;

namespace Aula07Navigation
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

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

}
