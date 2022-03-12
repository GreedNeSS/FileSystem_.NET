using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace FileStreamExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("***** File Stream Example *****");

            string path = @"F:\Text\File Stream Example.txt";
            string text = "Hello Ruslan!";
            string replaceText = "Marcus";

            await WriteFileAsync(path, text);
            await ReadFileAsync(path);
            await ReadChunkAsync(path);
            await WriteChunkAsync(path, 6, replaceText);
            await ReadFileAsync(path);
        }

        static async Task WriteFileAsync(string path, string text)
        {
            Console.WriteLine("\n=> WriteFileAsync():");

            using FileStream fstream = new FileStream(path, FileMode.OpenOrCreate);
            byte[] buffer = Encoding.Default.GetBytes(text);
            await fstream.WriteAsync(buffer, 0, buffer.Length);

            Console.WriteLine($"Файл {path} записан!");
        }

        static async Task ReadFileAsync(string path)
        {
            Console.WriteLine("\n=> WriteFileAsync():");

            using FileStream fstream = File.OpenRead(path);
            byte[] buffer = new byte[fstream.Length];
            await fstream.ReadAsync(buffer, 0, buffer.Length);
            string text = Encoding.Default.GetString(buffer);

            Console.WriteLine($"Содержимое файла {path}: \"{text}\"!");
        }

        static async Task ReadChunkAsync(string path)
        {
            Console.WriteLine("\n=> ReadChunkAsync():");

            byte[] buffer = new byte[6];
            using FileStream fstream = new FileStream(path, FileMode.OpenOrCreate);
            fstream.Seek(-7, SeekOrigin.End);
            await fstream.ReadAsync(buffer, 0, buffer.Length);
            string text = Encoding.Default.GetString(buffer);

            Console.WriteLine($"Результат смещения: {text}");
        }

        static async Task WriteChunkAsync(string path, int offset, string replaceText)
        {
            Console.WriteLine("\n=> WriteChunkAsync():");

            byte[] buffer = Encoding.Default.GetBytes(replaceText);
            using FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            fileStream.Seek(offset, SeekOrigin.Begin);
            await fileStream.WriteAsync(buffer, 0, buffer.Length);

            Console.WriteLine($"Файл {path} изменен!");
        }
    }
}
