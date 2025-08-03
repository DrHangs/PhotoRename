using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;

namespace PhotoRename
{
    public class SettingHandler : INotifyPropertyChanged
    {
        private static readonly string configPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "PhotoRename",
            "appsettings.json"
        );

        public Settings Data { get; set; } = new();
        public bool HardRunBinding
        {
            get => Data.HardRun;
            set
            {
                if (Data.HardRun != value)
                {
                    Data.HardRun = value;
                    OnPropertyChanged(nameof(HardRunBinding));
                }
            }
        }

        public ObservableCollection<Dateiformat> FormateBinding { get; internal set; }

        public bool Submit { get; set; } = false;

        public SettingHandler(Settings data)
        {
            Data = data;
            FormateBinding = new ObservableCollection<Dateiformat>(
                Data.Formate.Select(s => new Dateiformat { Format = s })
            );
        }

        public void Load()
        {
            if (File.Exists(configPath))
            {
                string json = File.ReadAllText(configPath);
                Data = JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
            }
        }

        public void Save()
        {
            string dir = Path.GetDirectoryName(configPath)!;
            Directory.CreateDirectory(dir);

            string json = JsonSerializer.Serialize(Data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
        }

        // All the stuff for the HardRun Binding
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void SaveBack()
        {
            Data.Formate = FormateBinding
                .Select(item => item.Format)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
        }

        // WrapperClass for Binding string value to DataGrid
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

        // DataClass for the Settings. Intentionally kept simple so it can be serialized to JSON
        public class Settings : IEquatable<Settings?>, ICloneable
        {
            public bool HardRun { get; set; } = true;
            public List<string> Formate { get; set; } = new();

            public static readonly Settings Default = new()
            {
                HardRun = false,
                Formate = [".png", ".jpg", ".jpeg"]
            };

            public object Clone()
            {
                Settings tmp = new()
                {
                    HardRun = this.HardRun
                };
                tmp.Formate.Clear();
                Formate.ForEach(x => { tmp.Formate.Add(x); });
                return tmp;
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as Settings);
            }

            public bool Equals(Settings? other)
            {
                return other is not null &&
                       HardRun == other.HardRun &&
                       EqualityComparer<List<string>>.Default.Equals(Formate, other.Formate);
            }

            public static bool operator ==(Settings? left, Settings? right)
            {
                return EqualityComparer<Settings>.Default.Equals(left, right);
            }

            public static bool operator !=(Settings? left, Settings? right)
            {
                return !(left == right);
            }
        }
    }
}
