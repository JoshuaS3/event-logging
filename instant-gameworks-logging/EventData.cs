﻿/*  Copyright (c) Joshua Stockin 2018
 *
 *  This file is part of Instant Gameworks.
 *
 *  Instant Gameworks is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Instant Gameworks is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Instant Gameworks.  If not, see <http://www.gnu.org/licenses/>.
 */


using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EventLogging
{
    public class EventData
    {

        private static bool _initiated = false;
        private static DateTime _initTimestamp;

        private static string _headerBlock = "Instant Gameworks (c)  2018\r\nThread Start: {0} {1}\r\n";

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
            public override string ToString()
            {
                return string.Format("[{0}] {1}:{2:00}:{3:00}.{4:000} - {5}", Count, Timestamp.Hour, Timestamp.Minute, Timestamp.Second, Timestamp.Millisecond, Message);
            }
        }

        private class ErrorEvent : IEvent
        {
            public int Count { get; set; }
            public DateTime Timestamp { get; }

            public string TraceSource { get; }
            public string Exception { get; }
            public string StackTrace { get; }

            public ErrorEvent(string message, string source, string trace)
            {
                Count = ++NumberOfEvents;
                Timestamp = DateTime.Now;

                TraceSource = source;
                Exception = message;
                StackTrace = trace;
            }

            public override string ToString()
            {
                return string.Format("[{0}] {1}:{2:00}:{3:00}.{4:000} - {6}: {5}\r\n{7}", Count, Timestamp.Hour, Timestamp.Minute, Timestamp.Second, Timestamp.Millisecond, Exception, TraceSource, StackTrace);
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

        public static void Error(Exception e)
        {
            if (!_initiated) _initiate();

            ErrorEvent newMessage = new ErrorEvent(e.Message, e.Source, e.StackTrace);
            Console.WriteLine(String.Format("[{0}] {1}:{2:00}:{3:00}.{4:000} - {6}: {5}\n{7}", newMessage.Count, newMessage.Timestamp.Hour, newMessage.Timestamp.Minute, newMessage.Timestamp.Second, newMessage.Timestamp.Millisecond, newMessage.Exception, newMessage.TraceSource, newMessage.StackTrace));
            ErrorEvents.Add(newMessage);
        }

        public static void WriteEvents()
        {

            Directory.CreateDirectory("EventLogging");
            string formattedTime = string.Format("{0}-{1:00}-{2:00} {3:00}꞉{4:00}꞉{5:00}.{6:000}", _initTimestamp.Year, _initTimestamp.Month, _initTimestamp.Day, _initTimestamp.Hour, _initTimestamp.Minute, _initTimestamp.Second, _initTimestamp.Millisecond);
            FileStream eventFile = new FileStream(string.Format(@"EventLogging\{0}.log", formattedTime), FileMode.Create, FileAccess.Write, FileShare.None);
            TextWriter writer = new StreamWriter(eventFile);

            writer.WriteLine(string.Format(_headerBlock, _initTimestamp.ToShortDateString(), _initTimestamp.ToLongTimeString()));

            for (int currentEventNumber = 0; currentEventNumber < (NumberOfEvents - 1); currentEventNumber++)
            {
                writer.WriteLine(MessageEvents[currentEventNumber]);
            }
            writer.WriteLine(ErrorEvents[0]);

            writer.Close();
            eventFile.Close();

        }




    }
}
