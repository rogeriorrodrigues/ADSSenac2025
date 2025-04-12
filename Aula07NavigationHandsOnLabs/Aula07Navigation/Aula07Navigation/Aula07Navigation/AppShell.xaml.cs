namespace Aula07Navigation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("itemdetail", typeof(ItemDetailPage));
        }
    }
}
