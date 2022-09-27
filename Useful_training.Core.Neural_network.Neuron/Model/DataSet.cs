using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network
{
	public class DataSet
	{
		public List<double> Values { get; set; }
		public List<double> Targets { get; set; }

		public DataSet(List<double> values, List<double> targets)
		{
			Values = values;
			Targets = targets;
		}
	}
}
