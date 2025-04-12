
## Hands-On Lab: Acesso às Funcionalidades da Plataforma com .NET MAUI

### Objetivo
Criar uma aplicação simples que acessa funcionalidades nativas como geolocalização, lanterna e Text-to-Speech utilizando APIs nativas disponibilizadas pelo .NET MAUI.

---

### Passo 1 – Configuração inicial

Crie um novo projeto .NET MAUI no Visual Studio:

- Escolha **.NET MAUI App**.
- Nomeie o projeto como `PlatformFeaturesApp`.
- Clique em **Criar**.

---

### Passo 2 – Criar a Interface Principal

Abra o arquivo `MainPage.xaml` e defina uma interface simples com botões para acessar funcionalidades:

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="PlatformFeaturesApp.MainPage"
             Title="Funcionalidades Nativas">

    <StackLayout Padding="20" Spacing="15">

        <Button Text="Obter Localização" Clicked="OnGetLocationClicked" />

        <Button Text="Ligar Lanterna" Clicked="OnFlashlightOnClicked" />

        <Button Text="Desligar Lanterna" Clicked="OnFlashlightOffClicked" />

        <Entry x:Name="TextEntry" Placeholder="Digite algo para falar" />
        <Button Text="Falar Texto" Clicked="OnSpeakTextClicked" />

    </StackLayout>

</ContentPage>
```

---

### Passo 3 – Implementar Código C#

Abra `MainPage.xaml.cs` e implemente os handlers:

```csharp
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnGetLocationClicked(object sender, EventArgs e)
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            await DisplayAlert("Localização", $"Latitude: {location.Latitude}, Longitude: {location.Longitude}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private async void OnFlashlightOnClicked(object sender, EventArgs e)
    {
        try
        {
            await Flashlight.TurnOnAsync();
        }
        catch (FeatureNotSupportedException)
        {
            await DisplayAlert("Erro", "Lanterna não suportada no dispositivo.", "OK");
        }
    }

    private async void OnFlashlightOffClicked(object sender, EventArgs e)
    {
        await Flashlight.TurnOffAsync();
    }

    private async void OnSpeakTextClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TextEntry.Text))
        {
            await TextToSpeech.SpeakAsync(TextEntry.Text);
        }
        else
        {
            await DisplayAlert("Erro", "Digite algum texto para falar.", "OK");
        }
    }
}
```

---

### Passo 4 – Executar o Aplicativo

- Compile e execute a aplicação no emulador ou em um dispositivo físico.
- Teste cada funcionalidade:
  - Clique em **Obter Localização** para visualizar as coordenadas atuais.
  - Use os botões **Ligar Lanterna** e **Desligar Lanterna**.
  - Insira um texto no campo e clique em **Falar Texto** para ouvir o dispositivo reproduzindo o texto digitado.

---

### Passo 5 – Verificar permissões no Android e iOS

Garanta que as seguintes permissões estejam configuradas para Android em `AndroidManifest.xml`:

```xml
<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
<uses-permission android:name="android.permission.FLASHLIGHT" />
```

Para iOS, no arquivo `Info.plist`, certifique-se de adicionar:

```xml
<key>NSLocationWhenInUseUsageDescription</key>
<string>Precisamos acessar sua localização para mostrar as coordenadas.</string>
```

---

### Conclusão

Neste laboratório você aprendeu a acessar funcionalidades nativas utilizando APIs .NET MAUI, testando geolocalização, controle da lanterna e síntese de voz (Text-to-Speech). Agora você pode explorar outras APIs nativas para expandir ainda mais seu conhecimento.
