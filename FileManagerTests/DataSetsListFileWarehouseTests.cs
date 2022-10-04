namespace Useful_training.Infrastructure.FileManagerTests
{
	public class DataSetsListFileWarehouseTests
    {
		private IDataSetsListWarehouse TestSubject;
        private Mock<INeuralNetworkTrainerContainer> NeuralNetworkTrainerContainerMocked;
        private string NameOfTheDataSet;

        public DataSetsListFileWarehouseTests()
        {
            NeuralNetworkTrainerContainerMocked = new Mock<INeuralNetworkTrainerContainer>();
			TestSubject = new DataSetsListFileWarehouse("testPath\\");
            CreateDataSet();
            NameOfTheDataSet = Guid.NewGuid().ToString();
        }
        private void CreateDataSet()
        {
            Random rand = new Random();
            List<DataSet> dataSets = new List<DataSet>();
            for (int i = 0; i < rand.Next(20,100); i++)
            {
                double input1, input2, output;
                input1 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;
                input2 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;

                output = (input1 == 1 && input2 == 1) ? 1 : 0;
                List<double> inputs = new List<double>() { input1, input2 };
                List<double> Outputs = new List<double>() { output };
                dataSets.Add(new DataSet(inputs, Outputs));
            }
            NeuralNetworkTrainerContainerMocked.Setup(c => c.DataSets).Returns(dataSets);
        }
        [Fact]
        public void SaveShouldBeGood()
        {
            Action Save = () =>
            {
                TestSubject.Save(NeuralNetworkTrainerContainerMocked.Object.DataSets, NameOfTheDataSet);
            };
            Save.Should().NotThrow();
        }

        [Fact]
        public void RetreiveShouldBeGood()
        {
            SaveShouldBeGood();
            List<DataSet> DataSetsRecovred = TestSubject.Retreive<List<DataSet>>(NameOfTheDataSet);
            DataSetsRecovred.Should().BeEquivalentTo(NeuralNetworkTrainerContainerMocked.Object.DataSets);
        }
        [Fact]
        public void OverrideShouldBeGood()
        {
            SaveShouldBeGood();

            List<DataSet> oldDataSet = NeuralNetworkTrainerContainerMocked.Object.DataSets;

            CreateDataSet();
            TestSubject.Override(NeuralNetworkTrainerContainerMocked.Object.DataSets, NameOfTheDataSet).Wait();

            List<DataSet> DataSetRecovred = TestSubject.Retreive<List<DataSet>>(NameOfTheDataSet);

            oldDataSet.Should().NotBeSameAs(DataSetRecovred);
        }
        [Fact]
        public void SearchAvailableShouldBeGood()
        {
            string BaseName = NameOfTheDataSet;
            string searchTerm = "abc";
            string modifiedDataSetName = NameOfTheDataSet;

            for (int i = 0; i < 10; i++)
            {
                modifiedDataSetName += searchTerm;
                TestSubject.Save(NeuralNetworkTrainerContainerMocked.Object.DataSets, modifiedDataSetName);
            }

            for (int i = 0; i < 10; i++)
                TestSubject.SearchAvailable(BaseName, 0, i).Count().Should().Be(i);
        }

        [Fact]
        public void SaveShouldThrowAlreadyExistException()
        {
            SaveShouldBeGood();
            Action Save = () =>
            {
                TestSubject.Save(NeuralNetworkTrainerContainerMocked.Object.DataSets, NameOfTheDataSet).Wait();
            };
            Save.Should().Throw<AlreadyExistException>();
        }
        [Fact]
        public void RetreiveShouldThrowCantFindException()
        {
            string nameOfTheNonExistentDataSet = Guid.NewGuid().ToString();
            Action Save = () =>
            {
                List<DataSet> DataSetsRecovred = TestSubject.Retreive<List<DataSet>>(nameOfTheNonExistentDataSet);
            };
            Save.Should().Throw<CantFindException>();
        }
        [Fact]
        public void OverrideShouldThrowCantFindException()
        {
            string nameOfTheNonExistentDataSet = Guid.NewGuid().ToString();
             Action Override = () =>
            {
                TestSubject.Override(NeuralNetworkTrainerContainerMocked.Object.DataSets, nameOfTheNonExistentDataSet).Wait();
            };
            Override.Should().Throw<CantFindException>();
        }
        [Fact]
        public void RetreiveShouldThrowInvalidCastException()
        {
            string nameOfTheDataSet = Guid.NewGuid().ToString();
            Action Save = () =>
            {
                IList<DataSet> DataSetsRecovred = TestSubject.Retreive<IList<DataSet>>(nameOfTheDataSet);
            };
            Save.Should().Throw<InvalidCastException>();
        }
    }
}