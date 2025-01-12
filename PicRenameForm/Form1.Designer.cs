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
            formatDefaultButton = new Button();
            formatBoxIn = new TextBox();
            formatBoxOut = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // dirpathbox
            // 
            dirpathbox.Location = new Point(74, 13);
            dirpathbox.Margin = new Padding(2);
            dirpathbox.Name = "dirpathbox";
            dirpathbox.Size = new Size(331, 27);
            dirpathbox.TabIndex = 0;
            // 
            // selectbutton
            // 
            selectbutton.Location = new Point(409, 13);
            selectbutton.Margin = new Padding(2);
            selectbutton.Name = "selectbutton";
            selectbutton.Size = new Size(90, 27);
            selectbutton.TabIndex = 1;
            selectbutton.Text = "Auswählen";
            selectbutton.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(14, 106);
            progressBar.Margin = new Padding(2);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(519, 27);
            progressBar.TabIndex = 3;
            // 
            // runbutton
            // 
            runbutton.Location = new Point(14, 75);
            runbutton.Margin = new Padding(2);
            runbutton.Name = "runbutton";
            runbutton.Size = new Size(519, 27);
            runbutton.TabIndex = 5;
            runbutton.Text = "Start!";
            runbutton.UseVisualStyleBackColor = true;
            // 
            // checkbox
            // 
            checkbox.AutoSize = true;
            checkbox.Location = new Point(503, 16);
            checkbox.Margin = new Padding(2, 0, 2, 0);
            checkbox.Name = "checkbox";
            checkbox.Size = new Size(30, 20);
            checkbox.TabIndex = 6;
            checkbox.Text = "🔄";
            checkbox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listView
            // 
            listView.Location = new Point(14, 137);
            listView.Margin = new Padding(2);
            listView.Multiline = true;
            listView.Name = "listView";
            listView.ReadOnly = true;
            listView.ScrollBars = ScrollBars.Both;
            listView.Size = new Size(520, 209);
            listView.TabIndex = 7;
            // 
            // formatDefaultButton
            // 
            formatDefaultButton.Location = new Point(409, 44);
            formatDefaultButton.Margin = new Padding(2);
            formatDefaultButton.Name = "formatDefaultButton";
            formatDefaultButton.Size = new Size(90, 27);
            formatDefaultButton.TabIndex = 8;
            formatDefaultButton.Text = "Default";
            formatDefaultButton.UseVisualStyleBackColor = true;
            // 
            // formatBoxIn
            // 
            formatBoxIn.Location = new Point(75, 44);
            formatBoxIn.Margin = new Padding(2);
            formatBoxIn.Name = "formatBoxIn";
            formatBoxIn.Size = new Size(146, 27);
            formatBoxIn.TabIndex = 9;
            // 
            // formatBoxOut
            // 
            formatBoxOut.Location = new Point(258, 44);
            formatBoxOut.Margin = new Padding(2);
            formatBoxOut.Name = "formatBoxOut";
            formatBoxOut.ReadOnly = true;
            formatBoxOut.Size = new Size(146, 27);
            formatBoxOut.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(225, 47);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(30, 20);
            label1.TabIndex = 11;
            label1.Text = "➡";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 16);
            label2.Name = "label2";
            label2.Size = new Size(55, 20);
            label2.TabIndex = 12;
            label2.Text = "Ordner";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 47);
            label3.Name = "label3";
            label3.Size = new Size(56, 20);
            label3.TabIndex = 13;
            label3.Text = "Format";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 355);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(formatBoxOut);
            Controls.Add(formatBoxIn);
            Controls.Add(formatDefaultButton);
            Controls.Add(listView);
            Controls.Add(checkbox);
            Controls.Add(runbutton);
            Controls.Add(progressBar);
            Controls.Add(selectbutton);
            Controls.Add(dirpathbox);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
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
        private Button formatDefaultButton;
        private TextBox formatBoxIn;
        private TextBox formatBoxOut;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}