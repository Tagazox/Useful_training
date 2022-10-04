namespace Useful_training.Applicative.NeuralNetworkApi.ViewModel
{
    public class ErrorResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public ErrorResponse(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
        }
    }
}
