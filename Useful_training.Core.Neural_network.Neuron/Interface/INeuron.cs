using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network.Interface
{
    public interface INeuron
    {
        internal void InitialiseWithRandomValues(uint NumberOfInputs);
        internal INeuron Clone();
        internal abstract double GetCalculationResult(IList<double> input);
        internal IList<double> UpdateWeights(double target, bool IsFirstLayer);

    }
}
