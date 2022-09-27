
namespace Useful_training.Core.Neural_network
{
    public interface INeural_Network
    {
        void BackPropagate(List<double> targets);
        IList<double> Calculate(IList<double> inputs);
    }
}