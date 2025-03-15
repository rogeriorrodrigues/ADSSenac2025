using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula4.navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigationAsync(string route);
    }
}
