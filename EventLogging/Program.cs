using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using EventLogging;

namespace EventLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            EventData.Message("Hello, World!");
            Thread.Sleep(500);
            EventData.Message("500 milliseconds later...");
            Thread.Sleep(260);
            try
            {
                throw new Exception();
            }
            catch (Exception e)
            {
                EventData.Error(e);
            }

            EventData.WriteEvents();
        }
    }
}
