namespace WizIns
{
    partial class Frm_PopUp_PackingPrintMode
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnTagID = new System.Windows.Forms.Button();
            this.btnTagID2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.Controls.Add(this.btnTagID2, 1, 0);
            this.tlpMain.Controls.Add(this.btnTagID, 0, 0);
            this.tlpMain.Controls.Add(this.button1, 2, 0);
            this.tlpMain.Location = new System.Drawing.Point(12, 11);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(618, 261);
            this.tlpMain.TabIndex = 0;
            // 
            // btnTagID
            // 
            this.btnTagID.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnTagID.Location = new System.Drawing.Point(3, 3);
            this.btnTagID.Name = "btnTagID";
            this.btnTagID.Size = new System.Drawing.Size(241, 255);
            this.btnTagID.TabIndex = 0;
            this.btnTagID.Text = "슬라이드 BOX 라벨\r\n(대구,대구유통,부산,울산,광주,창원,평택공장)";
            this.btnTagID.UseVisualStyleBackColor = true;
            this.btnTagID.Click += new System.EventHandler(this.btnTagID_Click);
            // 
            // btnTagID2
            // 
            this.btnTagID2.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnTagID2.Location = new System.Drawing.Point(250, 3);
            this.btnTagID2.Name = "btnTagID2";
            this.btnTagID2.Size = new System.Drawing.Size(241, 255);
            this.btnTagID2.TabIndex = 1;
            this.btnTagID2.Text = "슬라이드 BOX 라벨\r\n(서울,서울유통,수원,수원유통,인천,천안,대전)";
            this.btnTagID2.UseVisualStyleBackColor = true;
            this.btnTagID2.Click += new System.EventHandler(this.btnTagID2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(497, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 255);
            this.button1.TabIndex = 2;
            this.button1.Text = "닫 기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Frm_PopUp_PackingPrintMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 281);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm_PopUp_PackingPrintMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "라벨 양식 선택";
            this.Load += new System.EventHandler(this.Frm_PopUp_PackingPrintMode_Load);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button btnTagID2;
        private System.Windows.Forms.Button btnTagID;
        private System.Windows.Forms.Button button1;
    }
}