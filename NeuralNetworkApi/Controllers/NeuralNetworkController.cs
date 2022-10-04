using Microsoft.AspNetCore.Mvc;
using Useful_training.Core.NeuralNetwork;
using Useful_training.Core.NeuralNetwork.Interfaces;
using Useful_training.Applicative.NeuralNetworkApi.ViewModel;

namespace Useful_training.Applicative.NeuralNetworkApi.Controllers
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
            await NeuralNetworkWarehouse.Save(BuilderOfNeuralNetwork.GetNeuralNetwork(), $"{Name}_Input-{numberOfInput}_Output-{numberOfOutputs}");
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