using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;

[assembly: InternalsVisibleTo("Useful_training.Core.Neural_network.Neural_NetworkTests")]
namespace Useful_training.Core.Neural_network
{
	[Serializable]
	public class Neural_Network : INeural_Network
	{
		private readonly ILayerOfInputNeurons InputLayer;
		internal IList<ILayerOfNeurons> LayersOfNeurons { get; set; }
		private double LearnRate { get; set; }
		private double Momentum { get; set; }
		public Neural_Network(uint _numberOfInput, double? learnRate, double? momentum)
		{
			if (_numberOfInput <= 0)
				throw new CantInitializeWithZeroInputException("You can't create a neural network with 0 input");
			LearnRate = learnRate ?? .4;
			Momentum = momentum ?? .9;
			LayersOfNeurons = new List<ILayerOfNeurons>();
			InputLayer = new LayerOfInputNeurons(_numberOfInput);
		}
		internal void AddHiddenLayer(uint numberOfNeuron, NeuronType typeOfNeurons)
		{
			if (numberOfNeuron == 0)
				throw new CantInitializeWithZeroNeuronException("_numberOfNeuron need to be greater than 0, you can't create a layer with 0 neurons");
			LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
			if (LayersOfNeurons.Count == 0)
				layerOfNeurons.Initialize(numberOfNeuron, typeOfNeurons, InputLayer);
			else
				layerOfNeurons.Initialize(numberOfNeuron, typeOfNeurons, LayersOfNeurons.Last());
			LayersOfNeurons.Add(layerOfNeurons);
		}
		public IList<double> Calculate(IList<double> inputs)
		{
			if (inputs == null || inputs.Count == 0 || InputLayer.InputNeurons.Count() != inputs.Count)
				throw new WrongInputForCalculationException("Inputs for the calculation need to be equal as the input number specified at the creation of the neural network");

			int inputCounter = 0;
			foreach (IInputNeurons inputNeurons in InputLayer.InputNeurons)
				inputNeurons.OutputResult = inputs[inputCounter++];

			foreach (ILayerOfNeurons layerOfNeurons in LayersOfNeurons)
				layerOfNeurons.Calculate();

			return LayersOfNeurons.Last().Outputs;
		}
		public void BackPropagate(List<double> targets)
		{
			LayersOfNeurons = LayersOfNeurons.Reverse().ToList();

			ILayerOfNeurons outputLayer = LayersOfNeurons.First();
			List<ILayerOfNeurons> hiddenLayers = LayersOfNeurons.Skip(1).ToList();

			if (outputLayer.Neurons.Count != targets.Count)
				throw new ArgumentException("Targets need to have the same count as the outputs layer number of neurones");

			outputLayer.CalculateGradiant(targets);
			foreach (ILayerOfNeurons layers in hiddenLayers)
			{
				layers.CalculateGradiant();
				layers.UpdateWeights(LearnRate, Momentum);
			}
			outputLayer.UpdateWeights(LearnRate, Momentum);

			LayersOfNeurons = LayersOfNeurons.Reverse().ToList();
		}

		#region serialization
		protected Neural_Network(SerializationInfo info, StreamingContext context)
		{
			Momentum = info.GetDouble("Momentum");
			LearnRate = info.GetDouble("LearnRate");
			InputLayer = new LayerOfInputNeurons(info.GetUInt32("NumberOfInput"));
			LayersOfNeurons = new List<ILayerOfNeurons>();
			dynamic layerOfNeuronsData = info.GetValue("LayersOfNeurons", typeof(object));
			foreach (var layer in layerOfNeuronsData)
			{
				IList<NeuronSerializedData> layerData = layer.NeuronList.ToObject(typeof(List<NeuronSerializedData>));
				AddHiddenLayer((uint)layerData.Count, (NeuronType)layerData.First().Type);
				if (layerData.Count != LayersOfNeurons.Last().Neurons.Count)
					throw new Exception("incoherent Data");

                for (int i = 0; i < LayersOfNeurons.Last().Neurons.Count; i++)
                {
					LayersOfNeurons.Last().Neurons[i].Weight = layerData[i].Weight;
					LayersOfNeurons.Last().Neurons[i].WeightDelta = layerData[i].WeightDelta;
					LayersOfNeurons.Last().Neurons[i].Bias = layerData[i].Bias;
					LayersOfNeurons.Last().Neurons[i].BiasDelta = layerData[i].BiasDelta;
				}
			}
		}
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("NumberOfInput", (uint)InputLayer.InputNeurons.Count());
			info.AddValue("Momentum", Momentum);
			info.AddValue("LearnRate", LearnRate);
			info.AddValue("LayersOfNeurons", LayersOfNeurons.ToArray());
		}

		private class NeuronSerializedData
		{
			public List<double> Weight;
			public List<double> WeightDelta;
			public double Bias;
			public double BiasDelta;
			public int Type;
		}

		#endregion
	}
}
