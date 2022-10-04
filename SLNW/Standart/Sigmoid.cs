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
            double activated = Activation(input);

            return activated * (1 - activated);
        }
    }
}
