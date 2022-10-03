using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Interface;

namespace Useful_training.Core.Neural_network
{
    internal class Synapse
    {
        public IInputNeurons InputNeuron { get; set; }
        public INeuron OutputNeuron { get; set; }
        public double Weight { get; set; }
        public double WeightDelta { get; set; }
        public Synapse(IInputNeurons inputNeuron, INeuron outputNeuron)
        {
            Random _rand = new Random();
            InputNeuron = inputNeuron;
            OutputNeuron = outputNeuron;
            Weight = _rand.NextDouble() * 2 - 1;

        }

        internal void Reset()
        {
            Random _rand = new Random();
            Weight = _rand.NextDouble() * 2 - 1;
            WeightDelta = 0;
        }
    }
}
