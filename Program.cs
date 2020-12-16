using System;
using System.IO;

namespace TestForPath
{
    static class Program
    {
        static void Main(string[] args)
        {   int success = 0;
            try
            {
                foreach (string path in Properties.Settings.Default.PathsToTest)
                {
                    // U2U1104: Do not use composite formatting to concatenate strings
                    // Composite formatting is very powerful, but using it to concatenate strings is not only overkill, it is highly inefficient.
                    Console.WriteLine(DateTime.Now.ToString() + ": Testing: " + path);

                    if (Directory.Exists(path)) 
                    { DoDirectory(path); }
                    else if (File.Exists(path))
                    { DoFile(path); }
                    else
                    {
                        // We want to return success if the initial path is valid; not usre this will work.
                        success = 1;
                        Console.WriteLine(path + " is neither file nor folder.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception: " + e.Message);
            }

            if (Properties.Settings.Default.PromptToEnd)    
            {   Console.WriteLine(DateTime.Now.ToString() + ": Press Enter to end.");
                Console.ReadLine();
            }
            Environment.ExitCode = success; 
       }
        // Could not find a part of the path 'c:\Program Files\WindowsApps\25841LowtechStudios.io.Slither.io_15.9.0.0_x64__3bf2w4xg630q0'.
        // "invisible" WindowsApp folder?
        // RecurseesruceR
        static void DoDirectory(string directory)
        {
            string[] files=null;
            if (!Properties.Settings.Default.NoPathOutput) { Console.WriteLine("Folder: " + directory); }
            try { files = Directory.GetFiles(directory); }
            // todo: temporarily grabbing all exceptions here...
            catch (Exception unauthorizedAccessException)
            {
                if (!Properties.Settings.Default.NoExceptionOuput) { Console.WriteLine(unauthorizedAccessException.Message); }
                return;
            }

            foreach (string file in files) DoFile(file);

            string[] subdirs = Directory.GetDirectories(directory);
            foreach (string subdir in subdirs) DoDirectory(subdir);
        }

        static void DoFile(string file)
        { 
            if (!Properties.Settings.Default.NoPathOutput) { Console.WriteLine("\tFile " + file); }
        }
    }
}