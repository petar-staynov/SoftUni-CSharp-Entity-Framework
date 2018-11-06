using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMappingExercise.Core.Contracts
{
    interface ICommandInterpreter
    {
        string Read(string[] input);
    }
}
