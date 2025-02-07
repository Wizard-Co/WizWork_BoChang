
namespace WizInOut
{
    partial class Frm_tinout_Main
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_tinout_Main));
            this.tlptop = new System.Windows.Forms.TableLayoutPanel();
            this.btnOutwareSearch = new System.Windows.Forms.Button();
            this.btnOutware = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnStuffinSearch = new System.Windows.Forms.Button();
            this.btnStuffin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnChoiceWorker = new System.Windows.Forms.Button();
            this.tlptop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlptop
            // 
            this.tlptop.ColumnCount = 6;
            this.tlptop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlptop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlptop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlptop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlptop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlptop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlptop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlptop.Controls.Add(this.btnOutwareSearch, 4, 0);
            this.tlptop.Controls.Add(this.btnOutware, 2, 0);
            this.tlptop.Controls.Add(this.btnInfo, 0, 0);
            this.tlptop.Controls.Add(this.btnStuffinSearch, 3, 0);
            this.tlptop.Controls.Add(this.btnStuffin, 1, 0);
            this.tlptop.Controls.Add(this.btnExit, 5, 0);
            this.tlptop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlptop.Location = new System.Drawing.Point(0, 0);
            this.tlptop.Name = "tlptop";
            this.tlptop.RowCount = 1;
            this.tlptop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlptop.Size = new System.Drawing.Size(1005, 84);
            this.tlptop.TabIndex = 0;
            // 
            // btnOutwareSearch
            // 
            this.btnOutwareSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(166)))), ((int)(((byte)(244)))));
            this.btnOutwareSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOutwareSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOutwareSearch.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnOutwareSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOutwareSearch.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnOutwareSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnOutwareSearch.Image")));
            this.btnOutwareSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOutwareSearch.Location = new System.Drawing.Point(668, 0);
            this.btnOutwareSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnOutwareSearch.Name = "btnOutwareSearch";
            this.btnOutwareSearch.Size = new System.Drawing.Size(167, 84);
            this.btnOutwareSearch.TabIndex = 11;
            this.btnOutwareSearch.Tag = "4";
            this.btnOutwareSearch.Text = "제품 출고 조회";
            this.btnOutwareSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOutwareSearch.UseVisualStyleBackColor = false;
            this.btnOutwareSearch.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnOutware
            // 
            this.btnOutware.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(108)))), ((int)(((byte)(128)))));
            this.btnOutware.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOutware.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOutware.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnOutware.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOutware.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnOutware.Image = global::WizInOut.Properties.Resources.delivery_packages_on_a_trolley;
            this.btnOutware.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOutware.Location = new System.Drawing.Point(334, 0);
            this.btnOutware.Margin = new System.Windows.Forms.Padding(0);
            this.btnOutware.Name = "btnOutware";
            this.btnOutware.Size = new System.Drawing.Size(167, 84);
            this.btnOutware.TabIndex = 9;
            this.btnOutware.Tag = "2";
            this.btnOutware.Text = "제품 출고";
            this.btnOutware.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOutware.UseVisualStyleBackColor = false;
            this.btnOutware.Click += new System.EventHandler(this.btnControl_Click);
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
            this.btnInfo.Size = new System.Drawing.Size(167, 84);
            this.btnInfo.TabIndex = 1;
            this.btnInfo.Tag = "0";
            this.btnInfo.Text = "공지사항";
            this.btnInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnStuffinSearch
            // 
            this.btnStuffinSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(166)))), ((int)(((byte)(244)))));
            this.btnStuffinSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnStuffinSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStuffinSearch.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnStuffinSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStuffinSearch.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnStuffinSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnStuffinSearch.Image")));
            this.btnStuffinSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStuffinSearch.Location = new System.Drawing.Point(501, 0);
            this.btnStuffinSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnStuffinSearch.Name = "btnStuffinSearch";
            this.btnStuffinSearch.Size = new System.Drawing.Size(167, 84);
            this.btnStuffinSearch.TabIndex = 10;
            this.btnStuffinSearch.Tag = "3";
            this.btnStuffinSearch.Text = "원자재 입고 조회";
            this.btnStuffinSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStuffinSearch.UseVisualStyleBackColor = false;
            this.btnStuffinSearch.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnStuffin
            // 
            this.btnStuffin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(108)))), ((int)(((byte)(128)))));
            this.btnStuffin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnStuffin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStuffin.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnStuffin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStuffin.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnStuffin.Image = global::WizInOut.Properties.Resources.delivery_packages_on_a_trolley;
            this.btnStuffin.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStuffin.Location = new System.Drawing.Point(167, 0);
            this.btnStuffin.Margin = new System.Windows.Forms.Padding(0);
            this.btnStuffin.Name = "btnStuffin";
            this.btnStuffin.Size = new System.Drawing.Size(167, 84);
            this.btnStuffin.TabIndex = 8;
            this.btnStuffin.Tag = "1";
            this.btnStuffin.Text = "원자재 입고";
            this.btnStuffin.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStuffin.UseVisualStyleBackColor = false;
            this.btnStuffin.Click += new System.EventHandler(this.btnControl_Click);
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
            this.btnExit.Location = new System.Drawing.Point(835, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(170, 84);
            this.btnExit.TabIndex = 23;
            this.btnExit.Tag = "6";
            this.btnExit.Text = "작업종료";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnChoiceWorker
            // 
            this.btnChoiceWorker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(194)))), ((int)(((byte)(133)))));
            this.btnChoiceWorker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnChoiceWorker.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChoiceWorker.Font = new System.Drawing.Font("맑은 고딕", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnChoiceWorker.Image = ((System.Drawing.Image)(resources.GetObject("btnChoiceWorker.Image")));
            this.btnChoiceWorker.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnChoiceWorker.Location = new System.Drawing.Point(0, 87);
            this.btnChoiceWorker.Margin = new System.Windows.Forms.Padding(0);
            this.btnChoiceWorker.Name = "btnChoiceWorker";
            this.btnChoiceWorker.Size = new System.Drawing.Size(143, 84);
            this.btnChoiceWorker.TabIndex = 22;
            this.btnChoiceWorker.Tag = "5";
            this.btnChoiceWorker.Text = "작업자 선택";
            this.btnChoiceWorker.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChoiceWorker.UseVisualStyleBackColor = false;
            this.btnChoiceWorker.Visible = false;
            // 
            // Frm_tinout_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1005, 685);
            this.Controls.Add(this.tlptop);
            this.Controls.Add(this.btnChoiceWorker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MaximumSize = new System.Drawing.Size(1021, 724);
            this.MinimumSize = new System.Drawing.Size(1021, 724);
            this.Name = "Frm_tinout_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "현장 입고/출고 시스템";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Frm_tinout_Main_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_tinout_Main_FormClosing);
            this.Load += new System.EventHandler(this.Frm_tinout_Main_Load);
            this.tlptop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlptop;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnOutware;
        private System.Windows.Forms.Button btnStuffin;
        private System.Windows.Forms.Button btnOutwareSearch;
        private System.Windows.Forms.Button btnStuffinSearch;
        private System.Windows.Forms.Button btnChoiceWorker;
        private System.Windows.Forms.Button btnExit;
    }
}

