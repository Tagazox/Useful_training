using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.Neurons;

[assembly: InternalsVisibleTo("Useful_training.Core.Neural_network.Neural_NetworkTests")]
namespace Useful_training.Core.Neural_network
{
    internal class LayerOfInputNeurons : ILayerOfInputNeurons
    {
        private readonly IList<IInputNeurons> neurons;
        public IEnumerable<IInputNeurons> InputsNeurons { get => neurons; }

        public LayerOfInputNeurons(uint numberOfInputs)
        {
            if(numberOfInputs <= 0)
                throw new CantInitializeWithZeroInputException("You can't create a layer of inputs neurones with 0 input");
            neurons = new List<IInputNeurons>();
            for (int i = 0; i < numberOfInputs; i++)
                neurons.Add(new InputNeuron());
        }
        public ILayerOfInputNeurons Clone()
        {
            return (ILayerOfInputNeurons)this.MemberwiseClone();
        }
    }
}
