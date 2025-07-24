using MetadataExtractor.Formats.Exif;
using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace PicRenameGUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string Default_FileString = "IMG_{yyyyMMdd-HHmmss}";

        string DirPath { get; set; }
        bool path_ok { get; set; }
        string FormatString { get; set; }

        SettingData FileSettings { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            button_select.Click += Select_Click;

            text_checks.Text = "🔄";
            text_dirpath.TextChanged += Dirpathbox_TextChanged;
            button_run.IsEnabled = false;

            //list_out.ReadOnly = true;
            //list_out.ScrollBars = ScrollBars.Both;
            //list_out.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Visible;
            //list_out.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Visible;

            button_run.Click += Runbutton_Click;

            //bar_progress.Style = ProgressBarStyle.Blocks;

            text_format_in.TextChanged += FormatBoxIn_TextChanged;
            text_format_in.Text = Default_FileString;

            button_format_default.Click += FormatButton_Click;

            button_settings.Click += Button_settings_Click;

            // https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
            text_format_in.ToolTip = @"Zieldateiname
Datum/Zeit kann in '{}' angegeben werden. Formatierung möglich mit:
'yy' => Jahr, zweistellig
'yyyy' => Jahr, vierstellig
'MM' => Monat, zweistellig
'dd' => Tag, zweistellig
'HH' => Stunden im 24h Format
'mm' => Minuten
'ss' => Sekunden";
            text_format_out.ToolTip = @"Beispiel für einen Dateinamen";
            button_settings.ToolTip = @"Einstellungen";
            data_list_out.ToolTip = @"Liste aller Dateien und deren neuer Name";

            FileSettings = new SettingData();
            FileSettings.Dateiformate.Add(new SettingData.Dateiformat { Format = ".png" });
            FileSettings.Dateiformate.Add(new SettingData.Dateiformat { Format = ".jpg" });
            FileSettings.Dateiformate.Add(new SettingData.Dateiformat { Format = ".jpeg" });
            FileSettings.ReadDataFromDisk();
        }

        private void Button_settings_Click(object sender, RoutedEventArgs e)
        {
            SettingData tmpdata = (SettingData)FileSettings.Clone();
            var settwin = new Settinger(tmpdata);

            settwin.ShowDialog();
            if (settwin.Data.Submit)
            {
                if (FileSettings != settwin.Data)
                {
                    FileSettings = settwin.Data;
                    FileSettings.SaveDataToDisk();
                }
            }

            //System.Windows.MessageBox.Show($"Current Data:\nHardRun:{FileSettings.HardRun}");
        }

        private void FormatButton_Click(object sender, RoutedEventArgs e)
        {
            text_format_in.Text = Default_FileString;
        }
        private void FormatBoxIn_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime sampleDate = DateTime.Now;
            // "2023:09:15 18:29:58";
            string format = text_format_in.Text;
            text_format_out.Text = ParseDateFormat(format, sampleDate);
            FormatString = text_format_in.Text;
        }

        private void Runbutton_Click(object sender, RoutedEventArgs e)
        {
            if (!path_ok) { return; }

            new Thread(Rename_Mainloop).Start();
            return;
        }

        private void Dirpathbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.IO.Directory.Exists(text_dirpath.Text))
            {
                text_checks.Text = "✔";
                DirPath = text_dirpath.Text;
                path_ok = true;
                button_run.IsEnabled = true;
            }
            else
            {
                text_checks.Text = "❌";
                path_ok = false;
                button_run.IsEnabled = false;
            }
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = text_dirpath.Text;
            var succ = dialog.ShowDialog();
            if (succ == System.Windows.Forms.DialogResult.OK)
            {
                string DirPath = dialog.SelectedPath;
                text_dirpath.Text = dialog.SelectedPath;
                //checkbox.Text = "✔";
                // TODO error anzeigen
            }
            else
            {
                //checkbox.Text = "❌";
            }

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

        private string ParseDateFormat(string format, DateTime date)
        {
            Regex regex = new Regex(@"\{(.*?)\}");

            string parsed = regex.Replace(format, match =>
            {
                string placeholder = match.Groups[1].Value;
                //string placeholder = match.Value;

                try
                {
                    return date.ToString(placeholder);
                }
                catch (FormatException)
                {
                    return placeholder;
                }
            });

            return parsed;
        }

        private string SanitizeFileName(string input)
        {
            // Entferne ungültige Zeichen für Dateinamen
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
            {
                input = input.Replace(c.ToString(), "_");
            }
            return input;
        }

        private void Rename_Mainloop()
        {
            if (!path_ok) { return; }

            DirectoryInfo dirinfo;
            try
            {
                dirinfo = new DirectoryInfo(DirPath);
            }
            catch (Exception exept)
            {
                System.Windows.MessageBox.Show(
                    "Fehler beim Lesen des Ordners:\n" + exept.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var many = dirinfo.GetFiles();

            var extensions = FileSettings.Dateiformate.ConvertAll(x => x.Format.ToLower());
            many = many.ToList().FindAll(x =>
            {
                return extensions.Contains(x.Extension.ToLower());
            }).ToArray();

            Dispatcher.Invoke(() =>
            {
                bar_progress.Minimum = 0;
                bar_progress.Maximum = many.Length;
                bar_progress.Value = 0;
            });

            List<FileChangeData> files = new List<FileChangeData>();
            List<string> outlines = new List<string>();
            foreach (var file in many)
            {
                var fcd = new FileChangeData()
                {
                    Original = file.FullName,
                    OrigFilename = file.Name,
                };
                //fcd.Date = fcd.GetDate();
                fcd.ExtractDate();

                if (string.IsNullOrEmpty(fcd.Date))
                {
                    // No Date could be extracted
                    fcd.New = Path.GetFileNameWithoutExtension(fcd.Original);
                    fcd.State = "❔";
                }
                else
                {
                    DateTime tmp_date;
                    if (DateTime.TryParse(fcd.Date, out tmp_date))
                    {
                        fcd.New = ParseDateFormat(FormatString, tmp_date);
                        fcd.State = "☑";
                    }
                    else if(DateTime.TryParseExact(fcd.Date, "yyyy:MM:dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out tmp_date))
                    {
                        fcd.New = ParseDateFormat(FormatString, tmp_date);
                        fcd.State = "☑";
                    }
                    else
                    {
                        // No Date could be extracted
                        fcd.New = Path.GetFileNameWithoutExtension(fcd.Original);
                        fcd.State = "❔";
                    }
                }

                files.Add(fcd);

                Dispatcher.Invoke(() =>
                {
                    bar_progress.Value += 1;
                });
            }

            var same_names = files.GroupBy(i => i.New);
            foreach (var grp in same_names)
            {
                if (string.IsNullOrEmpty(grp.Key))
                {
                    continue;
                }
                if (grp.Count() == 1)
                {
                    //foreach(var nomnom in files.FindAll(i => i.New == grp.Key))
                    foreach (var filecd in grp)
                    {
                        filecd.Duplicate = -1;
                    }
                }
                else
                {
                    int count_up = 0;
                    string padding_string = new string('0', grp.Count().ToString().Length);
                    //foreach(var nomnom in files.FindAll(i=> i.New == grp.Key))
                    foreach (var nomnom in grp)
                    {
                        nomnom.Duplicate = count_up;
                        nomnom.New += "_" + (count_up + 1).ToString(padding_string);
                        nomnom.State = "🔢";
                        count_up++;
                    }
                }
            }

            foreach (var fcd in files)
            {
                if (!string.IsNullOrEmpty(fcd.New))
                {
                    // Got new Name (likely with date). Add Original Extension
                    // CAUTION: Also for not changed files, extension was just yanked
                    fcd.New += Path.GetExtension(fcd.Original);
                }

                // Now do the actual rename
                if (FileSettings.HardRun)
                {
                    string newfilename = Path.Combine(Path.GetDirectoryName(fcd.Original), fcd.New);
                    File.Move(fcd.Original, newfilename);
                }
            }

            Dispatcher.Invoke(() =>
            {
                data_list_out.ItemsSource = files;
                //data_list_out.FontFamily = Fonts.SystemFontFamilies.ToList().Find(i => i.FamilyNames.Values.Contains("Courier New"));
            });
        }
    }

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
                var dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTime);
                if (dateTime != string.Empty)
                {
                    dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
                    //ExifDirectoryBase.//TagDateTimeOriginal
                }
                return dateTime;
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
