using System;
using SLNW.Core;
using System.Collections.Generic;

namespace SLNW
{
    public sealed class NerualNetwork
    {
        private Layer[] _layers;

        public NerualNetwork(Layer[] layers)
        {
            _layers = layers;
        }

        public double[] Update(double[] input)
        {
            _layers[0].Update(input);

            for (int i = 1; i < _layers.Length; i++)
                _layers[i].Update(_layers[i - 1].GetOutput());

            return _layers[_layers.Length - 1].GetOutput();
        }

        public double GetError(double[] input)
        {
            var currentOutput = _layers[_layers.Length - 1].GetOutput();

            double error = 0;

            for (int i = 0; i < currentOutput.Length; i++)
                error += Math.Pow(input[i] - currentOutput[i], 2);

            return error;
        }

        public void Learn(double[] input)
        {
            UpdateLayersError(input);
            CorrectLayersWeights();
        }

        private void UpdateLayersError(double[] input)
        {
            var currentOutput = _layers[_layers.Length - 1].GetOutput();

            double[] outputLayerError = new double[_layers[_layers.Length - 1].countOfNeurons];

            for (int i = 0; i < outputLayerError.Length; i++)
                outputLayerError[i] = input[i] - currentOutput[i];

            double[][] error = new double[][] { outputLayerError };

            for (int i = _layers.Length - 1; i >= 0; i--)
            {
                error = _layers[i].PropogateError(error);
            }
        }

        private void CorrectLayersWeights()
        {
            for (int i = 1; i < _layers.Length; i++)
                _layers[i].CorrectLayerWeights(_layers[i - 1].GetOutput());
        }
    }
}
