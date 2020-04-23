using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_mmml
{
    /// <summary>
    /// Functions for processing mmml text files
    /// </summary>
    public static class Process
    {
        public static CommandList convert_macro_to_command_list(Macro m)
        {
            var result = new CommandList();
            foreach (string line in m.get_mmml())
            {
                List<Command> converted = convert_mmml_line(line);
                result.add(converted);
            }
            return result;
        }

        public static List<Command> convert_mmml_line(string line)
        {
            var commands = new List<Command>();
            for (int i = 0; i < line.Length; ++i)
            {
                String character = line[i].ToString();
                if (!Filters.is_command_valid(character) && character != "&") //check input is at all valid
                {
                    string error_message = "cs_mmml::convert_mmml_line: ERROR - Invalid command : '" + character + "'" +
                    " at idx " + i.ToString();
                    Print.text_line(error_message, ConsoleColor.Red);
                    error_message += "'" + line + "'";
                    Print.text_line("'" + line + "'", ConsoleColor.Yellow);
                    cs_mmml.m_sErrorLog.Add(error_message);
                    cs_mmml.Exit("Syntax errors in mmml, exiting...", ConsoleColor.Red);
                }

                if (Filters.mmml_non_note_commands.Contains(character))//is it a function or mod(otv) command?
                {
                    if (Filters.check_next_char.Contains(character)) //does next character need to be checked?
                    {
                        i++;
                        int value_string_length = get_size_of_num_at_idx(i, line); //size in decimal places ie 100 is 3, 10 is 2, 1 is 1 etc.
                        byte command_value = get_number_value(line, i, value_string_length); //don't increment as the loop will increment itself
                        if (value_string_length > 1)
                            i += value_string_length - 1;
                        commands.Add(new Command(character, command_value));
                        if (Filters.mmml_octave_commands.Contains(character))
                            store_octave(character, command_value);

                    }
                    else
                    {
                        if (Filters.mmml_octave_commands.Contains(character))
                        {
                            store_octave(character, 0);
                            commands.Add(new Command(character, Globals.m_last_octave));
                        }
                        else
                        {
                            commands.Add(new Command(character, 0));
                        }

                    }

                }
                else if (Filters.mmml_note_commands.Contains(character)) //is it a note command?
                {
                    Tuple<Command, int> result = _note_command(line, i);
                    commands.Add(result.Item1);
                    i = result.Item2;
                }
            }
            return commands;
        }

        private static Tuple<Command, int> _note_command(string line, int idx)
        {
            var command_result = new Command();

            command_result.name = line[idx].ToString();
            //is next character another note or note length?
            if (is_char_number(idx + 1, line))
            {
                _note_with_length_command(line, ref idx, ref command_result);
            }
            else if (is_char_sharp(line, idx + 1))
            {
                idx++;
                command_result.name += line[idx];
                if (is_char_number(idx + 1, line))
                {
                    _note_with_length_command(line, ref idx, ref command_result);
                }
                else
                {
                    _note_with_last_known_length(ref command_result);
                }
            }
            else
            {
                _note_with_last_known_length(ref command_result);
            }
            return new Tuple<Command, int>(command_result, idx);
        }

        private static void _note_with_length_command(string line, ref int idx, ref Command command_result)
        {
            idx++;
            int note_length_in_chars = get_size_of_num_at_idx(idx, line);
            byte note_length_value = get_number_value(line, idx, note_length_in_chars);
            idx += note_length_in_chars - 1;

            command_result.value = note_length_value;
            if (is_char_dot(idx + 1, line))
            {
                command_result.is_dotted = true;
                idx++;
            }

            Globals.m_last_note = command_result.name;
            Globals.m_last_note_length = note_length_value;
            Globals.m_last_note_length_dotted = command_result.is_dotted;
        }

        private static void _note_with_last_known_length(ref Command command_result)
        {
            command_result.value = Globals.m_last_note_length;
            command_result.is_dotted = Globals.m_last_note_length_dotted;
        }

        private static void store_octave(string character, byte value)
        {
            if (character == Command.m_sOctave)
                Globals.m_last_octave = value;
            else if (character == Command.m_sOctaveDown)
                Globals.m_last_octave--;
            else if (character == Command.m_sOctaveUp)
                Globals.m_last_octave++;
        }

        private static bool is_char_sharp(string s, int idx)
        {
            var result = false;
            if (idx < s.Length)
            {
                char c = s[idx];
                result = c == '+' || c == '#';
            }
            return result;
        }

        private static byte get_number_value(string s, int idx, int num_length)
        {
            byte result = 0;
            if (idx + (num_length - 1) < s.Length)
            {
                result = Byte.Parse(s.Substring(idx, num_length));
            }
            return result;
        }

        private static bool is_char_num_or_dot(int idx, string s)
        {
            return is_char_number(idx, s) || is_char_dot(idx, s);
        }

        private static bool is_char_number(int idx, string s)
        {
            var result = false;
            if (idx < s.Length)
            {
                string next_char = s.Substring(idx, 1);
                int num;
                result = Int32.TryParse(next_char, out num);
            }
            return result;
        }

        private static bool is_char_dot(int idx, string s)
        {
            var result = false;
            if (idx < s.Length)
            {
                string next_char = s.Substring(idx, 1);
                result = next_char == ".";
            }
            return result;
        }

        /// <summary>
        /// Length of the number found in the string in characters ie 200 = 3
        /// </summary>
        /// <param name="current_idx"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        private static int get_size_of_num_at_idx(int current_idx, string s)
        {
            int num_size = 0;
            int idx = current_idx;
            bool is_num = true;
            while (idx < s.Length && is_num)
            {
                is_num = is_char_number(idx, s);
                if (is_num)
                {
                    num_size++;
                    idx++;
                }

            }
            return num_size;
        }

    }
}
