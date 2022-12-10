using System;

//FICHIER POUR DESSIN DES ELEMENTS -->(COULEUR, X, Y, ENERGIE)
namespace Ecosysteme
{
    public class DessinObjet
    {
        Color color;
        double x, y;
        int energie;
        int vie1;
        int vie2;

        public DessinObjet(Color color, double x, double y, int energie, int vie1, int vie2)
        {
            this.color = color;
            this.x = x;
            this.y = y;
            this.energie = energie;
            this.vie1 = vie1;
            this.vie2 = vie2;
        }

        public Color Color {get{return this.color;}}
        public double X {get{return this.x;} set{this.x= value;}}
        public double Y {get{return this.y;} set{this.y= value;}}
        public int Energie {get{return this.energie;} set{this.energie= value;}}
        public int Vie1 {get{return this.vie1;} set{this.vie1= value;}}
        public int Vie2 {get{return this.vie2;} set {this.vie2= value;}}
    }
}
