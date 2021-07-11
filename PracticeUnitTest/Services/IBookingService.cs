using PracticeUnitTest.Models;

namespace PracticeUnitTest.Services
{
    public interface IBookingService
    {
        string OverlappingBookingsExist(Booking booking);
    }
}