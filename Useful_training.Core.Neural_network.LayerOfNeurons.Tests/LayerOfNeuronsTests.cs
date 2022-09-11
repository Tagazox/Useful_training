using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.Neurons;

namespace Useful_training.Core.Neural_network.LayerOfNeuronsTests
{
    public class LayerOfNeuronsTests
    {
        uint _numberOfNeurons;
        uint _numberOfInputs;
        IList<double> _inputs;
        Random _rand;
        IEnumerable<NeuronType> NeuronTypeAvailable;
        public LayerOfNeuronsTests()
        {
            _rand = new Random();
            _numberOfInputs = (uint)_rand.Next(1, 5);
            _numberOfNeurons = (uint)_rand.Next(1, 5);
            _inputs = new List<double>();
            for (int i = 0; i < _numberOfInputs; i++)
            {
                _inputs.Add(_rand.NextDouble() * 2 - 1);
            }
            NeuronTypeAvailable = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>();
        }
        [Fact]
        public void LayerOfNeuronsShouldInitializeGoodWithAllNeuroneTypeTest()
        {
            LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
            foreach (NeuronType type in NeuronTypeAvailable)
                layerOfNeurons.Initialize(_numberOfNeurons, _numberOfInputs, type);
        }
        [Fact]
        public void LayerOfNeuronsInitializeShouldThrowExceptionTest()
        {
            LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
            Action Initialise = () =>
            {
                layerOfNeurons.Initialize(_numberOfNeurons, _numberOfInputs, (NeuronType)int.MaxValue);
            };
            Initialise.Should().Throw<NeuronTypeDontExsistException>();
        }
        [Fact]
        public void LayerOfNeuronsInitializeShouldThrowCantInitializeWithZeroNeuronExceptionTest()
        {
            LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
            Action Initialise = () =>
            {
                layerOfNeurons.Initialize(0, _numberOfInputs, NeuronTypeAvailable.First());
            };
            Initialise.Should().Throw<CantInitializeWithZeroNeuronException>();
        }
        [Fact]
        public void LayerOfNeuronsCalculateShouldBeGoodTest()
        {
            LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
            layerOfNeurons.Initialize(_numberOfNeurons, _numberOfInputs, NeuronTypeAvailable.First());
            IList<double> returnedData = layerOfNeurons.Calculate(_inputs);
            returnedData.Count.Should().Be((int)_numberOfNeurons);
        }
        [Fact]
        public void LayerOfNeuronsCloneShouldBeGoodTest()
        {
            LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
            layerOfNeurons.Initialize(_numberOfNeurons, _numberOfInputs, NeuronTypeAvailable.First());
            IList<double> returnedData = layerOfNeurons.Calculate(_inputs);
            returnedData.Count.Should().Be((int)_numberOfNeurons);
            ILayerOfNeurons clonedLayerOfNeurons = layerOfNeurons.Clone();
            IList<double> cloneReturnedData = layerOfNeurons.Calculate(_inputs);
            cloneReturnedData.Should().BeEquivalentTo(returnedData);
        }
        [Fact]
        public void LayerOfNeuronsCalculateShouldThrowWrongInputForCalculationExceptionTest()
        {
            LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
            layerOfNeurons.Initialize(_numberOfNeurons, _numberOfInputs, NeuronTypeAvailable.First());
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                IList<double> returnedData = layerOfNeurons.Calculate(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }

    }
}