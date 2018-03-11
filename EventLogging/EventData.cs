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

        static int NumberOfEvents = 0;
        
        interface IEvent
        {
            int Count { get; }
            DateTime Timestamp { get; }
        }

        public class MessageEvent : IEvent
        {
            public int Count { get; }
            public DateTime Timestamp { get; }
            public string Message;
            MessageEvent(string messagePass)
            {
                Count = NumberOfEvents++;
                Timestamp = DateTime.Now;
                Message = messagePass;
            }
        }

        public class ErrorEvent : IEvent
        {
            public int Count { get; set; }
            public DateTime Timestamp { get; }

            public TraceSource TraceSource { get; }
            public Exception Exception { get; }
            public StackTrace StackTrace { get; }

            ErrorEvent(TraceSource source, Exception e, StackTrace trace)
            {
                TraceSource = source;
                Exception = e;
                StackTrace = trace;
            }

        }


        public List<MessageEvent> MessageEvents = new List<MessageEvent>();
        public List<ErrorEvent> ErrorEvents = new List<ErrorEvent>();



        public void Message(string m)
        {

        }




    }
}
