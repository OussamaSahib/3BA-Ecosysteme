using System;

//FICHIER POUR SIMULATION DES DESSINS(=REPREND INFO+UPDATE) -->(COULEUR, X, Y, ENERGIE)
namespace Ecosysteme
{
    public abstract class SimulationObjet: DessinObjet
    {
        public SimulationObjet(Color color, double x, double y, int energie, int vie1, int vie2, int vie_viande): base(color, x, y, energie, vie1, vie2, vie_viande) {}

        abstract public void Update();
    }
}
