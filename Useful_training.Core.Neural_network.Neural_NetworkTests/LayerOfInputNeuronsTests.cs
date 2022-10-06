using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.LayersOfNeurones;
using Useful_training.Core.NeuralNetwork.LayersOfNeurones.Interfaces;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests
{
    public class LayerOfInputNeuronsTests
    {
        uint NumberOfInputs;
        public LayerOfInputNeuronsTests()
        {
            Random _rand = new Random();
            NumberOfInputs = (uint)_rand.Next(1, 5);
        }
        [Fact]
        public void LayerOfInputNeuronsCreationShouldBeGood()
        {
            ILayerOfInputNeurons testSubject = new LayerOfInputNeurons(NumberOfInputs);
            testSubject.InputsNeurons.Count().Should().Be((int)NumberOfInputs);
        }
        [Fact]
        public void LayerOfInputNeuronsCreationShouldThrowCantInitializeWithZeroInputException()
        {
            Action CreationCase1 = () =>
            {
                ILayerOfInputNeurons testSubject = new LayerOfInputNeurons(0);
            };
            CreationCase1.Should().Throw<CantInitializeWithZeroInputException>();
        }
        [Fact]
        public void LayerOfInputNeuronsCloneShouldBeGood()
        {
            ILayerOfInputNeurons testSubject = new LayerOfInputNeurons(NumberOfInputs);
            ILayerOfInputNeurons cloneSubject = testSubject.Clone();
            testSubject.InputsNeurons.Count().Should().Be(cloneSubject.InputsNeurons.Count());
        }
    }
}