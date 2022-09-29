using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network.Interface
{
	public interface INeuralNetworkTrainerObserver
	{
		void Update(INeuralNetworkObservableData subject);
	}
}
