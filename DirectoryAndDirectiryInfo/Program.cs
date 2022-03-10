using System;
using System.IO;

namespace DirectoryAndDirectiryInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Directory And DirectiryInfo *****");

            string path = "C:\\";

            ShowDyrectoryesandFilesWithDirectory(path);
            ShowDyrectoryesandFilesWithDirectoryInfo(path);
            DirFiltering(path);
            FileFiltering(path);
            CreateDir(path + "SomeDir");
            DirMove(path + "SomeDir", path + "SomeDirectory");
            DeleteDir(path + "SomeDirectory");
            ShowDirInfo(path + "Program Files");
        }

        static void ShowDyrectoryesandFilesWithDirectory(string path)
        {
            Console.WriteLine("\n=> ShowDyrectoryesandFilesWithDirectory():");

            if (Directory.Exists(path))
            {
                Console.WriteLine("\nSubdirectory:");

                string[] directories = Directory.GetDirectories(path);

                foreach (var dir in directories)
                {
                    Console.WriteLine(dir);
                }
                
                Console.WriteLine("\nFiles:");

                string[] files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
            }
        }

        static void ShowDyrectoryesandFilesWithDirectoryInfo(string path)
        {
            Console.WriteLine("\n=> ShowDyrectoryesandFilesWithDirectoryInfo():");

            var directory = new DirectoryInfo(path);

            if (directory.Exists)
            {
                Console.WriteLine("\nSubdirectory:");

                DirectoryInfo[] directories = directory.GetDirectories();

                foreach (DirectoryInfo dir in directories)
                {
                    Console.WriteLine(dir.FullName);
                }
                
                Console.WriteLine("\nFiles:");

                FileInfo[] files = directory.GetFiles();

                foreach (FileInfo file in files)
                {
                    Console.WriteLine(file.FullName);
                }
            }
        }

        static void DirFiltering(string path)
        {
            Console.WriteLine("\n=> DirFiltering():");

            string[] dirs = Directory.GetDirectories(path, "*Files*");

            foreach (var dir in dirs)
            {
                Console.WriteLine(dir);
            }
        }

        static void FileFiltering(string path)
        {
            Console.WriteLine("\n=> FileFiltering():");

            string[] files = Directory.GetFiles(path, "*.sys");

            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }

        static void CreateDir(string path)
        {
            Console.WriteLine("\n=> CreateDir():");

            string subPath = @"program\avalon";

            DirectoryInfo newDir = new DirectoryInfo(path);

            if (!newDir.Exists)
            {
                newDir.Create();
                newDir.CreateSubdirectory(subPath);

                Console.WriteLine($"{path} created");
            }
        }

        static void ShowDirInfo(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            Console.WriteLine("\n=> ShowDirInfo()");
            Console.WriteLine($"Name: {directory.Name}");
            Console.WriteLine($"Full Name: {directory.FullName}");
            Console.WriteLine($"CreationTime: {directory.CreationTime}");
            Console.WriteLine($"Root: {directory.Root}");
        }

        static void DirMove(string oldPath, string newPath)
        {
            Console.WriteLine("\n=>DirMove():");

            if (Directory.Exists(oldPath) && !Directory.Exists(newPath))
            {
                Directory.Move(oldPath, newPath);
                Console.WriteLine($"Move {oldPath} to {newPath}");
            }
        }

        static void DeleteDir(string path)
        {
            Console.WriteLine("\n=> DeleteDir():");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Console.WriteLine($"{path} deleted.");
            }
            else
            {
                Console.WriteLine($"{path} not found.");
            }
        }
    }
}
