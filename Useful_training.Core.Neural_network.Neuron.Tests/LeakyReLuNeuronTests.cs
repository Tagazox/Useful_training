﻿

using Useful_training.Core.Neural_network.Neuron.Exceptions;

namespace Useful_training.Core.Neural_network.Neuron.Tests
{
    public class LeakyReLuNeuronTests
    {
        Random _rand;
        int _numberOnInputs;
        List<double> _inputs;
        public LeakyReLuNeuronTests()
        {
            _rand = new Random();
            _numberOnInputs = _rand.Next(1, 10);
            _inputs = new List<double>();
            for (int i = 0; i < _numberOnInputs; i++)
            {
                _inputs.Add(_rand.NextDouble() * 2 - 1);
            }
        }

        [Fact]
        public void NeuroneCalculationShouldBeOk()
        {
            LeakyReLuNeuron leakyReLu = new LeakyReLuNeuron();
            leakyReLu.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = leakyReLu.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeOfType(typeof(double));
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            LeakyReLuNeuron leakyReLu = new LeakyReLuNeuron();
            leakyReLu.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = leakyReLu.GetCalculationResult(_inputs);

            INeuron cloneNeuron = leakyReLu.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }
        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            LeakyReLuNeuron leakyReLu = new LeakyReLuNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = leakyReLu.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneCalculationShouldThrowCantInitialiseWithZeroInputException()
        {
            LeakyReLuNeuron leakyReLu = new LeakyReLuNeuron();
            Action Initialise = () =>
            {
                leakyReLu.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitialiseWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            LeakyReLuNeuron leakyReLu = new LeakyReLuNeuron();
            leakyReLu.InitialiseWithRandomValues(_numberOnInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = leakyReLu.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
