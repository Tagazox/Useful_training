using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network.Interface
{
    public interface INeuron
    {
        public void InitialiseWithRandomValues(int NumberOfInputs);
        public INeuron Clone();
        public abstract double GetCalculationResult(IList<double> input);
    }
}
