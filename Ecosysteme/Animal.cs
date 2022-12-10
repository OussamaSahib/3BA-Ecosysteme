using System;

//CLASSE ANIMAL
namespace Ecosysteme
{
    public class Animal: SimulationObjet
    {
        public Animal(double x, double y, int energie): base(Colors.Red, x, y, energie){}

        public override void Update()
        {
            //PAS RANDOM
            //Nb de pas
            int pas= 30;
            //Pour X: Rien, Gauche, Droite
            List<int>numbersX= new List<int>() {0, -pas, pas};
            //Pour X: Rien, Bas, Haut
            List<int>numbersY= new List<int>() {0, -pas, pas};

            Random rnd= new Random();
            //Pour X et Y: Choisir 1'élément aléatoire de la liste de pas
            int randIndexX= rnd.Next(numbersX.Count);
            int randomX= numbersX[randIndexX];
            int randIndexY= rnd.Next(numbersY.Count);
            int randomY= numbersY[randIndexY];

            X= X +randomX;
            Y= Y +randomY;

            //ENERGIE QUI DIMINUE
            if(Energie>0)
            {Energie= Energie-5;}
            if(Energie<=0) 
            {Energie= 0;}
        }
    }
}
