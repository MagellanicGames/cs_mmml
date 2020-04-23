using System.Collections.Generic;

namespace cs_mmml
{
    public class Command
    {
        public string name;
        public byte value;
        public bool is_dotted;

        public static string value_output_filter = "<>]";

        public static Dictionary<string, byte> string_to_bytecode = new Dictionary<string, byte>()
        {
            {"o", 0xD0}, {"<", 0xD0}, {">", 0xD0}, {"v", 0xE0},
            {"[", 0xF0 }, {"]", 0xF1}, {"m", 0xF2}, {"t", 0xF3}, {"@", 0xFF}
        };

        public static string m_sMacro = "m";
        public static string m_sStartLoop = "[";
        public static string m_sEndLoop = "]";
        public static string m_sTempo = "t";
        public static string m_sVolume = "v";
        public static string m_sOctave = "o";
        public static string m_sOctaveDown = "<";
        public static string m_sOctaveUp = ">";

        public Command()
        {

        }
        public Command(string name, byte value,bool is_dotted = false)
        {
            this.name = name;
            this.value = value;
            this.is_dotted = is_dotted;
        }

        public override string ToString()
        {
            string result = "Command: " + name + " value: " + value.ToString();
            if(is_dotted)
                result += "-dotted";
            return result;
        }

        public  string to_mmml()
        {
            string s = "";
            s += name;
            if(!value_output_filter.Contains(name))
             s += value.ToString();
            if (is_dotted)
                s += ".";
            return s;
        }

        public byte[] to_bytecode()
        {
            List<byte> result = new List<byte>();
            if(Filters.mmml_note_commands.Contains(name)) //0xNote|Length
            {
                byte len_key = value;
                if (is_dotted && len_key != 128 && len_key != 1)
                    len_key += (byte)(value / 2);
                byte len = Note.length_nibbles[(Note.len)len_key];
                byte note = Note.string_to_bytecode[name];
                byte byte_code = (byte)(len | note);
                result.Add(byte_code);           
            }
            else if(Filters.mmml_octave_commands.Contains(name))//0xCommand|Value
            {
                result.Add(Convert.octave_lookup[value - 1]);
            }
            else if (name == m_sTempo || name == m_sMacro || name == m_sStartLoop)//Uses whole byte and a second byte for tempo value;
            {
                byte command = string_to_bytecode[name]; //"m" in byte code m1 is m0
                result.Add(command);
                if (name == m_sMacro)
                    result.Add((byte)(value - 1));
                else
                    result.Add(value);
            }
            else if (name == m_sVolume) //0xCommand|Value - volume is in reverse, so needs to be reverse. 
            {
                result.Add(Convert.volume_lookup[value - 1]);
            }
            else if (name == m_sEndLoop)
            {
                result.Add(string_to_bytecode[name]);
            }
            else
            {
                Print.text_line("Command::to_bytecode: Error, unexpected command \"" + name + "\"", System.ConsoleColor.Red);
            }
            return result.ToArray();
        }
    }  
}