using Microsoft.AspNetCore.Mvc;

namespace Useful_training.Applicative.NeuralNetworkApi.ViewModel
{
	public class ResponseOk : StatusCodeResult
	{

		public string Message;
		public ResponseOk():base(StatusCodes.Status200OK)
		{
			Message = "";
		}
		public ResponseOk(string message) : base(StatusCodes.Status200OK)
		{
			Message = message;
		}
	}
}
