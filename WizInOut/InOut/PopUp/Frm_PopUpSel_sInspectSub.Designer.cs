namespace WizInOut
{
    partial class Frm_PopUpSel_sInspectSub
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
            this.cmdclose = new System.Windows.Forms.Button();
            this.tabInspectSub = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvInspectSub = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabInspectSub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspectSub)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdclose);
            this.splitContainer1.Panel1.Controls.Add(this.tabInspectSub);
            this.splitContainer1.Panel1.Controls.Add(this.dgvInspectSub);
            this.splitContainer1.Panel1.Controls.Add(this.lblTitle);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(950, 450);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 0;
            // 
            // cmdclose
            // 
            this.cmdclose.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdclose.Location = new System.Drawing.Point(841, 354);
            this.cmdclose.Name = "cmdclose";
            this.cmdclose.Size = new System.Drawing.Size(97, 84);
            this.cmdclose.TabIndex = 250;
            this.cmdclose.Text = "닫기";
            this.cmdclose.UseVisualStyleBackColor = true;
            this.cmdclose.Click += new System.EventHandler(this.cmdclose_Click);
            // 
            // tabInspectSub
            // 
            this.tabInspectSub.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabInspectSub.Controls.Add(this.tabPage1);
            this.tabInspectSub.Controls.Add(this.tabPage2);
            this.tabInspectSub.Location = new System.Drawing.Point(447, 63);
            this.tabInspectSub.Name = "tabInspectSub";
            this.tabInspectSub.SelectedIndex = 0;
            this.tabInspectSub.Size = new System.Drawing.Size(392, 375);
            this.tabInspectSub.TabIndex = 249;
            // 
            // tabPage1
            // 
            this.tabPage1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(384, 349);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "번호";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(384, 349);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "번호";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvInspectSub
            // 
            this.dgvInspectSub.AllowUserToAddRows = false;
            this.dgvInspectSub.AllowUserToDeleteRows = false;
            this.dgvInspectSub.AllowUserToResizeRows = false;
            this.dgvInspectSub.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInspectSub.Location = new System.Drawing.Point(8, 63);
            this.dgvInspectSub.Name = "dgvInspectSub";
            this.dgvInspectSub.RowHeadersVisible = false;
            this.dgvInspectSub.RowTemplate.Height = 23;
            this.dgvInspectSub.Size = new System.Drawing.Size(433, 375);
            this.dgvInspectSub.TabIndex = 248;
            this.dgvInspectSub.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvInspectSub_CellMouseClick);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(8, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(930, 39);
            this.lblTitle.TabIndex = 227;
            this.lblTitle.Text = "수 입 검 사 이 력 조 회";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Frm_PopUpSel_sInspectSub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 450);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(966, 489);
            this.Name = "Frm_PopUpSel_sInspectSub";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "검사이력";
            this.Load += new System.EventHandler(this.Frm_PopUpSel_sInspectSub_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabInspectSub.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspectSub)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvInspectSub;
        private System.Windows.Forms.TabControl tabInspectSub;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdclose;
    }
}