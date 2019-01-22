using System;

namespace Repetite
{
    public class MissingInputException : Exception
    {
        public MissingInputException(string key)
            : base($"Missing input: {key}")
        {
        }
    }
}