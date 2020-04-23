using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_mmml
{
    public static class Filters
    {
        public static string mmml_non_note_commands = "[]mtov<>";      
        public static string mmml_note_commands = "rc+c#d+d#e+e#f+f#g+g#a+a#b+b#";
        public static string check_next_char = "[tvomrcdefgab"; 
        public static string mmml_octave_commands = "o<>";

        public static string nibble_commands = "vo<>";

        public static bool is_command_valid(string command)
        {
            return mmml_non_note_commands.Contains(command) || mmml_note_commands.Contains(command);
        }
    }
}
