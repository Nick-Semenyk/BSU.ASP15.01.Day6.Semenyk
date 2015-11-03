using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GCDCalculationClass.GCDCalculationClass;

namespace GCDCalculationClassConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            long time;
            Console.WriteLine(EuclideanAlgorithm(110, 88, 22, 11));
            Console.WriteLine(BinaryAlgorithm(110, 88, 22, 11));
            Console.WriteLine(BinaryAlgorithm(out time, 572010296,1143980552,788));
            Console.WriteLine(time);
            Console.WriteLine(EuclideanAlgorithm(out time, 572010296, 1143980552, 788));
            Console.WriteLine(time);
            Random random = new Random();
            int[] randomArray = new int[100000];
            for (int i = 0; i < 100000; i++)
            {
                randomArray[i] = random.Next(int.MaxValue);
            }
            Console.WriteLine(BinaryAlgorithm(out time, randomArray));
            Console.WriteLine(time);
            Console.WriteLine(EuclideanAlgorithm(out time, randomArray));
            Console.WriteLine(time);
            try
            { 
                Console.WriteLine(BinaryAlgorithm());
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                Console.WriteLine(EuclideanAlgorithm());
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();

        }
    }
}
