using System;

//FICHIER POUR DESSIN DES ELEMENTS -->(COULEUR, X, Y)
namespace Ecosysteme
{
    public class DessinObjet
    {
        Color color;
        double x, y;
        public DessinObjet(Color color, double x, double y)
        {
            this.color= color;
            this.x= x;
            this.y= y;
        }

        public Color Color {get{return this.color;}}
        public double X {get{return this.x;} set{this.x= value;}}
        public double Y {get{return this.y;} set{this.y= value;}}
    }
}
