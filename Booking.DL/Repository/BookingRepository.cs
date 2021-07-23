using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.DL.Consume;
using Booking.DL.DBContext;
using Booking.DL.IRepository;
using Booking.DL.Models;
using Booking.DL.StatusEnums;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
namespace Booking.DL.Repository
{
    public class BookingRepository : IBookingDL
    {
        private readonly BookingDBContext _DB;
        public BookingRepository(BookingDBContext bookingDBContext)
        {
            _DB = bookingDBContext;
        }
        public Bookings AddBooking(Bookings booking)
        {
            _DB.Bookings.Add(booking);
            var result = _DB.SaveChanges();
            if (result > 0)
            {
                return booking;
            }
            return null;
        }


        public Bookings CancelBookingByID(int id)
        {
            var result = _DB.Bookings.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                result.BookingStatus = Status.Cancelled;
                var res = _DB.SaveChanges();
                if (res > 0)
                {
                    return result;
                }
            }
            return null;
        }

        public Bookings CancelBookingByPNR(string pnr)
        {
            var result = _DB.Bookings.FirstOrDefault(x => x.PNR == pnr);
            if (result != null)
            {
                result.BookingStatus = Status.Cancelled;
                var res = _DB.SaveChanges();
                if (res > 0)
                {
                    return result;
                }
            }
            return null;
        }

        public  List<Bookings> GetallBookings()
        {
           
            return _DB.Bookings.ToList<Bookings>();
            
        }

        public  Bookings GetBookingByPNR(string PNR)
        {

            string message = Consumemessage();
            if (message.Length > 0)
            {
                var data = message.Split("/");
                var res = _DB.Bookings.Where(x => x.FlightName == data[1].ToString() && x.JourneyStatus != Status.Completed && x.PNR == PNR).FirstOrDefault();
                if (res != null)
                {
                    res.FlightStatus = Status.Cancelled;
                    res.RefundStatus = Status.Completed;
                    res.JourneyStatus = Status.Cancelled;
                    _DB.SaveChanges();
                }
            }
            Thread.Sleep(5000);
            var result = _DB.Bookings.FirstOrDefault(x => x.PNR == PNR);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public  List<Bookings> GetBookingsByUser(string Username)
        { 
            var result =  _DB.Bookings.Where(x=>x.UserName == Username).ToList<Bookings>();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public Bookings UpdateBooking(Bookings booking)
        {
            throw new NotImplementedException();
        }

        public  string Consumemessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var message = "";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "FlightStatus",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received +=  (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                       
                        message = Encoding.UTF8.GetString(body);

                    };
                    channel.BasicConsume(queue: "FlightStatus", autoAck: true, consumer: consumer);
                }
            }
            return message;
        }
    }
}
