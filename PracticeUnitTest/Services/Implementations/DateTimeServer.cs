using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeUnitTest.Services.Implementations
{
    public class DateTimeServer : IDateTimeServer
    {
        public DateTime Now => DateTime.Now;
    }
}
