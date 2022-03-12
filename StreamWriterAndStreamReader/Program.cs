using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StreamWriterAndStreamReader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("***** StreamWriter And StreamReader *****");

            string path = @"F:\Text\note.dat";

            await WriteFileAsync(path, "Hello Ruslan!");
            await WriteFileAsync(path, "\nHello Greed!", true);
            await ReadToEndFileAsync(path);
            await ReadLineFileAsync(path);
            await WriteFileAsync(path, "New Info");
            ReadFile(path);
            await ReadFileAsync(path);
        }

        static async Task WriteFileAsync(string path, string text, bool append = false)
        {
            Console.WriteLine("\n=> WriteFileAsync():");

            using StreamWriter writer = new StreamWriter(path, append);
            await writer.WriteAsync(text);

            Console.WriteLine($"Файл {path} записан.");
        }

        static async Task ReadToEndFileAsync(string path)
        {
            Console.WriteLine("\n=> ReadToEndFileAsync():");

            using StreamReader reader = new StreamReader(path);
            string text = await reader.ReadToEndAsync();


            Console.WriteLine($"Результат: {text}");
        }

        static async Task ReadLineFileAsync(string path)
        {
            Console.WriteLine("\n=> ReadLineFileAsync():");

            string? line;
            using StreamReader reader = new StreamReader(path);
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Console.WriteLine($"Результат: {line}");
            }
        }

        static void ReadFile(string path)
        {
            Console.WriteLine("\n=> ReadFile():");

            int? simbol;
            List<char> chs = new List<char>();
            using StreamReader reader = new StreamReader(path);

            while ((simbol = reader.Read()) != -1)
            {
                chs.Add((char)simbol);
            }

            chs.ForEach(c => Console.Write(c));
            Console.WriteLine();
        }

        static async Task ReadFileAsync(string path)
        {
            Console.WriteLine("\n=> ReadFileAsync():");

            using StreamReader reader = new StreamReader(path);
            char[] buffer = new char[reader.BaseStream.Length];
            await reader.ReadAsync(buffer, 0, (int)reader.BaseStream.Length);

            foreach (char ch in buffer)
            {
                Console.Write(ch);
            }
        }
    }
}
