using System.Collections.Generic;

namespace SLNW.Core
{
    public sealed class Layer
    {
        public readonly int countOfNeurons;

        private Neuron[] _neurons;

        public Layer(Neuron[] neurons)
        {
            _neurons = neurons;
            countOfNeurons = neurons.Length;
        }

        public List<double[]> PropogateError(List<double[]> nextErrors)
        {
            List<double[]> currentErrors = new List<double[]>(countOfNeurons);

            for (int i = 0; i < countOfNeurons; i++)
            {
                double[] errorForNeuron = new double[countOfNeurons];

                for (int j = 0; j < nextErrors.Count; j++)
                    errorForNeuron[j] = nextErrors[j][i];

                currentErrors.Add(_neurons[i].PropogateError(errorForNeuron));
            }

            return currentErrors;
        }

        public void CorrectLayerWeights(double[] previousOutputs)
        {
            for (int i = 0; i < countOfNeurons; i++)
                _neurons[i].CorrectWeights(previousOutputs);
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
