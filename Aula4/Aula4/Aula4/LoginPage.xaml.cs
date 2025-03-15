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
            DisplayAlert("Login de Usuário", "Usuário ou senha inválidos", "Ok!");
        else
        {
            _settingsService.AuthAccessToken = response.Token;

            DisplayAlert("Login de Usuário", string.Format("Usuário {0} logado com sucesso", response.FirstName), "Ok!");

            await _navigationService.NavigationAsync("//Main/Main");
        }
    }
}

