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
                    Console.WriteLine("Testing: {0}", path);
                    Console.WriteLine();

                    if (Directory.Exists(path)) 
                    { DoDirectory(path); }
                    else if (File.Exists(path))
                    { DoFile(path); }
                    else
                    {
                        success = 1;
                        Console.WriteLine("{0} is neither file nor folder.", path);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception: {0}", e.Message);
            }

            if (Properties.Settings.Default.PromptToEnd)    
            {   Console.WriteLine();
                Console.WriteLine("Press Enter to end.");
                Console.ReadLine();
            }
            Environment.ExitCode = success; 
       }

        // RecurseesruceR
        static void DoDirectory(string directory)
        {
            string[] files=null;
            Console.WriteLine("Folder '{0}'.", directory);

            try { files = Directory.GetFiles(directory); }
            catch (Exception unauthorizedAccessException)
            {
                Console.WriteLine(unauthorizedAccessException.Message);
                return;
            }

            foreach (string file in files) DoFile(file);

            string[] subdirs = Directory.GetDirectories(directory);
            foreach (string subdir in subdirs) DoDirectory(subdir);
        }

        static void DoFile(string file)
        { Console.WriteLine("\tFile '{0}'.", file); }
    }
}