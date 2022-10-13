using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Observer.Interfaces;
using Useful_training.Core.NeuralNetwork.Trainers.Adapter;
using Useful_training.Core.NeuralNetwork.Trainers.Interfaces;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Core.NeuralNetwork.Trainers;

public class NeuralNetworkTrainer : INeuralNetworkTrainer
{
    private const double ErrorThreshold = 0.001;
    private readonly INeuralNetwork NeuralNetwork;
    private readonly List<DataSet> DataSetsList;
    private readonly int EpochRoundedTo;
    private readonly IList<INeuralNetworkTrainerObserver> _observers;
    private bool TrainingAbortIsRequested;
    private bool TrainingIsFinished;
    private bool TrainingAbortIsNotRequested => !TrainingAbortIsRequested;
    private bool TrainingIsNotFinished => !TrainingIsFinished;

    private bool ErrorIsAcceptable(IReadOnlyList<double> targets) => CalculateNeuralNetworkError(targets) < ErrorThreshold;

    private bool ErrorIsTooHigh(IReadOnlyList<double> targets) => !ErrorIsAcceptable(targets);

    public NeuralNetworkTrainer(INeuralNetworkTrainerContainer neuralNetworkContainer, int epochRoundedTo = 3)
    {
        TrainingAbortIsRequested = false;
        TrainingIsFinished = false;
        EpochRoundedTo = epochRoundedTo;
        NeuralNetwork = neuralNetworkContainer.NeuralNetwork;
        DataSetsList = neuralNetworkContainer.DataSets;

        if (NeuralNetwork == null)
            throw new NullReferenceException("Any Neural network has been found in the container");
        if (DataSetsList == null || !DataSetsList.Any())
            throw new NullReferenceException("Any data set has been found in the container");

        _observers = new List<INeuralNetworkTrainerObserver>();
    }

    public void TrainNeuralNetwork()
    {
        Random random = new Random();

        while (TrainingIsNotFinished && TrainingAbortIsNotRequested)
        {
            DataSet dataSetForThisIteration = DataSetsList[random.Next(DataSetsList.Count)];

            IList<double> resultsOfTheNeuralNetworkCalculation =
                NeuralNetwork.Calculate(dataSetForThisIteration.Inputs);

            if (NeuralNetwork.IsUnstable)
                NeuralNetwork.Reset();
            else
            {
                NotifyObserver(new NeuralNetworkObservableData(dataSetForThisIteration,
                    resultsOfTheNeuralNetworkCalculation));

                if (ErrorIsAcceptable(dataSetForThisIteration.TargetOutput))
                    TrainingIsFinished = CheckIfTheNeuralNetworkHasBeenEnoughTrained();
                else
                    NeuralNetwork.BackPropagate(dataSetForThisIteration.TargetOutput);
            }
        }
    }

    public void Destroy()
    {
        TrainingAbortIsRequested = true;
    }

    private double CalculateNeuralNetworkError(IReadOnlyList<double> targets)
    {
        double deltaError = -NeuralNetwork.LastCalculationResults.Select((r, i) => Math.Abs(r - targets[i])).Sum();
        return Math.Round(deltaError, EpochRoundedTo);
    }

    private bool CheckIfTheNeuralNetworkHasBeenEnoughTrained(int numberOfSamplesToTest = 20)
    {
        foreach (DataSet dataSet in DataSetsList.Take(numberOfSamplesToTest))
        {
            NeuralNetwork.Calculate(dataSet.Inputs);

            if (ErrorIsTooHigh(dataSet.TargetOutput))
                return false;
        }
        return true;
    }

    public void AttachObserver(INeuralNetworkTrainerObserver observer)
    {
        _observers.Add(observer);
    }

    public void DetachObserver(INeuralNetworkTrainerObserver observer)
    {
        _observers.Add(observer);
    }

    private void NotifyObserver(INeuralNetworkObservableData data)
    {
        if (DateTime.Now.Ticks % 5000 != 0) return;
        foreach (INeuralNetworkTrainerObserver observer in _observers)
            observer.Update(data);
    }
}