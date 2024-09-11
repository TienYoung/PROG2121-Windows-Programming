using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dairy d1 = new Dairy();
            Console.WriteLine(d1.GetProductInformation());
            Produce p1 = new Produce();
            Console.WriteLine(p1.GetProductInformation());
            Cereal c1 = new Cereal();
            Console.WriteLine(c1.GetProductInformation());
            Console.ReadKey();
        }
    }
}
