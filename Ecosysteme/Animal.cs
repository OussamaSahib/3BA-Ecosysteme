using System;

//CLASSE ANIMAL
namespace Ecosysteme
{
    public class Animal: EtreVivant
    {
        public Animal(double x, double y, int energie, int vie1, int vie2): base(Colors.Red, x, y, energie, vie1, vie2){}

        //PAS RANDOM
        public void Move()
        {   //Nb de pas
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
        }
        


        //ENERGIE+VIE QUI DIMINUE
        public void Energie_Vie()
        {   //Energie qui diminue
            if(Energie>0)
            {Energie= Energie-5;}
            if(Energie<0) 
            {Energie= 0;}

            //Vie qui diminue
            if(Energie==0 && Vie1!=0 && Vie2!=0)
            {Vie1= 0;
            Energie= 75;}

            if(Energie==0 && Vie1==0 && Vie2!=0)
            {Vie2= 0;
            Energie= 0;}
        }



        public override void Update()
        {   //PAS RANDOM
            Move();

            //ENERGIE+VIE QUI DIMINUE
            Energie_Vie();

        }
    }
}
