using Useful_training.Core.NeuralNetwork.Factory;
using Useful_training.Core.NeuralNetwork.NeuralNetwork;
using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;
using Useful_training.Core.NeuralNetwork.Trainers.Adapter;
using Useful_training.Infrastructure.FileManager.Exception;
using Useful_training.Infrastructure.FileManager.Warehouse;

namespace Useful_training.Infrastructure.FileManagerTests;

public class NeuralNetworkFileWarehouseTests
{
    private readonly NeuralNetworkFileWarehouse _testSubject;
    private readonly Mock<INeuralNetworkTrainerContainer> _neuralNetworkTrainerContainerMocked;
    private readonly List<double> _inputs;
    private readonly string _nameOfTheNeuralNetwork;

    public NeuralNetworkFileWarehouseTests()
    {
        _neuralNetworkTrainerContainerMocked = new Mock<INeuralNetworkTrainerContainer>();
        _testSubject = new NeuralNetworkFileWarehouse("testPath\\");
        _nameOfTheNeuralNetwork = Guid.NewGuid().ToString();
        _inputs = new List<double> { 1, 0 };

        CreateNewNeuralNetwork();
    }

    private void CreateNewNeuralNetwork()
    {
        const uint numberOfInput = 2;
        const uint numberOfOutput = 2;
        const uint numberOfNeuronesByHiddenLayerOutput = 5;
        const uint numberOfHiddenLayers = 3;
        NeuralNetworkBuilder mockedNeuralNetworkBuilder = new NeuralNetworkBuilder();
        mockedNeuralNetworkBuilder.Initialize(numberOfInput, .005, 0.025);
        mockedNeuralNetworkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayerOutput, numberOfHiddenLayers,
            NeuronType.Tanh);
        mockedNeuralNetworkBuilder.AddOutputLayers(numberOfOutput, NeuronType.Tanh);
        _neuralNetworkTrainerContainerMocked.Setup(c => c.NeuralNetwork)
            .Returns(mockedNeuralNetworkBuilder.GetNeuralNetwork());
    }

    [Fact]
    public void SaveShouldBeGood()
    {
        Action save = () =>
        {
            _testSubject.Save(_neuralNetworkTrainerContainerMocked.Object.NeuralNetwork, _nameOfTheNeuralNetwork)
                .Wait();
        };
        save.Should().NotThrow();
    }

    [Fact]
    public void RetrieveShouldBeGood()
    {
        SaveShouldBeGood();
        INeuralNetwork neuralNetRecovered = _testSubject.Retrieve<NeuralNetwork>(_nameOfTheNeuralNetwork);
        neuralNetRecovered.Calculate(_inputs).Should()
            .BeEquivalentTo(_neuralNetworkTrainerContainerMocked.Object.NeuralNetwork.Calculate(_inputs));
    }

    [Fact]
    public void OverrideShouldBeGood()
    {
        SaveShouldBeGood();

        INeuralNetwork oldSavedNeuralNetwork = _neuralNetworkTrainerContainerMocked.Object.NeuralNetwork;

        CreateNewNeuralNetwork();
        _testSubject.Override(_neuralNetworkTrainerContainerMocked.Object.NeuralNetwork, _nameOfTheNeuralNetwork)
            .Wait();

        INeuralNetwork neuralNetRecovered = _testSubject.Retrieve<NeuralNetwork>(_nameOfTheNeuralNetwork);

        oldSavedNeuralNetwork.Should().NotBeSameAs(neuralNetRecovered);
        neuralNetRecovered.Calculate(_inputs).Should()
            .BeEquivalentTo(_neuralNetworkTrainerContainerMocked.Object.NeuralNetwork.Calculate(_inputs));
    }

    [Fact]
    public void SearchNeuralNetworkAvailableShouldBeGood()
    {
        string modifiedDataSetName = _nameOfTheNeuralNetwork;
        string baseName = modifiedDataSetName;
        const string searchTerm = "abc";

        for (int i = 0; i < 10; i++)
        {
            modifiedDataSetName += searchTerm;
            _testSubject.Save(_neuralNetworkTrainerContainerMocked.Object.NeuralNetwork, modifiedDataSetName).Wait();
        }

        for (int i = 0; i < 10; i++)
            _testSubject.SearchAvailable(baseName, 0, i).Count().Should().Be(i);
    }

    [Fact]
    public void SaveShouldThrowAlreadyExistException()
    {
        _testSubject.Save(_neuralNetworkTrainerContainerMocked.Object.NeuralNetwork, _nameOfTheNeuralNetwork).Wait();

        Action save = () =>
        {
            _testSubject.Save(_neuralNetworkTrainerContainerMocked.Object.NeuralNetwork, _nameOfTheNeuralNetwork)
                .Wait();
        };
        save.Should().Throw<AlreadyExistException>();
    }

    [Fact]
    public void RetrieveShouldThrowCantFindNeuralNetworkException()
    {
        string nameOfTheNonExistentNeuralNetwork = Guid.NewGuid().ToString();
        Action save = () => { _testSubject.Retrieve<NeuralNetwork>(nameOfTheNonExistentNeuralNetwork); };
        save.Should().Throw<CantFindException>();
    }

    [Fact]
    public void OverrideShouldThrowCantFindException()
    {
        string nameOfTheNonExtantNeuralNetwork = Guid.NewGuid().ToString();
        Action @override = () =>
        {
            _testSubject.Override(_neuralNetworkTrainerContainerMocked.Object.NeuralNetwork,
                nameOfTheNonExtantNeuralNetwork).Wait();
        };
        @override.Should().Throw<CantFindException>();
    }

    [Fact]
    public void RetrieveShouldThrowInvalidCastException()
    {
        Action save = () => { _testSubject.Retrieve<INeuralNetwork>(_nameOfTheNeuralNetwork); };
        save.Should().Throw<InvalidCastException>();
    }
}