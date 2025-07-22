using System.Windows;

namespace PicRenameGUI
{
    /// <summary>
    /// Interaktionslogik für Settinger.xaml
    /// </summary>
    public partial class Settinger : Window
    {
        public SettingData Data { get; set; }

        public Settinger(SettingData data)
        {
            InitializeComponent();

            Data = data;
            Data.Submit = false;
            DataContext = Data;

            dataformats.CanUserAddRows = true;
            dataformats.ItemsSource = Data.Dateiformate;
            //formatlist.ItemsSource = Data.Dateiformate;



            cancel.Click += Cancel_Click;
            submit.Click += Submit_Click;

            dataformats.ToolTip = @"Dateierweiterungen, welche umbenannt werden sollen.
Erweiterungen, die nicht in dieser Liste sind, werden ignoriert.";
            Hardrun.ToolTip = @"Wenn deaktiviert, zeigt nur die Dateien an und deren neuer Name.
Dateien werden dann nicht umbenannt!";
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Data.Submit = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Data.Submit = false;
            Close();
        }
    }
}
