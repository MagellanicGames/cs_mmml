using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace cs_mmml
{
    public class cs_mmml
    {    
        public static string ext = ".mmml";

        public static List<string> m_sErrorLog = new List<string>();

        public enum Mode {  test, dev, live}

        public static Mode m_mode = Mode.live;

        public static void Main(string[] args)
        {
            switch(m_mode)
            {
                case Mode.live:
                    args_mode(args);
                    break;
            }

            Exit();           
        }   

        public static void args_mode(string [] args)
        {
            string welcome_message = "=== Welcome to CS MMML v 0.1 ===\n";
            if (args.Length < 1)
            {
                Print.text_line("Error - No file path input given.");
                return;
            }
            string file_folder = "";
            string file_name = args[0];
            if(args.Length == 2)
                file_folder = args[1];
            int num_channels = 4;
            Print.text_line(welcome_message, ConsoleColor.Green);
            Print.text("Reading data from ");
            Print.text_line(file_folder + file_name, ConsoleColor.Yellow);
            List<string> mmml = Utils.read_text_file_lines(file_folder + file_name);
            Print.text_line("Removing whitespace and comments...");
            Utils.remove_whitespace_comments(mmml);

            List<Macro> channels = Macro.extract_channels(mmml, num_channels);
            List<Macro> macros = Macro.extract_macros(mmml);

            Print.text("Found ");
            Print.text(channels.Count.ToString() + " channels", ConsoleColor.Green);
            Print.text("and ");
            Print.text_line(macros.Count.ToString() + " macros.", ConsoleColor.Cyan);
            Print.text_line("Converting to CommandMmml...");

            CommandMmml cs_mmml = new CommandMmml(channels, macros);
            File.WriteAllBytes(file_folder + file_name.Split('.')[0] + "_cs.mmmldata", cs_mmml.to_bytecode());
        }  

        public static void Exit(string message = "Finished!", ConsoleColor text_col = ConsoleColor.Green)
        {
            Print.text_line(message, text_col);
            File.WriteAllLines("error.log", m_sErrorLog.ToArray());
            Console.ResetColor();
            System.Environment.Exit(0);
        }
    } 
}
