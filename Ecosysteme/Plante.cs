﻿using System;

//CLASSE PLANTE
namespace Ecosysteme
{
    public class Plante: EtreVivant
    {
        public Plante(double x, double y, int energie, int vie1, int vie2): base(Colors.Green, x, y, energie, vie1, vie2){}

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
        {  //ENERGIE+VIE QUI DIMINUE
           Energie_Vie();
        }
    }
    }

