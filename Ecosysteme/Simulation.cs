//FICHIER REPRENANT SIMULATIONOBJET POUR Y INTEGRER LES ELEMENTS
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
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
            objects.Add(new Tigre(900, 350, 75, 15, 15));
            objects.Add(new Plante(600, 350,75, 15, 15));
        }


        //FCTS BOUTTON
        //Position random dans la map
        Random rnd= new Random();
        int min_x= 0;
        int max_x= 1800;
        int min_y= 0;
        int max_y= 700;

        //FCT QUI AJOUTE 1 PLANTE (-->AVEC BOUTTON)
        public void Add_Plante()
        {
            objects.Add(new Plante(rnd.Next(min_x, max_x), rnd.Next(min_y, max_y), 75, 15, 15));
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
                    objects.Add(new Viande(item.X, item.Y, 75));
                    objects.Remove(item);
                }

                //PLANTE ET VIANDE MORT-->DECHET
                if((item is Plante && item.Vie2==0) || (item.GetType()==typeof(Viande) && item.Vie_Viande==0))
                {
                    objects.Add(new Dechet(item.X, item.Y));
                    objects.Remove(item);
                }

                //ANIMAL CREE DES DECHETS REGULIERS
                if(item is Animal)
                {
                    if (item.Energie==75 || item.Energie==50 || item.Energie==25)
                    {
                        objects.Add(new Dechet(item.X, item.Y));
                    }
                }


                //FCT ZONE CONTACT ANIMAL
                bool ZoneContact(SimulationObjet AnimalZone, SimulationObjet ElemExt) 
                {
                    //Zone de contact défini
                    double contactDistance= 50+20;
                    double minX= AnimalZone.X -contactDistance;
                    double maxX= AnimalZone.X +contactDistance;
                    double minY= AnimalZone.Y -contactDistance;
                    double maxY= AnimalZone.Y +contactDistance;

                    //Si Element ext. rentre ds la Zone de contact
                    if (ElemExt.X>=minX && ElemExt.X<=maxX && ElemExt.Y>=minY && ElemExt.Y<=maxY)
                    {return true;}
                    else 
                    {return false;}
                }

                //ZONE CONTACT ANIMAL
                //Element rencontré
                foreach(SimulationObjet item2 in objects.ToList())
                {
                    //ZONE CONTACT HERBIVORE
                    if(item is Herbivore)
                    {
                        //Si Rencontre Plante: Mange +Full Energie-Vie
                        if(item2 is Plante)
                        {
                            if(ZoneContact(item, item2)==true)
                            {
                                objects.Remove(item2);
                                item.Energie= 75;
                                item.Vie1= 15;
                            }
                        }
                    }

                    //ZONE CONTACT CARNIVORE
                    if(item is Carnivore)
                    {
                        //Si Rencontre Herbivore OU Viande: Mange +Full Energie-Vie
                        if(item2 is Herbivore || item2.GetType()==typeof(Viande))
                        {
                            if(ZoneContact(item, item2)==true)
                            {
                                objects.Remove(item2);
                                item.Energie= 75;
                                item.Vie1= 15;
                            }
                        }
                    }
                }
            }


            //DESSIN
            foreach (SimulationObjet drawable in objects)
            {
                if((drawable is Animal || drawable is Plante) && drawable.Vie2!=0){
                    //ÊTRE VIVANT
                    canvas.FillColor= drawable.Color;
                    canvas.FillCircle(new Point(drawable.X, drawable.Y), 25.0);

                    //ZONE CONTACT
                    if(drawable is Animal)
                    {
                        canvas.StrokeColor= Colors.Blue;
                        canvas.StrokeSize= 10;
                        canvas.DrawEllipse(Convert.ToSingle(drawable.X)-50, Convert.ToSingle(drawable.Y)-50, 100, 100);
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


                //VIANDE
                if(drawable.GetType()==typeof(Viande))
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
