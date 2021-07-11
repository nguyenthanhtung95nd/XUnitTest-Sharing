using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeUnitTest.Services
{
    public interface IDateTimeServer
    {
        DateTime Now { get; }
    }
}
