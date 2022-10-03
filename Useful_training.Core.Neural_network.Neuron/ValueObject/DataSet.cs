using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network.ValueObject
{
	public class DataSet
	{
		public List<double> Inputs { get; set; }

		public List<double> TargetOutput { get; set; }
		
		[JsonConstructor]
		public DataSet()
        {
			Inputs = new List<double>();
			TargetOutput = new List<double>();
		}
		public DataSet(List<double> values, List<double> targets)
		{
			Inputs = values;
			TargetOutput = targets;
		}
	}
}
