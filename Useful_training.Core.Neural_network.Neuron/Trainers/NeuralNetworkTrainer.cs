using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Observer.Interfaces;
using Useful_training.Core.NeuralNetwork.Trainers.Adapter;
using Useful_training.Core.NeuralNetwork.Trainers.Interfaces;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Core.NeuralNetwork.Trainers;

public class NeuralNetworkTrainer : INeuralNetworkTrainer
{
    private const double ErrorThreshold = 0.001;
    private readonly INeuralNetwork _neuralNetwork;
    private readonly List<DataSet> _dataSetsList;
    private readonly int _epochRoundedTo;
    private readonly IList<INeuralNetworkTrainerObserver> _observers;
    private bool _trainingAbortIsRequested;
    private bool _trainingIsFinished;
    private bool TrainingAbortIsNotRequested => !_trainingAbortIsRequested;
    private bool TrainingIsNotFinished => !_trainingIsFinished;

    private bool ErrorIsAcceptable(IReadOnlyList<double> targets) => CalculateNeuralNetworkError(targets) < ErrorThreshold;

    private bool ErrorIsTooHigh(IReadOnlyList<double> targets) => !ErrorIsAcceptable(targets);

    public NeuralNetworkTrainer(INeuralNetworkTrainerContainer neuralNetworkContainer, int epochRoundedTo = 3)
    {
        _trainingAbortIsRequested = false;
        _trainingIsFinished = false;
        _epochRoundedTo = epochRoundedTo;
        _neuralNetwork = neuralNetworkContainer.NeuralNetwork;
        _dataSetsList = neuralNetworkContainer.DataSets;

        if (_neuralNetwork == null)
            throw new NullReferenceException("Any Neural network has been found in the container");
        if (_dataSetsList == null || !_dataSetsList.Any())
            throw new NullReferenceException("Any data set has been found in the container");

        _observers = new List<INeuralNetworkTrainerObserver>();
    }

    public void TrainNeuralNetwork()
    {
        Random random = new Random();

        while (TrainingIsNotFinished && TrainingAbortIsNotRequested)
        {
            DataSet dataSetForThisIteration = _dataSetsList[random.Next(_dataSetsList.Count)];

            IList<double> resultsOfTheNeuralNetworkCalculation =
                _neuralNetwork.Calculate(dataSetForThisIteration.Inputs);

            if (_neuralNetwork.IsUnstable)
                _neuralNetwork.Reset();
            else
            {
                NotifyObserver(new NeuralNetworkObservableData(dataSetForThisIteration,
                    resultsOfTheNeuralNetworkCalculation));

                if (ErrorIsAcceptable(dataSetForThisIteration.TargetOutputs))
                    _trainingIsFinished = CheckIfTheNeuralNetworkHasBeenEnoughTrained();
                else
                    _neuralNetwork.BackPropagate(dataSetForThisIteration.TargetOutputs);
            }
        }
    }

    public void Destroy()
    {
        _trainingAbortIsRequested = true;
    }

    private double CalculateNeuralNetworkError(IReadOnlyList<double> targets)
    {
        double deltaError = _neuralNetwork.LastCalculationResults.Select((r, i) => Math.Abs(r - targets[i])).Sum();
        return Math.Round(deltaError, _epochRoundedTo);
    }

    private bool CheckIfTheNeuralNetworkHasBeenEnoughTrained(int numberOfSamplesToTest = 20)
    {
        foreach (DataSet dataSet in _dataSetsList.Take(numberOfSamplesToTest))
        {
            _neuralNetwork.Calculate(dataSet.Inputs);

            if (ErrorIsTooHigh(dataSet.TargetOutputs))
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
        foreach (INeuralNetworkTrainerObserver observer in _observers)
            observer.Update(data);
    }

    private int _counter = 0;
    private const int CounterIterationReset  = 5000;
    private bool IsItTimeToUpdateObserver()
    {
        return _counter++ % CounterIterationReset == 0;
    }
}