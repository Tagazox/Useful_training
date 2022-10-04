namespace Useful_training.Infrastructure.FileManagerTests
{
	public class NeuralNetworkFileWarehouseTests
	{
		private NeuralNetworkFileWarehouse TestSubject;
		private Mock<INeuralNetworkTrainerContainer> NeuralNetworkTrainerContainerMocked;
		private List<double> Inputs;
		private string NameOfTheNeuralNetwork;
		public NeuralNetworkFileWarehouseTests()
		{
			NeuralNetworkTrainerContainerMocked = new Mock<INeuralNetworkTrainerContainer>();
			TestSubject = new NeuralNetworkFileWarehouse("testPath\\");
			NameOfTheNeuralNetwork = Guid.NewGuid().ToString();
			CreateNewNeuralNetwork();
		}
		private void CreateNewNeuralNetwork()
		{
			Inputs = new List<double> { 1, 0 };
			uint numberOfInput = 2;
			uint numberOfOutput = 2;
			uint numberOfInputNeurons = numberOfInput;
			uint numberOfNeuronesByHiddenLayerOutput = 5;
			uint numberOfHiddenLayers = 3;
			NeuralNetworkBuilder MockedNeuralNetworkBuilder = new NeuralNetworkBuilder();
			MockedNeuralNetworkBuilder.Initialize(numberOfInputNeurons, .005, 0.025);
			MockedNeuralNetworkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayerOutput, numberOfHiddenLayers, NeuronType.Tanh);
			MockedNeuralNetworkBuilder.AddOutputLayers(numberOfOutput, NeuronType.Tanh);
			NeuralNetworkTrainerContainerMocked.Setup(c => c.NeuralNetwork).Returns(MockedNeuralNetworkBuilder.GetNeuralNetwork());
		}

		[Fact]
		public void SaveShouldBeGood()
		{
			Action Save = () =>
			{
				TestSubject.Save(NeuralNetworkTrainerContainerMocked.Object.NeuralNetwork, NameOfTheNeuralNetwork).Wait();
			};
			Save.Should().NotThrow();
		}

		[Fact]
		public void RetreiveShouldBeGood()
		{
			SaveShouldBeGood();
			INeuralNetwork neuralNetRecovred = TestSubject.Retreive<NeuralNetwork>(NameOfTheNeuralNetwork);
			neuralNetRecovred.Calculate(Inputs).Should().BeEquivalentTo(NeuralNetworkTrainerContainerMocked.Object.NeuralNetwork.Calculate(Inputs));
		}
		[Fact]
		public void OverrideShouldBeGood()
		{
			SaveShouldBeGood();

			INeuralNetwork oldSavedNeuralNetwork = NeuralNetworkTrainerContainerMocked.Object.NeuralNetwork;

			CreateNewNeuralNetwork();
			TestSubject.Override(NeuralNetworkTrainerContainerMocked.Object.NeuralNetwork, NameOfTheNeuralNetwork).Wait();

			INeuralNetwork neuralNetRecovred = TestSubject.Retreive<NeuralNetwork>(NameOfTheNeuralNetwork);

			oldSavedNeuralNetwork.Should().NotBeSameAs(neuralNetRecovred);
			neuralNetRecovred.Calculate(Inputs).Should().BeEquivalentTo(NeuralNetworkTrainerContainerMocked.Object.NeuralNetwork.Calculate(Inputs));
		}
		[Fact]
		public void SearchNeuralNetworkAvailableShouldBeGood()
		{
			string modifiedDataSetName = NameOfTheNeuralNetwork;
			string BaseName = modifiedDataSetName;
			string searchTerm = "abc";

			for (int i = 0; i < 10; i++)
			{
				modifiedDataSetName += searchTerm;
				TestSubject.Save(NeuralNetworkTrainerContainerMocked.Object.NeuralNetwork, modifiedDataSetName).Wait();
			}
			for (int i = 0; i < 10; i++)
				TestSubject.SearchAvailable(BaseName, 0, i).Count().Should().Be(i);
		}

		[Fact]
		public void SaveShouldThrowAlreadyExistException()
		{
			TestSubject.Save(NeuralNetworkTrainerContainerMocked.Object.NeuralNetwork, NameOfTheNeuralNetwork).Wait();

			Action Save = () =>
			{
				TestSubject.Save(NeuralNetworkTrainerContainerMocked.Object.NeuralNetwork, NameOfTheNeuralNetwork).Wait();
			};
			Save.Should().Throw<AlreadyExistException>();
		}
		[Fact]
		public void RetreiveShouldThrowCantFindNeuralNetworkException()
		{
			string nameOfTheNonExistantNeuralNetwork = Guid.NewGuid().ToString();
			Action Save = () =>
			{
				INeuralNetwork neuralNetRecovred = TestSubject.Retreive<NeuralNetwork>(nameOfTheNonExistantNeuralNetwork);
			};
			Save.Should().Throw<CantFindException>();
		}
		[Fact]
		public void OverrideShouldThrowCantFindException()
		{
			string nameOfTheNonExistantNeuralNetwork = Guid.NewGuid().ToString();
			Action Override = () =>
			{
				TestSubject.Override(NeuralNetworkTrainerContainerMocked.Object.NeuralNetwork, nameOfTheNonExistantNeuralNetwork).Wait();
			};
			Override.Should().Throw<CantFindException>();
		}
		[Fact]
		public void RetreiveShouldThrowInvalidCastException()
		{
			Action Save = () =>
			{
				INeuralNetwork neuralNetRecovred = TestSubject.Retreive<INeuralNetwork>(NameOfTheNeuralNetwork);
			};
			Save.Should().Throw<InvalidCastException>();
		}
	}
}