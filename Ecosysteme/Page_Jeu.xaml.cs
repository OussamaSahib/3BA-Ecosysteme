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
}