using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;

[assembly: InternalsVisibleTo("Useful_training.Core.Neural_network.Neural_NetworkTests")]
namespace Useful_training.Core.Neural_network
{
    public class Neural_Network
    {
        internal IList<ILayerOfNeurons> _layersOfNeurons { get; set; }
        internal Neural_Network()
        {
            _layersOfNeurons = new List<ILayerOfNeurons>();
        }
        internal void AddLayer(uint _numberOfInput, uint _numberOfNeuron, NeuronType typeOfNeurons)
        {
            LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
            layerOfNeurons.Initialize(_numberOfNeuron, _numberOfInput , typeOfNeurons);
            _layersOfNeurons.Add(layerOfNeurons);
        }
        public IList<double> Calculate(List<double> inputs)
        {
            if (_layersOfNeurons.Count == 0)
                throw new NeedToBeCreatedByTheBuilderException("Neural network need to be created by the builder");
            IList<double> outputs = inputs;
            for (int i = 0; i < _layersOfNeurons.Count; i++)
            {
                outputs = _layersOfNeurons[i].Calculate(outputs);
            }
            return outputs;
        }
        public void BackPropagate(List<double> targets)
        {
            _layersOfNeurons=_layersOfNeurons.Reverse().ToList();
            foreach (ILayerOfNeurons layers in _layersOfNeurons)
            {   
                IList<IList<double>> outputsRetropropagation = layers.BackPropagate(targets);
                int countOfOutputs = outputsRetropropagation.First().Count;
                targets = new List<double>();
                for (int i = 0; i < countOfOutputs; i++)
                    targets.Add(outputsRetropropagation.Sum(o => o[i]));
            }
            _layersOfNeurons= _layersOfNeurons.Reverse().ToList();
        }
    }
}
