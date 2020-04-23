using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_mmml
{
    public static class Note
    {
        public enum len
        {
            whole = 1,
            half = 2, dotted_half = 3,
            quarter = 4, dotted_quarter = 6,
            eighth = 8, dotted_eighth = 12,
            sixteenth = 16, dotted_sixteenth = 24,
            thirtysecond = 32, dotted_thirtysecond = 48,
            sixtyforth = 64, dotted_sixtyforth = 96, 
            onetwentyeight = 128
        }

        public static Dictionary<len, byte> length_nibbles = new Dictionary<len, byte>(){//0xNote|Length
            {len.whole, 0x00},
            {len.half, 0x01}, {len.dotted_half, 0x08},
            {len.quarter,0x02}, {len.dotted_quarter, 0x09},
            {len.eighth, 0x03}, {len.dotted_eighth, 0x0A},
            {len.sixteenth, 0x04}, {len.dotted_sixteenth, 0x0B},
            {len.thirtysecond, 0x05}, {len.dotted_thirtysecond, 0x0c},
            {len.sixtyforth, 0x06}, {len.dotted_sixtyforth, 0x0D},
            {len.onetwentyeight, 0x07}
        };

        public static Dictionary<string, byte> string_to_bytecode = new Dictionary<string, byte>()
        {
            {"r",0x00},
            {"c", 0x10}, {"d", 0x30}, {"e", 0x50}, {"f", 0x60}, {"g", 0x80}, {"a", 0xA0}, {"b", 0xC0},
            {"c+", 0x20}, {"d+", 0x40}, {"e+", 0x70}, {"f+", 0x70}, {"g+", 0x90}, {"a+", 0xB0},{"b+", 0x10},
            {"c#", 0x20}, {"d#", 0x40}, {"e#", 0x70}, {"f#", 0x70}, {"g#", 0x90}, {"a#", 0xB0},{"b#",0x10}
        };
    }
}
