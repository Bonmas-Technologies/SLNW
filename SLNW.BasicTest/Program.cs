using System;
using SLNW;
using SLNW.Standart;

namespace SLNW.BasicTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] maket = { 2, 4, 1 };

            var network = NetworkFabric.CreateNetwork(maket, new LeakyReLU(), 0.2);

            double[][] tests =
            {
                new double[]{ 0, 0 },
                new double[]{ 1, 0 },
                new double[]{ 0, 1 },
                new double[]{ 1, 1 },
            };

            double[][] learn =
            {
                new double[]{ 0 },
                new double[]{ 1 },
                new double[]{ 1 },
                new double[]{ 0 },
            };

            for (int i = 0; i < 500_000; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    network.Update(tests[j]);
                    network.Learn(learn[j]);
                }
            }

            WriteDoubleArray(network.Update(tests[1]));
            Console.WriteLine(network.GetError(learn[1]).ToString("0.0000"));

            WriteDoubleArray(network.Update(tests[3]));
            Console.WriteLine(network.GetError(learn[3]).ToString("0.0000"));
        }

        private static void WriteDoubleArray(double[] outputs)
        {
            Console.WriteLine();
            foreach (var output in outputs)
            {
                Console.WriteLine(output.ToString("0.0000"));
            }
        }
    }
}
