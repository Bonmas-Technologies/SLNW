using System;

namespace SLNW.Core
{
    [Serializable]
    public sealed class Neuron
    {
        public double output;
        
        private double _error;
        private double _sumout;
        private double[] _weights;

        private IActivationFunc _functions;


        public Neuron(IActivationFunc functions, double[] weights)
        {
            _weights = weights.Clone() as double[];

            _functions = functions;
        }

        public void InsertData(double[] data)
        {
            _sumout = 0;

            for (int i = 0; i < (data.Length + 1); i++)
            {
                if (i == data.Length)
                    _sumout += 1d * _weights[i];
                else
                    _sumout += data[i] * _weights[i];
            }

            output = _functions.Activation(_sumout);
        }

        public double[] PropogateError(double[] inputError)
        {
            double[] outputError = new double[_weights.Length];

            _error = 0;

            for (int i = 0; i < inputError.Length; i++)
                _error += inputError[i];

            for (int i = 0; i < _weights.Length; i++)
                outputError[i] = _error * _weights[i];

            return outputError;
        }

        public void CorrectWeights(double[] prewLayerOutputs, double learnSpeed)
        {
            double delta = _error * _functions.Delta(_sumout);

            for (int i = 0; i < (prewLayerOutputs.Length + 1); i++)
            {
                if (i == prewLayerOutputs.Length)
                    _weights[i] += learnSpeed * delta * 1d;
                else
                    _weights[i] += learnSpeed * delta * prewLayerOutputs[i];

            }
        }
    }
}