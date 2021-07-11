using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeUnitTest.Models;

namespace PracticeUnitTest.Repository
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }
}
