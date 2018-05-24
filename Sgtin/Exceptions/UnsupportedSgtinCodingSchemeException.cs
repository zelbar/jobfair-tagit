using System;

namespace JobFair.Tagit.Sgtin.Exceptions
{
    public class UnsupportedSgtinCodingSchemeException : Exception
    {
        public UnsupportedSgtinCodingSchemeException(string message) 
            : base(message)
        {

        }
    }
}