using System;

//CLASSE ANIMAL
namespace Ecosysteme
{
    public class Animal: SimulationObjet
    {
        public Animal(double x, double y): base(Colors.Red, x, y){}

        public override void Update()
        {
            //Nb de pas
            X= X + 30;
        }
    }
}
