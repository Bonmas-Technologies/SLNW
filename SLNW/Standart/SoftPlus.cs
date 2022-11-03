using System;
using SLNW.Core;

namespace SLNW.Standart
{
    [Serializable]
    public class SoftPlus : IActivationFunc
    {
        public double Activation(double input)
        {
            return Math.Log(1 + Math.Pow(Math.E, -input));
        }

        public double Delta(double input)
        {
            return 1 / (1 + Math.Pow(Math.E, -input));
        }
    }
}
