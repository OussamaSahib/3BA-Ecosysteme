using System;

//CLASSE PLANTE
namespace Ecosysteme
{
    public class Plante: SimulationObjet
    {
        public Plante(double x, double y, int energie): base(Colors.Green, x, y, energie){}

        public override void Update()
        {
            //ENERGIE QUI DIMINUE
            if (Energie>0)
            {Energie= Energie-5;}
            if(Energie<=0)
            {Energie= 0;}
        }
    }
}
