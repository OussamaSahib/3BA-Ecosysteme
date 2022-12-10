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

            objects.Add(new Animal(900, 350));
            objects.Add(new Plante(600, 350));
        }

        //Dessin dans Simulation
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            foreach (SimulationObjet drawable in objects)
            {
                // ÊTRE VIVANT
                canvas.FillColor= drawable.Color;
                canvas.FillCircle(new Point(drawable.X, drawable.Y), 25.0);


                //BARRE D'ENERGIE
                //Barre grise
                canvas.FillColor= Colors.Gray;
                canvas.FillRoundedRectangle(Convert.ToSingle(drawable.X)-37, Convert.ToSingle(drawable.Y)-70, 75, 15, 5);
                
                //Barre d'energie
                canvas.FillColor = Colors.LimeGreen;
                canvas.FillRoundedRectangle(Convert.ToSingle(drawable.X)-37, Convert.ToSingle(drawable.Y)-70, 75, 15, 5);
             

                //COEURS DE VIE
                void Coeur(int posx, int posy)
                {
                    //2 cercles -->(x,y, largeur, hauteur, angle départ, angle fin, sens)
                    canvas.FillArc(Convert.ToSingle(drawable.X)-posx, Convert.ToSingle(drawable.Y)-65+15, 15, 15, 0, 180, false);
                    canvas.FillArc(Convert.ToSingle(drawable.X)-posx+15, Convert.ToSingle(drawable.Y)-65+15, 15, 15, 0, 180, false);
                    //Triangle -->sens horloger(pt à pt)
                    PathF path= new PathF();
                    path.MoveTo(Convert.ToSingle(drawable.X)-posx, Convert.ToSingle(drawable.Y)-65+15+Convert.ToSingle(7.5));
                    path.LineTo(Convert.ToSingle(drawable.X)-posx+15, Convert.ToSingle(drawable.Y)-65+40);
                    path.LineTo(Convert.ToSingle(drawable.X)-posx+30, Convert.ToSingle(drawable.Y)-65+15+Convert.ToSingle(7.5));
                    canvas.FillPath(path);
                }
                //COEUR DE VIE 1
                //Coeur gris
                canvas.FillColor= Colors.Gray;
                Coeur(35, 35);
                //Coeur de vie
                canvas.FillColor= Colors.Red;
                Coeur(35, 35);

                //COEUR DE VIE 2
                //Coeur gris
                canvas.FillColor= Colors.Gray;
                Coeur(0, 0);
                //Coeur de vie
                canvas.FillColor= Colors.Red;
                Coeur(0, 0);
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
