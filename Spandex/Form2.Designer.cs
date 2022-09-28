namespace Spandex
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.searchBox = new System.Windows.Forms.TextBox();
            this.resultlist = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(38, 42);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(917, 35);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // resultlist
            // 
            this.resultlist.FormattingEnabled = true;
            this.resultlist.ItemHeight = 30;
            this.resultlist.Location = new System.Drawing.Point(38, 118);
            this.resultlist.Name = "resultlist";
            this.resultlist.Size = new System.Drawing.Size(915, 304);
            this.resultlist.TabIndex = 1;
            this.resultlist.SelectedIndexChanged += new System.EventHandler(this.resultlist_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(391, 440);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 51);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 514);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.resultlist);
            this.Controls.Add(this.searchBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Asset search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox searchBox;
        private ListBox resultlist;
        private Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}