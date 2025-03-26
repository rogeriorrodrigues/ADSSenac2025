using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Aula05MvvmAlerts.ViewModels
{
    public class MensagemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string mensagem;
        public string Mensagem
        {
            get => mensagem;
            set
            {
                mensagem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mensagem)));
            }
        }

        public ICommand ExibirMensagemCommand { get; }

        public MensagemViewModel()
        {
            ExibirMensagemCommand = new Command(ExibirMensagem);
        }

        private void ExibirMensagem()
        {
            if (string.IsNullOrWhiteSpace((Mensagem)))
                Application.Current.MainPage.DisplayAlert("Erro", "Digite uma mensagem válida", "OK");
            else
                Application.Current.MainPage.DisplayAlert("Mensagem", Mensagem, "OK");
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
