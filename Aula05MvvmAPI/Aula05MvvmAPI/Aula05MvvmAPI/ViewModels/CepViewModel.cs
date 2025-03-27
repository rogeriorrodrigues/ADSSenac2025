using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Aula05MvvmAPI.Models;
using Aula05MvvmAPI.Services;

namespace Aula05MvvmAPI.ViewModels
{
    public class CepViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _cep;
        public string Cep
        {
            get => _cep;
            set
            {
                _cep = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cep)));
            }
        }

        private CepModel endereco;
        public CepModel Endereco
        {
            get => endereco;
            set
            {
                endereco = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Endereco)));
            }
        }

        public ICommand BuscarCepCommand { get; }
        private readonly ViaCepService viacepservice;

        public CepViewModel()
        {
            viacepservice = new ViaCepService();
            BuscarCepCommand = new Command(async () => await BuscarCep());
        }
        private async Task BuscarCep()
        {
            if (!string.IsNullOrWhiteSpace(Cep))
                Endereco = await viacepservice.GetCepAsync(Cep);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
