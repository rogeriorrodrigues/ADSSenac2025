using Aula3.DTO;
using Newtonsoft.Json;

namespace Aula3
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnFetchDataClicked(object sender, EventArgs e)
        {
            string url = "https://viacep.com.br/ws/"+ txtCep.Text.ToString() +"/json/";
            var result = await FetchDataFromApi(url);
            ResultLabel.Text = result;
        }

        private async Task<string> FetchDataFromApi(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<ViaCepDTO>(response);
                return
                    $"CEP: {data.Cep}, Logradouro: {data.Logradouro}, Bairro: {data.Bairro}, Localidade: {data.Localidade}, UF: {data.Uf}";
            }
        }
    }
}
