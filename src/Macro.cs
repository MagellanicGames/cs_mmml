using System.Collections.Generic;
using System;

namespace cs_mmml
{
    public class Macro
    {
        public enum TypeE {macro, channel};
        public TypeE type;
        public List<string> mmml;
        public string name;

        public Macro(TypeE t, List<string> mmml, string name = "")
        {
            type = t;
            this.mmml = mmml;
            this.name = name;
            remove_macro_symbols();
        }

        public List<string> get_mmml()
        {
            return mmml;
        }

        public void remove_macro_symbols()
        {
            for(int i = 0; i < mmml.Count; i++)
            {
                string s = mmml[i];
                if(s.Contains("@") && s.Length > 0)
                {
                    mmml[i] = s.Substring(1,s.Length - 1);
                }
            }
        }

        public override string ToString()
        {
            string result = "Macro Name: " + name + "\n";
            foreach(string s in mmml)
            {
                result += s + "\n";
            }
            return result;
        }

        public static Macro get_macro(int index, List<string> mmml, Macro.TypeE type, string name = "")
        {            
            int[] macro_extents = get_macro_extents(index, mmml); //macro extents = start line, count
            List<string> macro_mml =  mmml.GetRange(macro_extents[0], macro_extents[1]);
            Macro result = new Macro(type,macro_mml,name);
            return result;
        }
            
        public static List<Macro> extract_macros(List<string> mmml)
        {
            var macros = new List<Macro>();
            int num_macros = get_num_macros(mmml);
            for(int i = 0; i < num_macros; ++i)
            {                
                macros.Add(get_macro(i, mmml, Macro.TypeE.macro, (i + 1).ToString()));
            }
            return macros;
        }

        public static List<Macro> extract_channels(List<string> mmml, int num_channels)
        {
            string channel_letters = "abcdfghijklmnopqrstuvwxyz";
            var channels = new List<Macro>();
            int num_lines = 0;
            for(int i = 0; i < num_channels; i++)
            {
                Macro channel = get_macro(i, mmml, Macro.TypeE.channel, channel_letters.Substring(i,1));
                num_lines += channel.mmml.Count;
                channels.Add(channel);

            }
            mmml.RemoveRange(0, num_lines);
            return channels;
        }
        public static int get_num_macros(List<string> mmml)
        {
            return Utils.get_indices_of_lines_starting_with("@", mmml).Count;
        }

        public static int[] get_macro_extents(int macro_idx, List<string> mmml)
        {
            List<int> macro_line_numbers = Utils.get_indices_of_lines_starting_with("@", mmml);    

            int start_idx = macro_line_numbers[macro_idx];
            int count;
            if (macro_idx == macro_line_numbers.Count - 1)
            {
                count = mmml.Count - start_idx;
            }
            else
            {
                count = macro_line_numbers[macro_idx + 1] - start_idx;
            }            
            return new int[] {start_idx, count};
        }
    }
}
