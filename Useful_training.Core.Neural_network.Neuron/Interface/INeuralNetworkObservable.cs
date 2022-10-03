using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.ValueObject;

namespace Useful_training.Core.Neural_network.Interface
{
	public interface INeuralNetworkObservable
	{
        void Attach(INeuralNetworkTrainerObserver observer);
        void Detach(INeuralNetworkTrainerObserver observer);
        void Notify(INeuralNetworkObservableData datas);
    }
}
