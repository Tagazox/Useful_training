﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Exceptions
{
    public class NeuralNetworkAlreadyExistException : Exception
    {
            public NeuralNetworkAlreadyExistException(string message) : base(message)
            {
            }
    }
}
