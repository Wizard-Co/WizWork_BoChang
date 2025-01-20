namespace WizIns
{
    partial class Frm_tins_Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_tins_Main));
            this.stsInfo = new System.Windows.Forms.StatusStrip();
            this.stsInfo_Msg = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsInfo_ProMac = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsInfo_Team = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsInfo_Person = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsInfo_Mold = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsInfo_Time = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer_Clock = new System.Windows.Forms.Timer(this.components);
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.btnInfo = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnChoiceWorker = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PersonBox = new System.Windows.Forms.TextBox();
            this.txtPersonBox = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.stsInfo.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stsInfo
            // 
            this.stsInfo.AutoSize = false;
            this.stsInfo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.stsInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsInfo_Msg,
            this.stsInfo_ProMac,
            this.stsInfo_Team,
            this.stsInfo_Person,
            this.stsInfo_Mold,
            this.stsInfo_Time});
            this.stsInfo.Location = new System.Drawing.Point(0, 663);
            this.stsInfo.Name = "stsInfo";
            this.stsInfo.Size = new System.Drawing.Size(1005, 22);
            this.stsInfo.SizingGrip = false;
            this.stsInfo.TabIndex = 6;
            this.stsInfo.Text = "statusStrip1";
            // 
            // stsInfo_Msg
            // 
            this.stsInfo_Msg.AutoSize = false;
            this.stsInfo_Msg.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_Msg.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_Msg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_Msg.Name = "stsInfo_Msg";
            this.stsInfo_Msg.Size = new System.Drawing.Size(385, 17);
            this.stsInfo_Msg.Text = "Message";
            // 
            // stsInfo_ProMac
            // 
            this.stsInfo_ProMac.AutoSize = false;
            this.stsInfo_ProMac.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_ProMac.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_ProMac.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_ProMac.Name = "stsInfo_ProMac";
            this.stsInfo_ProMac.Size = new System.Drawing.Size(200, 17);
            // 
            // stsInfo_Team
            // 
            this.stsInfo_Team.AutoSize = false;
            this.stsInfo_Team.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_Team.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_Team.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_Team.Name = "stsInfo_Team";
            this.stsInfo_Team.Size = new System.Drawing.Size(70, 17);
            this.stsInfo_Team.Text = "주간";
            // 
            // stsInfo_Person
            // 
            this.stsInfo_Person.AutoSize = false;
            this.stsInfo_Person.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_Person.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_Person.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_Person.Name = "stsInfo_Person";
            this.stsInfo_Person.Size = new System.Drawing.Size(121, 17);
            this.stsInfo_Person.Text = "작업자 : 관리자";
            // 
            // stsInfo_Mold
            // 
            this.stsInfo_Mold.AutoSize = false;
            this.stsInfo_Mold.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_Mold.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_Mold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_Mold.Name = "stsInfo_Mold";
            this.stsInfo_Mold.Size = new System.Drawing.Size(121, 17);
            this.stsInfo_Mold.Text = "금형 : ";
            // 
            // stsInfo_Time
            // 
            this.stsInfo_Time.AutoSize = false;
            this.stsInfo_Time.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_Time.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_Time.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_Time.Name = "stsInfo_Time";
            this.stsInfo_Time.Size = new System.Drawing.Size(121, 17);
            this.stsInfo_Time.Text = "오전 12:00";
            // 
            // timer_Clock
            // 
            this.timer_Clock.Tick += new System.EventHandler(this.timer_Clock_Tick);
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 7;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTop.Controls.Add(this.btnInfo, 0, 0);
            this.tlpTop.Controls.Add(this.button3, 3, 0);
            this.tlpTop.Controls.Add(this.button1, 1, 0);
            this.tlpTop.Controls.Add(this.btnExit, 6, 0);
            this.tlpTop.Controls.Add(this.button2, 2, 0);
            this.tlpTop.Controls.Add(this.btnChoiceWorker, 4, 0);
            this.tlpTop.Controls.Add(this.tableLayoutPanel1, 5, 0);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTop.Location = new System.Drawing.Point(0, 0);
            this.tlpTop.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.Size = new System.Drawing.Size(1005, 84);
            this.tlpTop.TabIndex = 14;
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(192)))), ((int)(((byte)(92)))));
            this.btnInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInfo.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInfo.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInfo.Image")));
            this.btnInfo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInfo.Location = new System.Drawing.Point(0, 0);
            this.btnInfo.Margin = new System.Windows.Forms.Padding(0);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(143, 84);
            this.btnInfo.TabIndex = 0;
            this.btnInfo.Tag = "0";
            this.btnInfo.Text = "공지사항";
            this.btnInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(166)))), ((int)(((byte)(244)))));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3.Location = new System.Drawing.Point(429, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(143, 84);
            this.button3.TabIndex = 7;
            this.button3.Tag = "3";
            this.button3.Text = "전수검사 실적 \r\n조회";
            this.button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(108)))), ((int)(((byte)(128)))));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(143, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 84);
            this.button1.TabIndex = 7;
            this.button1.Tag = "1";
            this.button1.Text = "미검사 실적 조회";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(108)))), ((int)(((byte)(128)))));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button4.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button4.Location = new System.Drawing.Point(880, 84);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 84);
            this.button4.TabIndex = 23;
            this.button4.Tag = "5";
            this.button4.Text = "최종 검사기 자료 수집";
            this.button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(108)))), ((int)(((byte)(128)))));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(286, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 84);
            this.button2.TabIndex = 12;
            this.button2.Tag = "2";
            this.button2.Text = "전수 검사";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnChoiceWorker
            // 
            this.btnChoiceWorker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(194)))), ((int)(((byte)(133)))));
            this.btnChoiceWorker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnChoiceWorker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChoiceWorker.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChoiceWorker.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnChoiceWorker.Image = ((System.Drawing.Image)(resources.GetObject("btnChoiceWorker.Image")));
            this.btnChoiceWorker.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnChoiceWorker.Location = new System.Drawing.Point(572, 0);
            this.btnChoiceWorker.Margin = new System.Windows.Forms.Padding(0);
            this.btnChoiceWorker.Name = "btnChoiceWorker";
            this.btnChoiceWorker.Size = new System.Drawing.Size(143, 84);
            this.btnChoiceWorker.TabIndex = 21;
            this.btnChoiceWorker.Tag = "4";
            this.btnChoiceWorker.Text = "작업자 선택";
            this.btnChoiceWorker.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChoiceWorker.UseVisualStyleBackColor = false;
            this.btnChoiceWorker.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.PersonBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPersonBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(718, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(137, 78);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // PersonBox
            // 
            this.PersonBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(194)))), ((int)(((byte)(133)))));
            this.PersonBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PersonBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PersonBox.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PersonBox.Location = new System.Drawing.Point(2, 2);
            this.PersonBox.Margin = new System.Windows.Forms.Padding(0);
            this.PersonBox.Name = "PersonBox";
            this.PersonBox.ReadOnly = true;
            this.PersonBox.Size = new System.Drawing.Size(133, 36);
            this.PersonBox.TabIndex = 0;
            this.PersonBox.Text = "작업자";
            this.PersonBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPersonBox
            // 
            this.txtPersonBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPersonBox.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPersonBox.Location = new System.Drawing.Point(2, 40);
            this.txtPersonBox.Margin = new System.Windows.Forms.Padding(0);
            this.txtPersonBox.Name = "txtPersonBox";
            this.txtPersonBox.ReadOnly = true;
            this.txtPersonBox.Size = new System.Drawing.Size(101, 36);
            this.txtPersonBox.TabIndex = 1;
            this.txtPersonBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(162)))), ((int)(((byte)(143)))));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(858, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(147, 84);
            this.btnExit.TabIndex = 3;
            this.btnExit.Tag = "9";
            this.btnExit.Text = "작업종료";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // Frm_tins_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 685);
            this.Controls.Add(this.tlpTop);
            this.Controls.Add(this.stsInfo);
            this.Controls.Add(this.button4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MaximumSize = new System.Drawing.Size(1021, 724);
            this.MinimumSize = new System.Drawing.Size(1021, 724);
            this.Name = "Frm_tins_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "검사/포장 시스템";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Frm_tins_Main_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_tins_Main_FormClosing);
            this.Load += new System.EventHandler(this.MDIParent1_Load);
            this.stsInfo.ResumeLayout(false);
            this.stsInfo.PerformLayout();
            this.tlpTop.ResumeLayout(false);
            this.tlpTop.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        public System.Windows.Forms.StatusStrip stsInfo;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_ProMac;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_Team;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_Person;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_Mold;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_Time;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_Msg;
        private System.Windows.Forms.Timer timer_Clock;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnChoiceWorker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox PersonBox;
        private System.Windows.Forms.TextBox txtPersonBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnExit;
    }
}



