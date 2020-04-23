using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_mmml
{
    public class Header
    {
        public ushort size;
        public HeaderEntry channel_a;
        public HeaderEntry channel_b;
        public HeaderEntry channel_c;
        public HeaderEntry channel_d;
        public HeaderEntry[] macros;

        public Header(List<HeaderEntry> entries)
        {
            channel_a = entries[0];
            size = channel_a.pos;
            channel_b = entries[1];
            channel_c = entries[2];
            channel_d = entries[3];

            entries.RemoveRange(0, 4);
            macros = entries.ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Channel/Macro | Byte Position | Size (in bytes)\n").
                Append("Channel A").Append(channel_a.ToString()).Append("\n").
                Append("Channel B").Append(channel_b.ToString()).Append("\n").
                Append("Channel C").Append(channel_c.ToString()).Append("\n").
                Append("Channel D").Append(channel_d.ToString()).Append("\n");
            for (int i = 0; i < macros.Length; i++)
                sb.Append("Macro ").Append(i).Append(macros[i].ToString()).Append("\n");
            return sb.ToString();
        }
    }

    public class HeaderEntry
    {
        public ushort pos;
        public ushort size;

        public HeaderEntry(ushort pos, ushort size)
        {
            this.pos = pos;
            this.size = size;
        }

        public override string ToString()
        {
            return "{" + pos.ToString() + "," + size.ToString() + "}";
        }
    }
}
