using FileManager;
using Moq;
using FluentAssertions;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;
using FileManager.Exceptions;
using Useful_training.Core.Neural_network.ValueObject;

namespace FileManagerTests
{
    public class DataSetWarehouseTests
    {
        IDataSetsListWarehouse TestSubject;
        Mock<INeuralNetworkTrainerContainer> INeuralNetworkTrainerContainer;
        
        public DataSetWarehouseTests()
        {
            INeuralNetworkTrainerContainer = new Mock<INeuralNetworkTrainerContainer>();
            TestSubject = new DataSetsListWarehouse();
            CreateDataSet();
        }
        private void CreateDataSet()
        {
            Random rand = new Random();
            List<DataSet> dataSets = new List<DataSet>();
            for (int i = 0; i < rand.Next(20,100); i++)
            {
                double input1, input2, input3, input4, output;
                input1 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;
                input2 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;

                output = (input1 == 1 && input2 == 1) ? 1 : 0;
                List<double> inputs = new List<double>() { input1, input2 };
                List<double> Outputs = new List<double>() { output };
                dataSets.Add(new DataSet(inputs, Outputs));
            }
            INeuralNetworkTrainerContainer.Setup(c => c.DataSets).Returns(dataSets);
        }

        [Fact]
        public void SaveShouldBeGood()
        {
            string nameOfTheDataSet = Guid.NewGuid().ToString();

            Action Save = () =>
            {
                TestSubject.Save(INeuralNetworkTrainerContainer.Object.DataSets, nameOfTheDataSet);
            };
            Save.Should().NotThrow();
        }

        [Fact]
        public void RetreiveShouldBeGood()
        {
            string nameOfTheDataSet = Guid.NewGuid().ToString();

            TestSubject.Save(INeuralNetworkTrainerContainer.Object.DataSets, nameOfTheDataSet).Wait();
            List<DataSet> DataSetsRecovred = TestSubject.Retreive<List<DataSet>>(nameOfTheDataSet);
            DataSetsRecovred.Should().BeEquivalentTo(INeuralNetworkTrainerContainer.Object.DataSets);
        }

        [Fact]
        public void SearchAvailableShouldBeGood()
        {
            string nameOfTheDataSet = Guid.NewGuid().ToString();
            string BaseName = nameOfTheDataSet;
            string searchTerm = "abc";

            for (int i = 0; i < 10; i++)
            {
                nameOfTheDataSet += searchTerm;
                TestSubject.Save(INeuralNetworkTrainerContainer.Object.DataSets, nameOfTheDataSet);
            }
            for (int i = 0; i < 10; i++)
                TestSubject.SearchAvailable(BaseName, 0, i).Count().Should().Be(i);
        }

        [Fact]
        public void SaveShouldThrowAlreadyExistException()
        {
            string nameOfTheDataSet = Guid.NewGuid().ToString();
            TestSubject.Save(INeuralNetworkTrainerContainer.Object.DataSets, nameOfTheDataSet).Wait();

            Action Save = () =>
            {
                TestSubject.Save(INeuralNetworkTrainerContainer.Object.DataSets, nameOfTheDataSet).Wait();
            };
            Save.Should().Throw<AlreadyExistException>();
        }
        [Fact]
        public void RetreiveShouldThrowCantFindNeuralNetworkException()
        {
            string nameOfTheDataSet = Guid.NewGuid().ToString();
            Action Save = () =>
            {
                List<DataSet> DataSetsRecovred = TestSubject.Retreive<List<DataSet>>(nameOfTheDataSet);
            };
            Save.Should().Throw<CantFindException>();
        }
        [Fact]
        public void RetreiveShouldThrowException()
        {
            string nameOfTheDataSet = Guid.NewGuid().ToString();
            Action Save = () =>
            {
                List<DataSet> DataSetsRecovred = TestSubject.Retreive<List<DataSet>>(nameOfTheDataSet);
            };
            Save.Should().Throw<Exception>();
        }
    }
}