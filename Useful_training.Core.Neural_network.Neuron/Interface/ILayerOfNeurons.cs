using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Neurons;

namespace Useful_training.Core.Neural_network.Interface
{
    public interface ILayerOfNeurons
    {
        internal IList<INeuron> neurons { get; set; }
        internal ILayerOfNeurons Clone();
        internal void Initialize(uint numberOfNeuron,uint numberOfInput, NeuronType neuronType);
        internal IList<double> Calculate(IList<double> inputs);
        internal IList<IList<double>> BackPropagate(List<double> targets, bool IsFirstLayer);
    }
}
