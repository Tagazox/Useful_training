using Microsoft.AspNetCore.Mvc;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.ValueObject;

namespace NeuralNetworkApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]/")]
    public class CalculateController : ControllerBase
    {
        private readonly INeuralNetworkWarehouse NeuralNetworkWarehouse;
        public CalculateController(INeuralNetworkWarehouse neuralNetworkWarehouse)
        {
            NeuralNetworkWarehouse = neuralNetworkWarehouse;
        }

        [HttpGet("{Name}/{Input}", Name = "GetCalculationResult")]
        public IEnumerable<double> Get(string Name, [FromQuery] List<double> Inputs)
        {
            return NeuralNetworkWarehouse.Retreive<NeuralNetwork>(Name).Calculate(Inputs);
        }

    }
}