// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Useful_training.Applicative.NeuralNetworkApi.ViewModel;

public class ErrorResponse
{
    public string Type { get; }
    public string Message { get; }
    public ErrorResponse(Exception? ex)
    {
        if (ex != null)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
        }
        else
        {
            Type = "500";
            Message = string.Empty;
        }
    }
}