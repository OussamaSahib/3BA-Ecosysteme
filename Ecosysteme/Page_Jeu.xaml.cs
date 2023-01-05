namespace Ecosysteme;

//FICHIER LIE AVEC PAGE_JEU
public partial class Page_Jeu: ContentPage
{
    Simulation simulation;
    IDispatcherTimer timer;
    public Page_Jeu()
    {
        InitializeComponent();
        //Execute Fichier Simulation.cs
        simulation= Resources["simulation"] as Simulation;

        //Execute Fct Timer c-dessous
        timer= Dispatcher.CreateTimer();
        timer.Interval= TimeSpan.FromMilliseconds(600);
        timer.Tick+= this.OnTimeEvent;
        timer.Start();
    }

    //Fct Timer qui update la page
    private void OnTimeEvent(object source, EventArgs e)
    {
        simulation.Update();
        graphics.Invalidate();
    }

    /*FCT BOUTTON AJOUTER PLANTE*/
    private void OnButton_AddPlante(object sender, EventArgs e)
    {
        simulation.Add_Plante();
    }

    /*FCT BOUTTON AJOUTER ZEBRE*/
    private void OnButton_AddZebre(object sender, EventArgs e)
    {
        simulation.Add_Zebre();
    }

    /*FCT BOUTTON AJOUTER TIGRE*/
    private void OnButton_AddTigre(object sender, EventArgs e)
    {
        simulation.Add_Tigre();
    }
}