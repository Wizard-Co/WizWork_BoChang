namespace WizWork
{
    partial class frm_tprc_MoveMenuCollection
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
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCase4 = new System.Windows.Forms.Button();
            this.btnCase3 = new System.Windows.Forms.Button();
            this.btnCase2 = new System.Windows.Forms.Button();
            this.btnCase1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnClose);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.btnCase4);
            this.splitContainer1.Panel2.Controls.Add(this.btnCase3);
            this.splitContainer1.Panel2.Controls.Add(this.btnCase2);
            this.splitContainer1.Panel2.Controls.Add(this.btnCase1);
            this.splitContainer1.Size = new System.Drawing.Size(1012, 592);
            this.splitContainer1.SplitterDistance = 67;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightCoral;
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.Location = new System.Drawing.Point(570, 344);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(300, 200);
            this.btnClose.TabIndex = 237;
            this.btnClose.Text = "닫  기";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.RoyalBlue;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1006, 57);
            this.label2.TabIndex = 236;
            this.label2.Text = "이 동 메 뉴    선 택";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCase4
            // 
            this.btnCase4.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCase4.Font = new System.Drawing.Font("맑은 고딕", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCase4.Location = new System.Drawing.Point(119, 344);
            this.btnCase4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCase4.Name = "btnCase4";
            this.btnCase4.Size = new System.Drawing.Size(300, 200);
            this.btnCase4.TabIndex = 5;
            this.btnCase4.Text = "잔 량 이 동 처 리";
            this.btnCase4.UseVisualStyleBackColor = false;
            this.btnCase4.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnCase3
            // 
            this.btnCase3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCase3.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCase3.Location = new System.Drawing.Point(902, 101);
            this.btnCase3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCase3.Name = "btnCase3";
            this.btnCase3.Size = new System.Drawing.Size(74, 67);
            this.btnCase3.TabIndex = 4;
            this.btnCase3.Text = "금 형 입 고";
            this.btnCase3.UseVisualStyleBackColor = false;
            this.btnCase3.Visible = false;
            this.btnCase3.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnCase2
            // 
            this.btnCase2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCase2.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCase2.Location = new System.Drawing.Point(570, 101);
            this.btnCase2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCase2.Name = "btnCase2";
            this.btnCase2.Size = new System.Drawing.Size(300, 200);
            this.btnCase2.TabIndex = 3;
            this.btnCase2.Text = "제 품 출 고";
            this.btnCase2.UseVisualStyleBackColor = false;
            this.btnCase2.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnCase1
            // 
            this.btnCase1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCase1.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCase1.Location = new System.Drawing.Point(119, 101);
            this.btnCase1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCase1.Name = "btnCase1";
            this.btnCase1.Size = new System.Drawing.Size(300, 200);
            this.btnCase1.TabIndex = 2;
            this.btnCase1.Text = "원 자 재 입 고";
            this.btnCase1.UseVisualStyleBackColor = false;
            this.btnCase1.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // frm_tprc_MoveMenuCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 595);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm_tprc_MoveMenuCollection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "이동메뉴 모음선택";
            this.Load += new System.EventHandler(this.frm_tprc_MoveMenuCollection_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCase4;
        private System.Windows.Forms.Button btnCase3;
        private System.Windows.Forms.Button btnCase2;
        private System.Windows.Forms.Button btnCase1;
        private System.Windows.Forms.Button btnClose;
    }
}