using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Core.Exception
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
