namespace MauiApp2;

public partial class Sobre : ContentPage
{
	public Sobre()
	{
		InitializeComponent();
	}

    private void BtnAlert_OnClicked(object? sender, EventArgs e)
    {
        DisplayPromptAsync("Questão 1", "Qual o seu nome?", "Sim", "Não", "Digite aqui o nome");

    }
}

