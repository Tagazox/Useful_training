﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Neuron.Interface;

namespace Useful_training.Core.Neural_network.Neuron.Neurons
{
    public class DerivativeSigmoidNeuron : Neuron
    {
        public override double GetCalculationResult(IList<double> input)
        {
            var SigmoidResult = SigmoidFunction(GetCalculationResult(input));
            return SigmoidResult * (1.0- SigmoidResult);
        }
        private double SigmoidFunction(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
    }
}
