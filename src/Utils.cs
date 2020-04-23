using System;
using System.IO;
using System.Collections.Generic;

namespace cs_mmml
{
    public class Utils
    {
        public static List<string> read_text_file_lines(string path)
        {
            return new List<string>(File.ReadAllLines(path));
        }

        public static void remove_lines_starting_with(string what, List<string> text)
        {
            var lines_to_remove = new List<string>();
            foreach(var line in text)
                if(line.StartsWith(what))
                    lines_to_remove.Add(line);
        
            foreach(var line in lines_to_remove)
                text.Remove(line);
        }

        public static void remove_empty_lines(List<string> text)
        {
            var lines_to_remove = new List<string>();
            for(int i = 0; i < text.Count; ++i)
            {
                if(String.IsNullOrWhiteSpace(text[i]))
                    lines_to_remove.Add(text[i]);
                text[i] = text[i].Replace(" ", string.Empty);
            }

            
            foreach(var line in lines_to_remove)
                text.Remove(line);
        }
        public static void remove_whitespace_comments(List<string> mmml)
        {
            remove_lines_starting_with("%", mmml);
            remove_empty_lines(mmml);
        }
        public static List<int> get_indices_of_lines_starting_with(string what, List<string> text)
        {
            List<int> result = new List<int>();
            for(int i = 0; i < text.Count; i++)
            {
                if(text[i].StartsWith(what))
                    result.Add(i);
            }
            
            return result;
        }

        public static void remove_macro_symbols(List<string> mmml)
        {
            for(int i = 0; i < mmml.Count; i++)
            {
                string s = mmml[i];
                if(s.Contains("@") && s.Length > 1)
                {
                    mmml[i] = s.Substring(1,s.Length - 1);
                }
            }
        }


    }  
}