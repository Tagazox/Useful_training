using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.ValueObject;

namespace Useful_training.Core.Neural_network.Interface
{
    public interface INeuralNetworkTrainerContainer
    {
        List<DataSet> DataSets { get; set; }
        INeuralNetwork Neural_Network{ get; set; }
        public void CreateNeuralNetwork();
        public void CreateDataSets();
    }
}
