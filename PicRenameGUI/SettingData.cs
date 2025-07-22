using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace PicRenameGUI
{
    public class SettingData : IEquatable<SettingData>, ICloneable, INotifyPropertyChanged
    {
        public List<Dateiformat> Dateiformate { get; } = new List<Dateiformat>();
        public bool HardRun
        {
            get => _isHardRunEnabled;
            set
            {
                if (_isHardRunEnabled != value)
                {
                    _isHardRunEnabled = value;
                    OnPropertyChanged(nameof(HardRun));
                }
            }
        }
        public bool Submit { get; set; } = false;
        private static string SaveFile = "settings.ini";

        public void SaveDataToDisk()
        {
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SaveFile);
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            File.WriteAllLines(filename, Dateiformate.ConvertAll(x => x.Format), Encoding.UTF8);
            FileInfo fileInfo = new FileInfo(filename);
            fileInfo.Attributes |= FileAttributes.Hidden;
            fileInfo.Refresh();
        }

        public void ReadDataFromDisk()
        {
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SaveFile);
            if (!File.Exists(filename))
            {
                return;
            }
            Dateiformate.Clear();
            foreach (string line in File.ReadAllLines(filename).Where(x => !string.IsNullOrEmpty(x)))
            {
                Dateiformate.Add(new Dateiformat { Format = line });
            }
        }

        // All the stuff for the HardRun Binding
        private bool _isHardRunEnabled = true;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Overrides and stuff
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is SettingData sdata)
            {
                return Equals(sdata);
            }
            return false;
        }

        public bool Equals(SettingData other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (Enumerable.SequenceEqual(Dateiformate, other.Dateiformate))
            {
                return other.HardRun == this.HardRun;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return 956025618 + EqualityComparer<List<Dateiformat>>.Default.GetHashCode(Dateiformate);
        }

        public object Clone()
        {
            SettingData tmp = new SettingData();
            Dateiformate.ForEach(x =>
            {
                tmp.Dateiformate.Add(new Dateiformat { Format = x.Format });
            });
            tmp.HardRun = this.HardRun;
            return tmp;
        }

        public static bool operator ==(SettingData left, SettingData right)
        {
            return EqualityComparer<SettingData>.Default.Equals(left, right);
        }

        public static bool operator !=(SettingData left, SettingData right)
        {
            return !(left == right);
        }


        public class Dateiformat : IEquatable<Dateiformat>
        {
            public string Format { get; set; }

            public override string ToString()
            {
                return Format;
            }

            public override bool Equals(object obj)
            {
                if (obj is Dateiformat dformat)
                {
                    return Format == dformat.Format;
                }
                return false;
            }

            public bool Equals(Dateiformat other)
            {
                return !(other is null) &&
                       Format == other.Format;
            }

            public override int GetHashCode()
            {
                return 50578242 + EqualityComparer<string>.Default.GetHashCode(Format);
            }

            public static bool operator ==(Dateiformat left, Dateiformat right)
            {
                return EqualityComparer<Dateiformat>.Default.Equals(left, right);
            }

            public static bool operator !=(Dateiformat left, Dateiformat right)
            {
                return !(left == right);
            }
        }
    }
}
