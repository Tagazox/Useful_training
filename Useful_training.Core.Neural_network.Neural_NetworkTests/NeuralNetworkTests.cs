using Newtonsoft.Json;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class NeuralNetworkTests
    {
        NeuralNetwork TestSubject;
        NeuralNetwork InitializedTestSubject;
        IEnumerable<NeuronType> NeuronTypeAvailable;
        List<double> input;
        Random rand;
        public NeuralNetworkTests()
        {
            NeuronTypeAvailable = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>();
            input = new List<double> { 1, 0 };
            rand = new Random();
            TestSubject = new NeuralNetwork();
            InitializedTestSubject = new NeuralNetwork();
            uint lastLayerCount = 0;
            InitializedTestSubject.Initialize((uint)input.Count, null, null);
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                lastLayerCount = (uint)rand.Next(1, 10);
                InitializedTestSubject.AddHiddenLayer(lastLayerCount, type);
            }
        }
        [Fact]
        public void Neural_NetworkShouldBeInitializedGood()
        {
            TestSubject.Initialize((uint)input.Count, null, null);
            TestSubject.Initialize((uint)input.Count, .4, .5);
        }
        [Fact]
        public void Neural_NetworkShouldThrowArgumentException()
        {
            Action InitializeBadLearnRate = () =>
            {
                TestSubject.Initialize(2, -.5, .5);
            };
            Action InitializeBadMomentum = () =>
            {
                TestSubject.Initialize(2, .5, -.5);
            };
            InitializeBadLearnRate.Should().Throw<ArgumentException>();
            InitializeBadMomentum.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void Neural_NetworkShouldBeResetGood()
        {
            IList<double> result = InitializedTestSubject.Calculate(input);
            InitializedTestSubject.Reset();
            InitializedTestSubject.Calculate(input).Should().NotBeSameAs(result);
        }
        [Fact]
        public void Neural_NetworkAddHidenLayerShouldBeGood()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                InitializedTestSubject.AddHiddenLayer((uint)rand.Next(1, 20), type);
            }
        }
        [Fact]
        public void Neural_NetworkAddHidenLayerShouldThrowNeedToBeCreatedByTheBuilderException()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                Action AddHiddenLayer = () =>
                {
                    TestSubject.AddHiddenLayer(0, type);
                };
                AddHiddenLayer.Should().Throw<NeedToBeCreatedByTheBuilderException>();
            }
        }
        [Fact]
        public void Neural_NetworkAddHidenLayerShouldThrowCantInitializeWithZeroNeuronException()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                Action AddHiddenLayer = () =>
                {
                    InitializedTestSubject.AddHiddenLayer(0, type);
                };
                AddHiddenLayer.Should().Throw<CantInitializeWithZeroNeuronException>();
            }
        }
        [Fact]
        public IList<double> Neural_NetworkCalculateShouldBeGood()
        {
            uint lastLayerCount = 0;
            TestSubject.Initialize((uint)input.Count, null, null);
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                lastLayerCount = (uint)rand.Next(1, 10);
                TestSubject.AddHiddenLayer(lastLayerCount, type);
            }
            IList<double> result = TestSubject.Calculate(input);
            result.Count.Should().Be((int)lastLayerCount);
            return result;
        }
        [Fact]
        public void Neural_NetworkCalculateShouldThrowWrongInputForCalculationException()
        {
            uint lastLayerCount = 0;
            TestSubject.Initialize((uint)input.Count, null, null);
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                lastLayerCount = (uint)rand.Next(1, 10);
                TestSubject.AddHiddenLayer(lastLayerCount, type);
            }


            Action CalculateCase1 = () =>
            {
                input = new List<double> { 1 };
                TestSubject.Calculate(input);
            };
            Action CalculateCase2 = () =>
            {
                input = new List<double> { 1, 0, 0 };
                TestSubject.Calculate(input);
            };
            CalculateCase1.Should().Throw<WrongInputForCalculationException>();
            CalculateCase2.Should().Throw<WrongInputForCalculationException>();
        }
        [Fact]
        public void Neural_NetworkBackPropagateShouldBeGood()
        {
            uint lastLayerCount = 0;
            TestSubject.Initialize((uint)input.Count, null, null);
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                lastLayerCount = (uint)rand.Next(1, 10);
                TestSubject.AddHiddenLayer(lastLayerCount, type);
            }
            List<double> targets = new List<double>();
            for (int i = 0; i < lastLayerCount; i++)
            {
                targets.Add(1);
            }
            IList<double> output = TestSubject.Calculate(input);
            TestSubject.BackPropagate(targets);
            TestSubject.Calculate(input).Should().NotBeSameAs(output);
        }
        [Fact]
        public void Neural_NetworkBackPropagateShouldThrowArgumentException()
        {
            uint lastLayerCount = 0;
            TestSubject.Initialize((uint)input.Count, null, null);
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                lastLayerCount = (uint)rand.Next(1, 10);
                TestSubject.AddHiddenLayer(lastLayerCount, type);
            }
            List<double> targets = new List<double>();
            for (int i = 0; i < lastLayerCount - 1; i++)
            {
                targets.Add(1);
            }
            Action BackPropagateCase1 = () =>
            {
                TestSubject.BackPropagate(targets);
            };
            BackPropagateCase1.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Neural_NetworkSerialisationShouldBeGood()
        {
            uint lastLayerCount = 0;
            TestSubject.Initialize((uint)input.Count, null, null);
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                lastLayerCount = (uint)rand.Next(1, 10);
                TestSubject.AddHiddenLayer(lastLayerCount, type);
            }
            List<double> targets = new List<double>();
            for (int i = 0; i < lastLayerCount; i++)
            {
                targets.Add(1);
            }
            TestSubject.BackPropagate(targets);
            string json = JsonConvert.SerializeObject(TestSubject, Formatting.Indented);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
            INeuralNetwork neural_NetworkDeserialized = JsonConvert.DeserializeObject<NeuralNetwork>(json);

            IList<double> resultsOfDeserializedNetwork = neural_NetworkDeserialized.Calculate(input);
            IList<double> resultsOfTestSubject = TestSubject.Calculate(input);

            resultsOfDeserializedNetwork.Should().BeEquivalentTo(resultsOfTestSubject);

            TestSubject.BackPropagate(targets);
            neural_NetworkDeserialized.BackPropagate(targets);

            resultsOfDeserializedNetwork = neural_NetworkDeserialized.Calculate(input);
            resultsOfTestSubject = TestSubject.Calculate(input);

            resultsOfDeserializedNetwork.Should().BeEquivalentTo(resultsOfTestSubject);

        }
    }
}