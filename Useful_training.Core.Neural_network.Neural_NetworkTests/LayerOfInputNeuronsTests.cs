using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.LayersOfNeurones;
using Useful_training.Core.NeuralNetwork.LayersOfNeurones.Interfaces;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests;

public class LayerOfInputNeuronsTests
{
    private readonly uint _numberOfInputs;
    public LayerOfInputNeuronsTests()
    {
        Random rand = new Random();
        _numberOfInputs = (uint)rand.Next(1, 5);
    }
    [Fact]
    public void LayerOfInputNeuronsCreationShouldBeGood()
    {
        ILayerOfInputNeurons testSubject = new LayerOfInputNeurons(_numberOfInputs);
        testSubject.InputsNeurons.Count().Should().Be((int)_numberOfInputs);
    }
    [Fact]
    public void LayerOfInputNeuronsCreationShouldThrowCantInitializeWithZeroInputException()
    {
        Action creationCase1 = () =>
        {
            ILayerOfInputNeurons unused = new LayerOfInputNeurons(0);
        };
        creationCase1.Should().Throw<CantInitializeWithZeroInputException>();
    }
    [Fact]
    public void LayerOfInputNeuronsCloneShouldBeGood()
    {
        ILayerOfInputNeurons testSubject = new LayerOfInputNeurons(_numberOfInputs);
        ILayerOfInputNeurons cloneSubject = testSubject.Clone();
        testSubject.InputsNeurons.Count().Should().Be(cloneSubject.InputsNeurons.Count());
    }
}