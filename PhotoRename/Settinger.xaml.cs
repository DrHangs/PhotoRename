using System.Windows;

namespace PhotoRename
{
    /// <summary>
    /// Interaktionslogik für Settinger.xaml
    /// </summary>
    public partial class Settinger : Window
    {
        public SettingHandler Data { get; set; }

        public Settinger(SettingHandler data)
        {
            InitializeComponent();

            Data = data;
            Data.Submit = false;
            DataContext = Data;

            dataformats.CanUserAddRows = true;
            //dataformats.ItemsSource = Data.FormateBinding;
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
            Data.SaveBack();
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Data.Submit = false;
            Close();
        }
    }
}
