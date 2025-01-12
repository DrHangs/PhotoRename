using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace PicRenameForm
{
    class FileChangeData
    {
        public string Original { get; set; }
        public string New;
        public string Date;
        public int Duplicate;

        public string GetDate()
        {
            using (FileStream fs = new FileStream(Original, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                //BitmapSource img = BitmapFrame.Create(fs);
                //BitmapMetadata md = (BitmapMetadata)img.Metadata;
                //string date = md.DateTaken;
                //Console.WriteLine($"Date: {date}");
                var meta = ImageMetadataReader.ReadMetadata(fs);
                var subIfdDirectory = meta.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                var dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTime);
                if(dateTime != string.Empty)
                {
                    dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
                    //ExifDirectoryBase.//TagDateTimeOriginal
                }
                return dateTime;
            }
            //return "";
        }
    }
}