using Useful_training.Core.Neural_network.Neuron;
using Useful_training.Core.Neural_network.Neuron.Exceptions;
using Useful_training.Core.Neural_network.Neuron.Neurons;

namespace Useful_training.Core.Neural_network.LayerOfNeuronsTests
{
    public class LayerOfNeuronsTest
    {
        uint _numberOfNeurons;
        int _numberOfInputs;
        IList<double> _inputs;
        Random _rand;
        IEnumerable<NeuronType> NeuronTypeAvailable;
        public LayerOfNeuronsTest()
        {
            _rand = new Random();
            _numberOfInputs = _rand.Next(1, 5);
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
        public void LayerOfNeuronsShouldThrowExceptionTest()
        {
            LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
            Action Initialise = () =>
            {
                layerOfNeurons.Initialize(_numberOfNeurons, _numberOfInputs, (NeuronType)int.MaxValue);
            };
            Initialise.Should().Throw<Exception>();
        }
        [Fact]
        public void LayerOfNeuronsInitializeShouldThrowCantInitialiseWithZeroNeuronExceptionTest()
        {
            LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
            Action Initialise = () =>
            {
                layerOfNeurons.Initialize(0, _numberOfInputs, NeuronTypeAvailable.First());
            };
            Initialise.Should().Throw<CantInitialiseWithZeroNeuronException>();
        }
        [Fact]
        public void LayerOfNeuronsShouldCalculateGoodTest()
        {
            LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
            layerOfNeurons.Initialize(_numberOfNeurons, _numberOfInputs, NeuronTypeAvailable.First());
            IList<double> returnedData = layerOfNeurons.Calculate(_inputs);
            returnedData.Count.Should().Be((int)_numberOfNeurons);
        }
        [Fact]
        public void LayerOfNeuronsShouldCalculateThrowWrongInputForCalculationExceptionTest()
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