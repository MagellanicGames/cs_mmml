using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_mmml
{
    public static class Convert
    {
        public static byte[] volume_lookup = //due to 1-bit music, volume an octaves are inverted ie: E8 is v1 and E1 is v8
        {
            0xE8, 0xE7, 0xE6, 0xE5, 0xE4, 0xE3, 0xE2, 0xE1
        };

        public static byte[] octave_lookup =  //same as volume, lookup is used to invert values
        {
            0xD0, 0xD1, 0xD3, 0xD7, 0xDF
        };

        public static byte[] swap_short_bytes(byte[] bytes)
        {
            List<byte> swapped = new List<byte>();
            swapped.Add(bytes[1]);
            swapped.Add(bytes[0]);
            return swapped.ToArray();
        }

        public static ushort swap_bytes(ushort s)
        {
            return BitConverter.ToUInt16(Convert.swap_short_bytes(BitConverter.GetBytes(s)),0);
        }

        public static ushort swap_bytes(byte[] short_bytes)
        {
            short_bytes = swap_short_bytes(short_bytes);
            return BitConverter.ToUInt16(short_bytes,0);
        }
    }
}
