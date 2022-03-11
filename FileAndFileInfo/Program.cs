using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileAndFileInfo
{
    class Program
    {
        static CancellationTokenSource cancellationToken = new CancellationTokenSource();
        static void Main(string[] args)
        {
            Console.WriteLine("***** File And FileInfo *****");

            string path = "F:\\text.txt";
            string newPath = "F:\\Text\\text.txt";
            string text = "Hello world";
            string newText = "\nHello Ruslan";

            Task writeTask = WriteTextFileAsync(path, text, Encoding.Unicode, cancellationToken.Token);
            Task getInfo = writeTask.ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    GetFileInfo(path);
                    MoveFile(path, newPath);
                    //cancellationToken.Cancel();
                }
            });
            Task addedNewTextTask = getInfo.ContinueWith(async (t) =>
            {
                if (t.IsCompleted)
                {
                    await WriteTextFileAsync(newPath, newText, Encoding.Unicode, cancellationToken.Token);
                    GetFileInfo(newPath);
                    //cancellationToken.Cancel();
                }
            });
            Task showTextTask = addedNewTextTask.ContinueWith(async (t) =>
            {
                if (t.IsCompleted)
                {
                    string result = await ReadTextFileAsync(newPath, Encoding.Unicode, cancellationToken.Token);

                    Console.WriteLine("\nResult: " + result + "\n");

                    RemoveFile(newPath);
                }
            });

            do
            {
                Console.WriteLine("Напишите \"с\" для отмены задач.");

                string answer = Console.ReadLine();

                if (answer == "c")
                {
                    cancellationToken.Cancel();
                    break;
                }

            } while (true);
        }

        static async Task WriteTextFileAsync(string path, string text, Encoding encoding, CancellationToken token)
        {
            Console.WriteLine("\n=> WriteTextFileAsync():");

            await File.AppendAllTextAsync(path, text, encoding, token);

            Console.WriteLine($"Файл {path} сохранен!");
        }

        static async Task<string> ReadTextFileAsync(string path, Encoding encoding, CancellationToken token)
        {
            Console.WriteLine("\n=> ReadTextFileAsync():");

            string text = await File.ReadAllTextAsync(path, encoding, token);

            Console.WriteLine($"Файл {path} прочитан!");

            return text;
        }

        static void GetFileInfo(string path)
        {
            Console.WriteLine("\n=> GetFileInfo():");

            FileInfo file = new FileInfo(path);

            if (file.Exists)
            {
                Console.WriteLine($"Имя файла: {file.Name}");
                Console.WriteLine($"Время создания: {file.CreationTime}");
                Console.WriteLine($"Размер: {file.Length}");
            }
        }

        static void MoveFile(string path, string newPath)
        {
            Console.WriteLine("\n=> MoveFile():");

            File.Move(path, newPath, true);

            Console.WriteLine($"Файл {path} перемещён в {newPath}!");
        }

        static void RemoveFile(string path)
        {
            Console.WriteLine("=> RemoveFile():");

            File.Delete(path);

            Console.WriteLine($"Файл {path} удалён!");
        }
    }
}
