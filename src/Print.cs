using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_mmml
{
    public static class Print
    {
        public static void text(string what, ConsoleColor text_color = ConsoleColor.White)
        {
            Console.ForegroundColor = text_color;
            Console.Write(what);
        }

        public static void text_line(string what, ConsoleColor text_color = ConsoleColor.White)
        {
            text(what + "\n", text_color);
        }
    }
}
