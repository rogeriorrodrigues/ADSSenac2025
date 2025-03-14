using System.Text.Json;
using MauiApp3.DTO;
using Steeltoe.Common.Http;

namespace MauiApp3;

public partial class GridPage : ContentPage
{
    public GridPage()
    {
        InitializeComponent();
    }

    private async void BtnConsultarCep_OnClicked(object? sender, EventArgs e)
    {
        string url = $"https://viacep.com.br/ws/{txtCep.Text}/json/";
        var result = await FetchDataFromAPI(url);
        if (result != null)
        {
            await DisplayAlert("Erro", result, "OK");
        }
    }

    private async Task<string> FetchDataFromAPI(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetStringAsync(url);
            if (response != null)
            {
                var viaCepDTO = JsonSerializer.Deserialize<ViaCepDTO>(response);
                if (viaCepDTO != null)
                {
                    lblLogradouro.Text = viaCepDTO.logradouro;
                    lblBairro.Text = viaCepDTO.bairro;
                    lblCidade.Text = viaCepDTO.localidade;
                    lblEstado.Text = viaCepDTO.uf;
                    return null; // No error
                }
                else
                {
                    return "Erro ao desserializar o JSON.";
                }
            }
            else
            {
                return "Erro ao acessar a API.";
            }
        }
    }
}