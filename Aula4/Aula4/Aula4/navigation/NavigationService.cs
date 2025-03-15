using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula4.navigation
{
    public class NavigationService : INavigationService
    {
        public Task InitializeAsync()
        {
            var route = "home";
            if (!string.IsNullOrEmpty(route))
                route = "//Main/Main";

            return NavigationAsync(route);
        }

        public Task NavigationAsync(string route)
        {
            return Shell.Current.GoToAsync(route);
        }
    }
}
