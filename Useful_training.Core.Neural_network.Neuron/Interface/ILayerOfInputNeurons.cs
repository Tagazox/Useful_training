using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Neurons;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Useful_training.Core.Neural_network.Interface
{
    internal interface ILayerOfInputNeurons
    {
        internal IEnumerable<IInputNeurons> InputNeurons { get; }
        public ILayerOfInputNeurons Clone();
    }
}
