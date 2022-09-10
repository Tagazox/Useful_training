﻿
using Useful_training.Core.Neural_network.Neuron.Exceptions;

namespace Useful_training.Core.Neural_network.Neuron.Tests
{
    public class DerivativeSigmoidNeuronTests
    {
        Random _rand;
        int _numberOnInputs;
        List<double> _inputs;
        public DerivativeSigmoidNeuronTests()
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
            DerivativeSigmoidNeuron derivativeSigmoidNeuron = new DerivativeSigmoidNeuron();
            derivativeSigmoidNeuron.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = derivativeSigmoidNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeInRange(0, 0.3);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            DerivativeSigmoidNeuron derivativeSigmoidNeuron = new DerivativeSigmoidNeuron();
            derivativeSigmoidNeuron.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = derivativeSigmoidNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = derivativeSigmoidNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }
        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            DerivativeSigmoidNeuron derivativeSigmoidNeuron = new DerivativeSigmoidNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = derivativeSigmoidNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneCalculationShouldThrowCantInitialiseWithZeroInputException()
        {
            DerivativeSigmoidNeuron derivativeSigmoidNeuron = new DerivativeSigmoidNeuron();
            Action Calculate = () =>
            {
                derivativeSigmoidNeuron.InitialiseWithRandomValues(0);
            };
            Calculate.Should().Throw<CantInitialiseWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            DerivativeSigmoidNeuron derivativeSigmoidNeuron = new DerivativeSigmoidNeuron();
            derivativeSigmoidNeuron.InitialiseWithRandomValues(_numberOnInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = derivativeSigmoidNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
