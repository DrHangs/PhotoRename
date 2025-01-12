namespace PicRenameForm
{
    public partial class Form1 : Form
    {
        string DirPath { get; set; }
        bool path_ok { get; set; }

        public Form1()
        {
            InitializeComponent();


            checkbox.Text = "🔄";
            dirpathbox.TextChanged += Dirpathbox_TextChanged;
            runbutton.Enabled = false;

            listView.ReadOnly = true;
            listView.ScrollBars = ScrollBars.Both;

            selectbutton.Click += DirSelect_Click;
            runbutton.Click += Runbutton_Click;

            progressBar.Style = ProgressBarStyle.Blocks;

        }

        private void Runbutton_Click(object? sender, EventArgs e)
        {
            if (!path_ok) { return; }

            var dirinfo = new DirectoryInfo(DirPath);

            var many = dirinfo.GetFiles();

            //List<FileChangeData> files = new List<FileChangeData>();
            //files.Add(new FileChangeData()
            //var file = new FileChangeData()
            //{
            //    Original = many[0].FullName,
            //};
            //file.Date = file.GetDate();

            //var ll = listView.Lines.ToList();
            //ll.Add($"{Path.GetFileName(file.Original)} -> {file.Date}");
            //listView.Lines = ll.ToArray();
            //listView.ScrollToCaret();


            //listView.AppendText($"\n{Path.GetFileName(file.Original)} -> {file.Date}");
            //listView.Text = $"{Path.GetFileName(file.Original)} -> {file.Date}\n";

            progressBar.Minimum = 0;
            progressBar.Maximum = many.Length;
            progressBar.Step = 1;
            List<FileChangeData> files = new List<FileChangeData>();
            List<string> outlines = new List<string>();
            foreach ( var file in many )
            {
                var fcd = new FileChangeData()
                {
                    Original = file.FullName
                };
                fcd.Date = fcd.GetDate();
                files.Add(fcd);
                outlines.Add($"{Path.GetFileName(fcd.Original)} -> {fcd.Date}");
                progressBar.PerformStep();
            }

            listView.Lines = outlines.ToArray();
        }

        private void Dirpathbox_TextChanged(object? sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(dirpathbox.Text))
            {
                checkbox.Text = "✔";
                DirPath = dirpathbox.Text;
                path_ok = true;
                runbutton.Enabled = true;
            }
            else
            {
                checkbox.Text = "❌";
                path_ok = false;
                runbutton.Enabled = false;
            }
        }

        private void DirSelect_Click(object? sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            var succ = dialog.ShowDialog();
            if (succ == DialogResult.OK)
            {
                DirPath = dialog.SelectedPath;
                dirpathbox.Text = dialog.SelectedPath;
                //checkbox.Text = "✔";
                // TODO error anzeigen
            }
            else
            {
                //checkbox.Text = "❌";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}