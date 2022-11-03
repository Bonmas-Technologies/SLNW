using System;

namespace SLNW.Core
{
    [Serializable]
    public sealed class Layer
    {
        public readonly int countOfNeurons;

        private Neuron[] _neurons;

        public Layer(Neuron[] neurons)
        {
            _neurons = neurons;
            countOfNeurons = neurons.Length;
        }

        public double[][] PropogateError(double[][] nextErrors)
        {
            double[][] currentErrors = new double[countOfNeurons][];

            for (int i = 0; i < countOfNeurons; i++)
            {
                double[] errorForNeuron = new double[countOfNeurons];

                for (int j = 0; j < nextErrors.Length; j++)
                    errorForNeuron[j] = nextErrors[j][i];

                currentErrors[i] = _neurons[i].PropogateError(errorForNeuron);
            }

            return currentErrors;
        }

        public void CorrectLayerWeights(double[] previousOutputs, double learnSpeed)
        {
            for (int i = 0; i < countOfNeurons; i++)
                _neurons[i].CorrectWeights(previousOutputs, learnSpeed);
        }


        public void Update(double[] previousOutputs)
        {
            for (int i = 0; i < _neurons.Length; i++)
                _neurons[i].InsertData(previousOutputs);
        }

        public double[] GetOutput()
        {
            double[] output = new double[_neurons.Length];

            for (int i = 0; i < _neurons.Length; i++)
                output[i] = _neurons[i].output;

            return output;
        }

    }
}
