using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_mmml
{
    public class CommandMmml
    {
        List<CommandList> m_channels;
        List<CommandList> m_macros;

        public CommandMmml(List<Macro> channels, List<Macro> macros )
        {
            m_channels = new List<CommandList>();
            m_macros = new List<CommandList>();

            store_macros(m_channels, channels);
            store_macros(m_macros, macros);
        }

        private void store_macros(List<CommandList> where,List<Macro> mmml_macros)
        {
            foreach (Macro m in mmml_macros)
                where.Add(Process.convert_macro_to_command_list(m));
        }

        public int get_num_channels() { return m_channels.Count; }
        public int get_num_macros() { return m_macros.Count; }

        public string to_mmml()
        {
            string mmml = "";
            foreach (var m in m_channels)
                mmml += m.to_mmml();
            foreach (var m in m_macros)
                mmml += m.to_mmml();

            return mmml;
        }

        public byte[] generate_header(List<ByteCodeMacro> bytecode)//each entry is byte start position of macros held in uint16
        {
            var header = new List<byte>();

            foreach (ByteCodeMacro macro in bytecode)
            {
                ushort byte_pos = macro.byte_position;
                header.AddRange(Convert.swap_short_bytes(BitConverter.GetBytes(byte_pos)));
            }
            return header.ToArray();
        }

        public byte[] to_bytecode()
        {
            List<ByteCodeMacro> macro_bytes = new List<ByteCodeMacro>();

            ushort current_position = (ushort)((m_channels.Count + m_macros.Count) * 2);
            foreach(var cmd_list in m_channels)
            {
                ByteCodeMacro macro = new ByteCodeMacro(cmd_list, current_position);
                macro_bytes.Add(macro);
                current_position += macro.size;
            }

            foreach(var cmd_list in m_macros)
            {
                ByteCodeMacro macro = new ByteCodeMacro(cmd_list, current_position);
                macro_bytes.Add(macro);
                current_position += macro.size;
            }

            List<byte> bytecode = new List<byte>();
            bytecode.AddRange(generate_header(macro_bytes));
            macro_bytes.ForEach(macro => bytecode.AddRange(macro.bytecode));

            return bytecode.ToArray();
        }


    }
}
