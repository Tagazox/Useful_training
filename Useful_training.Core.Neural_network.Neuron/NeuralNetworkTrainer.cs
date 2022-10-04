using Useful_training.Core.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Core.NeuralNetwork
{
	public class NeuralNetworkTrainer : INeuralNetworkTrainer
    {
        private readonly INeuralNetwork NeuralNetwork;
        private readonly List<DataSet> DataSetsList;
        private readonly int EpochRoundedTo;
        private readonly IList<INeuralNetworkTrainerObserver> Observers;
        public NeuralNetworkTrainer(INeuralNetworkTrainerContainer neuralNetworkContainer, int epochRoundedTo = 3)
        {
            EpochRoundedTo = epochRoundedTo;
            neuralNetworkContainer.CreateNeuralNetwork();
            NeuralNetwork = neuralNetworkContainer.NeuralNetwork;
            neuralNetworkContainer.CreateDataSets();
            DataSetsList = neuralNetworkContainer.DataSets;

            if (NeuralNetwork == null)
                throw new NullReferenceException("Any Neural network hasn't be found in the container");
            if (DataSetsList == null || !DataSetsList.Any())
                throw new NullReferenceException("Any data set hasn't be found in the container");

            Observers = new List<INeuralNetworkTrainerObserver>();


        }

        public void TrainNeuralNetwork()
        {
            bool trainFinish = false;
            Random random = new Random();

            while (!trainFinish)
            {
                DataSet dataSetForThisIteration = DataSetsList[random.Next(DataSetsList.Count)];

                IList<double> resultsOfTheNeuralNetworkCalculation = NeuralNetwork.Calculate(dataSetForThisIteration.Inputs);
                if (resultsOfTheNeuralNetworkCalculation.Any(d => double.IsNaN(d)))
                    NeuralNetwork.Reset();
                else
                {
                    NotifyObserver(new NeuralNetworkObservableData(dataSetForThisIteration, resultsOfTheNeuralNetworkCalculation));
                    if (CalculateError(dataSetForThisIteration.TargetOutput, resultsOfTheNeuralNetworkCalculation) < 0.001)
                        trainFinish = VerifyIfTrainingIsFinish();
                    if (!trainFinish)
                        NeuralNetwork.BackPropagate(dataSetForThisIteration.TargetOutput);
                }
            }
        }
        private double CalculateError(List<double> targets, IList<double> results)
        {
            double deltaError = 0;
            for (int i = 0; i < results.Count; i++)
                deltaError += Math.Abs(results[i] - targets[i]);
            return Math.Round(deltaError, EpochRoundedTo);
        }
        private bool VerifyIfTrainingIsFinish()
        {
            foreach (DataSet set in DataSetsList.Take(20))
            {
                IList<double> results = NeuralNetwork.Calculate(set.Inputs);
                if (results.Any(d => double.IsNaN(d)))
                {
                    NeuralNetwork.Reset();
                    return false;
                }
                for (int i = 0; i < results.Count; i++)
                    if (Math.Round(Math.Abs(results[i] - set.TargetOutput[i]), EpochRoundedTo) > 0.001)
                    {
                        return false;
                    }
            }
            return true;
        }
        public void AttachObserver(INeuralNetworkTrainerObserver observer)
        {
            Observers.Add(observer);
        }
        public void DetachObserver(INeuralNetworkTrainerObserver observer)
        {
            Observers.Add(observer);
        }
        public void NotifyObserver(INeuralNetworkObservableData datas)
        {
            foreach (INeuralNetworkTrainerObserver observer in Observers)
                observer.Update(datas);
        }
    }
}
