//FICHIER REPRENANT SIMULATIONOBJET POUR Y INTEGRER LES ELEMENTS(-->Animal, Plante, ...)
namespace Ecosysteme
{
    //NB: Pour IDrawable, ds Page_Jeu.xaml, ds ContentPage, rajouter lien
    public class Simulation: IDrawable
    {
        //Liste de Simultion Objet, auquel on rajoute les éléments par défaut
        List<SimulationObjet> objects;
        public Simulation()
        {
            objects= new List<SimulationObjet>();

            objects.Add(new Animal(300, 100));
            objects.Add(new Plante(50, 100));
        }

        //Dessin dans Simulation
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            foreach (SimulationObjet drawable in objects)
            {
                canvas.FillColor = drawable.Color;
                canvas.FillCircle(new Point(drawable.X, drawable.Y), 27.0);
            }
        }

        //Update
        public void Update()
        {
            foreach (SimulationObjet drawable in objects)
            {
                drawable.Update();
            }
        }
    }
}
