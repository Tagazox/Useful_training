using System.Runtime.Serialization;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.Helpers;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Core.NeuralNetwork.Neurons;

[Serializable]
internal abstract class Neuron : INeuron
{
    public double Gradiant { get; set; }
    public double OutputResult { get; set; }
    public double Bias { protected get; set; }
    public double BiasDelta { protected get; set; }
    List<Synapse> IInputNeuron.OutputSynapses { get; } = new List<Synapse>();
    protected List<Synapse> InputSynapses { get; set; } = new List<Synapse>();

    #region serialization

    public List<double> WeightDelta
    {
        set
        {
            if (value.Count != InputSynapses.Count)
                throw new ArgumentException(
                    "if you set the weight delta you need to have the same count as number neuron");
            for (int i = 0; i < value.Count; i++)
                InputSynapses[i].WeightDelta = value[i];
        }
    }

    public List<double> Weight
    {
        set
        {
            if (value.Count != InputSynapses.Count)
                throw new ArgumentException(
                    "if you set the weight you need to have the same count as number neuron");
            for (int i = 0; i < value.Count; i++)
                InputSynapses[i].Weight = value[i];
        }
    }

    #endregion

    public abstract void GetCalculationResult();
    internal abstract double DerivativeFunctionResultCalculation();

    protected Neuron(IEnumerable<IInputNeuron> inputNeurons)
    {
        var inputNeuronsEnumerable = inputNeurons as IInputNeuron[] ?? inputNeurons.ToArray();
        if (inputNeurons == null || !inputNeuronsEnumerable.Any())
            throw new CantInitializeWithZeroInputException("Neuron need to be initialize with at least one input");
        Random random = new Random();
        BiasDelta = RandomNumber.GetMirrorDouble();
        Bias = RandomNumber.GetMirrorDouble();
        foreach (IInputNeuron inputNeuron in inputNeuronsEnumerable)
        {
            Synapse synapse = new Synapse(inputNeuron, this);
            inputNeuron.OutputSynapses.Add(synapse);
            InputSynapses.Add(synapse);
        }
    }

    public INeuron Clone()
    {
        return (INeuron)MemberwiseClone();
    }

    internal double GetInterpolationResult()
    {
        return InputSynapses.Sum(a => a.Weight * a.InputNeuron.OutputResult) + Bias;
    }

    public void UpdateWeights(double learnRate, double momentum)
    {
        UpdateBiasWeights(learnRate, momentum);
        UpdateSynapsesWeights(learnRate, momentum);
    }

    private void UpdateBiasWeights(double learnRate, double momentum)
    {
        double prevDelta = BiasDelta;
        BiasDelta = learnRate * Gradiant;
        Bias += BiasDelta + momentum * prevDelta;
    }

    public double CalculateGradient(double? target = null)
    {
        if (target == null)
            return Gradiant = ((IInputNeuron)this).OutputSynapses.Sum(a => a.OutputNeuron.Gradiant * a.Weight) *
                              DerivativeFunctionResultCalculation();

        return Gradiant = CalculateError(target.Value) * DerivativeFunctionResultCalculation();
    }

    private void UpdateSynapsesWeights(double learnRate, double momentum)
    {
        foreach (var synapse in InputSynapses)
        {
            double prevDelta = synapse.WeightDelta;
            synapse.WeightDelta = learnRate * Gradiant * synapse.InputNeuron.OutputResult;
            synapse.Weight += synapse.WeightDelta + momentum * prevDelta;
            synapse.Weight = synapse.Weight;
        }
    }

    private double CalculateError(double target)
    {
        return target - OutputResult;
    }

    IInputNeuron IInputNeuron.Clone()
    {
        return (IInputNeuron)MemberwiseClone();
    }

    #region serialization

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Weight", InputSynapses.Select(w => w.Weight));
        info.AddValue("WeightDelta", InputSynapses.Select(w => w.WeightDelta));
        info.AddValue("Gradiant", Gradiant);
        info.AddValue("Bias", Bias);
        info.AddValue("BiasDelta", BiasDelta);
        info.AddValue("Type", (int)GetNeuronType());
    }

    protected abstract NeuronType GetNeuronType();

    public void Reset()
    {
        foreach (Synapse synapse in InputSynapses)
        {
            Bias = RandomNumber.GetMirrorDouble();
            BiasDelta = RandomNumber.GetMirrorDouble();
            synapse.Reset();
        }
    }

    #endregion
}