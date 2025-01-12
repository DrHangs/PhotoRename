namespace PicRenameForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dirpathbox = new TextBox();
            selectbutton = new Button();
            progressBar = new ProgressBar();
            runbutton = new Button();
            checkbox = new Label();
            listView = new TextBox();
            SuspendLayout();
            // 
            // dirpathbox
            // 
            dirpathbox.Location = new Point(17, 16);
            dirpathbox.Name = "dirpathbox";
            dirpathbox.Size = new Size(488, 31);
            dirpathbox.TabIndex = 0;
            // 
            // selectbutton
            // 
            selectbutton.Location = new Point(511, 14);
            selectbutton.Name = "selectbutton";
            selectbutton.Size = new Size(112, 34);
            selectbutton.TabIndex = 1;
            selectbutton.Text = "Auswählen";
            selectbutton.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(17, 95);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(649, 34);
            progressBar.TabIndex = 3;
            // 
            // runbutton
            // 
            runbutton.Location = new Point(17, 55);
            runbutton.Name = "runbutton";
            runbutton.Size = new Size(649, 34);
            runbutton.TabIndex = 5;
            runbutton.Text = "Start!";
            runbutton.UseVisualStyleBackColor = true;
            // 
            // checkbox
            // 
            checkbox.AutoSize = true;
            checkbox.Location = new Point(629, 20);
            checkbox.Name = "checkbox";
            checkbox.Size = new Size(37, 25);
            checkbox.TabIndex = 6;
            checkbox.Text = "🔄";
            checkbox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listView
            // 
            listView.Location = new Point(17, 135);
            listView.Multiline = true;
            listView.Name = "listView";
            listView.ReadOnly = true;
            listView.ScrollBars = ScrollBars.Both;
            listView.Size = new Size(649, 297);
            listView.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(678, 444);
            Controls.Add(listView);
            Controls.Add(checkbox);
            Controls.Add(runbutton);
            Controls.Add(progressBar);
            Controls.Add(selectbutton);
            Controls.Add(dirpathbox);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox dirpathbox;
        private Button selectbutton;
        private ProgressBar progressBar;
        private Button runbutton;
        private Label checkbox;
        private TextBox listView;
    }
}