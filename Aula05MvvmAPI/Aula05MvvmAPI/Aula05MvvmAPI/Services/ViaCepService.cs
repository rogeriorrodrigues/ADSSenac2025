using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Aula05MvvmAPI.Models;

namespace Aula05MvvmAPI.Services
{
    public class ViaCepService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<CepModel> GetCepAsync(string cep)
        {
            var response = await _httpClient.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            return JsonSerializer.Deserialize<CepModel>(response);

        }
    }
}
