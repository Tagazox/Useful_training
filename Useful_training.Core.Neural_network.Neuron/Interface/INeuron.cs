﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network.Interface
{
	internal interface INeuron : IInputNeurons , ISerializable
	{
		double Gradiant { get; set; }
		double CalculateGradient(double? target = null);
		new INeuron Clone();
		void GetCalculationResult();
		void UpdateWeights(double learnRate, double momentum);
	}
}
