using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace CompressingAndDecompresingFiles
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("***** Compressing and decompressing files *****");
            string sourcePath = @"F:\Text\Основы C#.pdf";
            string compressPath = @"F:\Text\Compress.gz";
            string decompressPath = @"F:\Text\Decompress.pdf";

            await CompessAsync(sourcePath, compressPath);
            await DecompessAsync(compressPath, decompressPath);
            CompessDirectory(@"F:\TextFolder", @"F:\CompressFolder.zip");
            DecompessDirectory(@"F:\CompressFolder.zip", @"F:\New Text");
        }

        static async Task CompessAsync(string sourcePath, string compressPath)
        {
            Console.WriteLine("\n=> CompessAsync():");

            using FileStream source = new FileStream(sourcePath, FileMode.OpenOrCreate);
            using FileStream destination = new FileStream(compressPath, FileMode.OpenOrCreate);
            using GZipStream gZip = new GZipStream(destination, CompressionMode.Compress);
            await source.CopyToAsync(gZip);

            Console.WriteLine($"Файл {sourcePath} сжат!");
            Console.WriteLine($"Исходный размер {source.Length}, сжатый размер {destination.Length}");
        }

        static async Task DecompessAsync(string sourcePath, string decompressPath)
        {
            Console.WriteLine("\n=> DecompessAsync():");

            using FileStream source = new FileStream(sourcePath, FileMode.Open);
            using FileStream destination = new FileStream(decompressPath, FileMode.Create);
            using GZipStream gZip = new GZipStream(source, CompressionMode.Decompress);
            await gZip.CopyToAsync(destination);

            Console.WriteLine($"Файл {sourcePath} разархивирован");
        }

        static void CompessDirectory(string sourcePath, string compressPath)
        {
            Console.WriteLine("\n=> CompessDirectory():");

            ZipFile.CreateFromDirectory(sourcePath, compressPath);

            Console.WriteLine($"Архив {compressPath} создан!");
        }

        static void DecompessDirectory(string sourcePath, string decompressPath)
        {
            Console.WriteLine("\n=> DecompessDirectory():");

            ZipFile.ExtractToDirectory(sourcePath, decompressPath);

            Console.WriteLine($"Архив {sourcePath} разархивирован в {decompressPath}.");
        }
    }
}
