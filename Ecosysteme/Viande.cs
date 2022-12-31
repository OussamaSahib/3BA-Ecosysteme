using System;

namespace Ecosysteme
{
    public class Viande: SimulationObjet
    {
        
        public Viande(double x, double y, int vie_viande): base(Colors.SaddleBrown, x, y, 0,0,0, vie_viande) { }

        public void VieViande()
        {   //Vie_Viande qui diminue
            if (Vie_Viande>0)
                Vie_Viande= Vie_Viande-5;
            else
                Vie_Viande= 0;
        }

        public override void Update()
        {
            VieViande();
        }
    }
}
