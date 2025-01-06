using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspiringQuotes.Common.CustomExceptions
{
    public class UserFriendlyException: ApplicationException
    {
        public UserFriendlyException() { }
        public UserFriendlyException(string message) : base(message) { }
    }
}
