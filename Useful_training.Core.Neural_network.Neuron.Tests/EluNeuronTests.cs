
using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Tests
{
	public class EluNeuronTests
	{
		Random _rand;
		uint _numberOfInputs;
		IList<EluNeuron> InputNeurons;
		public EluNeuronTests()
		{
			_rand = new Random();
			_numberOfInputs = (uint)_rand.Next(1, 10);
			InputNeurons=new List<EluNeuron>();
			for (int i = 0; i < _numberOfInputs; i++)
			{
				EluNeuron InputNeuron = new EluNeuron();
				InputNeuron.OutputResult = (_rand.NextDouble() * 2 - 1);
				InputNeurons.Add(InputNeuron);
			}
		}
		[Fact]
		public void NeuroneCalculationShouldBeOk()
		{
			EluNeuron eluNeuron = new EluNeuron(InputNeurons);
			double outputOfTheNeuron = eluNeuron.GetCalculationResult();
			outputOfTheNeuron.Should().BeInRange(-1, double.MaxValue);
		}
		[Fact]
		public void NeuroneCloneShouldBeOk()
		{
			EluNeuron eluNeuron = new EluNeuron(InputNeurons);
			double outputOfTheNeuron = eluNeuron.GetCalculationResult();

			INeuron cloneNeuron = eluNeuron.Clone();
			double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult();
			outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
		}
		[Fact]
		public void NeuroneCalculationShouldThrowCantCalculateWithInputNeurons()
		{
			EluNeuron eluNeuron = new EluNeuron();
			Action Calculate = () =>
			{
				double outputOfTheNeuron = eluNeuron.GetCalculationResult();
			};
			Calculate.Should().Throw<CantCalculateWithInputNeurons>();
		}


	}
}
