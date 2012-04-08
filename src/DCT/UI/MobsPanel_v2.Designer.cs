namespace DCT.UI
{
    partial class MobsPanel_v2
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lnkMobsSelect = new System.Windows.Forms.LinkLabel();
            this.btnMobRage = new System.Windows.Forms.Button();
            this.lblMobRage = new System.Windows.Forms.Label();
            this.btnPotionMobsSelect = new System.Windows.Forms.Button();
            this.cmbPotionMobs = new System.Windows.Forms.ComboBox();
            this.lnkUncheckMobs = new System.Windows.Forms.LinkLabel();
            this.btnMobGo = new System.Windows.Forms.Button();
            this.dgvMobs = new System.Windows.Forms.DataGridView();
            this.CheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LevelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMobs)).BeginInit();
            this.SuspendLayout();
            // 
            // lnkMobsSelect
            // 
            this.lnkMobsSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkMobsSelect.AutoSize = true;
            this.lnkMobsSelect.Location = new System.Drawing.Point(2, 258);
            this.lnkMobsSelect.Name = "lnkMobsSelect";
            this.lnkMobsSelect.Size = new System.Drawing.Size(117, 13);
            this.lnkMobsSelect.TabIndex = 16;
            this.lnkMobsSelect.TabStop = true;
            this.lnkMobsSelect.Text = "Select mobs by name...";
            this.lnkMobsSelect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMobsSelect_LinkClicked);
            // 
            // btnMobRage
            // 
            this.btnMobRage.Location = new System.Drawing.Point(131, 2);
            this.btnMobRage.Name = "btnMobRage";
            this.btnMobRage.Size = new System.Drawing.Size(75, 23);
            this.btnMobRage.TabIndex = 15;
            this.btnMobRage.Text = "Recalculate";
            this.btnMobRage.UseVisualStyleBackColor = true;
            this.btnMobRage.Click += new System.EventHandler(this.btnMobRage_Click);
            // 
            // lblMobRage
            // 
            this.lblMobRage.AutoSize = true;
            this.lblMobRage.Location = new System.Drawing.Point(7, 7);
            this.lblMobRage.Name = "lblMobRage";
            this.lblMobRage.Size = new System.Drawing.Size(81, 13);
            this.lblMobRage.TabIndex = 14;
            this.lblMobRage.Text = "Rage per run: ?";
            // 
            // btnPotionMobsSelect
            // 
            this.btnPotionMobsSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPotionMobsSelect.Location = new System.Drawing.Point(365, 244);
            this.btnPotionMobsSelect.Name = "btnPotionMobsSelect";
            this.btnPotionMobsSelect.Size = new System.Drawing.Size(54, 22);
            this.btnPotionMobsSelect.TabIndex = 13;
            this.btnPotionMobsSelect.Text = "Select";
            this.btnPotionMobsSelect.UseVisualStyleBackColor = true;
            this.btnPotionMobsSelect.Click += new System.EventHandler(this.btnPotionMobsSelect_Click);
            // 
            // cmbPotionMobs
            // 
            this.cmbPotionMobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbPotionMobs.Items.AddRange(new object[] {
            "Kix",
            "Kinetic",
            "Fire",
            "Holy",
            "Shadow",
            "Arcane"});
            this.cmbPotionMobs.Location = new System.Drawing.Point(244, 244);
            this.cmbPotionMobs.Name = "cmbPotionMobs";
            this.cmbPotionMobs.Size = new System.Drawing.Size(115, 21);
            this.cmbPotionMobs.TabIndex = 12;
            this.cmbPotionMobs.Text = "Choose a potion...";
            // 
            // lnkUncheckMobs
            // 
            this.lnkUncheckMobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkUncheckMobs.AutoSize = true;
            this.lnkUncheckMobs.Location = new System.Drawing.Point(2, 241);
            this.lnkUncheckMobs.Name = "lnkUncheckMobs";
            this.lnkUncheckMobs.Size = new System.Drawing.Size(65, 13);
            this.lnkUncheckMobs.TabIndex = 10;
            this.lnkUncheckMobs.TabStop = true;
            this.lnkUncheckMobs.Text = "Uncheck All";
            this.lnkUncheckMobs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUncheckMobs_LinkClicked);
            // 
            // btnMobGo
            // 
            this.btnMobGo.AutoEllipsis = true;
            this.btnMobGo.Location = new System.Drawing.Point(266, 3);
            this.btnMobGo.Name = "btnMobGo";
            this.btnMobGo.Size = new System.Drawing.Size(153, 23);
            this.btnMobGo.TabIndex = 18;
            this.btnMobGo.Text = "Go to selection";
            this.btnMobGo.UseVisualStyleBackColor = true;
            this.btnMobGo.Click += new System.EventHandler(this.btnMobGo_Click);
            // 
            // dgvMobs
            // 
            this.dgvMobs.AllowUserToAddRows = false;
            this.dgvMobs.AllowUserToDeleteRows = false;
            this.dgvMobs.AllowUserToOrderColumns = true;
            this.dgvMobs.AllowUserToResizeRows = false;
            this.dgvMobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMobs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckBoxColumn,
            this.NameColumn,
            this.CountColumn,
            this.LevelColumn,
            this.RageColumn});
            this.dgvMobs.Location = new System.Drawing.Point(10, 56);
            this.dgvMobs.Name = "dgvMobs";
            this.dgvMobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMobs.Size = new System.Drawing.Size(409, 182);
            this.dgvMobs.TabIndex = 19;
            this.dgvMobs.CurrentCellChanged += new System.EventHandler(this.dgvMobs_CurrentCellChanged);
            this.dgvMobs.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvMobs_SortCompare);
            this.dgvMobs.Leave += new System.EventHandler(this.dgvMobs_Leave);
            // 
            // CheckBoxColumn
            // 
            this.CheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CheckBoxColumn.HeaderText = "";
            this.CheckBoxColumn.Name = "CheckBoxColumn";
            this.CheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CheckBoxColumn.Width = 25;
            // 
            // NameColumn
            // 
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Width = 180;
            // 
            // CountColumn
            // 
            this.CountColumn.HeaderText = "Count";
            this.CountColumn.Name = "CountColumn";
            this.CountColumn.ReadOnly = true;
            this.CountColumn.Width = 50;
            // 
            // LevelColumn
            // 
            this.LevelColumn.HeaderText = "Level";
            this.LevelColumn.Name = "LevelColumn";
            this.LevelColumn.ReadOnly = true;
            this.LevelColumn.Width = 50;
            // 
            // RageColumn
            // 
            this.RageColumn.HeaderText = "Rage";
            this.RageColumn.Name = "RageColumn";
            this.RageColumn.ReadOnly = true;
            this.RageColumn.Width = 50;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(196, 20);
            this.textBox1.TabIndex = 20;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // MobsPanel_v2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgvMobs);
            this.Controls.Add(this.btnMobGo);
            this.Controls.Add(this.lnkMobsSelect);
            this.Controls.Add(this.btnMobRage);
            this.Controls.Add(this.lblMobRage);
            this.Controls.Add(this.btnPotionMobsSelect);
            this.Controls.Add(this.cmbPotionMobs);
            this.Controls.Add(this.lnkUncheckMobs);
            this.Name = "MobsPanel_v2";
            this.Size = new System.Drawing.Size(426, 278);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMobs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.LinkLabel lnkMobSave;
        private System.Windows.Forms.LinkLabel lnkMobsSelect;
        private System.Windows.Forms.Button btnMobRage;
        private System.Windows.Forms.Label lblMobRage;
        private System.Windows.Forms.Button btnPotionMobsSelect;
        private System.Windows.Forms.ComboBox cmbPotionMobs;
        //private System.Windows.Forms.LinkLabel lnkMobLoad;
        private System.Windows.Forms.LinkLabel lnkUncheckMobs;
        private System.Windows.Forms.Button btnMobGo;
        private System.Windows.Forms.DataGridView dgvMobs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LevelColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RageColumn;
        private System.Windows.Forms.TextBox textBox1;
    }
}
