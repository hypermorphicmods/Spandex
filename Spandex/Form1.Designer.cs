namespace Spandex
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openbutton = new System.Windows.Forms.Button();
            this.savebutton = new System.Windows.Forms.Button();
            this.stringGrid = new System.Windows.Forms.DataGridView();
            this.IDdisplay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InternalValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TemplateValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stringGridEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.valueGrid = new System.Windows.Forms.DataGridView();
            this.iDdisplayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.templateValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueGridBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.removeUndefInts = new System.Windows.Forms.CheckBox();
            this.removeUndefFloats = new System.Windows.Forms.CheckBox();
            this.removeUndefTextures = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.stringGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stringGridEntryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueGridBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openbutton
            // 
            this.openbutton.Location = new System.Drawing.Point(16, 16);
            this.openbutton.Name = "openbutton";
            this.openbutton.Size = new System.Drawing.Size(216, 48);
            this.openbutton.TabIndex = 0;
            this.openbutton.Text = "Load Material...";
            this.openbutton.UseVisualStyleBackColor = true;
            this.openbutton.Click += new System.EventHandler(this.openbutton_Click);
            // 
            // savebutton
            // 
            this.savebutton.Enabled = false;
            this.savebutton.Location = new System.Drawing.Point(16, 88);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(216, 48);
            this.savebutton.TabIndex = 0;
            this.savebutton.Text = "Save Material As...";
            this.savebutton.UseVisualStyleBackColor = true;
            this.savebutton.Click += new System.EventHandler(this.savebutton_Click);
            // 
            // stringGrid
            // 
            this.stringGrid.AllowUserToAddRows = false;
            this.stringGrid.AllowUserToDeleteRows = false;
            this.stringGrid.AllowUserToOrderColumns = true;
            this.stringGrid.AllowUserToResizeRows = false;
            this.stringGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stringGrid.AutoGenerateColumns = false;
            this.stringGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stringGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDdisplay,
            this.InternalValue,
            this.Value,
            this.TemplateValue});
            this.stringGrid.DataSource = this.stringGridEntryBindingSource;
            this.stringGrid.Location = new System.Drawing.Point(16, 48);
            this.stringGrid.Margin = new System.Windows.Forms.Padding(16);
            this.stringGrid.MinimumSize = new System.Drawing.Size(1090, 238);
            this.stringGrid.Name = "stringGrid";
            this.stringGrid.RowHeadersVisible = false;
            this.stringGrid.RowHeadersWidth = 72;
            this.stringGrid.RowTemplate.Height = 37;
            this.stringGrid.Size = new System.Drawing.Size(1102, 244);
            this.stringGrid.TabIndex = 1;
            this.stringGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.stringGrid_CellBeginEdit);
            this.stringGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.stringGrid_CellValueChanged);
            this.stringGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.stringGrid_DataBindingComplete);
            // 
            // IDdisplay
            // 
            this.IDdisplay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.IDdisplay.DataPropertyName = "IDdisplay";
            this.IDdisplay.HeaderText = "ID";
            this.IDdisplay.MinimumWidth = 9;
            this.IDdisplay.Name = "IDdisplay";
            this.IDdisplay.ReadOnly = true;
            this.IDdisplay.Width = 75;
            // 
            // InternalValue
            // 
            this.InternalValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.InternalValue.DataPropertyName = "InternalValue";
            this.InternalValue.HeaderText = "Internal Shader";
            this.InternalValue.MinimumWidth = 9;
            this.InternalValue.Name = "InternalValue";
            this.InternalValue.Width = 179;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "External Template Overrides";
            this.Value.MinimumWidth = 9;
            this.Value.Name = "Value";
            this.Value.Width = 284;
            // 
            // TemplateValue
            // 
            this.TemplateValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TemplateValue.DataPropertyName = "TemplateValue";
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.TemplateValue.DefaultCellStyle = dataGridViewCellStyle1;
            this.TemplateValue.HeaderText = "External Template Defaults";
            this.TemplateValue.MinimumWidth = 9;
            this.TemplateValue.Name = "TemplateValue";
            this.TemplateValue.ReadOnly = true;
            this.TemplateValue.Width = 275;
            // 
            // stringGridEntryBindingSource
            // 
            this.stringGridEntryBindingSource.DataSource = typeof(Spandex.GridEntry);
            // 
            // valueGrid
            // 
            this.valueGrid.AllowUserToAddRows = false;
            this.valueGrid.AllowUserToDeleteRows = false;
            this.valueGrid.AllowUserToResizeRows = false;
            this.valueGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueGrid.AutoGenerateColumns = false;
            this.valueGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.valueGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.valueGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDdisplayDataGridViewTextBoxColumn,
            this.Type,
            this.dataGridViewTextBoxColumn1,
            this.valueDataGridViewTextBoxColumn,
            this.templateValueDataGridViewTextBoxColumn});
            this.valueGrid.DataSource = this.valueGridBindingSource;
            this.valueGrid.Location = new System.Drawing.Point(16, 48);
            this.valueGrid.Margin = new System.Windows.Forms.Padding(16);
            this.valueGrid.MinimumSize = new System.Drawing.Size(580, 231);
            this.valueGrid.Name = "valueGrid";
            this.valueGrid.RowHeadersVisible = false;
            this.valueGrid.RowHeadersWidth = 72;
            this.valueGrid.RowTemplate.Height = 37;
            this.valueGrid.Size = new System.Drawing.Size(610, 237);
            this.valueGrid.TabIndex = 3;
            this.valueGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.valueGrid_CellPainting);
            this.valueGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.valueGrid_DataBindingComplete);
            // 
            // iDdisplayDataGridViewTextBoxColumn
            // 
            this.iDdisplayDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.iDdisplayDataGridViewTextBoxColumn.DataPropertyName = "IDdisplay";
            this.iDdisplayDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDdisplayDataGridViewTextBoxColumn.MinimumWidth = 9;
            this.iDdisplayDataGridViewTextBoxColumn.Name = "iDdisplayDataGridViewTextBoxColumn";
            this.iDdisplayDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDdisplayDataGridViewTextBoxColumn.Width = 75;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.MinimumWidth = 9;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 97;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "InternalValue";
            this.dataGridViewTextBoxColumn1.HeaderText = "Internal Shader";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 179;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "External Template Override";
            this.valueDataGridViewTextBoxColumn.MinimumWidth = 9;
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.Width = 276;
            // 
            // templateValueDataGridViewTextBoxColumn
            // 
            this.templateValueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.templateValueDataGridViewTextBoxColumn.DataPropertyName = "TemplateValue";
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.templateValueDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.templateValueDataGridViewTextBoxColumn.HeaderText = "External Template Default";
            this.templateValueDataGridViewTextBoxColumn.MinimumWidth = 9;
            this.templateValueDataGridViewTextBoxColumn.Name = "templateValueDataGridViewTextBoxColumn";
            this.templateValueDataGridViewTextBoxColumn.ReadOnly = true;
            this.templateValueDataGridViewTextBoxColumn.Width = 266;
            // 
            // valueGridBindingSource
            // 
            this.valueGridBindingSource.DataSource = typeof(Spandex.GridEntry);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1142, 623);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.valueGrid);
            this.panel2.Location = new System.Drawing.Point(495, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(644, 305);
            this.panel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(234, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Numeric inputs";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.removeUndefInts);
            this.panel1.Controls.Add(this.removeUndefFloats);
            this.panel1.Controls.Add(this.removeUndefTextures);
            this.panel1.Controls.Add(this.openbutton);
            this.panel1.Controls.Add(this.savebutton);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.MinimumSize = new System.Drawing.Size(486, 300);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(486, 305);
            this.panel1.TabIndex = 4;
            // 
            // removeUndefInts
            // 
            this.removeUndefInts.AutoSize = true;
            this.removeUndefInts.Location = new System.Drawing.Point(17, 243);
            this.removeUndefInts.Name = "removeUndefInts";
            this.removeUndefInts.Size = new System.Drawing.Size(422, 34);
            this.removeUndefInts.TabIndex = 3;
            this.removeUndefInts.Text = "Remove undefined (red) integer overrides";
            this.removeUndefInts.UseVisualStyleBackColor = true;
            // 
            // removeUndefFloats
            // 
            this.removeUndefFloats.AutoSize = true;
            this.removeUndefFloats.Location = new System.Drawing.Point(17, 203);
            this.removeUndefFloats.Name = "removeUndefFloats";
            this.removeUndefFloats.Size = new System.Drawing.Size(399, 34);
            this.removeUndefFloats.TabIndex = 3;
            this.removeUndefFloats.Text = "Remove undefined (red) float overrides";
            this.removeUndefFloats.UseVisualStyleBackColor = true;
            // 
            // removeUndefTextures
            // 
            this.removeUndefTextures.AutoSize = true;
            this.removeUndefTextures.Location = new System.Drawing.Point(17, 163);
            this.removeUndefTextures.Name = "removeUndefTextures";
            this.removeUndefTextures.Size = new System.Drawing.Size(422, 34);
            this.removeUndefTextures.TabIndex = 3;
            this.removeUndefTextures.Text = "Remove undefined (red) texture overrides";
            this.removeUndefTextures.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 2);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.stringGrid);
            this.panel3.Location = new System.Drawing.Point(3, 314);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1136, 306);
            this.panel3.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(464, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "Texture inputs";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 638);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1166, 37);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 28);
            // 
            // statusLabel
            // 
            this.statusLabel.Image = global::Spandex.Properties.Resources.ok;
            this.statusLabel.Margin = new System.Windows.Forms.Padding(8, 5, 0, 4);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.statusLabel.Size = new System.Drawing.Size(38, 28);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1166, 675);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1190, 739);
            this.Name = "Form1";
            this.Text = "Spandex";
            ((System.ComponentModel.ISupportInitialize)(this.stringGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stringGridEntryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueGridBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button openbutton;
        private Button savebutton;
        private DataGridView stringGrid;
        private BindingSource bindingSource1;
        private DataGridViewTextBoxColumn sourceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn assetPathDataGridViewTextBoxColumn;
        private BindingSource bindingSource2;
        private BindingSource stringGridEntryBindingSource;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridView valueGrid;
        private BindingSource valueGridBindingSource;
        private DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn templateTypeDataGridViewTextBoxColumn;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private DataGridViewTextBoxColumn IDdisplay;
        private DataGridViewTextBoxColumn InternalValue;
        private DataGridViewTextBoxColumn Value;
        private DataGridViewTextBoxColumn TemplateValue;
        private DataGridViewTextBoxColumn iDdisplayDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn Type;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn templateValueDataGridViewTextBoxColumn;
        private CheckBox removeUndefInts;
        private CheckBox removeUndefFloats;
        private CheckBox removeUndefTextures;
        private Label label2;
        private Label label3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel statusLabel;
    }
}
