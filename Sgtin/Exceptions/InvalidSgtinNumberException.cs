using System;

namespace JobFair.Tagit.Sgtin.Exceptions
{
    public class InvalidSgtinNumberException : Exception
    {
        public InvalidSgtinNumberException(string message) 
            : base(message)
        {

        }
    }
}