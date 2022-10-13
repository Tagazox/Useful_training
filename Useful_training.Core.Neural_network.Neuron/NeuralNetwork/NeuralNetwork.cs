using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.LayersOfNeurones;
using Useful_training.Core.NeuralNetwork.LayersOfNeurones.Interfaces;
using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork;

[Serializable]
public class NeuralNetwork : INeuralNetwork
{
    private ILayerOfInputNeurons? _inputsLayer;
    private IList<double>? CalculationResults { get; set; }
    internal IList<ILayerOfNeurons> LayersOfNeurons { get; set; }
    private double LearnRate { get; set; }
    private double Momentum { get; set; }
    public bool IsUnstable => CalculationResults?.Contains(double.NaN) ?? false;
    public IList<double> LastCalculationResults => CalculationResults ??
                                                   throw new InvalidOperationException(
                                                       "You need to calculate using the neural network to get calculation results");
    public NeuralNetwork()
    {
        CalculationResults = null;
        LayersOfNeurons = new List<ILayerOfNeurons>();
    }

    internal void Initialize(uint numberOfInput, double? learnRate, double? momentum)
    {
        if (numberOfInput <= 0)
            throw new CantInitializeWithZeroInputException("You can't create a neural network with 0 input");
        if (learnRate is <= 0 or > 1)
            throw new ArgumentException("Learn rate need to be between 0 and 1");
        if (momentum is <= 0 or > 1)
            throw new ArgumentException("Momentum need to be between 0 and 1");
        _inputsLayer = new LayerOfInputNeurons(numberOfInput);
        LearnRate = learnRate ?? .4;
        Momentum = momentum ?? .9;
    }

    internal void AddHiddenLayer(uint numberOfNeuron, NeuronType typeOfNeurons)
    {
        if (_inputsLayer == null)
            throw new NeedToBeCreatedByTheBuilderException("You need to have initialized the neural network first");
        if (numberOfNeuron == 0)
            throw new CantInitializeWithZeroNeuronException(
                "Number of neuron need to be greater than 0, you can't create a layer with 0 neurons");

        LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
        layerOfNeurons.Initialize(numberOfNeuron, typeOfNeurons,
            (LayersOfNeurons.Count == 0) ? _inputsLayer : LayersOfNeurons.Last());
        LayersOfNeurons.Add(layerOfNeurons);
    }

    public IList<double> Calculate(IList<double> inputs)
    {
        if (LayersOfNeurons == null || _inputsLayer == null)
            throw new InvalidCastException("You need to create the neural network first");
        if (inputs == null || inputs.Count == 0 || _inputsLayer.InputsNeurons.Count() != inputs.Count)
            throw new WrongInputForCalculationException(
                "Inputs for the calculation need to be equal as the input number specified at the creation of the neural network");

        int inputCounter = 0;
        foreach (IInputNeurons inputNeurons in _inputsLayer.InputsNeurons)
            inputNeurons.OutputResult = inputs[inputCounter++];

        foreach (ILayerOfNeurons layerOfNeurons in LayersOfNeurons)
            layerOfNeurons.Calculate();

        return CalculationResults = LayersOfNeurons.Last().Outputs;
    }


    void INeuralNetwork.BackPropagate(List<double>? targets)
    {
        ILayerOfNeurons outputLayer = LayersOfNeurons.Last();
        if (targets == null || outputLayer.Neurons.Count != targets.Count)
            throw new ArgumentException(
                "Targets need to have the same count as the outputs layer number of neurones");
        outputLayer.CalculateGradiant(targets);
        for (int i = LayersOfNeurons.Count - 2; i <= 0; i--)
        {
            LayersOfNeurons[i].CalculateGradiant();
            LayersOfNeurons[i].UpdateWeights(LearnRate, Momentum);
        }

        outputLayer.UpdateWeights(LearnRate, Momentum);
    }

    public void Reset()
    {
        foreach (ILayerOfNeurons layer in LayersOfNeurons)
            layer.Reset();
    }

    #region serialization

    protected NeuralNetwork(SerializationInfo info, StreamingContext context)
    {
        Momentum = info.GetDouble("Momentum");
        LearnRate = info.GetDouble("LearnRate");
        _inputsLayer = new LayerOfInputNeurons(info.GetUInt32("NumberOfInput"));
        LayersOfNeurons = new List<ILayerOfNeurons>();
        dynamic? layerOfNeuronsData = info.GetValue("LayersOfNeurons", typeof(object));
        if (layerOfNeuronsData == null)
            throw new SerializationException("You can't deserialize without layer of neurons");

        foreach (dynamic? layer in layerOfNeuronsData)
        {
            IList<NeuronSerializedData> layerOfNeuronData =
                layer.NeuronList.ToObject(typeof(List<NeuronSerializedData>));
            if (layerOfNeuronData == null || !layerOfNeuronData.Any())
                throw new SerializationException("You can't deserialize without neurons data's");
            AddHiddenLayer((uint)layerOfNeuronData.Count,
                (NeuronType)layerOfNeuronData.First().Type);


            for (int i = 0; i < LayersOfNeurons.Last().Neurons.Count; i++)
            {
                LayersOfNeurons.Last().Neurons[i].Weight = layerOfNeuronData[i].Weight ??
                                                           throw new SerializationException(
                                                               "You can't deserialize without neurons weight data's");
                LayersOfNeurons.Last().Neurons[i].WeightDelta = layerOfNeuronData[i].WeightDelta ??
                                                                throw new SerializationException(
                                                                    "You can't deserialize without neurons weight delta data's");
                LayersOfNeurons.Last().Neurons[i].Bias = layerOfNeuronData[i].Bias;
                LayersOfNeurons.Last().Neurons[i].BiasDelta = layerOfNeuronData[i].BiasDelta;
                LayersOfNeurons.Last().Neurons[i].Gradiant = layerOfNeuronData[i].Gradiant;
            }
        }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("NumberOfInput", (uint)_inputsLayer!.InputsNeurons.Count());
        info.AddValue("Momentum", Momentum);
        info.AddValue("LearnRate", LearnRate);
        info.AddValue("LayersOfNeurons", LayersOfNeurons.ToArray());
    }

    [Serializable]
    private class NeuronSerializedData
    {
        public List<double>? Weight { get; set; }
        public List<double>? WeightDelta { get; set; }
        public double Bias { get; set; }
        public double BiasDelta { get; set; }
        public double Gradiant { get; set; }
        public int Type { get; set; }
    }

    #endregion
}