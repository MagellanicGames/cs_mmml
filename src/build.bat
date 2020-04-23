@echo off
:: Requires setting a PATH variable for  C# compiler, else replace 'csc' with \path\to\compiler\csc.exe
csc -debug ByteCodeMacro.cs Globals.cs Command.cs CommandList.cs CommandMmml.cs Convert.cs Macro.cs Utils.cs cs_mmml.cs Filters.cs Header.cs Note.cs Print.cs Process.cs
