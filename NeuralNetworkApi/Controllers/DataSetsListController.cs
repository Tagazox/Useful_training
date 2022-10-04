using Microsoft.AspNetCore.Mvc;
using NeuralNetworkApi.ViewModel;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.ValueObject;

namespace NeuralNetworkApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]/")]
    public class DataSetsListController : ControllerBase
    {
        private readonly IDataSetsListWarehouse DataSetsListWarehouse;
        public DataSetsListController(IDataSetsListWarehouse dataSetsListWarehouse)
        {
            DataSetsListWarehouse = dataSetsListWarehouse;
        }

        [HttpPost("{Name}", Name = "PostDataSetsList")]
        public async Task<ResponseOk> Post(string Name, [FromBody] List<DataSet> dataSets)
        {
            if (!dataSets.TrueForAll(x => x.Inputs.Count == dataSets.First().Inputs.Count))
                throw new ArgumentException("Input need to always have the same count of data");
            if (!dataSets.TrueForAll(x => x.TargetOutput.Count == dataSets.First().TargetOutput.Count))
                throw new ArgumentException("Outputs need to always have the same count of data");
            if(dataSets.Select(d => d.Inputs).Distinct().Count() != dataSets.Count)
                throw new ArgumentException("You cannot have duplicate input");

            await DataSetsListWarehouse.Save(dataSets, Name);
            return new ResponseOk("Dataset list created");
        }

        [HttpGet("{Name}",Name = "GetDataSetListByName")]
        public IEnumerable<DataSet> Get(string Name)
        {
            return DataSetsListWarehouse.Retreive<List<DataSet>>(Name);
        }

        [HttpGet("{seamslike}/{start}/{count}", Name = "SearchDataSetListByName")]
        public IEnumerable<string> Get(string? seamslike,int start=0,int count=10)
        {
            if (seamslike == null)
                seamslike = "";
            return DataSetsListWarehouse.SearchAvailable(seamslike,start,count).Select(p => Path.GetFileNameWithoutExtension(p));
        }
    }
}