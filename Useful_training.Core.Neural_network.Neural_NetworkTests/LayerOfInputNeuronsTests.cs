using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.Neurons;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class LayerOfInputNeuronsTests
    {
        uint _numberOfInputs;
        public LayerOfInputNeuronsTests()
        {
            Random _rand = new Random();
            _numberOfInputs = (uint)_rand.Next(1, 5);
        }
        [Fact]
        public void LayerOfInputNeuronsShouldCreateGoodTests()
        {
            ILayerOfInputNeurons testSubject = new LayerOfInputNeurons(_numberOfInputs);
            testSubject.InputsNeurons.Count().Should().Be((int)_numberOfInputs);
        }
        [Fact]
        public void LayerOfInputNeuronsShouldThrowCantInitializeWithZeroInputExceptionTests()
        {
            Action CreationCase1 = () =>
            {
                ILayerOfInputNeurons testSubject = new LayerOfInputNeurons(0);
            };
            CreationCase1.Should().Throw<CantInitializeWithZeroInputException>();
        }
        [Fact]
        public void LayerOfInputNeuronsShouldCloneGoodTests()
        {
            ILayerOfInputNeurons testSubject = new LayerOfInputNeurons(_numberOfInputs);
            ILayerOfInputNeurons cloneSubject = testSubject.Clone();
            testSubject.InputsNeurons.Count().Should().Be(cloneSubject.InputsNeurons.Count());
        }
    }
}