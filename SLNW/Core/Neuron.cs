namespace SLNW.Core
{
    public sealed class Neuron
    {
        public double output;
        
        private readonly double _learnSpeed;

        private double _error;
        private double _sumout;
        private double[] _weights;

        private readonly NeruonFunction _activation;
        private readonly NeruonFunction _delta;

        delegate double NeruonFunction(double input);

        public Neuron(double learnSpeed, IActivationFunc functions, double[] weights) // todo: нормальная фабрика нейросети и нейронов
        {
            _learnSpeed = learnSpeed;
            _weights = weights.Clone() as double[];

            _activation = functions.Activation;
            _delta = functions.Delta;
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

            output = _activation.Invoke(_sumout);
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

        public void CorrectWeights(double[] prewLayerOutputs)
        {
            double delta = _error * _delta.Invoke(_sumout);

            for (int i = 0; i < (prewLayerOutputs.Length + 1); i++)
            {
                if (i == prewLayerOutputs.Length)
                    _weights[i] += _learnSpeed * delta * 1d;
                else
                    _weights[i] += _learnSpeed * delta * prewLayerOutputs[i];

            }
        }
    }
}