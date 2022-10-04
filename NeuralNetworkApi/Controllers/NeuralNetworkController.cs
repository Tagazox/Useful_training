using Microsoft.AspNetCore.Mvc;
using NeuralNetworkApi.ViewModel;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;

namespace NeuralNetworkApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]/")]
    public class NeuralNetworkController : ControllerBase
    {
        private readonly INeuralNetworkWarehouse NeuralNetworkWarehouse;
        private readonly INeuralNetworkBuilder BuilderOfNeuralNetwork;
        public NeuralNetworkController(INeuralNetworkWarehouse neuralNetworkWarehouse,INeuralNetworkBuilder builderOfNeuralNetwork)
        {
            NeuralNetworkWarehouse = neuralNetworkWarehouse;
            BuilderOfNeuralNetwork = builderOfNeuralNetwork;
        }

        [HttpPost(Name = "PostNeuralNetwork")]
        public async Task<ResponseOk> Post(string Name,uint numberOfInput,uint numberOfOutputs,uint numberOfHiddenLayer, uint numberOfNeuronByHiddenLayer, double learnRate,double momentum,NeuronType typeOfNeuron)
        {
            NeuralNetworkDirector neuralNetworkDirector = new NeuralNetworkDirector();
            neuralNetworkDirector.networkBuilder = BuilderOfNeuralNetwork;
            neuralNetworkDirector.BuildComplexeNeuralNetwork(numberOfInput,learnRate,momentum,numberOfOutputs, numberOfHiddenLayer, numberOfNeuronByHiddenLayer, typeOfNeuron);
            await NeuralNetworkWarehouse.Save(BuilderOfNeuralNetwork.GetNeural_Network(), $"{Name}_Input-{numberOfInput}_Output-{numberOfOutputs}");
            return new ResponseOk("Neural network created");
        }

        [HttpGet("{seamslike}/{start}/{count}", Name = "SearchNeuralNetworkByName")]
        public IEnumerable<string> Get(string? seamslike, int start = 0, int count = 10)
        {
            if (seamslike == null)
                seamslike = "";
            return NeuralNetworkWarehouse.SearchAvailable(seamslike, start, count).Select(p => Path.GetFileNameWithoutExtension(p));
        }


    }
}