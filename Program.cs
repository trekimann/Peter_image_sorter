using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Peter_image_sorter
{

    public class DateObject
    {
        public string Year;
        public string Month;
        public string Day;
    }

    public class Program
    {
        private static Regex r = new Regex(":");
        public static string LaunchLocation;
        static void Main(string[] args)
        {
            try
            {
                LaunchLocation = AppContext.BaseDirectory;

                Console.WriteLine(LaunchLocation);
                Console.WriteLine("Welcome to the Peter Picture sorter");
                if (args.Length == 0)
                {
                    // present menu of options
                    var quit = false;
                    while (!quit)
                    {
                        Console.WriteLine("What do you want to do? Enter the coresponding number");
                        Console.WriteLine("1. Create the next months folder");
                        Console.WriteLine("2. Sort all files in a folder into dated files");
                        Console.WriteLine("3. Sort a single file");
                        Console.WriteLine("4. Close Application");
                        Console.WriteLine("5. Debug File regex");
                        var choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "1":
                                Console.WriteLine("TODO");
                                break;
                            case "2":
                                Console.WriteLine("Enter Folder directory to sort");
                                var folder = Console.ReadLine();
                                LoopFilesInFolder(folder);
                                break;
                            case "3":
                                Console.WriteLine("Enter file path to sort");
                                var file = Console.ReadLine();
                                SortFileIntoFolder(file);
                                break;
                            case "4":
                                quit = true;
                                break;
                            case "5":
                                debugDate();
                                break;
                            default:
                                Console.WriteLine("selection not recognised");
                                Console.ReadLine();
                                break;
                        }
                        Console.WriteLine("Action Complete");
                    }
                }
                else
                {
                    // Drag the images and video to sort here and this program will check the date on them and file them in the correct folder
                    // paths of files dragged onto an exe are sent as args to the program
                    foreach (var file in args)
                    {
                        LoopFilesInFolder(file);
                    }
                    Console.WriteLine("Action Complete. Press enter to close");
                    Console.ReadLine();
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                Console.ReadLine();
            }


        }

        public static void CreateNewFolder(string folderDir)
        {
            System.IO.Directory.CreateDirectory(LaunchLocation + folderDir);
        }

        public static void LoopFilesInFolder(string dir)
        {
            // Check that this is the lowest folder
            var FolderOrDirectory = File.GetAttributes(dir);
            if (FolderOrDirectory.HasFlag(FileAttributes.Directory))
            {
                // when a dir, go inside and do this again
                var sub = System.IO.Directory.GetFiles(dir);
                foreach (var file in sub)
                {
                    LoopFilesInFolder(file);
                }
            }
            else
            {
                // if its a file, sort it
                SortFileIntoFolder(dir);
            }
        }

        public static void SortFileIntoFolder(string FilePath)
        {
            // move the file from its OG path to the appropriate dated folder
            // Find the date, use filename for now but maybe metadata later?
            var FileName = Path.GetFileName(FilePath);

            var FileDate = GetDateTaken(FilePath, FileName);
            if (FileDate.Day != null)
            {            
            var day = FileDate.Day;
            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Int32.Parse(FileDate.Month));
            var year = FileDate.Year;

            // with the date, move the file to the right directory
            var NewDestination = year + "\\" + month + "\\" + day + "\\" + FileName;
            // Check no file exists, if it does rename with a _#
            var NameInUse = true;
            var variant = 0;
            while (NameInUse)
            {
                if (File.Exists(NewDestination))
                {
                    //remove extension
                    var nameArray = FileName.Split(".");
                    variant += 1;
                    NewDestination = year + "\\" + month + "\\" + day + "\\" + nameArray[0] + "_" + variant.ToString() + "." + nameArray[1];
                }
                else
                {
                    NameInUse = false;
                }
            }

            if (!System.IO.Directory.Exists(year + "\\" + month + "\\" + day))
            {
                CreateNewFolder(year + "\\" + month + "\\" + day);
            }
            File.Move(FilePath, LaunchLocation + NewDestination);
            }
            else
            {
                Console.WriteLine($"Date could not be extracted,{FileName} not moved");
            }
        }

        public static void debugDate()
        {
            // _2022_06_08_19_46_08.mp4
            Console.WriteLine("Enter example file Name: ");
            String fileName = Console.ReadLine();
            DateObject extractedDate = GetDateFromFileName(fileName);
            Console.WriteLine("Extracted date is:");
            Console.WriteLine("Year: " + extractedDate.Year);
            Console.WriteLine("Month: " + extractedDate.Month);
            Console.WriteLine("Day: " + extractedDate.Day);
        }

        public static DateObject GetDateFromFileName(string videoLocation)
        {
            // Assuming that the video has a nice date conventaion VID_{YYYYMMDD}_{HHMMSS}.mp4. Could do a regex check?
            DateObject toReturn = new DateObject();

            var filename = Path.GetFileName(videoLocation);

            // VID_20220103_205223.mp4
            Regex fileNameFormatPlus9Video = new Regex(@"VID_[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]_[0-9][0-9][0-9][0-9][0-9][0-9]\.mp4");
            
            // 20220102_191014.mp4
            Regex fileNameFormatS21Video = new Regex(@"[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]_(.*)\.mp4");
            
            // VID20220126210857.mp4
            Regex fileNameFormatPlus9Pro_2 = new Regex(@"VID[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]\.mp4");

            // VID-20211111-WA0008.mp4
            Regex fileNameFormat_wa = new Regex(@"VID-[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]-WA(.*)\.mp4");

            // 20211114_190938.heic
            Regex fileNameFormat_heic_s21 = new Regex(@"[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]_(.*)\.heic");

            // IMG20220122112747.heic
            Regex fileNameFormat_heic_9pro = new Regex(@"IMG[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]\.heic");

            // _2022_06_08_19_46_08.mp4
            Regex fileNameFormatEufyDirectOnPhone = new Regex(@"^_[0-9][0-9][0-9][0-9]_[0-9][0-9]_[0-9][0-9]_[0-9][0-9]_[0-9][0-9]_[0-9][0-9]\.mp4");

            // _2022_06_08_19_46_08.mp4
            Regex fileNameFormatEufyRecordFromDevice = new Regex(@"_[A-Za-z]*_[0-9][0-9][0-9][0-9]_[0-9][0-9]_[0-9][0-9]_[0-9][0-9]_[0-9][0-9]_[0-9][0-9]\.mp4");

            if (fileNameFormatPlus9Video.IsMatch(filename))
            {
                var titleArray = filename.Split("_");
                var dateBit = titleArray[1];
                Console.WriteLine(dateBit);
                toReturn.Year = dateBit[0..4];
                toReturn.Month = dateBit[4..6];
                toReturn.Day = dateBit[6..8];
            }
            else if(fileNameFormatS21Video.IsMatch(filename)){
                // Other type of file nameing
                var titleArray = filename.Split("_");
                var dateBit = titleArray[0];
                Console.WriteLine(dateBit);
                toReturn.Year = dateBit[0..4];
                toReturn.Month = dateBit[4..6];
                toReturn.Day = dateBit[6..8];
            }
            else if (fileNameFormatPlus9Pro_2.IsMatch(filename))
            {
                // remove the vid bit
                var titleArray = filename.Split("VID");
                var dateBit = titleArray[1];
                Console.WriteLine(dateBit);
                toReturn.Year = dateBit[0..4];
                toReturn.Month = dateBit[4..6];
                toReturn.Day = dateBit[6..8];

            }
            else if (fileNameFormat_wa.IsMatch(filename))
            {
                //whats app videos
                var titleArray = filename.Split("-");
                var dateBit = titleArray[1];
                Console.WriteLine(dateBit);
                toReturn.Year = dateBit[0..4];
                toReturn.Month = dateBit[4..6];
                toReturn.Day = dateBit[6..8];
            }
            else if (fileNameFormat_heic_s21.IsMatch(filename))
            {
                // Other type of file nameing
                var titleArray = filename.Split("_");
                var dateBit = titleArray[0];
                Console.WriteLine(dateBit);
                toReturn.Year = dateBit[0..4];
                toReturn.Month = dateBit[4..6];
                toReturn.Day = dateBit[6..8];
            }
            else if (fileNameFormat_heic_9pro.IsMatch(filename))
            {
                // remove the vid bit
                var titleArray = filename.Split("IMG");
                var dateBit = titleArray[1];
                Console.WriteLine(dateBit);
                toReturn.Year = dateBit[0..4];
                toReturn.Month = dateBit[4..6];
                toReturn.Day = dateBit[6..8];

            }
            else if (fileNameFormatEufyDirectOnPhone.IsMatch(filename))
            {
                var titleArray = filename.Split("_");
                Console.WriteLine(titleArray);
                toReturn.Day = titleArray[3];
                toReturn.Month = titleArray[2];
                toReturn.Year = titleArray[1];
            }
            else if (fileNameFormatEufyRecordFromDevice.IsMatch(filename))
            {
                var titleArray = filename.Split("_");
                Console.WriteLine(titleArray);
                toReturn.Day = titleArray[4];
                toReturn.Month = titleArray[3];
                toReturn.Year = titleArray[2];
            }
            else
            {
                Console.WriteLine("Video file name did not match the requirements");
            }
            return toReturn;
        }

        public static DateObject GetDateTaken(string imageLocation, string fileName)
        {
            var toReturn = new DateObject();
            FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
            try
            {
                if (IsImage(fs))
                {
                    var directories = ImageMetadataReader.ReadMetadata(imageLocation);
                    Image myImage = Image.FromStream(fs, false, false);
                    var dateTaken = "";
                    bool dateFound = false;
                    foreach (var directory in directories)
                    {
                        if (dateFound)
                        {
                            break;
                        }
                        foreach (var tag in directory.Tags)
                        {
                            Console.WriteLine($"{directory.Name} - {tag.Name} = {tag.Description}");
                            // USE GPS details if you can as they are more likekly the creation date of the image
                            if (tag.Name == "GPS Date Stamp")
                            {
                                int i = 0;
                                var GPSformats = new string[] { "yyyy:MM:dd hh:mm:ss", "yyyy:MM:dd" };
                                while (i < GPSformats.Length && !dateFound)
                                {
                                    toReturn = CameraTimestampToArray(tag.Description, GPSformats[i]);
                                    dateFound = toReturn.Day == null ? false: true;
                                    i++;
                                }
                                break;
                                
                            }
                            else if (tag.Name == "Date/Time Original" && !dateFound)
                            {
                                toReturn = CameraTimestampToArray(tag.Description, "yyyy:MM:dd HH:mm:ss");
                                dateFound = toReturn.Day == null ? false : true;                              
                            }
                            else if (tag.Name == "File Modified Date" && !dateFound)
                            {                       
                                toReturn = CameraTimestampToArray(tag.Description, "ddd MMM dd HH:mm:ss zzz yyyy");
                                break;
                            }
                            else
                            {
                                if (!dateFound)
                                {
                                try
                                    {
                                        PropertyItem propItem = myImage.GetPropertyItem(36867);
                                        dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                                        var DateArray = dateTaken.Split("-");
                                        toReturn.Day = DateArray[2].Split(" ")[0];
                                        toReturn.Month = DateArray[1];
                                        toReturn.Year= DateArray[0];

                                    } catch (Exception ex)
                                    {}
                                }
                            }
                        }
                    }        
                }
                else
                {
                    Console.WriteLine("Only Images supported at this time");
                    toReturn = GetDateFromFileName(imageLocation);
                    //var directories = ImageMetadataReader.ReadMetadata(imageLocation);
                    //var fileDirectory = directories;
                    //foreach (var directory in directories)
                    //{
                    //    foreach (var tag in directory.Tags)
                    //    {
                    //        Console.WriteLine($"{directory.Name} - {tag.Name} = {tag.Description}");
                    //    }
                    //}

                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Could not read date, not moving file");
                Console.WriteLine(e);
            }
            finally
            {
                fs.Close();
                Console.WriteLine("File closed");
            }
            return toReturn;
        }

        public static DateObject CameraTimestampToArray(string timeStamp, string format)
        {
            var toReturn = new DateObject();
            CultureInfo provider = CultureInfo.InvariantCulture; 
            try
            {
                var date = DateTime.ParseExact(timeStamp, format, provider);
                toReturn.Day = date.Day.ToString("00");
                toReturn.Month = date.Month.ToString();
                toReturn.Year = date.Year.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not parse date");
                Console.WriteLine(e);
            }
            return toReturn;
        }

        public static bool IsImage(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            List<string> jpg = new List<string> { "FF", "D8" };
            List<string> bmp = new List<string> { "42", "4D" };
            List<string> gif = new List<string> { "47", "49", "46" };
            List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
            List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

            List<string> bytesIterated = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                string bit = stream.ReadByte().ToString("X2");
                bytesIterated.Add(bit);

                bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                if (isImage)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
