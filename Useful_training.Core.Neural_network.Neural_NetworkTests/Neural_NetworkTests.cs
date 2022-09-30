using Newtonsoft.Json;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class Neural_NetworkTests
    {
        Neural_Network TestSubject;
        IEnumerable<NeuronType> NeuronTypeAvailable;
        List<double> input;
        Random rand;
        public Neural_NetworkTests()
        {
            NeuronTypeAvailable = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>();
            input = new List<double> { 1, 0 };
            rand = new Random();
            TestSubject = new Neural_Network((uint)input.Count, null, null);
        }
        [Fact]
        public void Neural_NetworkShouldBeCreatedGood()
        {
            TestSubject = new Neural_Network((uint)input.Count, null, null);
            TestSubject = new Neural_Network((uint)input.Count, .4, .5);
        }
        [Fact]
        public void Neural_NetworkAddHidenLayerShouldBeGood()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                TestSubject.AddHiddenLayer((uint)rand.Next(1, 20), type);
            }
        }
        [Fact]
        public void Neural_NetworkAddHidenLayerShouldThrowCantInitializeWithZeroNeuronException()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                Action AddHiddenLayer = () =>
                {
                    TestSubject.AddHiddenLayer(0, type);
                };
                AddHiddenLayer.Should().Throw<CantInitializeWithZeroNeuronException>();
            }
        }
        [Fact]
        public void Neural_NetworkCalculateShouldBeGood()
        {
            uint lastLayerCount = 0;
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                lastLayerCount = (uint)rand.Next(1, 10);
                TestSubject.AddHiddenLayer(lastLayerCount, type);
            }
            TestSubject.Calculate(input).Count.Should().Be((int)lastLayerCount);
        }
        [Fact]
        public void Neural_NetworkCalculateShouldThrowWrongInputForCalculationException()
        {
            uint lastLayerCount = 0;
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
                input = new List<double> { 1,0,0 };
                TestSubject.Calculate(input);
            };
            CalculateCase1.Should().Throw<WrongInputForCalculationException>();
            CalculateCase2.Should().Throw<WrongInputForCalculationException>();
        }
        [Fact]
        public void Neural_NetworkBackPropagateShouldBeGood()
        {
            uint lastLayerCount = 0;
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
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                lastLayerCount = (uint)rand.Next(1, 10);
                TestSubject.AddHiddenLayer(lastLayerCount, type);
            }
            List<double> targets = new List<double>();
            for (int i = 0; i < lastLayerCount-1; i++)
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
            foreach (NeuronType type in NeuronTypeAvailable)
            {
                lastLayerCount = (uint)rand.Next(1, 10);
                TestSubject.AddHiddenLayer(lastLayerCount, type);
            }
			string json = JsonConvert.SerializeObject(TestSubject, Formatting.Indented);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
            INeural_Network neural_NetworkDeserialized = JsonConvert.DeserializeObject<Neural_Network>(json);
            neural_NetworkDeserialized.Calculate(input).Should().AllBeEquivalentTo(TestSubject.Calculate(input));

        }
    }
}