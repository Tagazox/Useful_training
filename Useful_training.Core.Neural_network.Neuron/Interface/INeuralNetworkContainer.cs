using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.ValueObject;

namespace Useful_training.Core.Neural_network
{
    public interface INeuralNetworkTrainerContainer
    {
        List<DataSet> DataSets { get;  }
        INeural_Network Neural_Network{ get;  }
        public void CreateNeuralNetwork();
        public void CreateDataSets();
    }
}
