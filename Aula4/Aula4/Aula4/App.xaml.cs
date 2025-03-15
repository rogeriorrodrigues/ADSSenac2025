﻿using Aula4.navigation;

namespace Aula4
{
    public partial class App : Application
    {
        public App(INavigationService navigationService)
        {
            InitializeComponent();
            MainPage = new AppShell(navigationService);
        }
    }
}
