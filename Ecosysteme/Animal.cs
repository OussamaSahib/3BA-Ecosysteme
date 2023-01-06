using Microsoft.Maui.Controls;
using System;

//CLASSE ANIMAL
namespace Ecosysteme
{
    public class Animal: EtreVivant
    {
        //VARIABLES UTILISEES DS SIMULTATION
        //GENRE
        public string Genre{get;set;}= "inconnu";
        //ENCEINTE
        public int gestationCompteur{get;set;}= 0;
        public int gestation{get;set;}= 50;
        public bool isPregnant{get;set;}= false;
        //REPOS ENCEINTE
        public int repos{get;set;}= 0;
        //AGE
        public int age{get;set;}= 0;
        public int ageLimit{get;set;}= 35;
        public bool isChild =>age<ageLimit;



        public Animal(Color color, double x, double y, int energie, int vie1, int vie2, string genre= "inconnu"): base(color, x, y, energie, vie1, vie2)
        {
            //Paramètre GENRE attribué aleatoirement
            if (genre=="inconnu")
            {
                List<string> genres= new List<string>() {"male","femelle"};
                Random rnd= new Random();
                int randIndex= rnd.Next(genres.Count);
                Genre= genres[randIndex];
            }
            else
            {Genre= genre;}
        }


        //PAS RANDOM
        public void Move()
        {   
            //Nb de pas
            int pas= 20;
            //Pour X: Rien, Gauche, Droite
            List<int>numbersX= new List<int>(){0, -pas, pas};
            //Pour X: Rien, Bas, Haut
            List<int> numbersY= new List<int>(){ 0, -pas, pas };

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
            {Energie= Energie-1;}
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
        {   //PAS RANDOM (TANT QUE PAS ENCEINTE ET PAS EN REPOS)
            if(!isPregnant && repos==0)
            {Move();}

            //ENERGIE+VIE QUI DIMINUE
            Energie_Vie();

            //AGE AUGMENTE
            age++;

            //REPOS APRES ENCEINTE DIMINUE
            if(repos>0)
            {repos--;}
        }
    }
}
