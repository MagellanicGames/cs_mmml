# CS μMML
*A port of **PROTODOME**'s Micro Music Macro Language compiler to C# for desktop.* 

You're probably here because you know what **μMML** is, but if not, head of to https://github.com/protodomemusic/mmml.  Currently, this project only has a bare minimum implementation of the μMML compiler.  A synthesizer is in the works alongside developments and improvements to the compiler.

Feel free to contact me if you have any queries about the project.  Any queries about the actual language and such though, you're probably best contacting PROTODOME.

### Compilation

The source works well with either Mono or Visual C#.  There are two provided source layouts, one that can be compiled from the terminal with a script file and one with a project file.

To compile on in the terminal, ensure you either have Mono (https://www.mono-project.com/) or Visual C# compilers installed.  Just run the script and you should have your shiny executable ready to run.

### Running the compiler

Under Ubuntu simply run with the following command: ``mono cs_mmml.exe [file]`` or if in Windows, simply call and provide a file name: ``cs_mmml.exe [file]``
