using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLogging
{
    public class EventData
    {

        private static bool _initiated = false;
        private static DateTime _initTimestamp;

        private static string _headerBlock = "Instant Gameworks (c)  2018\nThread Start: {0} {1}\n";

        public static int NumberOfEvents = 0;
        
        private interface IEvent
        {
            int Count { get; }
            DateTime Timestamp { get; }
        }

        private class MessageEvent : IEvent
        {
            public int Count { get; }
            public DateTime Timestamp { get; }
            public string Message;
            public MessageEvent(string messagePass)
            {
                Count = ++NumberOfEvents;
                Timestamp = DateTime.Now;

                Message = messagePass;
            }
        }

        private class ErrorEvent : IEvent
        {
            public int Count { get; set; }
            public DateTime Timestamp { get; }

            public TraceSource TraceSource { get; }
            public Exception Exception { get; }
            public StackTrace StackTrace { get; }

            public ErrorEvent(TraceSource source, Exception e, StackTrace trace)
            {
                Count = ++NumberOfEvents;
                Timestamp = DateTime.Now;

                TraceSource = source;
                Exception = e;
                StackTrace = trace;
            }

        }


        private static List<MessageEvent> MessageEvents = new List<MessageEvent>();
        private static List<ErrorEvent> ErrorEvents = new List<ErrorEvent>();


        private static void _initiate()
        {
            _initiated = true;
            _initTimestamp = DateTime.Now;
            Console.WriteLine(string.Format(_headerBlock, _initTimestamp.ToShortDateString(), _initTimestamp.ToLongTimeString()));
        }

        public static void Message(string m)
        {
            if (!_initiated) _initiate();

            MessageEvent newMessage = new MessageEvent(m);
            Console.WriteLine(String.Format("[{0}] {1}:{2:00}:{3:00}.{4:000} - {5}", newMessage.Count, newMessage.Timestamp.Hour, newMessage.Timestamp.Minute, newMessage.Timestamp.Second, newMessage.Timestamp.Millisecond, newMessage.Message));
            MessageEvents.Add(newMessage);
        }




    }
}
