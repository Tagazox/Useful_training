using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class Neural_NetworkTests
    {
        Neural_Network _neural_Network;
        IEnumerable<NeuronType> NeuronTypeAvailable;
        List<double> input;
        uint numberOfInput;
        uint numberOfNeurons;
        Random rand;

        public Neural_NetworkTests()
        {
            _neural_Network = new Neural_Network();
            NeuronTypeAvailable = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>();
            numberOfInput = 2;
            numberOfNeurons = 3;
            input = new List<double> { 1, 0 };
            rand = new Random();
        }
        [Fact]
        public void Neural_NetworkShouldAddLayerGood()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
                _neural_Network.AddLayer(1, 1, type);
            _neural_Network._layersOfNeurons.Count.Should().Be(NeuronTypeAvailable.Count());
        }
        [Fact]
        public void Neural_NetworkShouldCalculateGood()
        {
            _neural_Network.AddLayer(numberOfInput, numberOfNeurons, NeuronType.Sigmoid);
            for (int i = 0; i < 5; i++)
            {
                _neural_Network.AddLayer(numberOfNeurons, numberOfNeurons, NeuronType.Sigmoid);
            }
            _neural_Network.Calculate(input).Count.Should().Be((int)numberOfNeurons);
        }
        [Fact]
        public void Neural_NetworkShouldBackPropagateGood()
        {
            uint NumberOfOutput = 2;
            _neural_Network.AddLayer(numberOfInput, numberOfNeurons, NeuronType.Elu);
            uint PreviousNumberOfNeurones = numberOfNeurons;
            int nbHiddenLayer = rand.Next(5, 20);
            for (int i = 0; i < nbHiddenLayer; i++)
            {
                numberOfNeurons = (uint)rand.Next(2, 5);
                _neural_Network.AddLayer(PreviousNumberOfNeurones, numberOfNeurons, NeuronType.Elu);
                PreviousNumberOfNeurones = numberOfNeurons;
            }
            _neural_Network.AddLayer(PreviousNumberOfNeurones, NumberOfOutput, NeuronType.Elu);

            _neural_Network.Calculate(input).Count.Should().Be((int)NumberOfOutput);
            List<double> target = new List<double>();
            for (int i = 0; i < NumberOfOutput; i++)
                target.Add(rand.NextDouble());

            bool trainfinish = false;
            double deltaError = double.MaxValue;
            while (!trainfinish)
            {
                trainfinish = true;
                IList<double> results = _neural_Network.Calculate(input);
                for (int i = 0; i < results.Count; i++)
                {
                    deltaError = results[i] - target[i];
                    if (Math.Abs(deltaError) > 0.00001 && trainfinish)
                    {
                        trainfinish = false;
                    }
                }
                
                _neural_Network.BackPropagate(target);
            }

        }
        [Fact]
        public void Neural_NetworkBackPropagateShoudthrowArgumentException()
        {
            uint NumberOfOutput = 2;
            _neural_Network.AddLayer(numberOfInput, numberOfNeurons, NeuronType.Elu);
            uint PreviousNumberOfNeurones = numberOfNeurons;
            int nbHiddenLayer = rand.Next(5, 20);
            for (int i = 0; i < nbHiddenLayer; i++)
            {
                numberOfNeurons = (uint)rand.Next(2, 5);
                _neural_Network.AddLayer(PreviousNumberOfNeurones, numberOfNeurons, NeuronType.Elu);
                PreviousNumberOfNeurones = numberOfNeurons;
            }
            _neural_Network.AddLayer(PreviousNumberOfNeurones, NumberOfOutput, NeuronType.Elu);

            _neural_Network.Calculate(input).Count.Should().Be((int)NumberOfOutput);
            List<double> target = new List<double>();
            for (int i = 0; i < NumberOfOutput - 1; i++)
                target.Add(rand.NextDouble());

            int counter = 3;
            double deltaError = double.MaxValue;

            IList<double> results = _neural_Network.Calculate(input);
            Action BackPropagate = () =>
            {
                _neural_Network.BackPropagate(target);
            };
            BackPropagate.Should().Throw<ArgumentException>();


        }
        [Fact]
        public void Neural_NetworkCalculateShouldThrowNeedToBeCreatedByTheBuilderException()
        {
            Action Calculate = () =>
            {
                _neural_Network.Calculate(input);
            };
            Calculate.Should().Throw<NeedToBeCreatedByTheBuilderException>();
        }
        [Fact]
        public void Neural_NetworkCalculateShouldThrowWrongInputForCalculationException()
        {
            List<double> inputForThisTest = new List<double> { 1 };
            _neural_Network.AddLayer(numberOfInput, numberOfNeurons, NeuronType.Sigmoid);
            Action Calculate = () =>
            {
                _neural_Network.Calculate(inputForThisTest);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
        [Fact]
        public void Neural_NetworkAddLayerShouldThrowCantInitializeWithZeroInputException()
        {
            Action AddLayer = () =>
            {
                _neural_Network.AddLayer(0, numberOfNeurons, NeuronType.Sigmoid);
            };
            AddLayer.Should().Throw<CantInitializeWithZeroInputException>();
        }
        [Fact]
        public void Neural_NetworkAddLayerShouldThrowCantInitializeWithZeroNeuronException()
        {
            Action AddLayer = () =>
            {
                _neural_Network.AddLayer(numberOfInput, 0, NeuronType.Sigmoid);
            };
            AddLayer.Should().Throw<CantInitializeWithZeroNeuronException>();
        }

    }
}