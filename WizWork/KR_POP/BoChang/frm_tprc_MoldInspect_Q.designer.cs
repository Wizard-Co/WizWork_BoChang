namespace WizWork
{
    partial class frm_tprc_MoldInspect_Q
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
            this.txtMoldLoT = new System.Windows.Forms.TextBox();
            this.btnEndDate = new System.Windows.Forms.Button();
            this.btnStartDate = new System.Windows.Forms.Button();
            this.mtb_To = new System.Windows.Forms.MaskedTextBox();
            this.mtb_From = new System.Windows.Forms.MaskedTextBox();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.tabc = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvMold = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvMoldSub = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabc.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMold)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoldSub)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.txtMoldLoT);
            this.splitContainer1.Panel1.Controls.Add(this.btnEndDate);
            this.splitContainer1.Panel1.Controls.Add(this.btnStartDate);
            this.splitContainer1.Panel1.Controls.Add(this.mtb_To);
            this.splitContainer1.Panel1.Controls.Add(this.mtb_From);
            this.splitContainer1.Panel1.Controls.Add(this.chkDate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabc);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 583);
            this.splitContainer1.SplitterDistance = 78;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnMoldLoT
            // 
            this.btnMoldLoT.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMoldLoT.Location = new System.Drawing.Point(4, 40);
            this.btnMoldLoT.Name = "btnMoldLoT";
            this.btnMoldLoT.Size = new System.Drawing.Size(128, 35);
            this.btnMoldLoT.TabIndex = 10;
            this.btnMoldLoT.Text = "금형로트번호";
            this.btnMoldLoT.UseVisualStyleBackColor = true;
            this.btnMoldLoT.Click += new System.EventHandler(this.btnMoldLoT_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.Location = new System.Drawing.Point(887, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 74);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelete.Location = new System.Drawing.Point(771, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 74);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnFillGrid
            // 
            this.btnFillGrid.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFillGrid.Location = new System.Drawing.Point(655, 3);
            this.btnFillGrid.Name = "btnFillGrid";
            this.btnFillGrid.Size = new System.Drawing.Size(110, 74);
            this.btnFillGrid.TabIndex = 7;
            this.btnFillGrid.Text = "검색";
            this.btnFillGrid.UseVisualStyleBackColor = true;
            this.btnFillGrid.Click += new System.EventHandler(this.btnFillGrid_Click);
            // 
            // txtMoldLoT
            // 
            this.txtMoldLoT.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtMoldLoT.Location = new System.Drawing.Point(138, 40);
            this.txtMoldLoT.Name = "txtMoldLoT";
            this.txtMoldLoT.Size = new System.Drawing.Size(205, 33);
            this.txtMoldLoT.TabIndex = 6;
            this.txtMoldLoT.Click += new System.EventHandler(this.txtMoldLoT_Click);
            // 
            // btnEndDate
            // 
            this.btnEndDate.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnEndDate.Location = new System.Drawing.Point(459, 3);
            this.btnEndDate.Name = "btnEndDate";
            this.btnEndDate.Size = new System.Drawing.Size(52, 35);
            this.btnEndDate.TabIndex = 4;
            this.btnEndDate.UseVisualStyleBackColor = true;
            this.btnEndDate.Click += new System.EventHandler(this.mtEndDate_Click);
            // 
            // btnStartDate
            // 
            this.btnStartDate.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnStartDate.Location = new System.Drawing.Point(248, 3);
            this.btnStartDate.Name = "btnStartDate";
            this.btnStartDate.Size = new System.Drawing.Size(52, 35);
            this.btnStartDate.TabIndex = 3;
            this.btnStartDate.UseVisualStyleBackColor = true;
            this.btnStartDate.Click += new System.EventHandler(this.mtStartDate_Click);
            // 
            // mtb_To
            // 
            this.mtb_To.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mtb_To.Location = new System.Drawing.Point(306, 3);
            this.mtb_To.Mask = "0000-00-00";
            this.mtb_To.Name = "mtb_To";
            this.mtb_To.Size = new System.Drawing.Size(147, 33);
            this.mtb_To.TabIndex = 2;
            this.mtb_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_To.Click += new System.EventHandler(this.mtEndDate_Click);
            // 
            // mtb_From
            // 
            this.mtb_From.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mtb_From.Location = new System.Drawing.Point(95, 3);
            this.mtb_From.Mask = "0000-00-00";
            this.mtb_From.Name = "mtb_From";
            this.mtb_From.Size = new System.Drawing.Size(147, 33);
            this.mtb_From.TabIndex = 1;
            this.mtb_From.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_From.Click += new System.EventHandler(this.mtStartDate_Click);
            // 
            // chkDate
            // 
            this.chkDate.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkDate.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkDate.Checked = true;
            this.chkDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDate.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkDate.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkDate.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDate.ForeColor = System.Drawing.Color.White;
            this.chkDate.Location = new System.Drawing.Point(4, 3);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(86, 35);
            this.chkDate.TabIndex = 0;
            this.chkDate.Text = "일자";
            this.chkDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDate.UseVisualStyleBackColor = false;
            // 
            // tabc
            // 
            this.tabc.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabc.Controls.Add(this.tabPage1);
            this.tabc.Controls.Add(this.tabPage2);
            this.tabc.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabc.ItemSize = new System.Drawing.Size(450, 40);
            this.tabc.Location = new System.Drawing.Point(0, 3);
            this.tabc.Name = "tabc";
            this.tabc.SelectedIndex = 0;
            this.tabc.Size = new System.Drawing.Size(997, 495);
            this.tabc.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabc.TabIndex = 0;
            this.tabc.SelectedIndexChanged += new System.EventHandler(this.tabc_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvMold);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(989, 447);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "금형별";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvMold
            // 
            this.dgvMold.AllowUserToAddRows = false;
            this.dgvMold.AllowUserToDeleteRows = false;
            this.dgvMold.AllowUserToResizeRows = false;
            this.dgvMold.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMold.Location = new System.Drawing.Point(3, 3);
            this.dgvMold.Name = "dgvMold";
            this.dgvMold.RowHeadersVisible = false;
            this.dgvMold.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMold.Size = new System.Drawing.Size(983, 441);
            this.dgvMold.TabIndex = 0;
            this.dgvMold.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMold_CellClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvMoldSub);
            this.tabPage2.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(989, 447);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "상세";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvMoldSub
            // 
            this.dgvMoldSub.AllowUserToAddRows = false;
            this.dgvMoldSub.AllowUserToDeleteRows = false;
            this.dgvMoldSub.AllowUserToResizeRows = false;
            this.dgvMoldSub.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMoldSub.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMoldSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMoldSub.Location = new System.Drawing.Point(3, 3);
            this.dgvMoldSub.Name = "dgvMoldSub";
            this.dgvMoldSub.RowHeadersVisible = false;
            this.dgvMoldSub.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMoldSub.Size = new System.Drawing.Size(983, 441);
            this.dgvMoldSub.TabIndex = 0;
            // 
            // frm_tprc_MoldInspect_Q
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 587);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(7, 103);
            this.Name = "frm_tprc_MoldInspect_Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "생산관리시스템 - 금형점검 조회";
            this.Load += new System.EventHandler(this.frm_tprc_MoldInspect_Q_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabc.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMold)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoldSub)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.TabControl tabc;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnEndDate;
        private System.Windows.Forms.Button btnStartDate;
        private System.Windows.Forms.MaskedTextBox mtb_To;
        private System.Windows.Forms.MaskedTextBox mtb_From;
        private System.Windows.Forms.TextBox txtMoldLoT;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnFillGrid;
        private System.Windows.Forms.DataGridView dgvMold;
        private System.Windows.Forms.DataGridView dgvMoldSub;
        private System.Windows.Forms.Button btnMoldLoT;
    }
}