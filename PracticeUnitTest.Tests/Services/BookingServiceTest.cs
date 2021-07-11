using FluentAssertions;
using Moq;
using PracticeUnitTest.Models;
using PracticeUnitTest.Repository;
using PracticeUnitTest.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PracticeUnitTest.Tests.Services
{
    public class BookingServiceTest
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly BookingService _bookingService;

        private Booking _existingBooking = new Booking
        {
            Id = 2,
            ArrivalDate = ArriveOn(2017, 1, 15),
            DepartureDate = DepartOn(2017, 1, 20),
            Reference = "a"
        };

        public BookingServiceTest()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _bookingRepository.Setup(x => x.GetActiveBookings(It.IsAny<int?>()))
                .Returns(new List<Booking>
                {
                    _existingBooking
                }.AsQueryable());
            _bookingService = new BookingService(_bookingRepository.Object);
        }

        #region Helper

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private static DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private static DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        #endregion Helper

        [Fact]
        public void OverlappingBookingsExist_BookingStatusIsCancelled_ReturnEmptyString()
        {
            // Act
            var actual = _bookingService.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
                Status = "Cancelled"
            });

            // Assert
            actual.Should().BeEmpty();
        }

        [Fact]
        public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            // Act
            var actual = _bookingService.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            });

            // Assert
            actual.Should().BeEmpty();
        }

        [Fact]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingsReference()
        {
            // Act
            var actual = _bookingService.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate)
            });

            // Assert
            actual.Should().Be(_existingBooking.Reference);
        }
    }
}