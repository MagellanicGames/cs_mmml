using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_mmml
{
    public class ByteCodeMacro
    {
        public ushort byte_position;
        public List<byte> bytecode;
        public ushort size;

        public ByteCodeMacro(CommandList macro, ushort byte_position)
        {
            this.byte_position = byte_position;
            bytecode = new List<byte>();
            bytecode.AddRange(macro.to_bytecode());
            bytecode.Add(0xFF);//'@' 
            size = (ushort)bytecode.Count;            
        }
    }
}
