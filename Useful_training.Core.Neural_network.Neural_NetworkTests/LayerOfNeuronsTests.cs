using Moq;
using System.Runtime.CompilerServices;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.Neurons;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class LayerOfNeuronsTests
    {
        Mock<ILayerOfInputNeurons> layerOfInputNeurones;
        uint _numberOfNeurons;
        Random _rand;
        IEnumerable<NeuronType> NeuronTypeAvailable;
        ILayerOfNeurons testLayerOfNeurons;
        List<double> Targets;
        public LayerOfNeuronsTests()
        {
            _rand = new Random();
            uint _numberOfInputs = (uint)_rand.Next(1, 5);
            _numberOfNeurons = (uint)_rand.Next(1, 5);

            layerOfInputNeurones = new Mock<ILayerOfInputNeurons>();

            IList<IInputNeurons> _inputsNeurones = new List<IInputNeurons>();
            Targets = new List<double>();
            for (int i = 0; i < _numberOfInputs; i++)
            {
                _inputsNeurones.Add(new InputNeuron());
                _inputsNeurones[i].OutputResult = 1;
            }
            for (int i = 0; i < _numberOfNeurons; i++)
            {
                Targets.Add(0);
            }
            layerOfInputNeurones.Setup(s => s.InputsNeurons).Returns(_inputsNeurones);
            NeuronTypeAvailable = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>();
            testLayerOfNeurons = new LayerOfNeurons();
        }
        [Fact]
        public void LayerOfNeuronsShouldInitializeGoodWithAllNeuroneTypeTest()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                testLayerOfNeurons.Initialize(_numberOfNeurons, type, layerOfInputNeurones.Object);
            }
        }
        [Fact]
        public void LayerOfNeuronsShouldThrowCantInitializeWithZeroNeuronExceptionTest()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                Action CreationCase1 = () =>
                {
                    testLayerOfNeurons.Initialize(0, type, layerOfInputNeurones.Object);
                };
                CreationCase1.Should().Throw<CantInitializeWithZeroNeuronException>();
            }
        }
        [Fact]
        public void LayerOfNeuronsShouldThrowCBadInputLayerExceptionTest()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                layerOfInputNeurones.Setup(s => s.InputsNeurons).Returns(new List<InputNeuron>());

                Action CreationCase1 = () =>
                {
                    testLayerOfNeurons.Initialize(_numberOfNeurons, type, layerOfInputNeurones.Object);
                };
                Action CreationCase2 = () =>
                {
                    testLayerOfNeurons.Initialize(_numberOfNeurons, type, null);
                };
                CreationCase1.Should().Throw<BadInputLayerException>();
                CreationCase2.Should().Throw<BadInputLayerException>();
            }
        }
        [Fact]
        public void LayerOfNeuronsShouldCalculateGoodTest()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                testLayerOfNeurons.Initialize(_numberOfNeurons, type, layerOfInputNeurones.Object);
                testLayerOfNeurons.Calculate();
                foreach (double output in testLayerOfNeurons.Outputs)
                {
                    output.Should().BeOfType(typeof(double));
                }
            }
        }
        [Fact]
        public void LayerOfNeuronsShouldCalculateGradiantGoodTest()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                testLayerOfNeurons.Initialize(_numberOfNeurons, type, layerOfInputNeurones.Object);
                testLayerOfNeurons.Calculate();
                testLayerOfNeurons.CalculateGradiant(Targets);
                foreach (INeuron neuron in testLayerOfNeurons.Neurons)
                {
                    neuron.Gradiant.Should().BeOfType(typeof(double));
                }
            }
        }
        [Fact]
        public void LayerOfNeuronsCalculateGradiantShouldThrowArgumentException()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                testLayerOfNeurons.Initialize(_numberOfNeurons, type, layerOfInputNeurones.Object);
                testLayerOfNeurons.Calculate();
                Targets = new List<double>();
                Action CalculateGradiant = () =>
                {
                    testLayerOfNeurons.CalculateGradiant(Targets);
                };
                CalculateGradiant.Should().Throw<ArgumentException>();
            }
        }
        [Fact]
        public void LayerOfNeuronsShouldUpdateWeightGoodTest()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                testLayerOfNeurons.Initialize(_numberOfNeurons, type, layerOfInputNeurones.Object);
                testLayerOfNeurons.Calculate();
                IList<double> outputs = testLayerOfNeurons.Outputs;
                testLayerOfNeurons.CalculateGradiant(Targets);
                testLayerOfNeurons.UpdateWeights(.5, .5);
                testLayerOfNeurons.Calculate();
                testLayerOfNeurons.Outputs.Should().NotBeSameAs(outputs);
            }
        }
       
    }
}