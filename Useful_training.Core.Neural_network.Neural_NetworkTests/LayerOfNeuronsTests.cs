using Moq;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.LayersOfNeurones;
using Useful_training.Core.NeuralNetwork.LayersOfNeurones.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests
{
    public class LayerOfNeuronsTests
    {
        IEnumerable<NeuronType> NeuronTypeAvailable;
        ILayerOfNeurons testLayerOfNeurons;
        Mock<ILayerOfInputNeurons> LayerOfInputNeuronesMocked;
        uint NumberOfNeurons;
        List<double>? Targets;
        Random Rand;
        public LayerOfNeuronsTests()
        {
            Rand = new Random();
            uint _numberOfInputs = (uint)Rand.Next(1, 5);
            NumberOfNeurons = (uint)Rand.Next(1, 5);

            LayerOfInputNeuronesMocked = new Mock<ILayerOfInputNeurons>();
            LayerOfInputNeuronesMocked.Setup(s => s.InputsNeurons).Returns(MockInputNeuronsList());
            
            NeuronTypeAvailable = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>();
            testLayerOfNeurons = new LayerOfNeurons();
        }
        private IList<IInputNeurons> MockInputNeuronsList()
        {
            IList<IInputNeurons> InputsNeurons = new List<IInputNeurons>();
            int NumberOfInputs = 5;
            double Inputsvalue = 1;
            for (int i = 0; i < NumberOfInputs; i++)
            {
                Mock<IInputNeurons> mockedInputNeuron = new Mock<IInputNeurons>();
                mockedInputNeuron.Setup(i => i.OutputResult).Returns(Inputsvalue);
                mockedInputNeuron.Setup(i => i.OutputSynapses).Returns(new List<Synapse>());
                InputsNeurons.Add(mockedInputNeuron.Object);
            }
            return InputsNeurons;
        }
        [Fact]
        public void LayerOfNeuronsInitializeShouldBeGoodWithAllNeuroneType()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                testLayerOfNeurons.Initialize(NumberOfNeurons, type, LayerOfInputNeuronesMocked.Object);
            }
        }
        [Fact]
        public void LayerOfNeuronsInitializeShouldThrowCantInitializeWithZeroNeuronException()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                Action CreationCase1 = () =>
                {
                    testLayerOfNeurons.Initialize(0, type, LayerOfInputNeuronesMocked.Object);
                };
                CreationCase1.Should().Throw<CantInitializeWithZeroNeuronException>();
            }
        }
        [Fact]
        public void LayerOfNeuronsInitializeShouldThrowNeuronTypeDontExsistException()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                Action CreationCase1 = () =>
                {
                    testLayerOfNeurons.Initialize(NumberOfNeurons, (NeuronType)(NeuronTypeAvailable.Count() + 1), LayerOfInputNeuronesMocked.Object);
                };
                CreationCase1.Should().Throw<NeuronTypeDontExsistException>();
            }
        }
        [Fact]
        public void LayerOfNeuronsInitializeShouldThrowBadInputLayerException()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                LayerOfInputNeuronesMocked.Setup(s => s.InputsNeurons).Returns(new List<InputNeuron>());

                Action CreationCase1 = () =>
                {
                    testLayerOfNeurons.Initialize(NumberOfNeurons, type, LayerOfInputNeuronesMocked.Object);
                };
                Action CreationCase2 = () =>
                {
                    testLayerOfNeurons.Initialize(NumberOfNeurons, type, null);
                };
                CreationCase1.Should().Throw<BadInputLayerException>();
                CreationCase2.Should().Throw<BadInputLayerException>();
            }
        }
        [Fact]
        public void LayerOfNeuronsCalculateShouldBeGood()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                testLayerOfNeurons.Initialize(NumberOfNeurons, type, LayerOfInputNeuronesMocked.Object);
                testLayerOfNeurons.Calculate();
                foreach (double output in testLayerOfNeurons.Outputs)
                {
                    output.Should().BeOfType(typeof(double));
                }
            }
        }
        [Fact]
        public void LayerOfNeuronsCalculateGradiantShouldBeGood()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                testLayerOfNeurons.Initialize(NumberOfNeurons, type, LayerOfInputNeuronesMocked.Object);
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
                testLayerOfNeurons.Initialize(NumberOfNeurons, type, LayerOfInputNeuronesMocked.Object);
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
        public void LayerOfNeuronsUpdateWeightShouldBeGood()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                testLayerOfNeurons.Initialize(NumberOfNeurons, type, LayerOfInputNeuronesMocked.Object);
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