using System;
using System.IO;

namespace Drives
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Drivers *****");

            DriveInfo[] drives = DriveInfo.GetDrives();

            ShowDrivesInfo(drives);
        }

        static void ShowDrivesInfo(DriveInfo[] drives)
        {
            foreach (var drive in drives)
            {
                Console.WriteLine($"\nName: {drive.Name}");
                Console.WriteLine($"Type: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Total size: {drive.TotalSize}");
                    Console.WriteLine($"Total free space: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Volume Label: {drive.VolumeLabel}");
                }
            }
        }
    }
}
