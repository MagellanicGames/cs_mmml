using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_mmml
{
    public class CommandList
    {
        private List<Command> m_commands = new List<Command>();

        public CommandList()
        {
            m_commands = new List<Command>();
        }

        public CommandList(List<Command> commands)
        {
            m_commands = commands;
        }

        public void add(Command c)
        {
            m_commands.Add(c);
        }

        public void add(List<Command> commands)
        {
            m_commands.AddRange(commands);
        }

        public List<Command> get_commands()
        {
            return m_commands;
        }

        public override string ToString()
        {
            string s = "";
            foreach (var command in m_commands)
            {
                s += command.ToString() + " | ";
            }
            return s;
        }

        public string to_mmml()
        {
            string s = "@";
            foreach (Command c in m_commands)
            {
                s += c.to_mmml();
            }
            s += "\n";
            return s;
        }

        public byte[] to_bytecode()
        {
            List<byte> bytecode = new List<byte>();
            foreach (var cmd in m_commands)
            {
                bytecode.AddRange(cmd.to_bytecode());
            }

            return bytecode.ToArray();
        }
    }
}
