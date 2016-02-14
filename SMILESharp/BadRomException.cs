using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMILESharp
{
    /// <summary>
    /// An exception thrown when the ROM could not be loaded, or the ROM is not a valid Super Metroid ROM
    /// </summary>
    class BadRomException : Exception
    {
        public BadRomException(string message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}
