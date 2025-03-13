namespace MauiApp2
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
            {
                CounterBtn.Text = $"Clicked {count} time";
                lblBemVindo.Text = "Deu certo turma";

            }
            else
            {
                CounterBtn.Text = $"Clicked {count} times";
                lblTeste.Text = "Enfim programando";

            }
                

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

    }

}
