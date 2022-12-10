using System;

//FICHIER POUR DESSIN DES ELEMENTS -->(COULEUR, X, Y, ENERGIE)
namespace Ecosysteme
{
    public class DessinObjet
    {
        Color color;
        double x, y;
        int energie;

        public DessinObjet(Color color, double x, double y, int energie)
        {
            this.color= color;
            this.x= x;
            this.y= y;
            this.energie= energie;
        }

        public Color Color {get{return this.color;}}
        public double X {get{return this.x;} set{this.x= value;}}
        public double Y {get{return this.y;} set{this.y= value;}}
        public int Energie {get{return this.energie;} set{this.energie= value;}}
    }
}
