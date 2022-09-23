using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Neurons;

namespace Useful_training.Core.Neural_network.Interface
{
    internal interface ILayerOfNeurons : ILayerOfInputNeurons
    {
        internal IList<double> Outputs { get;  }
        internal IList<INeuron> Neurons { get; set; }
        internal new ILayerOfNeurons Clone();
        internal void Initialize(uint numberOfNeuron, NeuronType neuronType, ILayerOfInputNeurons inputLayer);
		void UpdateWeights(double learnRate, double momentum);
        void CalculateGradiant(List<double> targets=null);
        void Calculate();
    }
}
