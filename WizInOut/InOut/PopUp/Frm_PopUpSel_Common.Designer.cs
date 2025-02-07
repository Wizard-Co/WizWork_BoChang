namespace WizInOut
{
    partial class Frm_PopUpSel_Common
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
            this.grdData = new System.Windows.Forms.DataGridView();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpOC = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkBuyerArticle = new System.Windows.Forms.CheckBox();
            this.chkArticle = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.TextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.tlpOC.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grdData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Location = new System.Drawing.Point(3, 71);
            this.grdData.MultiSelect = false;
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grdData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.grdData.RowTemplate.Height = 23;
            this.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new System.Drawing.Size(927, 338);
            this.grdData.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpOC, 0, 2);
            this.tlpMain.Controls.Add(this.grdData, 0, 1);
            this.tlpMain.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpMain.Location = new System.Drawing.Point(3, 3);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpMain.Size = new System.Drawing.Size(933, 459);
            this.tlpMain.TabIndex = 190;
            // 
            // tlpOC
            // 
            this.tlpOC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(149)))), ((int)(((byte)(168)))));
            this.tlpOC.ColumnCount = 2;
            this.tlpOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOC.Controls.Add(this.btnCancel, 1, 0);
            this.tlpOC.Controls.Add(this.btnOK, 0, 0);
            this.tlpOC.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpOC.Location = new System.Drawing.Point(3, 415);
            this.tlpOC.Name = "tlpOC";
            this.tlpOC.RowCount = 1;
            this.tlpOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tlpOC.Size = new System.Drawing.Size(927, 41);
            this.tlpOC.TabIndex = 190;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(463, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(464, 41);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(73)))));
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOK.Location = new System.Drawing.Point(0, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(463, 41);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.chkBuyerArticle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkArticle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.TextBox, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(927, 64);
            this.tableLayoutPanel1.TabIndex = 191;
            // 
            // chkBuyerArticle
            // 
            this.chkBuyerArticle.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBuyerArticle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkBuyerArticle.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkBuyerArticle.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black;
            this.chkBuyerArticle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBuyerArticle.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkBuyerArticle.ForeColor = System.Drawing.Color.White;
            this.chkBuyerArticle.Image = global::WizInOut.Properties.Resources.Check_32pix;
            this.chkBuyerArticle.Location = new System.Drawing.Point(94, 2);
            this.chkBuyerArticle.Margin = new System.Windows.Forms.Padding(2);
            this.chkBuyerArticle.Name = "chkBuyerArticle";
            this.chkBuyerArticle.Size = new System.Drawing.Size(88, 43);
            this.chkBuyerArticle.TabIndex = 222;
            this.chkBuyerArticle.Text = "품번";
            this.chkBuyerArticle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBuyerArticle.UseVisualStyleBackColor = false;
            this.chkBuyerArticle.Click += new System.EventHandler(this.chkBuyerArticle_Click);
            // 
            // chkArticle
            // 
            this.chkArticle.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkArticle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkArticle.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkArticle.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black;
            this.chkArticle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkArticle.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkArticle.ForeColor = System.Drawing.Color.White;
            this.chkArticle.Image = global::WizInOut.Properties.Resources.Check_32pix;
            this.chkArticle.Location = new System.Drawing.Point(2, 2);
            this.chkArticle.Margin = new System.Windows.Forms.Padding(2);
            this.chkArticle.Name = "chkArticle";
            this.chkArticle.Size = new System.Drawing.Size(88, 43);
            this.chkArticle.TabIndex = 215;
            this.chkArticle.Text = "품명";
            this.chkArticle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkArticle.UseVisualStyleBackColor = false;
            this.chkArticle.Click += new System.EventHandler(this.chkArticle_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Image = global::WizInOut.Properties.Resources.icons8_search_48;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(789, 2);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(135, 60);
            this.btnSearch.TabIndex = 221;
            this.btnSearch.Text = "검색";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // TextBox
            // 
            this.TextBox.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TextBox.Location = new System.Drawing.Point(187, 2);
            this.TextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(596, 71);
            this.TextBox.TabIndex = 218;
            this.TextBox.Click += new System.EventHandler(this.TextBox_Click);
            // 
            // Frm_PopUpSel_Common
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 461);
            this.Controls.Add(this.tlpMain);
            this.MaximumSize = new System.Drawing.Size(950, 500);
            this.MinimumSize = new System.Drawing.Size(950, 500);
            this.Name = "Frm_PopUpSel_Common";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "선택";
            this.Load += new System.EventHandler(this.Frm_PopUpSel_Common_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.tlpOC.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpOC;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox TextBox;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkArticle;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkBuyerArticle;
    }
}