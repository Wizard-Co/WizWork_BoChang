namespace WizWork
{
    partial class frm_tprc_MoldInOutware_Q
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnMoldLoT = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnFillGrid = new System.Windows.Forms.Button();
            this.cboGbn = new System.Windows.Forms.ComboBox();
            this.txtMoldLoT = new System.Windows.Forms.TextBox();
            this.btnEndDate = new System.Windows.Forms.Button();
            this.btnStartDate = new System.Windows.Forms.Button();
            this.mtb_To = new System.Windows.Forms.MaskedTextBox();
            this.mtb_From = new System.Windows.Forms.MaskedTextBox();
            this.chkGbn = new System.Windows.Forms.CheckBox();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.dgvMoldLoT = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoldLoT)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnMoldLoT);
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel1.Controls.Add(this.btnFillGrid);
            this.splitContainer1.Panel1.Controls.Add(this.cboGbn);
            this.splitContainer1.Panel1.Controls.Add(this.txtMoldLoT);
            this.splitContainer1.Panel1.Controls.Add(this.btnEndDate);
            this.splitContainer1.Panel1.Controls.Add(this.btnStartDate);
            this.splitContainer1.Panel1.Controls.Add(this.mtb_To);
            this.splitContainer1.Panel1.Controls.Add(this.mtb_From);
            this.splitContainer1.Panel1.Controls.Add(this.chkGbn);
            this.splitContainer1.Panel1.Controls.Add(this.chkDate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvMoldLoT);
            this.splitContainer1.Size = new System.Drawing.Size(1003, 584);
            this.splitContainer1.SplitterDistance = 90;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnMoldLoT
            // 
            this.btnMoldLoT.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMoldLoT.Location = new System.Drawing.Point(0, 44);
            this.btnMoldLoT.Name = "btnMoldLoT";
            this.btnMoldLoT.Size = new System.Drawing.Size(140, 35);
            this.btnMoldLoT.TabIndex = 12;
            this.btnMoldLoT.Text = "금형로트번호";
            this.btnMoldLoT.UseVisualStyleBackColor = true;
            this.btnMoldLoT.Click += new System.EventHandler(this.btnMoldLoT_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.Location = new System.Drawing.Point(886, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 74);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelete.Location = new System.Drawing.Point(770, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 74);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnFillGrid
            // 
            this.btnFillGrid.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFillGrid.Location = new System.Drawing.Point(654, 5);
            this.btnFillGrid.Name = "btnFillGrid";
            this.btnFillGrid.Size = new System.Drawing.Size(110, 74);
            this.btnFillGrid.TabIndex = 9;
            this.btnFillGrid.Text = "검색";
            this.btnFillGrid.UseVisualStyleBackColor = true;
            this.btnFillGrid.Click += new System.EventHandler(this.btnFillGrid_Click);
            // 
            // cboGbn
            // 
            this.cboGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGbn.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboGbn.FormattingEnabled = true;
            this.cboGbn.Location = new System.Drawing.Point(508, 42);
            this.cboGbn.Name = "cboGbn";
            this.cboGbn.Size = new System.Drawing.Size(128, 33);
            this.cboGbn.TabIndex = 8;
            // 
            // txtMoldLoT
            // 
            this.txtMoldLoT.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtMoldLoT.Location = new System.Drawing.Point(146, 44);
            this.txtMoldLoT.Name = "txtMoldLoT";
            this.txtMoldLoT.Size = new System.Drawing.Size(185, 33);
            this.txtMoldLoT.TabIndex = 7;
            this.txtMoldLoT.Click += new System.EventHandler(this.txtMoldLoT_Click);
            // 
            // btnEndDate
            // 
            this.btnEndDate.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnEndDate.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnEndDate.Location = new System.Drawing.Point(490, 3);
            this.btnEndDate.Name = "btnEndDate";
            this.btnEndDate.Size = new System.Drawing.Size(54, 35);
            this.btnEndDate.TabIndex = 6;
            this.btnEndDate.UseVisualStyleBackColor = true;
            this.btnEndDate.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // btnStartDate
            // 
            this.btnStartDate.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnStartDate.Location = new System.Drawing.Point(277, 3);
            this.btnStartDate.Name = "btnStartDate";
            this.btnStartDate.Size = new System.Drawing.Size(54, 35);
            this.btnStartDate.TabIndex = 5;
            this.btnStartDate.UseVisualStyleBackColor = true;
            this.btnStartDate.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // mtb_To
            // 
            this.mtb_To.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mtb_To.Location = new System.Drawing.Point(337, 3);
            this.mtb_To.Mask = "0000-00-00";
            this.mtb_To.Name = "mtb_To";
            this.mtb_To.ReadOnly = true;
            this.mtb_To.Size = new System.Drawing.Size(147, 33);
            this.mtb_To.TabIndex = 4;
            this.mtb_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // mtb_From
            // 
            this.mtb_From.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mtb_From.Location = new System.Drawing.Point(124, 3);
            this.mtb_From.Mask = "0000-00-00";
            this.mtb_From.Name = "mtb_From";
            this.mtb_From.ReadOnly = true;
            this.mtb_From.Size = new System.Drawing.Size(147, 33);
            this.mtb_From.TabIndex = 3;
            this.mtb_From.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // chkGbn
            // 
            this.chkGbn.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkGbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkGbn.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkGbn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkGbn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkGbn.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkGbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkGbn.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkGbn.ForeColor = System.Drawing.Color.White;
            this.chkGbn.Location = new System.Drawing.Point(337, 42);
            this.chkGbn.Name = "chkGbn";
            this.chkGbn.Size = new System.Drawing.Size(165, 37);
            this.chkGbn.TabIndex = 2;
            this.chkGbn.Text = "입출고세척 구분";
            this.chkGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkGbn.UseVisualStyleBackColor = false;
            // 
            // chkDate
            // 
            this.chkDate.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkDate.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkDate.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkDate.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkDate.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDate.ForeColor = System.Drawing.Color.White;
            this.chkDate.Location = new System.Drawing.Point(0, 3);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(118, 33);
            this.chkDate.TabIndex = 0;
            this.chkDate.Text = "일자";
            this.chkDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDate.UseVisualStyleBackColor = false;
            // 
            // dgvMoldLoT
            // 
            this.dgvMoldLoT.AllowUserToAddRows = false;
            this.dgvMoldLoT.AllowUserToDeleteRows = false;
            this.dgvMoldLoT.AllowUserToResizeRows = false;
            this.dgvMoldLoT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMoldLoT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMoldLoT.Location = new System.Drawing.Point(0, 0);
            this.dgvMoldLoT.Name = "dgvMoldLoT";
            this.dgvMoldLoT.RowHeadersVisible = false;
            this.dgvMoldLoT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMoldLoT.Size = new System.Drawing.Size(1003, 490);
            this.dgvMoldLoT.TabIndex = 0;
            this.dgvMoldLoT.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMoldLoT_CellClick);
            // 
            // frm_tprc_MoldInOutware_Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 587);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_tprc_MoldInOutware_Q";
            this.Text = "금형세척 조회";
            this.Load += new System.EventHandler(this.frm_tprc_MoldInOutware_Q_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoldLoT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.Button btnMoldLoT;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnFillGrid;
        private System.Windows.Forms.ComboBox cboGbn;
        private System.Windows.Forms.TextBox txtMoldLoT;
        private System.Windows.Forms.Button btnEndDate;
        private System.Windows.Forms.Button btnStartDate;
        private System.Windows.Forms.MaskedTextBox mtb_To;
        private System.Windows.Forms.MaskedTextBox mtb_From;
        private System.Windows.Forms.CheckBox chkGbn;
        private System.Windows.Forms.DataGridView dgvMoldLoT;
    }
}