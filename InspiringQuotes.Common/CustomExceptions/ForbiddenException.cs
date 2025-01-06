using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspiringQuotes.Common.CustomExceptions
{
    public class ForbiddenException: ApplicationException
    {
        public ForbiddenException() { }
        public ForbiddenException(string message) : base(message) { }
    }
}
