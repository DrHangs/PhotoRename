using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace PicRenameGUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            select.Click += Select_Click;
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            var DirOpenDialog = new OpenFileDialog()
            {
                CheckFileExists = false,
                
                //InitialDirectory = Environment.CurrentDirectory,

            };
            if (DirOpenDialog.ShowDialog() == true)
            {
                string path = DirOpenDialog.FileName;
                dirpath.Text = path;
                if (Directory.Exists(Path.GetDirectoryName(path)))
                {
                    checks.Text = "✔";
                }
                else
                {
                    checks.Text = "❌";
                }
            }
        }
    }
}
