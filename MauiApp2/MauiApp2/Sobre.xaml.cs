namespace MauiApp2;

public partial class Sobre : ContentPage
{
	public Sobre()
	{
		InitializeComponent();
	}

    private void BtnAlert_OnClicked(object? sender, EventArgs e)
    {
        DisplayPromptAsync("Quest�o 1", "Qual o seu nome?", "Sim", "N�o", "Digite aqui o nome");

    }
}

