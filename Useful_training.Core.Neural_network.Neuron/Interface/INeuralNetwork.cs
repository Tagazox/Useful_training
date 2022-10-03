
using System.Runtime.Serialization;

namespace Useful_training.Core.Neural_network.Interface
{
    public interface INeuralNetwork : ISerializable
    {
        void BackPropagate(List<double> targets);
        IList<double> Calculate(IList<double> inputs);
        void Reset();
    }
}