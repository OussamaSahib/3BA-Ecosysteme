using System;

namespace Ecosysteme
{
    public class Tigre: Carnivore
    {
        public Tigre(double x, double y, int energie, int vie1, int vie2): base(Colors.OrangeRed, x, y, energie, vie1, vie2) { }
    }
}
