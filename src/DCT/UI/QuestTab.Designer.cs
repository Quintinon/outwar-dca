namespace DCT.UI
{
    partial class QuestTab
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
            this.dgvQuests = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.clmCheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmQuestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCompleted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvQuests
            // 
            this.dgvQuests.AllowUserToAddRows = false;
            this.dgvQuests.AllowUserToDeleteRows = false;
            this.dgvQuests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCheckBox,
            this.clmQuestName,
            this.clmCompleted});
            this.dgvQuests.Location = new System.Drawing.Point(3, 28);
            this.dgvQuests.Name = "dgvQuests";
            this.dgvQuests.RowHeadersVisible = false;
            this.dgvQuests.Size = new System.Drawing.Size(239, 258);
            this.dgvQuests.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(248, 28);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(234, 258);
            this.dataGridView2.TabIndex = 1;
            // 
            // clmCheckBox
            // 
            this.clmCheckBox.HeaderText = "";
            this.clmCheckBox.Name = "clmCheckBox";
            this.clmCheckBox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmCheckBox.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmCheckBox.Width = 25;
            // 
            // clmQuestName
            // 
            this.clmQuestName.HeaderText = "Quest Name";
            this.clmQuestName.Name = "clmQuestName";
            this.clmQuestName.Width = 130;
            // 
            // clmCompleted
            // 
            this.clmCompleted.HeaderText = "Completed";
            this.clmCompleted.Name = "clmCompleted";
            this.clmCompleted.Width = 70;
            // 
            // QuestTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dgvQuests);
            this.Name = "QuestTab";
            this.Size = new System.Drawing.Size(482, 303);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvQuests;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQuestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCompleted;
    }
}
