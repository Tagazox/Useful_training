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
    private bool _trainAbortIsRequested;

    public NeuralNetworkTrainer(INeuralNetworkTrainerContainer neuralNetworkContainer, int epochRoundedTo = 3)
    {
        _trainAbortIsRequested = false;
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
        var random = new Random();
        while (TrainingIsNotFinished && TrainAbortIsNotRequested)
        {
            var dataSetForThisIteration = _dataSetsList[random.Next(_dataSetsList.Count)];
            var resultsOfTheNeuralNetworkCalculation = _neuralNetwork.Calculate(dataSetForThisIteration.Inputs);
            
            // ici tu utilises à plusieurs reprise result.Any(double.IsNaN)). Je pense que tu gagnerais à wrapper ton output dans une classe et mettre ca dans une methode. 
            // ca permetterait en plus d'expliciter ce que tu cherches à verifier à travers le nom de la methode, car typiquement je ne sais pas ce que ca signifie si un output a une valeur Nan.
            if (resultsOfTheNeuralNetworkCalculation.Any(double.IsNaN))
            {
                _neuralNetwork.Reset();
                continue;
            }

            NotifyObserver(new NeuralNetworkObservableData(dataSetForThisIteration, resultsOfTheNeuralNetworkCalculation));
            
            if (ErrorIsTooHigh(dataSetForThisIteration.TargetOutput, resultsOfTheNeuralNetworkCalculation))
                _neuralNetwork.BackPropagate(dataSetForThisIteration.TargetOutput);
        }
    }

    public void Destroy()
    {
        _trainAbortIsRequested = true;
    }

    public bool ErrorIsAcceptable(IReadOnlyList<double> targets, IEnumerable<double> results)
        => CalculateError(targets, results) < ErrorThreshold;

    public bool ErrorIsTooHigh(IReadOnlyList<double> targets, IEnumerable<double> results) => !ErrorIsAcceptable(targets, results);

    private double CalculateError(IReadOnlyList<double> targets, IEnumerable<double> results)
    {
        var deltaError = results.Select((r, i) => Math.Abs(r - targets[i])).Sum();
        return Math.Round(deltaError, _epochRoundedTo);
    }

    // dans ce genre de méthode, on évite d'avoir des effets de bord, il faudrait donc déplacer l'appel à _neuralNetwork.Reset() qui était fait ici.
    private bool TrainingIsFinished => 
        _dataSetsList
            .Take(20)
            .All(set => 
            {
                var results = _neuralNetwork.Calculate(set.Inputs);
                return !results.Any(double.IsNaN) && ErrorIsAcceptable(set.TargetOutput, results);
            });

    private bool TrainingIsNotFinished => !TrainingIsFinished;

    private bool TrainAbortIsNotRequested => !_trainAbortIsRequested;

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
        foreach (var observer in _observers)
            observer.Update(data);
    }
}