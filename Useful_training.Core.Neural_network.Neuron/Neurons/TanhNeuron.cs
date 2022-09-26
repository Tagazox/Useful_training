﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Interface;
using System.Runtime.CompilerServices;

namespace Useful_training.Core.Neural_network.Neurons
{
    internal class TanhNeuron : Neuron
    {
        public TanhNeuron(IEnumerable<IInputNeurons> inputNeurons) : base(inputNeurons)
        {
        }
        public override void GetCalculationResult()
        {
            OutputResult = Math.Tanh(GetInterpolationResult());
        }
        internal override double DerivativeFunctionResultCalculation()
        {
            return (Math.Exp(-OutputResult) * (OutputResult + 1) + 1) / (Math.Pow(1 + Math.Exp(-OutputResult), 2));
        }
    }
}
