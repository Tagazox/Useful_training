using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace NeuralNetworkApi.ViewModel
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
