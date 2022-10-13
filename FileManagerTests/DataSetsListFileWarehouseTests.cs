using System.Globalization;
using Newtonsoft.Json;
using Useful_training.Applicative.Application.Ports;
using Useful_training.Core.NeuralNetwork.Trainers.Adapter;
using Useful_training.Infrastructure.FileManager.Exception;
using Useful_training.Infrastructure.FileManager.Warehouse;

namespace Useful_training.Infrastructure.FileManagerTests;

public class DataSetsListFileWarehouseTests
{
    private readonly IDataSetsListWarehouse _testSubject;
    private readonly Mock<INeuralNetworkTrainerContainer> _neuralNetworkTrainerContainerMocked;
    private readonly string _nameOfTheDataSet;

    public DataSetsListFileWarehouseTests()
    {
        _neuralNetworkTrainerContainerMocked = new Mock<INeuralNetworkTrainerContainer>();
        _testSubject = new DataSetsListFileWarehouse("testPath\\");
        CreateDataSet();
        _nameOfTheDataSet = Guid.NewGuid().ToString();
    }

    private void CreateDataSet()
    {
        Random rand = new Random();
        List<DataSet> dataSets = new List<DataSet>();
        for (int i = 0; i < 250; i++)
        {
            double input1 = rand.NextDouble() * 2 - 1;
            double input2 = rand.NextDouble() * 2 - 1;
            double input3 = rand.NextDouble() * 2 - 1;
            double input4 = rand.NextDouble() * 2 - 1;
            double input5 = rand.NextDouble() * 2 - 1;
            double input6 = rand.NextDouble() * 2 - 1;
            double input7 = rand.NextDouble() * 2 - 1;

            double output = (input1 + input2 + input3 + input4 + input5 + input6 + input7);

            List<double> inputs = new List<double>
            {
                input1,
                input2,
                input3,
                input4,
                input5,
                input6,
                input7
            };
            List<double> outputs = new List<double> { output > 0 ? 1 : 0 };
            dataSets.Add(new DataSet(inputs, outputs));
        }

        _neuralNetworkTrainerContainerMocked.Setup(c => c.DataSets).Returns(dataSets);
        List<DataSet> dataSetsToSave =
            dataSets.DistinctBy(d => string.Concat(d.Inputs.Select(i => i.ToString(CultureInfo.InvariantCulture)))).ToList();
        foreach (DataSet dataSet in dataSets)
        {
            dataSet.TargetOutputs[0] /= dataSets.Max(d => d.TargetOutputs[0]);
        }

        string toCopy = JsonConvert.SerializeObject(dataSetsToSave);
        Console.WriteLine(toCopy);
    }

    [Fact]
    public void SaveShouldBeGood()
    {
        Action save = () => { _testSubject.Save(_neuralNetworkTrainerContainerMocked.Object.DataSets, _nameOfTheDataSet).Wait(); };
        save.Should().NotThrow();
    }

    [Fact]
    public void RetrieveShouldBeGood()
    {
        SaveShouldBeGood();
        List<DataSet> dataSetsRecovered = _testSubject.Retrieve<List<DataSet>>(_nameOfTheDataSet);
        dataSetsRecovered.Should().BeEquivalentTo(_neuralNetworkTrainerContainerMocked.Object.DataSets);
    }

    [Fact]
    public void OverrideShouldBeGood()
    {
        SaveShouldBeGood();

        List<DataSet> oldDataSet = _neuralNetworkTrainerContainerMocked.Object.DataSets;

        CreateDataSet();
        _testSubject.Override(_neuralNetworkTrainerContainerMocked.Object.DataSets, _nameOfTheDataSet).Wait();

        List<DataSet> dataSetRecovered = _testSubject.Retrieve<List<DataSet>>(_nameOfTheDataSet);

        oldDataSet.Should().NotBeSameAs(dataSetRecovered);
    }

    [Fact]
    public void SearchAvailableShouldBeGood()
    {
        const string searchTerm = "abc";
        string modifiedDataSetName = _nameOfTheDataSet;

        for (int i = 0; i < 10; i++)
        {
            modifiedDataSetName += searchTerm;
            _testSubject.Save(_neuralNetworkTrainerContainerMocked.Object.DataSets, modifiedDataSetName);
        }

        for (int i = 0; i < 10; i++)
            _testSubject.SearchAvailable(_nameOfTheDataSet, 0, i).Count().Should().Be(i);
    }

    [Fact]
    public void SaveShouldThrowAlreadyExistException()
    {
        SaveShouldBeGood();
        Action save = () => { _testSubject.Save(_neuralNetworkTrainerContainerMocked.Object.DataSets, _nameOfTheDataSet).Wait(); };
        save.Should().Throw<AlreadyExistException>();
    }

    [Fact]
    public void RetrieveShouldThrowCantFindException()
    {
        string nameOfTheNonExistentDataSet = Guid.NewGuid().ToString();
        Action save = () => { _testSubject.Retrieve<List<DataSet>>(nameOfTheNonExistentDataSet); };
        save.Should().Throw<CantFindException>();
    }

    [Fact]
    public void OverrideShouldThrowCantFindException()
    {
        string nameOfTheNonExistentDataSet = Guid.NewGuid().ToString();
        Action @override = () =>
        {
            _testSubject.Override(_neuralNetworkTrainerContainerMocked.Object.DataSets, nameOfTheNonExistentDataSet)
                .Wait();
        };
        @override.Should().Throw<CantFindException>();
    }

    [Fact]
    public void RetrieveShouldThrowInvalidCastException()
    {
        string nameOfTheDataSet = Guid.NewGuid().ToString();
        Action save = () => { _testSubject.Retrieve<IList<DataSet>>(nameOfTheDataSet); };
        save.Should().Throw<InvalidCastException>();
    }
}