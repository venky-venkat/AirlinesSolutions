using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booking.DL.DBContext;
using Booking.DL.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Booking.DL.StatusEnums;
namespace Booking.DL.Consume
{
   public class FlightStatus : IFlightStatus
    {
        private readonly BookingDBContext _DB;
        public FlightStatus(BookingDBContext bookingDBContext)
        {
            _DB = bookingDBContext;
        }

        public void Consumemessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            
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
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        
                        var message = Encoding.UTF8.GetString(body);
                        if (message.Length > 0)
                        {
                            var data = message.Split("/");
                            var result = _DB.Bookings.Where(x => x.FlightName == data[1] && x.JourneyStatus != Status.Completed).ToList<Bookings>();
                            foreach(var v in result)
                            {
                                v.FlightStatus = Status.Cancelled;
                                v.RefundStatus = Status.Completed;
                                v.JourneyStatus = Status.Cancelled;
                                _DB.SaveChanges();
                            }
                        }
                    };
                    channel.BasicConsume(queue: "FlightStatus", autoAck: true, consumer: consumer);
                }
            }
             
        }
    }
}
