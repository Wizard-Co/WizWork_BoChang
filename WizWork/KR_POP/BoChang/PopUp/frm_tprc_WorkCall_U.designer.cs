namespace WizWork
{
    partial class frm_tprc_WorkCall_U
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
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.btnCal_Date = new System.Windows.Forms.Button();
            this.btnCallDay = new System.Windows.Forms.Button();
            this.mtb_Date = new System.Windows.Forms.MaskedTextBox();
            this.dtCallTime = new System.Windows.Forms.DateTimePicker();
            this.btnCallReason = new System.Windows.Forms.Button();
            this.cboCallReason = new System.Windows.Forms.ComboBox();
            this.btnCallTime = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnprocess = new System.Windows.Forms.Button();
            this.btnMachineID = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cboProcess = new System.Windows.Forms.ComboBox();
            this.cboMachineID = new System.Windows.Forms.ComboBox();
            this.cboPerson = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.Color.LightSalmon;
            this.cmdExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdExit.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdExit.Image = global::WizWork.Properties.Resources.enter;
            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExit.Location = new System.Drawing.Point(264, 3);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(255, 38);
            this.cmdExit.TabIndex = 279;
            this.cmdExit.Text = "닫기";
            this.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdClear.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdClear.Image = global::WizWork.Properties.Resources.square_delete;
            this.cmdClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClear.Location = new System.Drawing.Point(549, 12);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(120, 95);
            this.cmdClear.TabIndex = 278;
            this.cmdClear.Text = "초기화";
            this.cmdClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Visible = false;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.BackColor = System.Drawing.Color.LightPink;
            this.cmdSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdSave.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdSave.Image = global::WizWork.Properties.Resources.completed_tasks;
            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSave.Location = new System.Drawing.Point(3, 3);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(255, 38);
            this.cmdSave.TabIndex = 277;
            this.cmdSave.Text = "저  장";
            this.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // btnCal_Date
            // 
            this.btnCal_Date.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCal_Date.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnCal_Date.Location = new System.Drawing.Point(291, 3);
            this.btnCal_Date.Name = "btnCal_Date";
            this.btnCal_Date.Size = new System.Drawing.Size(66, 34);
            this.btnCal_Date.TabIndex = 207;
            this.btnCal_Date.UseVisualStyleBackColor = true;
            this.btnCal_Date.Click += new System.EventHandler(this.btnCal_Date_Click);
            // 
            // btnCallDay
            // 
            this.btnCallDay.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCallDay.Location = new System.Drawing.Point(3, 141);
            this.btnCallDay.Name = "btnCallDay";
            this.btnCallDay.Size = new System.Drawing.Size(150, 40);
            this.btnCallDay.TabIndex = 284;
            this.btnCallDay.Text = "호출일자 :";
            this.btnCallDay.UseVisualStyleBackColor = true;
            this.btnCallDay.Click += new System.EventHandler(this.btnCallDay_Click);
            // 
            // mtb_Date
            // 
            this.mtb_Date.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtb_Date.Font = new System.Drawing.Font("맑은 고딕", 18.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mtb_Date.Location = new System.Drawing.Point(3, 3);
            this.mtb_Date.Mask = "0000-00-00";
            this.mtb_Date.Name = "mtb_Date";
            this.mtb_Date.ReadOnly = true;
            this.mtb_Date.Size = new System.Drawing.Size(282, 41);
            this.mtb_Date.TabIndex = 206;
            this.mtb_Date.TabStop = false;
            this.mtb_Date.Text = "20201012";
            this.mtb_Date.ValidatingType = typeof(System.DateTime);
            this.mtb_Date.Click += new System.EventHandler(this.CallDay_From_Click);
            // 
            // dtCallTime
            // 
            this.dtCallTime.CalendarFont = new System.Drawing.Font("맑은 고딕", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtCallTime.CustomFormat = "";
            this.dtCallTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtCallTime.Font = new System.Drawing.Font("맑은 고딕", 18.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtCallTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtCallTime.Location = new System.Drawing.Point(156, 184);
            this.dtCallTime.Margin = new System.Windows.Forms.Padding(0);
            this.dtCallTime.Name = "dtCallTime";
            this.dtCallTime.ShowUpDown = true;
            this.dtCallTime.Size = new System.Drawing.Size(366, 41);
            this.dtCallTime.TabIndex = 264;
            this.dtCallTime.Enter += new System.EventHandler(this.btnCallTime_Click);
            // 
            // btnCallReason
            // 
            this.btnCallReason.Enabled = false;
            this.btnCallReason.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCallReason.Location = new System.Drawing.Point(3, 233);
            this.btnCallReason.Name = "btnCallReason";
            this.btnCallReason.Size = new System.Drawing.Size(150, 41);
            this.btnCallReason.TabIndex = 285;
            this.btnCallReason.Text = "호출사유 :";
            this.btnCallReason.UseVisualStyleBackColor = true;
            // 
            // cboCallReason
            // 
            this.cboCallReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboCallReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCallReason.Font = new System.Drawing.Font("맑은 고딕", 18.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboCallReason.FormattingEnabled = true;
            this.cboCallReason.IntegralHeight = false;
            this.cboCallReason.Location = new System.Drawing.Point(159, 233);
            this.cboCallReason.MaxDropDownItems = 5;
            this.cboCallReason.Name = "cboCallReason";
            this.cboCallReason.Size = new System.Drawing.Size(360, 43);
            this.cboCallReason.TabIndex = 3;
            // 
            // btnCallTime
            // 
            this.btnCallTime.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCallTime.Location = new System.Drawing.Point(3, 187);
            this.btnCallTime.Name = "btnCallTime";
            this.btnCallTime.Size = new System.Drawing.Size(150, 40);
            this.btnCallTime.TabIndex = 285;
            this.btnCallTime.Text = "호출시간 :";
            this.btnCallTime.UseVisualStyleBackColor = true;
            this.btnCallTime.Click += new System.EventHandler(this.btnCallTime_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 336);
            this.panel1.TabIndex = 283;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(528, 333);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.Controls.Add(this.cboCallReason, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.dtCallTime, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.btnCallTime, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.btnCallDay, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.btnCallReason, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.btnprocess, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnMachineID, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.button3, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.cboProcess, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.cboMachineID, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.cboPerson, 1, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(522, 277);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Controls.Add(this.btnCal_Date, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.mtb_Date, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(159, 141);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(360, 40);
            this.tableLayoutPanel5.TabIndex = 286;
            // 
            // btnprocess
            // 
            this.btnprocess.Enabled = false;
            this.btnprocess.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnprocess.Location = new System.Drawing.Point(3, 3);
            this.btnprocess.Name = "btnprocess";
            this.btnprocess.Size = new System.Drawing.Size(150, 40);
            this.btnprocess.TabIndex = 287;
            this.btnprocess.Text = "공 정      :";
            this.btnprocess.UseVisualStyleBackColor = true;
            // 
            // btnMachineID
            // 
            this.btnMachineID.Enabled = false;
            this.btnMachineID.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMachineID.Location = new System.Drawing.Point(3, 49);
            this.btnMachineID.Name = "btnMachineID";
            this.btnMachineID.Size = new System.Drawing.Size(150, 40);
            this.btnMachineID.TabIndex = 288;
            this.btnMachineID.Text = "호 기      :";
            this.btnMachineID.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button3.Location = new System.Drawing.Point(3, 95);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 40);
            this.button3.TabIndex = 289;
            this.button3.Text = "작업자    :";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // cboProcess
            // 
            this.cboProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProcess.Font = new System.Drawing.Font("맑은 고딕", 18.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboProcess.FormattingEnabled = true;
            this.cboProcess.IntegralHeight = false;
            this.cboProcess.Location = new System.Drawing.Point(159, 3);
            this.cboProcess.MaxDropDownItems = 5;
            this.cboProcess.Name = "cboProcess";
            this.cboProcess.Size = new System.Drawing.Size(360, 43);
            this.cboProcess.TabIndex = 290;
            this.cboProcess.SelectedIndexChanged += new System.EventHandler(this.cboProcess_SelectedIndexChanged);
            // 
            // cboMachineID
            // 
            this.cboMachineID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMachineID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMachineID.Enabled = false;
            this.cboMachineID.Font = new System.Drawing.Font("맑은 고딕", 18.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboMachineID.FormattingEnabled = true;
            this.cboMachineID.IntegralHeight = false;
            this.cboMachineID.Location = new System.Drawing.Point(159, 49);
            this.cboMachineID.MaxDropDownItems = 5;
            this.cboMachineID.Name = "cboMachineID";
            this.cboMachineID.Size = new System.Drawing.Size(360, 43);
            this.cboMachineID.TabIndex = 291;
            this.cboMachineID.DropDown += new System.EventHandler(this.cboMachineID_DropDown);
            // 
            // cboPerson
            // 
            this.cboPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPerson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPerson.Font = new System.Drawing.Font("맑은 고딕", 18.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboPerson.FormattingEnabled = true;
            this.cboPerson.IntegralHeight = false;
            this.cboPerson.Location = new System.Drawing.Point(159, 95);
            this.cboPerson.MaxDropDownItems = 5;
            this.cboPerson.Name = "cboPerson";
            this.cboPerson.Size = new System.Drawing.Size(360, 43);
            this.cboPerson.TabIndex = 292;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.cmdExit, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmdSave, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 286);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(522, 44);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // frm_tprc_WorkCall_U
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 353);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdClear);
            this.Name = "frm_tprc_WorkCall_U";
            this.Text = "현장 호출 처리";
            this.Load += new System.EventHandler(this.frm_tprc_CallMgr_U_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.MaskedTextBox mtb_Date;
        private System.Windows.Forms.Button btnCal_Date;
        private System.Windows.Forms.DateTimePicker dtCallTime;
        private System.Windows.Forms.Button btnCallReason;
        private System.Windows.Forms.Button btnCallDay;
        private System.Windows.Forms.ComboBox cboCallReason;
        private System.Windows.Forms.Button btnCallTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnprocess;
        private System.Windows.Forms.Button btnMachineID;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cboProcess;
        private System.Windows.Forms.ComboBox cboMachineID;
        private System.Windows.Forms.ComboBox cboPerson;
    }
}