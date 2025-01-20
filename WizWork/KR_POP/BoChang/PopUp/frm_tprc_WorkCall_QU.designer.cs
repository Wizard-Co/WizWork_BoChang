namespace WizWork
{
    partial class frm_tprc_WorkCall_QU
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
            this.cmdSaveN = new System.Windows.Forms.Button();
            this.cmdSaveY = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cboPerson = new System.Windows.Forms.ComboBox();
            this.btnPerson = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cboRepond = new System.Windows.Forms.ComboBox();
            this.btnRespond = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSaveN
            // 
            this.cmdSaveN.BackColor = System.Drawing.Color.LightSalmon;
            this.cmdSaveN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdSaveN.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdSaveN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSaveN.Location = new System.Drawing.Point(177, 3);
            this.cmdSaveN.Name = "cmdSaveN";
            this.cmdSaveN.Size = new System.Drawing.Size(168, 57);
            this.cmdSaveN.TabIndex = 279;
            this.cmdSaveN.Text = "처 리 불 가";
            this.cmdSaveN.UseVisualStyleBackColor = true;
            this.cmdSaveN.Click += new System.EventHandler(this.cmdSaveN_Click);
            // 
            // cmdSaveY
            // 
            this.cmdSaveY.BackColor = System.Drawing.Color.LightPink;
            this.cmdSaveY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdSaveY.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdSaveY.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSaveY.Location = new System.Drawing.Point(3, 3);
            this.cmdSaveY.Name = "cmdSaveY";
            this.cmdSaveY.Size = new System.Drawing.Size(168, 57);
            this.cmdSaveY.TabIndex = 277;
            this.cmdSaveY.Text = "정 상 처 리 완 료";
            this.cmdSaveY.UseVisualStyleBackColor = true;
            this.cmdSaveY.Click += new System.EventHandler(this.cmdSaveY_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 212);
            this.panel1.TabIndex = 283;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(531, 209);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.cmdSaveN, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmdSaveY, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmdClose, 2, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(522, 63);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // cmdClose
            // 
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdClose.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdClose.Location = new System.Drawing.Point(351, 3);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(168, 57);
            this.cmdClose.TabIndex = 280;
            this.cmdClose.Text = "닫    기";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.Controls.Add(this.cboPerson, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnPerson, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 72);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(525, 63);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // cboPerson
            // 
            this.cboPerson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPerson.Font = new System.Drawing.Font("맑은 고딕", 30.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboPerson.FormattingEnabled = true;
            this.cboPerson.IntegralHeight = false;
            this.cboPerson.Location = new System.Drawing.Point(160, 3);
            this.cboPerson.MaxDropDownItems = 5;
            this.cboPerson.Name = "cboPerson";
            this.cboPerson.Size = new System.Drawing.Size(362, 63);
            this.cboPerson.TabIndex = 2;
            // 
            // btnPerson
            // 
            this.btnPerson.BackColor = System.Drawing.Color.White;
            this.btnPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPerson.Enabled = false;
            this.btnPerson.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPerson.ForeColor = System.Drawing.Color.Black;
            this.btnPerson.Location = new System.Drawing.Point(3, 3);
            this.btnPerson.Name = "btnPerson";
            this.btnPerson.Size = new System.Drawing.Size(151, 57);
            this.btnPerson.TabIndex = 3;
            this.btnPerson.Text = "처리자";
            this.btnPerson.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.Controls.Add(this.cboRepond, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnRespond, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 141);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(525, 65);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // cboRepond
            // 
            this.cboRepond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRepond.Font = new System.Drawing.Font("맑은 고딕", 30.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboRepond.FormattingEnabled = true;
            this.cboRepond.IntegralHeight = false;
            this.cboRepond.Location = new System.Drawing.Point(160, 3);
            this.cboRepond.MaxDropDownItems = 5;
            this.cboRepond.Name = "cboRepond";
            this.cboRepond.Size = new System.Drawing.Size(362, 63);
            this.cboRepond.TabIndex = 3;
            // 
            // btnRespond
            // 
            this.btnRespond.BackColor = System.Drawing.Color.White;
            this.btnRespond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRespond.Enabled = false;
            this.btnRespond.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRespond.ForeColor = System.Drawing.Color.Black;
            this.btnRespond.Location = new System.Drawing.Point(3, 3);
            this.btnRespond.Name = "btnRespond";
            this.btnRespond.Size = new System.Drawing.Size(151, 59);
            this.btnRespond.TabIndex = 4;
            this.btnRespond.Text = "조치내용";
            this.btnRespond.UseVisualStyleBackColor = false;
            // 
            // frm_tprc_WorkCall_QU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 229);
            this.Controls.Add(this.panel1);
            this.Name = "frm_tprc_WorkCall_QU";
            this.Text = "현장 호출 처리";
            this.Load += new System.EventHandler(this.frm_tprc_CallMgr_U_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cmdSaveN;
        private System.Windows.Forms.Button cmdSaveY;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ComboBox cboPerson;
        private System.Windows.Forms.ComboBox cboRepond;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnPerson;
        private System.Windows.Forms.Button btnRespond;
        private System.Windows.Forms.Button cmdClose;
    }
}