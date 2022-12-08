using System;

//FICHIER POUR SIMULATION DES DESSINS(=REPREND INFO+UPDATE) -->(COULEUR, X, Y)
namespace Ecosysteme
{


    public abstract class SimulationObjet: DessinObjet
    {
        public SimulationObjet(Color color, double x, double y): base(color, x, y){}

        abstract public void Update();
    }
}
