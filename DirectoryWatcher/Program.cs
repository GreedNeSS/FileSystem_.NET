using System;
using System.IO;

namespace DirectoryWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Directory Watcher *****");

            FileSystemWatcher fsWatcher = new FileSystemWatcher();

            try
            {
                fsWatcher.Path = @"F:\Text";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            fsWatcher.NotifyFilter = NotifyFilters.FileName 
                | NotifyFilters.DirectoryName
                | NotifyFilters.CreationTime 
                | NotifyFilters.LastWrite 
                | NotifyFilters.LastAccess;

            fsWatcher.Filter = "*.txt";

            fsWatcher.Changed += (s, e) =>
            {
                Console.WriteLine($"{e.ChangeType}: {e.Name}, path: {e.FullPath}");
            };

            fsWatcher.Created += (s, e) =>
            {
                Console.WriteLine($"{e.ChangeType}: {e.Name}, path: {e.FullPath}");
            };

            fsWatcher.Deleted += (s, e) =>
            {
                Console.WriteLine($"{e.ChangeType}: {e.Name}, path: {e.FullPath}");
            };

            fsWatcher.Renamed += (s, e) =>
            {
                Console.WriteLine($"{e.ChangeType}: {e.Name}, path: {e.FullPath}");
            };

            fsWatcher.EnableRaisingEvents = true;

            while (Console.Read() != 'q');
        }
    }
}
