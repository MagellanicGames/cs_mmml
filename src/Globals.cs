namespace cs_mmml
{
    public static class Globals
    {
        public static byte m_last_octave = 0;
        public static string m_last_note = "";
        public static byte m_last_note_length = 0;
        public static bool m_last_note_length_dotted = false;

        public static class Paths
        {
            public static string m_output_root = "Output/";
            public static string m_resources = "Resources/";
            public static string m_mmml_output = m_output_root + "mmml/";
            public static string m_bytecode_output =  m_output_root + "bytecode/";
        }

    }
}