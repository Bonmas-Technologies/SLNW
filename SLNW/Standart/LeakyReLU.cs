using System;
using SLNW.Core;


namespace SLNW.Standart
{
    public class LeakyReLU : IActivationFunc
    {
        public double Activation(double input)
        {
            return input >= 0 ? input : input * 0.01;
        }

        public double Delta(double input)
        {
            return input >= 0 ? 1 : 0.01;

        }
    }
}
