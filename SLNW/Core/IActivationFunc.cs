using System;
using System.Collections.Generic;
using System.Text;

namespace SLNW.Core
{
    public interface IActivationFunc
    {
        double Activation(double input);

        double Delta(double input);
    }
}
