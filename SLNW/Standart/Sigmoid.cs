using System;
using SLNW.Core;

namespace SLNW.Standart
{
    public class Sigmoid : IActivationFunc
    {
        public double Activation(double input)
        {
            return 1 / (1 + Math.Pow(Math.E, -input));
        }

        public double Delta(double input)
        {
            return Activation(input) * (1 - Activation(input));
        }
    }
}
