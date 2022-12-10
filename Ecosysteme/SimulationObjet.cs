using System;

//FICHIER POUR SIMULATION DES DESSINS(=REPREND INFO+UPDATE) -->(COULEUR, X, Y, ENERGIE)
namespace Ecosysteme
{


    public abstract class SimulationObjet: DessinObjet
    {
        public SimulationObjet(Color color, double x, double y, int energie): base(color, x, y, energie){}

        abstract public void Update();
    }
}
