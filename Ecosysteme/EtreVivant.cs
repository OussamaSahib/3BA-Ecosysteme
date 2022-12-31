using System;

namespace Ecosysteme
{
    public class EtreVivant : SimulationObjet
    {
        public EtreVivant(Color color, double x, double y, int energie, int vie1, int vie2) : base(color, x, y, energie, vie1, vie2) { }

        public override void Update()
        {   
        }
    }
}