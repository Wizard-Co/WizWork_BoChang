namespace WizWork
{
    partial class Frm_PopUpSel_KPI_InstID
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PopUpSel_KPI_InstID));
            this.dgvInst = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkInsDate = new System.Windows.Forms.CheckBox();
            this.btnCal_From = new System.Windows.Forms.Button();
            this.mtb_From = new System.Windows.Forms.MaskedTextBox();
            this.btnCal_To = new System.Windows.Forms.Button();
            this.mtb_To = new System.Windows.Forms.MaskedTextBox();
            this.btnFillGrid = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvInst
            // 
            this.dgvInst.AllowUserToAddRows = false;
            this.dgvInst.AllowUserToDeleteRows = false;
            this.dgvInst.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dgvInst.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInst.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInst.Location = new System.Drawing.Point(3, 47);
            this.dgvInst.MultiSelect = false;
            this.dgvInst.Name = "dgvInst";
            this.dgvInst.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInst.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvInst.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dgvInst.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvInst.RowTemplate.Height = 23;
            this.dgvInst.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInst.Size = new System.Drawing.Size(1004, 489);
            this.dgvInst.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(73)))));
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOK.Location = new System.Drawing.Point(3, 539);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(500, 48);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(507, 539);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(500, 48);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnFillGrid);
            this.splitContainer1.Panel1.Controls.Add(this.btnCal_To);
            this.splitContainer1.Panel1.Controls.Add(this.mtb_From);
            this.splitContainer1.Panel1.Controls.Add(this.mtb_To);
            this.splitContainer1.Panel1.Controls.Add(this.btnCal_From);
            this.splitContainer1.Panel1.Controls.Add(this.chkInsDate);
            this.splitContainer1.Panel1.Controls.Add(this.btnOK);
            this.splitContainer1.Panel1.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel1.Controls.Add(this.dgvInst);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1010, 593);
            this.splitContainer1.SplitterDistance = 157;
            this.splitContainer1.TabIndex = 191;
            // 
            // chkInsDate
            // 
            this.chkInsDate.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkInsDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkInsDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkInsDate.BackgroundImage")));
            this.chkInsDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkInsDate.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkInsDate.Checked = true;
            this.chkInsDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInsDate.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkInsDate.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkInsDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkInsDate.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkInsDate.ForeColor = System.Drawing.Color.White;
            this.chkInsDate.Location = new System.Drawing.Point(3, 2);
            this.chkInsDate.Margin = new System.Windows.Forms.Padding(2);
            this.chkInsDate.Name = "chkInsDate";
            this.chkInsDate.Size = new System.Drawing.Size(79, 39);
            this.chkInsDate.TabIndex = 201;
            this.chkInsDate.Text = "지시일";
            this.chkInsDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkInsDate.UseVisualStyleBackColor = false;
            // 
            // btnCal_From
            // 
            this.btnCal_From.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnCal_From.Location = new System.Drawing.Point(309, 3);
            this.btnCal_From.Name = "btnCal_From";
            this.btnCal_From.Size = new System.Drawing.Size(46, 38);
            this.btnCal_From.TabIndex = 0;
            this.btnCal_From.UseVisualStyleBackColor = true;
            this.btnCal_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // mtb_From
            // 
            this.mtb_From.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.mtb_From.Location = new System.Drawing.Point(87, 2);
            this.mtb_From.Mask = "0000-00-00";
            this.mtb_From.Name = "mtb_From";
            this.mtb_From.ReadOnly = true;
            this.mtb_From.Size = new System.Drawing.Size(216, 39);
            this.mtb_From.TabIndex = 205;
            this.mtb_From.TabStop = false;
            this.mtb_From.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_From.ValidatingType = typeof(System.DateTime);
            this.mtb_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // btnCal_To
            // 
            this.btnCal_To.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnCal_To.Location = new System.Drawing.Point(583, 3);
            this.btnCal_To.Name = "btnCal_To";
            this.btnCal_To.Size = new System.Drawing.Size(46, 38);
            this.btnCal_To.TabIndex = 0;
            this.btnCal_To.UseVisualStyleBackColor = true;
            this.btnCal_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // mtb_To
            // 
            this.mtb_To.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.mtb_To.Location = new System.Drawing.Point(361, 2);
            this.mtb_To.Mask = "0000-00-00";
            this.mtb_To.Name = "mtb_To";
            this.mtb_To.ReadOnly = true;
            this.mtb_To.Size = new System.Drawing.Size(216, 39);
            this.mtb_To.TabIndex = 205;
            this.mtb_To.TabStop = false;
            this.mtb_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_To.ValidatingType = typeof(System.DateTime);
            this.mtb_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // btnFillGrid
            // 
            this.btnFillGrid.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFillGrid.Location = new System.Drawing.Point(910, 3);
            this.btnFillGrid.Name = "btnFillGrid";
            this.btnFillGrid.Size = new System.Drawing.Size(97, 38);
            this.btnFillGrid.TabIndex = 206;
            this.btnFillGrid.Text = "조회";
            this.btnFillGrid.UseVisualStyleBackColor = true;
            this.btnFillGrid.Click += new System.EventHandler(this.btnFillGrid_Click);
            // 
            // Frm_PopUpSel_KPI_InstID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 596);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(1028, 635);
            this.MinimumSize = new System.Drawing.Size(1028, 635);
            this.Name = "Frm_PopUpSel_KPI_InstID";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "KPI 선택";
            this.Load += new System.EventHandler(this.Frm_PopUpSel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInst)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInst;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkInsDate;
        private System.Windows.Forms.Button btnCal_From;
        private System.Windows.Forms.MaskedTextBox mtb_From;
        private System.Windows.Forms.Button btnCal_To;
        private System.Windows.Forms.MaskedTextBox mtb_To;
        private System.Windows.Forms.Button btnFillGrid;
    }
}