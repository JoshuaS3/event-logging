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
        static void a()
        {
            throw new Exception(" should not happen.");
        }
        static void test()
        {
            a();
        }
        static void Main(string[] args)
        {
            LogForm.InitializeWindow();
        }
    }
}
