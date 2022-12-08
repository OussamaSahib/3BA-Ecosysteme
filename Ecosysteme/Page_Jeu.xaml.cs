namespace Ecosysteme;

public partial class Page_Jeu: ContentPage
{
    Simulation simulation;
    public Page_Jeu()
	{
		InitializeComponent();
        simulation= Resources["simulation"] as Simulation;
    }
}