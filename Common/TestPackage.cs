using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Common
{
    public static class TestPackage
    {
        public static string Hello(string name)
        {
            var str = "hello " + name;
            Console.WriteLine(str);

            return str;
        }

        public static string Hello2(string name)
        {
            var str = "hello " + name;
            Console.WriteLine(str);

            return str;
        }
    }
}
