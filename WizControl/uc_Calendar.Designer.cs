namespace WizControl
{
    partial class uc_Calendar
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_Calendar));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.excelCalendar = new AxMSACAL.AxCalendar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.excelCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnPrev);
            this.panel1.Controls.Add(this.excelCalendar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 562);
            this.panel1.TabIndex = 1;
            // 
            // btnNext
            // 
            this.btnNext.Image = global::WizControl.Properties.Resources.forward_right_arrow_button;
            this.btnNext.Location = new System.Drawing.Point(675, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(112, 86);
            this.btnNext.TabIndex = 14;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(592, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 43);
            this.label1.TabIndex = 15;
            // 
            // btnPrev
            // 
            this.btnPrev.Image = global::WizControl.Properties.Resources.left_arrow;
            this.btnPrev.Location = new System.Drawing.Point(15, 0);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(113, 86);
            this.btnPrev.TabIndex = 13;
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // excelCalendar
            // 
            this.excelCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.excelCalendar.Enabled = true;
            this.excelCalendar.Location = new System.Drawing.Point(0, 0);
            this.excelCalendar.Name = "excelCalendar";
            this.excelCalendar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("excelCalendar.OcxState")));
            this.excelCalendar.Size = new System.Drawing.Size(804, 560);
            this.excelCalendar.TabIndex = 12;
            this.excelCalendar.ClickEvent += new System.EventHandler(this.excelCalendar_ClickEvent);
            // 
            // uc_Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "uc_Calendar";
            this.Size = new System.Drawing.Size(806, 562);
            this.Load += new System.EventHandler(this.uc_Calendar_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.excelCalendar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrev;
        private AxMSACAL.AxCalendar excelCalendar;
    }
}
