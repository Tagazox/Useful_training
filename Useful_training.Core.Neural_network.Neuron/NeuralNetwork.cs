using System.Runtime.Serialization;
using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.Interfaces;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork
{
	[Serializable]
    public class NeuralNetwork : INeuralNetwork
    {
        private ILayerOfInputNeurons? InputsLayer;
        internal IList<ILayerOfNeurons> LayersOfNeurons { get; set; }
        private double LearnRate { get; set; }
        private double Momentum { get; set; }
        public NeuralNetwork()
        {
            LayersOfNeurons = new List<ILayerOfNeurons>();
        }
        internal void Initialize(uint _numberOfInput, double? learnRate, double? momentum)
        {
            if (_numberOfInput <= 0)
                throw new CantInitializeWithZeroInputException("You can't create a neural network with 0 input");
            if (learnRate <= 0 || learnRate > 1)
                throw new ArgumentException("Learn rate need to be between 0 and 1");
            if (momentum <= 0 || momentum > 1)
                throw new ArgumentException("Momentum need to be between 0 and 1");
            InputsLayer = new LayerOfInputNeurons(_numberOfInput);
            LearnRate = learnRate ?? .4;
            Momentum = momentum ?? .9;
        }
        internal void AddHiddenLayer(uint NumberOfNeuron, NeuronType typeOfNeurons)
        {
            if (InputsLayer == null)
                throw new NeedToBeCreatedByTheBuilderException("You need to have initalized the neural network first");
            if (NumberOfNeuron == 0)
                throw new CantInitializeWithZeroNeuronException("Number of neuron need to be greater than 0, you can't create a layer with 0 neurons");
            
            LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
            if (LayersOfNeurons.Count == 0)
                layerOfNeurons.Initialize(NumberOfNeuron, typeOfNeurons, InputsLayer);
            else
                layerOfNeurons.Initialize(NumberOfNeuron, typeOfNeurons, LayersOfNeurons.Last());
            LayersOfNeurons.Add(layerOfNeurons);
        }
        public IList<double> Calculate(IList<double> inputs)
        {
            if (LayersOfNeurons == null)
                throw new InvalidCastException("You need to create the neural netowrk first");
            if (inputs == null || inputs.Count == 0 || InputsLayer.InputsNeurons.Count() != inputs.Count)
                throw new WrongInputForCalculationException("Inputs for the calculation need to be equal as the input number specified at the creation of the neural network");

            int inputCounter = 0;
            foreach (IInputNeurons inputNeurons in InputsLayer.InputsNeurons)
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
        public void Reset()
        {
            foreach (ILayerOfNeurons layer in LayersOfNeurons)
            {
                layer.Reset();
            }
        }
        #region serialization
        protected NeuralNetwork(SerializationInfo info, StreamingContext context)
        {
            Momentum = info.GetDouble("Momentum");
            LearnRate = info.GetDouble("LearnRate");
            InputsLayer = new LayerOfInputNeurons(info.GetUInt32("NumberOfInput"));
            LayersOfNeurons = new List<ILayerOfNeurons>();
            dynamic? layerOfNeuronsData = info.GetValue("LayersOfNeurons", typeof(object));
            if (layerOfNeuronsData == null)
                throw new SerializationException("You can't deserialize without layer of neurons");

            foreach (var layer in layerOfNeuronsData)
            {
                IList<NeuronSerializedData> layerOfNeuronData = layer.NeuronList.ToObject(typeof(List<NeuronSerializedData>));
                AddHiddenLayer((uint)layerOfNeuronData.Count, (NeuronType)layerOfNeuronData.First().Type);

                for (int i = 0; i < LayersOfNeurons.Last().Neurons.Count; i++)
                {
                    LayersOfNeurons.Last().Neurons[i].Weight = layerOfNeuronData[i].Weight;
                    LayersOfNeurons.Last().Neurons[i].WeightDelta = layerOfNeuronData[i].WeightDelta;
                    LayersOfNeurons.Last().Neurons[i].Bias = layerOfNeuronData[i].Bias;
                    LayersOfNeurons.Last().Neurons[i].BiasDelta = layerOfNeuronData[i].BiasDelta;
                }
            }
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("NumberOfInput", (uint)InputsLayer.InputsNeurons.Count());
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
