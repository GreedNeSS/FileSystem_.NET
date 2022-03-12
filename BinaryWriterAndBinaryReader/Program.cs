using System;
using System.Collections.Generic;
using System.IO;

namespace BinaryWriterAndBinaryReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** BinaryWriter And BinaryReader *****");

            Person[] people =
            {
                new Person("Ruslan", 30),
                new Person("Marcus", 40),
                new Person("Henry", 28)
            };
            string path = @"F:\Text\Person.dat";
            List<Person> personList = new List<Person>();

            WriteBinaryFileAsync(path, people);
            ReadBinaryFileAsync(path, personList);

            Console.WriteLine("\nСодержимое personList:");
            personList.ForEach(person => Console.WriteLine(person));
        }

        static void WriteBinaryFileAsync(string path, IEnumerable<Person> people)
        {
            Console.WriteLine("\n=> WriteBinaryFileAsync():");

            using BinaryWriter writer = new(File.Open(path, FileMode.OpenOrCreate));

            foreach (Person person in people)
            {
                writer.Write(person.Name);
                writer.Write(person.Age);
            }

            Console.WriteLine($"Файл{path} записан!");
        }

        static void ReadBinaryFileAsync(string path, List<Person> resultList)
        {
            Console.WriteLine("\n=> ReadBinaryFileAsync():");

            using BinaryReader reader = new(File.Open(path, FileMode.OpenOrCreate));

            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                int age = reader.ReadInt32();
                resultList.Add(new Person(name, age));
            }

            Console.WriteLine($"Файл{path} прочитан!");
        }
    }
}
