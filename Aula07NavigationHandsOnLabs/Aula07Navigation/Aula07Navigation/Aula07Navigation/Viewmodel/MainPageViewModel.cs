using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula07Navigation.Viewmodel
{
    public class MainPageViewModel
    {
        public ObservableCollection<ItemViewModel> Items { get; set; }

        public MainPageViewModel()
        {
            Items = new ObservableCollection<ItemViewModel>
            {
                new ItemViewModel { Id = 1, Name = "Item 1" },
                new ItemViewModel { Id = 2, Name = "Item 2" },
                new ItemViewModel { Id = 3, Name = "Item 3" }
            };
        }
    }
}
