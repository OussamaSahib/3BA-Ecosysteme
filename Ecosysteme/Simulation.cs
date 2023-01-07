//FICHIER REPRENANT SIMULATIONOBJET POUR Y INTEGRER LES ELEMENTS
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Text;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Ecosysteme
{
    //NB: Pour IDrawable, ds Page_Jeu.xaml, ds ContentPage, rajouter lien
    public class Simulation: IDrawable
    {
        //LISTE DE SIMULATIONOBJET, AUQUEL ON RAJOUTE LES ELEMENTS PAR DEFAUT
        List<SimulationObjet> objects;
        public Simulation()
        {
            objects= new List<SimulationObjet>();

            objects.Add(new Zebre(900, 500, 75, 15, 15));
            objects.Add(new Zebre(940, 500, 75, 15, 15));
            objects.Add(new Tigre(1400, 350, 75, 15, 15));
            objects.Add(new Buisson(400, 500,75, 15, 15));
            objects.Add(new Buisson(1300, 200, 75, 15, 15));

            //SI AJOUT DE NOUVELLES ESPECES:
            //0)Créer la Classe de la nouvelle espèce +Créer Animal ici
            //1)Si Herbivore:
            //Copier lignes du Zèbre, mais avec le nouvel animal voulu: Lignes 155-156(=FCT NAISSANCE); 195-196(=RENCONTRE MM ESPECE)
            //2)Si Carnivore:
            //Copier lignes du Lion, mais avec le nouvel animal voulu: Lignes 158-159(=FCT NAISSANCE); 255-256(=RENCONTRE MM ESPECE)
            //3)Si Plante:
            //Copier lignes du Buisson, mais avec la nouvelle plante voulu: Lignes 342-346(=ZONE SEMIS)
        }


        //FCTS BOUTTON
        //Position random dans la map
        Random rnd= new Random();
        int min_x= 0;
        int max_x= 1800;
        int min_y= 0;
        int max_y= 700;

        //FCT QUI AJOUTE 1 PLANTE (-->AVEC BOUTTON)
        public void Add_Buisson()
        {
            objects.Add(new Buisson(rnd.Next(min_x, max_x), rnd.Next(min_y, max_y), 75, 15, 15));
        }
        //FCT QUI AJOUTE 1 ZEBRE (-->AVEC BOUTTON)
        public void Add_Zebre()
        {
            objects.Add(new Zebre(rnd.Next(min_x, max_x), rnd.Next(min_y, max_y), 75, 15, 15));
        }
        //FCT QUI AJOUTE 1 TIGRE (-->AVEC BOUTTON)
        public void Add_Tigre()
        {
            objects.Add(new Tigre(rnd.Next(min_x, max_x), rnd.Next(min_y, max_y), 75, 15, 15));
        }





        //DESSIN DS SIMULATION
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            //MODIFICATION ELEMENT DE LA LISTE
            foreach(SimulationObjet item in objects.ToList())
            {
                //ANIMAL MORT-->VIANDE
                if(item is Animal && item.Vie2==0)
                {
                    objects.Remove(item);
                    objects.Add(new Viande(item.X, item.Y, 75));    
                }

                //PLANTE MORT ET VIANDE MORT-->DECHET
                if((item is Plante && item.Vie2==0) || (item.GetType()==typeof(Viande) && item.Vie_Viande==0))
                {
                    objects.Remove(item);
                    objects.Add(new Dechet(item.X, item.Y));
                }

                //ANIMAL CREE DES DECHETS REGULIERS
                if(item is Animal animal && animal.DechetCompteur>=animal.DechetSeuilCompteur)
                {
                    {objects.Add(new Dechet(item.X, item.Y));
                    animal.DechetCompteur= 0;}
                }



                //ZONES ANIMAL
                //Element rencontré
                foreach(SimulationObjet item2 in objects.ToList())
                {
                    //FCT ZONE CONTACT ANIMAL
                    bool ZoneContact(SimulationObjet AnimalZone, SimulationObjet ElemExt) 
                    {
                        //Zone Contact défini
                        double contactDistance= 50+20;
                        double minX= AnimalZone.X -contactDistance;
                        double maxX= AnimalZone.X +contactDistance;
                        double minY= AnimalZone.Y -contactDistance;
                        double maxY= AnimalZone.Y +contactDistance;

                        //Si Element ext. rentre ds la Zone Contact
                        if(ElemExt.X>=minX && ElemExt.X<=maxX && ElemExt.Y>=minY && ElemExt.Y<=maxY)
                        {return true;}
                        else 
                        {return false;}
                    }

                    //FCT ZONE VISION ANIMAL
                    bool ZoneVision(SimulationObjet AnimalZone, SimulationObjet ElemExt)
                    {
                        //Zone Vision défini
                        double visionDistance= 250+20;
                        double minX= AnimalZone.X -visionDistance;
                        double maxX= AnimalZone.X +visionDistance;
                        double minY= AnimalZone.Y -visionDistance;
                        double maxY= AnimalZone.Y +visionDistance;

                        //Si Element ext. rentre ds la Zone Vision
                        if (ElemExt.X>=minX && ElemExt.X<=maxX && ElemExt.Y>=minY && ElemExt.Y<=maxY)
                        {return true;}
                        else
                        {return false;}
                    }

                    //FCT NAISSANCE
                    void Naissance(SimulationObjet item, SimulationObjet item2)
                    {
                        if(item is Animal Animal && item2 is Animal Animal2)
                        { 
                            if((Animal.Genre=="male" && Animal2.Genre=="femelle") && (!Animal.isChild && !Animal2.isChild))
                            {
                                if(ZoneContact(item, item2)==true)
                                {
                                    if(!Animal2.isPregnant && Animal2.Genre=="femelle" && Animal2.repos==0)
                                    {Animal2.isPregnant= true;}
                                }
                            }

                            if(Animal2.isPregnant && Animal2.Genre=="femelle" && !Animal2.isChild)
                            {
                                Animal2.gestationCompteur++;
                                if (Animal2.gestationCompteur>=Animal2.gestationSeuilCompteur)
                                { 
                                    Animal2.isPregnant= false;
                                    Animal2.gestationCompteur= 0;
                                    Animal2.repos= 30;

                                    if(Animal is Zebre && Animal2 is Zebre)
                                    {objects.Add(new Zebre(Animal2.X+50, Animal2.Y, 75, 15, 15));}

                                    if(Animal is Tigre && Animal2 is Tigre)
                                    {objects.Add(new Tigre(Animal2.X+50, Animal2.Y, 75, 15, 15));}
                                }
                            }
                        }
                    }


                    //ZONE CONTACT HERBIVORE
                    if(item is Herbivore)
                    {
                        //RENCONTRE PLANTE
                        if(item2 is Plante)
                        {
                            //Si Vision: Herbivore se rapproche de Plante
                            if (ZoneVision(item, item2)==true)
                            {
                                double distanceX= item.X -item2.X;
                                double distanceY= item.Y -item2.Y;
                                item.X= item.X -(distanceX*0.2);
                                item.Y= item.Y -(distanceY*0.2);

                                //Si Contact: Manger +Full Energie-Vie
                                if(ZoneContact(item, item2)==true)
                                {
                                    item.X= item2.X;
                                    item.Y= item2.Y;
                                    item.Energie= 75;
                                    item.Vie1= 15;
                                    objects.Remove(item2);
                                }
                            }
                        }

                        //RENCONTRE HERBIVORE
                        if(item2 is Herbivore)
                        {   //Si mm Espece: Reproduction
                            if(item is Zebre && item2 is Zebre)
                            {Naissance(item, item2);}
                        }
                    }
                    


                    //ZONE CONTACT CARNIVORE
                    if(item is Carnivore)
                    {
                        //RENCONTRE VIANDE
                        if(item2.GetType()==typeof(Viande) && item.Vie2>0)
                        {
                            //Si Vision: Carnivore se rapproche de Viande
                            if(ZoneVision(item, item2)==true)
                            {
                                double distanceX= item.X -item2.X;
                                double distanceY= item.Y -item2.Y;
                                item.X= item.X -(distanceX*0.1);
                                item.Y= item.Y -(distanceY*0.1);

                                //Si Contact: Manger +Full Energie-Vie
                                if(ZoneContact(item, item2)==true)
                                {
                                    item.X= item2.X;
                                    item.Y= item2.Y;
                                    item.Energie= 75;
                                    item.Vie1= 15;
                                    objects.Remove(item2);
                                }
                            }
                        }
                        
                        //RENCONTRE HERBIVORE 
                        if(item2 is Herbivore)
                        {
                            //Si Vision: Carnivore se rapproche de Herbivore
                            if(ZoneVision(item, item2)==true)
                            {
                                double distanceX= item.X -item2.X;
                                double distanceY= item.Y -item2.Y;
                                item.X= item.X -(distanceX*0.2);
                                item.Y= item.Y -(distanceY*0.2);

                                //Si Contact: Tuer 
                                if(ZoneContact(item, item2)==true)
                                {
                                    item.X= item2.X;
                                    item.Y= item2.Y;
                                    item2.Energie= 0;
                                    item2.Vie1= 0;
                                    item2.Vie2= 0;
                                }
                            }
                        }

                        //RENCONTRE CARNIVORE
                        if(item2 is Carnivore)
                        {
                            //Si mm Espece: Reproduction
                            if(item is Tigre && item2 is Tigre)
                            {Naissance(item, item2);}

                            //Si pas mm Espece
                            else
                            {                                
                                //Si Vision: Carnivore se rapproche de Carnivore
                                if(ZoneVision(item, item2)==true)
                                {
                                    double distanceX= item.X -item2.X;
                                    double distanceY= item.Y -item2.Y;
                                    item.X= item.X -(distanceX*0.2);
                                    item.Y= item.Y -(distanceY*0.2);

                                    //Si Contact: Tuer 
                                    if(ZoneContact(item, item2)==true)
                                    {
                                        item.X= item2.X;
                                        item.Y= item2.Y;
                                        item2.Energie= 0;
                                        item2.Vie1= 0;
                                        item2.Vie2= 0;
                                    }
                                }
                            }
                        }
                    }
                }




                //ZONES PLANTE
                //Element rencontré
                foreach(SimulationObjet item2 in objects.ToList())
                {
                    //FCT ZONE RACINE PLANTE
                    bool ZoneRacine(SimulationObjet PlanteZone, SimulationObjet ElemExt) 
                    {
                        //Zone Racine défini
                        double contactDistance= 50+20;
                        double minX= PlanteZone.X -contactDistance;
                        double maxX= PlanteZone.X +contactDistance;
                        double minY= PlanteZone.Y -contactDistance;
                        double maxY= PlanteZone.Y +contactDistance;

                        //Si Element ext. rentre ds Zone Racine
                        if(ElemExt.X>=minX && ElemExt.X<=maxX && ElemExt.Y>=minY && ElemExt.Y<=maxY)
                        {return true;}
                        else 
                        {return false;}
                    }


                    //ZONE RACINE PLANTE
                    if(item is Plante)
                    {
                        //Si Rencontre Dechet: Manger +Full Energie-Vie
                        if(item2 is Dechet && item.Vie2>0)
                        {
                            if(ZoneRacine(item, item2)==true)
                            {                               
                                item.Energie= 75;
                                item.Vie1= 15;
                                objects.Remove(item2);
                            }
                        }
                    }

                    //ZONE SEMIS PLANTE
                    if(item is Plante Plante && Plante.NaissanceCompteur>=Plante.NaissanceSeuilCompteur)
                    {
                        //Liste des positions possibles des futurs plantes autour la plante Mère
                        List<Tuple<double,double>> positions= new List<Tuple<double,double>>
                        {
                            new Tuple<double,double>(item.X-75, item.Y),
                            new Tuple<double,double>(item.X+75, item.Y),
                            new Tuple<double,double>(item.X, item.Y-75),
                            new Tuple<double,double>(item.X, item.Y+75)
                        };

                        //Choix aléatoire  de la position de la liste
                        Random random= new Random();
                        int index= random.Next(positions.Count);
                        var position= positions[index];

                        //Naissance de 1 nouvelle plante (+Compteur remis à zero)
                        if(item is Buisson)
                        {
                            objects.Add(new Buisson(position.Item1, position.Item2, 75, 15, 15));
                            Plante.NaissanceCompteur = 0;
                        }                   
                    }
                }
            }




            //DESSIN
            foreach(SimulationObjet drawable in objects)
            {
                if((drawable is Animal || drawable is Plante) && drawable.Vie2!=0)
                {
                    //ÊTRE VIVANT
                    //ANIMAL
                    if(drawable is Animal animal)
                    {
                        //FORME ANIMAL
                        //Si Enfant
                        if(animal.isChild)
                        {canvas.FillColor= drawable.Color;
                         canvas.FillCircle(new Point(drawable.X, drawable.Y), 13.0);}

                        //Si Adulte
                        if(!animal.isChild)
                        {canvas.FillColor= drawable.Color;
                         canvas.FillCircle(new Point(drawable.X, drawable.Y), 25.0);}

                        //ZONE CONTACT ANIMAL
                        canvas.StrokeColor= Colors.Purple;
                        canvas.StrokeSize= 10;
                        canvas.DrawEllipse(Convert.ToSingle(drawable.X)-50, Convert.ToSingle(drawable.Y)-50, 100, 100);
                        
                        //ZONE VISION ANIMAL
                        canvas.StrokeColor= Colors.Yellow;
                        canvas.StrokeSize= 4;
                        canvas.DrawEllipse(Convert.ToSingle(drawable.X)-125, Convert.ToSingle(drawable.Y)-125, 250, 250);
                    }


                    //PLANTE
                    if(drawable is Plante)
                    {
                        //FORME PLANTE
                        canvas.FillColor= drawable.Color;
                        canvas.FillCircle(new Point(drawable.X, drawable.Y), 25.0);

                        //ZONE RACINE PLANTE
                        canvas.StrokeColor= Colors.DarkRed;
                        canvas.StrokeSize= 10;
                        canvas.DrawEllipse(Convert.ToSingle(drawable.X)-50, Convert.ToSingle(drawable.Y)-50, 100, 100);

                        //ZONE SEMIS PLANTE
                        canvas.StrokeColor= Colors.Gray;
                        canvas.StrokeSize= 10;
                        canvas.DrawEllipse(Convert.ToSingle(drawable.X)-100, Convert.ToSingle(drawable.Y)-100, 200, 200);
                    }





                    //BARRE D'ENERGIE
                    //Barre grise
                    canvas.FillColor= Colors.Gray;
                    canvas.FillRoundedRectangle(Convert.ToSingle(drawable.X)-37, Convert.ToSingle(drawable.Y)-70, 75, 15, 5);

                    //Barre d'energie
                    canvas.FillColor= Colors.LimeGreen;
                    canvas.FillRoundedRectangle(Convert.ToSingle(drawable.X)-37, Convert.ToSingle(drawable.Y)-70, drawable.Energie, 15, 5);


                    //COEURS DE VIE
                    void CoeurGris(int posx)
                    {
                        canvas.FillColor= Colors.Gray;
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

                    void CoeurRouge(int posx, float vie)
                    {
                        canvas.FillColor= Colors.Red;
                        //2 cercles -->(x,y, largeur, hauteur, angle départ, angle fin, sens)
                        canvas.FillArc(Convert.ToSingle(drawable.X)-posx, Convert.ToSingle(drawable.Y)-65+15, vie, vie, 0, 180, false);
                        canvas.FillArc(Convert.ToSingle(drawable.X)-posx+15, Convert.ToSingle(drawable.Y)-65+15, vie, vie, 0, 180, false);
                        //Triangle -->sens horloger(pt à pt)
                        if(vie!=0)
                        {
                            PathF path= new PathF();
                            path.MoveTo(Convert.ToSingle(drawable.X)-posx, Convert.ToSingle(drawable.Y)-65+15+Convert.ToSingle(7.5));
                            path.LineTo(Convert.ToSingle(drawable.X)-posx+15, Convert.ToSingle(drawable.Y)-65+40);
                            path.LineTo(Convert.ToSingle(drawable.X)-posx+30, Convert.ToSingle(drawable.Y)-65+15+Convert.ToSingle(7.5));
                            canvas.FillPath(path);
                        }
                        else{}
                    }
                    //COEUR DE VIE 1
                    //Coeur gris
                    CoeurGris(0);
                    //Coeur de vie
                    CoeurRouge(0, drawable.Vie1);

                    //COEUR DE VIE 2
                    //Coeur gris
                    CoeurGris(35);
                    //Coeur de vie
                    CoeurRouge(35, drawable.Vie2);
                }


                //GENRE ANIMAL
                if(drawable is Animal animaal)
                {
                    //MALE
                    if(animaal.Genre=="male")
                    {
                        //Symbole Male                       
                        canvas.StrokeColor= Colors.Blue;
                        canvas.StrokeSize= 5;
                        canvas.DrawEllipse(Convert.ToSingle(drawable.X)-68, Convert.ToSingle(drawable.Y)-73, 20, 20);
                        canvas.DrawLine(Convert.ToSingle(drawable.X)-50, Convert.ToSingle(drawable.Y)-70, Convert.ToSingle(drawable.X)-50+10, Convert.ToSingle(drawable.Y)-70-10);
                        canvas.DrawLine(Convert.ToSingle(drawable.X)-50+10, Convert.ToSingle(drawable.Y)-70-10, Convert.ToSingle(drawable.X)-50+8-10, Convert.ToSingle(drawable.Y)-70-10);
                        canvas.DrawLine(Convert.ToSingle(drawable.X)-50+10, Convert.ToSingle(drawable.Y)-70-10-Convert.ToSingle(2.5), Convert.ToSingle(drawable.X)-50+10, Convert.ToSingle(drawable.Y)-70-10+12);
                    }
                    //FEMELLE
                    if(animaal.Genre=="femelle")
                    {
                        //Symbole Femelle
                        canvas.StrokeColor= Colors.HotPink;
                        canvas.StrokeSize= 5;
                        canvas.DrawEllipse(Convert.ToSingle(drawable.X)-63, Convert.ToSingle(drawable.Y)-78, 20, 20);
                        canvas.DrawLine(Convert.ToSingle(drawable.X)-Convert.ToSingle(53.5), Convert.ToSingle(drawable.Y)-60, Convert.ToSingle(drawable.X)-Convert.ToSingle(53.5), Convert.ToSingle(drawable.Y)-60+17);
                        canvas.DrawLine(Convert.ToSingle(drawable.X)-62, Convert.ToSingle(drawable.Y)-50, Convert.ToSingle(drawable.X)-62+17, Convert.ToSingle(drawable.Y)-50);
                    }

                    //Si Animal Enceinte: Oeuf dans corps
                    if(animaal.isPregnant == true)
                    {
                        canvas.FillColor = Colors.DarkCyan;
                        canvas.FillCircle(Convert.ToSingle(drawable.X), Convert.ToSingle(drawable.Y), 10);
                    }

                    //Si Animal en Repos: Croix dans corps
                    if(animaal.repos>0)
                    {
                        canvas.StrokeColor= Colors.DarkCyan;
                        canvas.StrokeSize= 5;
                        canvas.DrawLine(Convert.ToSingle(drawable.X)-18,Convert.ToSingle(drawable.Y)-18, Convert.ToSingle(drawable.X)+18, Convert.ToSingle(drawable.Y)+18);
                        canvas.DrawLine(Convert.ToSingle(drawable.X)+18,Convert.ToSingle(drawable.Y)-18, Convert.ToSingle(drawable.X)-18, Convert.ToSingle(drawable.Y)+18);
                    }
                }


                //VIANDE
                if(drawable.GetType()==typeof(Viande) && drawable.Vie_Viande!=0)
                {
                    //Viande brun +Os blanc
                    //1 rectangle brun +2 rectangles blanches -->(x,y, longueur, hauteur)
                    canvas.FillColor= drawable.Color;
                    canvas.FillRectangle(Convert.ToSingle(drawable.X)-37, Convert.ToSingle(drawable.Y), 60, 35);
                    canvas.FillColor= Colors.GhostWhite;
                    canvas.FillRectangle(Convert.ToSingle(drawable.X)-37-20, Convert.ToSingle(drawable.Y)+14, 20, 10);
                    canvas.FillRectangle(Convert.ToSingle(drawable.X)+23, Convert.ToSingle(drawable.Y)+14, 20, 10);
                    //4 cercles -->(x,y, largeur, hauteur, angle départ, angle fin, sens)
                    canvas.FillArc(Convert.ToSingle(drawable.X)+37, Convert.ToSingle(drawable.Y)+5, Convert.ToSingle(12.5), Convert.ToSingle(12.5), 226, 225, false);
                    canvas.FillArc(Convert.ToSingle(drawable.X)+37, Convert.ToSingle(drawable.Y+17.5), Convert.ToSingle(12.5), Convert.ToSingle(12.5), 136, 135, false);
                    canvas.FillArc(Convert.ToSingle(drawable.X)-60, Convert.ToSingle(drawable.Y)+5, Convert.ToSingle(12.5), Convert.ToSingle(12.5), 46, 45, false);
                    canvas.FillArc(Convert.ToSingle(drawable.X)-60, Convert.ToSingle(drawable.Y+17.5), Convert.ToSingle(12.5), Convert.ToSingle(12.5), 136, 135, false);

                    //Barre de vie de viande
                    canvas.FillColor= Colors.Gray;
                    canvas.FillRoundedRectangle(Convert.ToSingle(drawable.X)-47, Convert.ToSingle(drawable.Y)-20, 75, 15, 5);
                    canvas.FillColor= Colors.LimeGreen;
                    canvas.FillRoundedRectangle(Convert.ToSingle(drawable.X)-47, Convert.ToSingle(drawable.Y)-20, drawable.Vie_Viande, 15, 5);
                }


                //DECHET
                if(drawable.GetType()==typeof(Dechet))
                {
                    //Partie petit
                    //Remplissage +Contour -->(x, y, longueur, hauteur)
                    canvas.FillColor= drawable.Color;
                    canvas.FillEllipse(Convert.ToSingle(drawable.X)+10, Convert.ToSingle(drawable.Y)-16, 15, 15);
                    canvas.StrokeColor= Colors.Black;
                    canvas.StrokeSize= 4;
                    canvas.DrawEllipse(Convert.ToSingle(drawable.X)+10, Convert.ToSingle(drawable.Y)-16, 15, 15);

                    //Partie moyen
                    //Remplissage +Contour -->(x, y, longueur, hauteur)
                    canvas.FillColor= drawable.Color;
                    canvas.FillEllipse(Convert.ToSingle(drawable.X)+5, Convert.ToSingle(drawable.Y)-10, 25, 15);
                    canvas.StrokeColor= Colors.Black;
                    canvas.StrokeSize= 4;
                    canvas.DrawEllipse(Convert.ToSingle(drawable.X)+5, Convert.ToSingle(drawable.Y)-10, 25, 15);

                    //Partie gd
                    //Remplissage +Contour -->(x, y, longueur, hauteur)
                    canvas.FillColor= drawable.Color;
                    canvas.FillEllipse(Convert.ToSingle(drawable.X), Convert.ToSingle(drawable.Y), 35, 15);
                    canvas.StrokeColor= Colors.Black;
                    canvas.StrokeSize= 4;
                    canvas.DrawEllipse(Convert.ToSingle(drawable.X), Convert.ToSingle(drawable.Y), 35, 15);
                }
            }
        }


        //UPDATE
        public void Update()
        {
            foreach(SimulationObjet drawable in objects)
            {
                drawable.Update();
            }
            foreach(SimulationObjet item in objects)
            {
                item.Update();
            }
            foreach(SimulationObjet item2 in objects)
            {
                item2.Update();
            }
        }
    }
}
