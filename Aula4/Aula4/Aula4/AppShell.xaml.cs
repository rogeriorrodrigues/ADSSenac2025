using Aula4.navigation;

namespace Aula4
{
    public partial class AppShell : Shell
    {
        private readonly INavigationService _navigationService;
        public AppShell(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeRouting();
            InitializeComponent();
        }
        protected override async void OnHandlerChanged()
        {
            base.OnHandlerChanged();

            if (Handler is not null)
            {
                await _navigationService.InitializeAsync();
            }
        }

        public static void InitializeRouting()
        {
            Routing.RegisterRoute("cadastro", typeof(CadastroPage));
            Routing.RegisterRoute("home", typeof(MainPage));
            Routing.RegisterRoute("login", typeof(LoginPage));
        }
    }
}
