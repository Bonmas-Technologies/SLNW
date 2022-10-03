using System;
using System.Collections.Generic;
using System.Text;
using SLNW.Core;

namespace SLNW
{
    public static class NetworkFabric
    {
        private static Random generator;

        static NetworkFabric()
        {
            generator = new Random();
        }

        public static NerualNetwork CreateNetwork(int[] maket, IActivationFunc activationFunc, double learnSpeed)
        {
            Layer[] layers = new Layer[maket.Length - 1];

            int previousNeuronsCount = maket[0];

            for (int i = 1; i < maket.Length; i++)
            {
                Neuron[] neurons = new Neuron[maket[i]];

                for (int j = 0; j < maket[i]; j++)
                {
                    double[] weights = new double[previousNeuronsCount + 1];

                    for (int k = 0; k < weights.Length; k++)
                        weights[k] = generator.NextDouble() * 2 - 1;

                    neurons[j] = new Neuron(learnSpeed, activationFunc, weights);
                }

                previousNeuronsCount = maket[i];
                layers[i - 1] = new Layer(neurons);
            }

            return new NerualNetwork(layers);
        }
    }
}
