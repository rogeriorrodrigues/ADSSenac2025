using Aula4.navigation;

namespace Aula4;

public partial class LoginPage : ContentPage
{
    private readonly INavigationService _navigationService;
    public LoginPage(INavigationService navigationService)
	{
		_navigationService = navigationService;
		InitializeComponent();
	}

    private async void Login_Clicked(object sender, EventArgs e)
    {
        var auth = new UserAuth()
        {
            Username = Username.Text,
            Password = Password.Text,
            ExpiresInMins = 30
        };

        var response = await _userService.Authenticate(auth);

        if (response == null)
            DisplayAlert("Login de Usu�rio", "Usu�rio ou senha inv�lidos", "Ok!");
        else
        {
            _settingsService.AuthAccessToken = response.Token;

            DisplayAlert("Login de Usu�rio", string.Format("Usu�rio {0} logado com sucesso", response.FirstName), "Ok!");

            await _navigationService.NavigationAsync("//Main/Main");
        }
    }
}

