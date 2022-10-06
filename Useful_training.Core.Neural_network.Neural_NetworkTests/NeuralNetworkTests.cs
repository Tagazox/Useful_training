using Newtonsoft.Json;
using Useful_training.Core.NeuralNetwork;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests
{
    public class NeuralNetworkTests
    {
        private readonly NeuralNetwork _testSubject;
        private readonly NeuralNetwork _initializedTestSubject;
        private readonly IEnumerable<NeuronType> _neuronTypeAvailable;
        private List<double> _input;
        readonly Random _rand;
        public NeuralNetworkTests()
        {
            _neuronTypeAvailable = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>();
            _input = new List<double> { 1, 0 };
            _rand = new Random();
            _testSubject = new NeuralNetwork();
            _initializedTestSubject = new NeuralNetwork();
			_initializedTestSubject.Initialize((uint)_input.Count, null, null);
            foreach (NeuronType type in _neuronTypeAvailable)
            {
				_initializedTestSubject.AddHiddenLayer((uint)_rand.Next(1, 10), type);
            }
        }
        [Fact]
        public void NeuralNetworkInitializeShouldBeGood()
        {
            _testSubject.Initialize((uint)_input.Count, null, null);
            _testSubject.Initialize((uint)_input.Count, .4, .5);
        }
        [Fact]
        public void NeuralNetworkShouldThrowArgumentException()
        {
            Action InitializeBadLearnRate = () =>
            {
                _testSubject.Initialize(2, -.5, .5);
            };
            Action InitializeBadMomentum = () =>
            {
                _testSubject.Initialize(2, .5, -.5);
            };
            InitializeBadLearnRate.Should().Throw<ArgumentException>();
            InitializeBadMomentum.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void NeuralNetworkShouldBeResetGood()
        {
            IList<double> result = _initializedTestSubject.Calculate(_input);
            _initializedTestSubject.Reset();
            _initializedTestSubject.Calculate(_input).Should().NotBeSameAs(result);
        }
        [Fact]
        public void NeuralNetworkAddHidenLayerShouldBeGood()
        {
            foreach (NeuronType type in _neuronTypeAvailable)
            {
                _initializedTestSubject.AddHiddenLayer((uint)_rand.Next(1, 20), type);
            }
        }
        [Fact]
        public void NeuralNetworkAddHidenLayerShouldThrowNeedToBeCreatedByTheBuilderException()
        {
            foreach (NeuronType type in _neuronTypeAvailable)
            {
                Action AddHiddenLayer = () =>
                {
                    _testSubject.AddHiddenLayer(0, type);
                };
                AddHiddenLayer.Should().Throw<NeedToBeCreatedByTheBuilderException>();
            }
        }
        [Fact]
        public void NeuralNetworkAddHidenLayerShouldThrowCantInitializeWithZeroNeuronException()
        {
            foreach (NeuronType type in _neuronTypeAvailable)
            {
                Action AddHiddenLayer = () =>
                {
                    _initializedTestSubject.AddHiddenLayer(0, type);
                };
                AddHiddenLayer.Should().Throw<CantInitializeWithZeroNeuronException>();
            }
        }
        [Fact]
        public IList<double> NeuralNetworkCalculateShouldBeGood()
        {
            uint lastLayerCount = 0;
            _testSubject.Initialize((uint)_input.Count, null, null);
            foreach (NeuronType type in _neuronTypeAvailable)
            {
                lastLayerCount = (uint)_rand.Next(1, 10);
                _testSubject.AddHiddenLayer(lastLayerCount, type);
            }
            IList<double> result = _testSubject.Calculate(_input);
            result.Count.Should().Be((int)lastLayerCount);
            return result;
        }
        [Fact]
        public void NeuralNetworkCalculateShouldThrowWrongInputForCalculationException()
        {
            uint lastLayerCount = 0;
            _testSubject.Initialize((uint)_input.Count, null, null);
            foreach (NeuronType type in _neuronTypeAvailable)
            {
                lastLayerCount = (uint)_rand.Next(1, 10);
                _testSubject.AddHiddenLayer(lastLayerCount, type);
            }


            Action CalculateCase1 = () =>
            {
                _input = new List<double> { 1 };
                _testSubject.Calculate(_input);
            };
            Action CalculateCase2 = () =>
            {
                _input = new List<double> { 1, 0, 0 };
                _testSubject.Calculate(_input);
            };
            CalculateCase1.Should().Throw<WrongInputForCalculationException>();
            CalculateCase2.Should().Throw<WrongInputForCalculationException>();
        }
        [Fact]
        public void NeuralNetworkBackPropagateShouldBeGood()
        {
            uint lastLayerCount = 0;
            _testSubject.Initialize((uint)_input.Count, null, null);
            foreach (NeuronType type in _neuronTypeAvailable)
            {
                lastLayerCount = (uint)_rand.Next(1, 10);
                _testSubject.AddHiddenLayer(lastLayerCount, type);
            }
            List<double> targets = new List<double>();
            for (int i = 0; i < lastLayerCount; i++)
            {
                targets.Add(1);
            }
            IList<double> output = _testSubject.Calculate(_input);
            ((INeuralNetwork)_testSubject).BackPropagate(targets);
            _testSubject.Calculate(_input).Should().NotBeSameAs(output);
        }
        [Fact]
        public void NeuralNetworkBackPropagateShouldThrowArgumentException()
        {
            uint lastLayerCount = 0;
            _testSubject.Initialize((uint)_input.Count, null, null);
            foreach (NeuronType type in _neuronTypeAvailable)
            {
                lastLayerCount = (uint)_rand.Next(1, 10);
                _testSubject.AddHiddenLayer(lastLayerCount, type);
            }
            List<double> targets = new List<double>();
            for (int i = 0; i < lastLayerCount - 1; i++)
            {
                targets.Add(1);
            }
            Action BackPropagateCase1 = () =>
            {
                ((INeuralNetwork)_testSubject).BackPropagate(targets);
            };
            BackPropagateCase1.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void NeuralNetworkSerialisationShouldBeGood()
        {
            uint lastLayerCount = 0;
            _testSubject.Initialize((uint)_input.Count, null, null);
            foreach (NeuronType type in _neuronTypeAvailable)
            {
                lastLayerCount = (uint)_rand.Next(1, 10);
                _testSubject.AddHiddenLayer(lastLayerCount, type);
            }
            List<double>? targets = new List<double>();
            for (int i = 0; i < lastLayerCount; i++)
            {
                targets.Add(1);
            }
            ((INeuralNetwork)_testSubject).BackPropagate(targets);
            string json = JsonConvert.SerializeObject((object?)_testSubject, Formatting.Indented);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
            INeuralNetwork NeuralNetworkDeserialized = JsonConvert.DeserializeObject<NeuralNetwork>(json);

            IList<double> resultsOfDeserializedNetwork = NeuralNetworkDeserialized.Calculate(_input);
            IList<double> resultsOfTestSubject = _testSubject.Calculate(_input);

            resultsOfDeserializedNetwork.Should().BeEquivalentTo(resultsOfTestSubject);

            ((INeuralNetwork)_testSubject).BackPropagate(targets);
            NeuralNetworkDeserialized.BackPropagate(targets);

            resultsOfDeserializedNetwork = NeuralNetworkDeserialized.Calculate(_input);
            resultsOfTestSubject = _testSubject.Calculate(_input);

            resultsOfDeserializedNetwork.Should().BeEquivalentTo(resultsOfTestSubject);

        }
    }
}