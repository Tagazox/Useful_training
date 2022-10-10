using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Observer.Interfaces;
using Useful_training.Core.NeuralNetwork.Trainers.Adapter;
using Useful_training.Core.NeuralNetwork.Trainers.Interfaces;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Core.NeuralNetwork.Trainers;

public class NeuralNetworkTrainer : INeuralNetworkTrainer
{
    private readonly INeuralNetwork _neuralNetwork;
    private readonly List<DataSet> _dataSetsList;
    private readonly int _epochRoundedTo;
    private readonly IList<INeuralNetworkTrainerObserver> _observers;
    private bool trainAbort;

    public NeuralNetworkTrainer(INeuralNetworkTrainerContainer neuralNetworkContainer, int epochRoundedTo = 3)
    {
        trainAbort = false;
        _epochRoundedTo = epochRoundedTo;
        _neuralNetwork = neuralNetworkContainer.NeuralNetwork;
        _dataSetsList = neuralNetworkContainer.DataSets;

        if (_neuralNetwork == null)
            throw new NullReferenceException("Any Neural network hasn't be found in the container");
        if (_dataSetsList == null || !_dataSetsList.Any())
            throw new NullReferenceException("Any data set hasn't be found in the container");

        _observers = new List<INeuralNetworkTrainerObserver>();
    }

    public void TrainNeuralNetwork()
    {
        bool trainFinish = false;
        Random random = new Random();
        IList<double> resultsOfTheNeuralNetworkCalculation;
        while (!trainFinish && !trainAbort)
        {
            DataSet dataSetForThisIteration = _dataSetsList[random.Next(_dataSetsList.Count)];
            {
                {
                    resultsOfTheNeuralNetworkCalculation =
                        _neuralNetwork.Calculate(dataSetForThisIteration.Inputs);
                    if (resultsOfTheNeuralNetworkCalculation.Any(double.IsNaN))
                        _neuralNetwork.Reset();
                    else
                    {
                        NotifyObserver(new NeuralNetworkObservableData(dataSetForThisIteration,
                            resultsOfTheNeuralNetworkCalculation));
                        if (CalculateError(dataSetForThisIteration.TargetOutput, resultsOfTheNeuralNetworkCalculation) <
                            0.001)
                            trainFinish = VerifyIfTrainingIsFinish();
                        else
                            _neuralNetwork.BackPropagate(dataSetForThisIteration.TargetOutput);

                        if (trainFinish)
                            break;
                    }
                }
            }
        }
    }

    public void Destroy()
    {
        trainAbort = true;
    }

    private double CalculateError(IReadOnlyList<double> targets, IEnumerable<double> results)
    {
        double deltaError = results.Select((r, i) => Math.Abs(r - targets[i])).Sum();
        return Math.Round(deltaError, _epochRoundedTo);
    }

    private bool VerifyIfTrainingIsFinish()
    {
        foreach (DataSet set in _dataSetsList.Take(20))
        {
            IList<double> results = _neuralNetwork.Calculate(set.Inputs);
            if (results.Any(double.IsNaN))
            {
                _neuralNetwork.Reset();
                return false;
            }

            if (Math.Round(CalculateError(set.TargetOutput, results), _epochRoundedTo) > 0.001)
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