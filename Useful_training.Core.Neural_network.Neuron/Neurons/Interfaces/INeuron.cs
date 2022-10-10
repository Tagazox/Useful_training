using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests.Test")]
namespace Useful_training.Core.NeuralNetwork.Neurons.Interfaces;

internal interface INeuron : IInputNeurons , ISerializable
{
	List<double> WeightDelta { set; }
	List<double> Weight { set; }
	double Bias { set; }
	double BiasDelta { set; }
	double Gradiant { get; set; }
	double CalculateGradient(double? target = null);
	new INeuron Clone();
	void GetCalculationResult();
	void UpdateWeights(double learnRate, double momentum);
	void Reset();
}