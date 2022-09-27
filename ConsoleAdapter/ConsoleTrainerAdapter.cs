using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network;

namespace ConsoleAdapter
{
    internal class ConsoleTrainerAdapter
    {
        INeuralNetworkTrainer trainer;

        public ConsoleTrainerAdapter(INeuralNetworkTrainer InjectedTrainer)
        {
            trainer = InjectedTrainer;
        }
        public void FollowNetworkTraining()
        {
            trainer.TrainNeuralNetwork();
        }

    }
}
