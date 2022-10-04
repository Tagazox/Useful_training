using System.Runtime.Serialization;
namespace Useful_training.Core.NeuralNetwork.Interfaces
{
    public interface INeuralNetwork : ISerializable
    {
        public void BackPropagate(List<double> targets);
        public IList<double> Calculate(IList<double> inputs);
        public void Reset();
    }
}