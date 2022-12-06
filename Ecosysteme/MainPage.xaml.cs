namespace Ecosysteme;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    /*FCT BOUTTON START*/
    private async void OnButton_Start(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Page_Jeu());
    }

    /*FCT BOUTTON EXIT*/
    private void OnButton_Exit(object sender, EventArgs e)
    { System.Environment.Exit(0);}

}

