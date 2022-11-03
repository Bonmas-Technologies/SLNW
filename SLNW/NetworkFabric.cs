using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SLNW.Core;

namespace SLNW
{
    public static class NetworkBuilder
    {
        private static Random generator;
        private static BinaryFormatter formatter;

        static NetworkBuilder()
        {
            formatter = new BinaryFormatter();
            generator = new Random();
        }

        public static NerualNetwork BuildNetwork(int[] maket, IActivationFunc activationFunc, double learnSpeed)
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

                    neurons[j] = new Neuron(activationFunc, weights);
                }

                previousNeuronsCount = maket[i];
                layers[i - 1] = new Layer(neurons);
            }

            return new NerualNetwork(layers, learnSpeed);
        }

        public static NerualNetwork ReadNetworkFrom(Stream saveStream)
        {
            return formatter.Deserialize(saveStream) as NerualNetwork;
        }

        public static MemoryStream GetNetworkStream(NerualNetwork network)
        {
            MemoryStream saveStream = new MemoryStream();
            
            formatter.Serialize(saveStream, network);
            saveStream.Position = 0;
            
            return saveStream;
        }
        
        public static NerualNetwork LoadNetwork(string path)
        {
            var networkStream = new MemoryStream();

            using (StreamReader reader = new StreamReader(path))
            {
                BinaryReader binary = new BinaryReader(reader.BaseStream);
                
                int count = binary.ReadInt32();
                networkStream.Write(binary.ReadBytes(count));
            }
            
            networkStream.Position = 0;

            return ReadNetworkFrom(networkStream);
        }

        public static void SaveNetwork(NerualNetwork network, string path)
        {
            var networkStream = GetNetworkStream(network);

            using (StreamWriter saver = new StreamWriter(path, false))
            {
                BinaryWriter binary = new BinaryWriter(saver.BaseStream);
                
                binary.Write(networkStream.GetBuffer().Length);
                binary.Write(networkStream.GetBuffer());
            }
        }

        
    }
}
