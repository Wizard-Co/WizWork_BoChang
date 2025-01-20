namespace WizWork
{
    partial class Frm_PopUp_LabelPrint
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
            this.components = new System.ComponentModel.Container();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgvWorking = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnArticleSelect = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.txtScan = new System.Windows.Forms.TextBox();
            this.txtWorkQty = new System.Windows.Forms.TextBox();
            this.btnWorkQty = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnArticle = new System.Windows.Forms.Button();
            this.txtArticle = new System.Windows.Forms.TextBox();
            this.txtBuyerArticleNO = new System.Windows.Forms.TextBox();
            this.btnBuyerArticleNO = new System.Windows.Forms.Button();
            this.btnArticleEnabled = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorking)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.dgvWorking, 0, 1);
            this.tlpMain.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tlpMain.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpMain.Location = new System.Drawing.Point(10, 10);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpMain.Size = new System.Drawing.Size(912, 590);
            this.tlpMain.TabIndex = 190;
            // 
            // dgvWorking
            // 
            this.dgvWorking.AllowUserToAddRows = false;
            this.dgvWorking.AllowUserToResizeColumns = false;
            this.dgvWorking.AllowUserToResizeRows = false;
            this.dgvWorking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWorking.Location = new System.Drawing.Point(3, 357);
            this.dgvWorking.Name = "dgvWorking";
            this.dgvWorking.RowHeadersVisible = false;
            this.dgvWorking.RowTemplate.Height = 23;
            this.dgvWorking.Size = new System.Drawing.Size(906, 171);
            this.dgvWorking.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnPrint, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnClose, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 534);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(906, 53);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.DarkOrange;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPrint.Location = new System.Drawing.Point(3, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(447, 47);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "발행";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DarkViolet;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.Location = new System.Drawing.Point(456, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(447, 47);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.btnArticleSelect, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnScan, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnArticle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtArticle, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtBuyerArticleNO, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnBuyerArticleNO, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnArticleEnabled, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(906, 348);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // btnArticleSelect
            // 
            this.btnArticleSelect.BackColor = System.Drawing.Color.Yellow;
            this.btnArticleSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnArticleSelect.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnArticleSelect.Location = new System.Drawing.Point(274, 3);
            this.btnArticleSelect.Name = "btnArticleSelect";
            this.btnArticleSelect.Size = new System.Drawing.Size(629, 63);
            this.btnArticleSelect.TabIndex = 6;
            this.btnArticleSelect.Text = "선택";
            this.btnArticleSelect.UseVisualStyleBackColor = false;
            this.btnArticleSelect.Click += new System.EventHandler(this.btnArticleSelect_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.Controls.Add(this.txtScan, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.txtWorkQty, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnWorkQty, 1, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(274, 72);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(629, 63);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // txtScan
            // 
            this.txtScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScan.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtScan.Location = new System.Drawing.Point(5, 5);
            this.txtScan.Margin = new System.Windows.Forms.Padding(5);
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(367, 57);
            this.txtScan.TabIndex = 5;
            this.txtScan.Click += new System.EventHandler(this.txtScan_Click);
            this.txtScan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtScan_KeyPress);
            // 
            // txtWorkQty
            // 
            this.txtWorkQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWorkQty.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtWorkQty.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.txtWorkQty.Location = new System.Drawing.Point(507, 5);
            this.txtWorkQty.Margin = new System.Windows.Forms.Padding(5);
            this.txtWorkQty.Name = "txtWorkQty";
            this.txtWorkQty.Size = new System.Drawing.Size(117, 57);
            this.txtWorkQty.TabIndex = 10;
            this.txtWorkQty.Click += new System.EventHandler(this.txtWorkQty_Click);
            // 
            // btnWorkQty
            // 
            this.btnWorkQty.BackColor = System.Drawing.Color.Yellow;
            this.btnWorkQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWorkQty.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnWorkQty.Location = new System.Drawing.Point(380, 3);
            this.btnWorkQty.Name = "btnWorkQty";
            this.btnWorkQty.Size = new System.Drawing.Size(119, 57);
            this.btnWorkQty.TabIndex = 7;
            this.btnWorkQty.Text = "포장수량";
            this.btnWorkQty.UseVisualStyleBackColor = false;
            this.btnWorkQty.Click += new System.EventHandler(this.btnWorkQty_Click);
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.Yellow;
            this.btnScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnScan.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnScan.Location = new System.Drawing.Point(3, 72);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(265, 63);
            this.btnScan.TabIndex = 1;
            this.btnScan.Text = "스캔";
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnArticle
            // 
            this.btnArticle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnArticle.Enabled = false;
            this.btnArticle.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnArticle.Location = new System.Drawing.Point(3, 3);
            this.btnArticle.Name = "btnArticle";
            this.btnArticle.Size = new System.Drawing.Size(265, 63);
            this.btnArticle.TabIndex = 0;
            this.btnArticle.Text = "품명";
            this.btnArticle.UseVisualStyleBackColor = true;
            // 
            // txtArticle
            // 
            this.txtArticle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArticle.Font = new System.Drawing.Font("맑은 고딕", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtArticle.ForeColor = System.Drawing.Color.Lime;
            this.txtArticle.Location = new System.Drawing.Point(276, 247);
            this.txtArticle.Margin = new System.Windows.Forms.Padding(5);
            this.txtArticle.Name = "txtArticle";
            this.txtArticle.ReadOnly = true;
            this.txtArticle.Size = new System.Drawing.Size(625, 93);
            this.txtArticle.TabIndex = 9;
            // 
            // txtBuyerArticleNO
            // 
            this.txtBuyerArticleNO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuyerArticleNO.Font = new System.Drawing.Font("맑은 고딕", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtBuyerArticleNO.ForeColor = System.Drawing.Color.Lime;
            this.txtBuyerArticleNO.Location = new System.Drawing.Point(276, 143);
            this.txtBuyerArticleNO.Margin = new System.Windows.Forms.Padding(5);
            this.txtBuyerArticleNO.Name = "txtBuyerArticleNO";
            this.txtBuyerArticleNO.ReadOnly = true;
            this.txtBuyerArticleNO.Size = new System.Drawing.Size(625, 93);
            this.txtBuyerArticleNO.TabIndex = 8;
            // 
            // btnBuyerArticleNO
            // 
            this.btnBuyerArticleNO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBuyerArticleNO.Enabled = false;
            this.btnBuyerArticleNO.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnBuyerArticleNO.Location = new System.Drawing.Point(3, 141);
            this.btnBuyerArticleNO.Name = "btnBuyerArticleNO";
            this.btnBuyerArticleNO.Size = new System.Drawing.Size(265, 98);
            this.btnBuyerArticleNO.TabIndex = 11;
            this.btnBuyerArticleNO.Text = "품번";
            this.btnBuyerArticleNO.UseVisualStyleBackColor = true;
            // 
            // btnArticleEnabled
            // 
            this.btnArticleEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnArticleEnabled.Enabled = false;
            this.btnArticleEnabled.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnArticleEnabled.Location = new System.Drawing.Point(3, 245);
            this.btnArticleEnabled.Name = "btnArticleEnabled";
            this.btnArticleEnabled.Size = new System.Drawing.Size(265, 100);
            this.btnArticleEnabled.TabIndex = 12;
            this.btnArticleEnabled.Text = "품명";
            this.btnArticleEnabled.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Frm_PopUp_LabelPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 603);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Frm_PopUp_LabelPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "라벨 발행";
            this.Load += new System.EventHandler(this.Frm_PopUp_LabelPrint_Load);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorking)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.DataGridView dgvWorking;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnArticle;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtScan;
        private System.Windows.Forms.Button btnArticleSelect;
        private System.Windows.Forms.Button btnWorkQty;
        private System.Windows.Forms.TextBox txtBuyerArticleNO;
        private System.Windows.Forms.TextBox txtArticle;
        private System.Windows.Forms.Button btnBuyerArticleNO;
        private System.Windows.Forms.Button btnArticleEnabled;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtWorkQty;
    }
}