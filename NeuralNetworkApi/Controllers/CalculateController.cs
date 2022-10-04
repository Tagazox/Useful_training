using Microsoft.AspNetCore.Mvc;
using Useful_training.Core.NeuralNetwork;
using Useful_training.Core.NeuralNetwork.Interfaces;

namespace Useful_training.Applicative.NeuralNetworkApi.Controllers
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