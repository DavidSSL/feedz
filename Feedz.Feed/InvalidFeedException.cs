using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedz.Feed
{
    [Serializable]
    public class InvalidFeedException : Exception
    {
        public InvalidFeedException() {}

        public InvalidFeedException(string message) : base(message) {}

        public InvalidFeedException(string message, Exception inner) : base(message, inner) {}
    }
}