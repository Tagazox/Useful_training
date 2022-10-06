using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.LayersOfNeurones.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.LayersOfNeurones
{
	internal class LayerOfInputNeurons : ILayerOfInputNeurons
    {
        private readonly IList<IInputNeurons> neurons;
        IEnumerable<IInputNeurons> ILayerOfInputNeurons.InputsNeurons { get => neurons; }

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
