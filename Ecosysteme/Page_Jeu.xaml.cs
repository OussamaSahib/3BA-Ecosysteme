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
        timer.Interval= TimeSpan.FromMilliseconds(500);
        timer.Tick += this.OnTimeEvent;
        timer.Start();
    }

    //Fct Timer qui update la page
    private void OnTimeEvent(object source, EventArgs e)
    {
        simulation.Update();
        graphics.Invalidate();
    }

    /*FCT BOUTTON AJOUTER ANIMAL*/
    private void OnButton_AddAnimal(object sender, EventArgs e)
    {
        simulation.Add_Animal();
    }

    /*FCT BOUTTON AJOUTER PLANTE*/
    private void OnButton_AddPlante(object sender, EventArgs e)
    {
        simulation.Add_Plante();
     }
}