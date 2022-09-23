using Moq;
using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
	public class Neural_NetworkTrainerTests
	{
		Mock<INeuralNetworkContainer> _neural_NetworkContainer;
		List<DataSet> dataSets;
		uint numberOfInput;
		uint numberOfOutput;
		Random rand;
		NeuralNetworkTrainer _neuralNetworkTrainer;

		public Neural_NetworkTrainerTests()
		{
			rand = new Random();
			numberOfInput = 4;
			numberOfOutput = 1;
			int numberOfDataset = 500;

			dataSets = new List<DataSet>();
			for (int i = 0; i < numberOfDataset; i++)
			{
				double input1, input2, input3, input4, output;
				input1 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;
				input2 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;
				input3 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;
				input4 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;

				output = (input1 == 1 || input2 == 1) && (input3 == 0 || input4 == 0) ? 1 : 0;
				List<double> inputs = new List<double>() { input1, input2, input3, input4 };
				List<double> Outputs = new List<double>() { output };
				dataSets.Add(new DataSet(inputs, Outputs));
			}

		}
		private void CreateNewNeuralNetwork()
		{
			uint numberOfInputNeurons = (uint)rand.Next(7, 10);
			uint numberOfNeuronesByHiddenLayerOutput = numberOfInputNeurons;
			uint numberOfHiddenLayers = (uint)rand.Next(2, 5);
			int enumTypeNeuronMaxNumber = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>().Count();
			_neural_NetworkContainer = new Mock<INeuralNetworkContainer>();
			NeuralNetworkBuilder MockedNeuralNetworkBuilder = new NeuralNetworkBuilder();
			MockedNeuralNetworkBuilder.AddInputLayer(numberOfInput, NeuronType.LeakyRelu);
			MockedNeuralNetworkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayerOutput, numberOfHiddenLayers, NeuronType.LeakyRelu);
			MockedNeuralNetworkBuilder.AddOutputLayers(numberOfOutput, NeuronType.Sigmoid);
			_neural_NetworkContainer.Setup(c => c.GetNeuralNetwork()).Returns(MockedNeuralNetworkBuilder.GetNeural_Network());
		}
		[Fact]
		public void Neural_NetworkShouldBeTrainedGood()
		{
			Neural_Network TrainedNeuralNetwork = null;
			do
			{
				CreateNewNeuralNetwork();
				_neuralNetworkTrainer = new NeuralNetworkTrainer(_neural_NetworkContainer.Object);
				TrainedNeuralNetwork = _neuralNetworkTrainer.GetTrainedNeuralNetwork(dataSets);
			} while (TrainedNeuralNetwork == null);

			List<DataSet> testDataSets = new List<DataSet>();
			DataSet testDatasetCase1 = (new DataSet(new List<double>() { 1, 1, 1 ,1}, new List<double>() { 0 }));
			DataSet testDatasetCase2 = (new DataSet(new List<double>() { 0, 0, 0, 1 }, new List<double>() { 0 }));
			DataSet testDatasetCase3 = (new DataSet(new List<double>() { 1, 0, 0, 1 }, new List<double>() { 1 }));
			testDataSets.Add(testDatasetCase1);
			testDataSets.Add(testDatasetCase2);
			testDataSets.Add(testDatasetCase3);
			foreach (DataSet set in testDataSets)
			{
				IList<double> ResultOutputs = TrainedNeuralNetwork.Calculate(set.Values);
				for (int i = 0; i < set.Targets.Count; i++)
				{
					ResultOutputs[i].Should().BeApproximately(set.Targets[i], 0.01);
				}
			}
			

		}

	}
}