namespace Aula07Features
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGetLocationClicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                await DisplayAlert("Localização", $"Latitude: {location.Latitude}, Longitude: {location.Longitude}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void OnFlashlightOnClicked(object sender, EventArgs e)
        {
            try
            {
                await Flashlight.TurnOnAsync();
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Erro", "Lanterna não suportada no dispositivo.", "OK");
            }
        }

        private async void OnFlashlightOffClicked(object sender, EventArgs e)
        {
            await Flashlight.TurnOffAsync();
        }

        private async void OnSpeakTextClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextEntry.Text))
            {
                await TextToSpeech.SpeakAsync(TextEntry.Text);
            }
            else
            {
                await DisplayAlert("Erro", "Digite algum texto para falar.", "OK");
            }
        }
    }

}
