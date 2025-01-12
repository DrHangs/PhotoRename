using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace PicRename
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading current directory...");
            DirectoryInfo curdir = new DirectoryInfo(Directory.GetCurrentDirectory());

            //var subdir = curdir.CreateSubdirectory("RENAMED");
            //var unclass = curdir.CreateSubdirectory("UNCHANGED");
            var subdir = new DirectoryInfo(Path.Combine(curdir.FullName, "RENAMED"));
            var unclass = new DirectoryInfo(Path.Combine(curdir.FullName, "UNCHANGED"));


            if (subdir.Exists || unclass.Exists)
            {
                Console.WriteLine($"Directories \"{subdir.Name}\" or \"{unclass.Name}\" already exists!");
                Console.WriteLine("Please move or delete these, or else I don't know what to do 🥺🥺");
                Console.WriteLine();
                Console.Write("Press any key to quit");
                Console.ReadKey(true);
                Environment.Exit(1);
            }

            subdir.Create();
            unclass.Create();

            string changed = "->";
            string notnged = "x ";
            int terminalWidth = Console.WindowWidth;
            int firstlen = terminalWidth /2;
            int secondlen = firstlen - 5;

            var files = curdir.GetFiles();
            Console.WriteLine($"Found {files.Length} files total. Will only look at files of type png, jpg and jpeg!");
            Console.WriteLine();

            string legendleft = "Filename".PadRight(firstlen).Substring(0, firstlen-1);
            string legendright = "New name".PadRight(secondlen).Substring(0, secondlen-2);
            Console.WriteLine($"{legendleft} x/-> {legendright}");
            Console.WriteLine(new string('-', firstlen + secondlen));

            foreach (var file in files)
            {
                if (file.Extension == ".png" || file.Extension == ".jpg" || file.Extension == ".jpeg")
                {
                    bool rename = true;

                    var date = GetDate(file);

                    if (string.IsNullOrEmpty(date))
                    {
                        rename = false;
                    }
                    DateTime dateTime;
                    if (!DateTime.TryParse(date, out dateTime))
                    {
                        rename = false;
                    }

                    if (rename)
                    {
                        string formatted = dateTime.ToString("yyyy-MM-dd_HHmmss") + file.Extension;
                        File.Copy(file.FullName, Path.Combine(subdir.FullName, formatted));

                        string leftside = file.Name.PadRight(firstlen).Substring(0, firstlen);
                        string rightside = formatted.PadRight(secondlen).Substring(0, secondlen);
                        Console.WriteLine($"{leftside} {changed} {rightside}");
                    }
                    else
                    {
                        File.Copy(file.FullName, Path.Combine(unclass.FullName, file.Name));
                        string leftside = file.Name.PadRight(firstlen).Substring(0, firstlen);
                        Console.WriteLine($"{leftside} {notnged}");
                    }

                    //if (string.IsNullOrEmpty(date))
                    //{
                    //    File.Copy(file.FullName, Path.Combine(unclass.FullName, file.Name));
                    //    //Console.WriteLine($"{file.Name:30}: NO RENAME");
                    //    Console.WriteLine($"{file.Name,-20} {notnged,-10} {file.Name}");
                    //}
                    //else
                    //{
                    //    DateTime dateTime;
                    //    if (DateTime.TryParse(date, out dateTime))
                    //    {
                    //        string formatted = dateTime.ToString("yyyy-MM-dd_HHmmss") + file.Extension;
                    //        File.Copy(file.FullName, Path.Combine(subdir.FullName, formatted));
                    //        //Console.WriteLine($"{file.Name:30}: RENAME {formatted}");
                    //        Console.WriteLine($"{file.Name,-20} {changed,-10} {formatted}");
                    //    }
                    //    else
                    //    {
                    //        // Couldn't parse date, to unclassified
                    //        File.Copy(file.FullName, Path.Combine(unclass.FullName, file.Name));
                    //        //Console.WriteLine($"{file.Name:30}: NO RENAME");
                    //        Console.WriteLine($"{file.Name,-20} {notnged,-10} {file.Name}");
                    //        // file1 -> fr
                    //        // file2 x  no
                    //    }
                    //}
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Press any key to quit");
            Console.ReadKey(true);
        }

        public static string GetDate(FileInfo f)
        {
            using (FileStream fs = new FileStream(f.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BitmapSource img = BitmapFrame.Create(fs);
                BitmapMetadata md = (BitmapMetadata)img.Metadata;
                string date = md.DateTaken;
                //Console.WriteLine($"Date: {date}");
                return date;
            }
        }
    }
}
