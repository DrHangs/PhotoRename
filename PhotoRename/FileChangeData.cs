using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System.IO;
using System.Windows.Media.Imaging;

namespace PhotoRename
{
    class FileChangeData
    {
        public string Original { get; set; }
        public string OrigFilename { get; set; }
        public string New { get; set; }
        public string Date;
        public int Duplicate;
        public string State { get; set; }

        public string GetDate()
        {
            // Using .NET internal classes, Fallback in case of Nuclear desaster
            using (FileStream fs = new FileStream(Original, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                try
                {
                    BitmapSource img = BitmapFrame.Create(fs);
                    BitmapMetadata md = (BitmapMetadata)img.Metadata;
                    string date = md.DateTaken;
                    //Console.WriteLine($"Date: {date}");
                    return date;
                }
                catch (NotSupportedException)
                {
                    return string.Empty;
                }
                catch (FileFormatException)
                {
                    return string.Empty;
                }
            }
        }

        public string GetDateExt()
        {
            // Using external Lib MetadataExtractor
            using (FileStream fs = new FileStream(Original, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var meta = ImageMetadataReader.ReadMetadata(fs);
                var subIfdDirectory = meta.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                var dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
                if (string.IsNullOrEmpty(dateTime))
                {
                    dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTime);
                    //ExifDirectoryBase.//TagDateTimeOriginal
                }
                return dateTime ?? string.Empty;
            }
        }

        public void ExtractDate()
        {
            Date = GetDateExt();
            if (string.IsNullOrEmpty(Date))
            {
                Date = GetDate();
            }
        }
    }
}