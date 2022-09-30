
using System.Runtime.Serialization;

namespace Useful_training.Core.Neural_network.Interface
{
    public interface INeural_Network : ISerializable
    {
        void BackPropagate(List<double> targets);
        IList<double> Calculate(IList<double> inputs);
    }
}