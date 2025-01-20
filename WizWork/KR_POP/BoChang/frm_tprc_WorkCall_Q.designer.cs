namespace WizWork
{
    partial class frm_tprc_WorkCall_Q
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
            this.tlp_To = new System.Windows.Forms.TableLayoutPanel();
            this.btnCal_To = new System.Windows.Forms.Button();
            this.mtb_To = new System.Windows.Forms.MaskedTextBox();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.tlp_From = new System.Windows.Forms.TableLayoutPanel();
            this.btnCal_From = new System.Windows.Forms.Button();
            this.mtb_From = new System.Windows.Forms.MaskedTextBox();
            this.cboState = new System.Windows.Forms.ComboBox();
            this.cboMachineID = new System.Windows.Forms.ComboBox();
            this.cboProcess = new System.Windows.Forms.ComboBox();
            this.tlpForm = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.btnSave_OK = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdUp = new System.Windows.Forms.Button();
            this.cmdDown = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.chkMachine = new System.Windows.Forms.CheckBox();
            this.chkprocess = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.chkState = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CallSumAll = new System.Windows.Forms.Button();
            this.CallSumYN = new System.Windows.Forms.Button();
            this.txtCallSumAll = new System.Windows.Forms.TextBox();
            this.txtCallSumYN = new System.Windows.Forms.TextBox();
            this.btnSave_No = new System.Windows.Forms.Button();
            this.tlp_To.SuspendLayout();
            this.tlp_From.SuspendLayout();
            this.tlpForm.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tlpRight.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_To
            // 
            this.tlp_To.ColumnCount = 2;
            this.tlp_To.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlp_To.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_To.Controls.Add(this.btnCal_To, 1, 0);
            this.tlp_To.Controls.Add(this.mtb_To, 0, 0);
            this.tlp_To.Location = new System.Drawing.Point(284, 3);
            this.tlp_To.Name = "tlp_To";
            this.tlp_To.RowCount = 1;
            this.tlp_To.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_To.Size = new System.Drawing.Size(276, 26);
            this.tlp_To.TabIndex = 206;
            // 
            // btnCal_To
            // 
            this.btnCal_To.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCal_To.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnCal_To.Location = new System.Drawing.Point(223, 3);
            this.btnCal_To.Name = "btnCal_To";
            this.btnCal_To.Size = new System.Drawing.Size(50, 20);
            this.btnCal_To.TabIndex = 0;
            this.btnCal_To.UseVisualStyleBackColor = true;
            this.btnCal_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // mtb_To
            // 
            this.mtb_To.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtb_To.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mtb_To.Location = new System.Drawing.Point(3, 3);
            this.mtb_To.Mask = "0000-00-00";
            this.mtb_To.Name = "mtb_To";
            this.mtb_To.ReadOnly = true;
            this.mtb_To.Size = new System.Drawing.Size(214, 39);
            this.mtb_To.TabIndex = 205;
            this.mtb_To.TabStop = false;
            this.mtb_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_To.ValidatingType = typeof(System.DateTime);
            this.mtb_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // chkDate
            // 
            this.chkDate.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkDate.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkDate.Checked = true;
            this.chkDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkDate.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkDate.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkDate.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkDate.ForeColor = System.Drawing.Color.White;
            this.chkDate.Location = new System.Drawing.Point(2, 2);
            this.chkDate.Margin = new System.Windows.Forms.Padding(2);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(239, 34);
            this.chkDate.TabIndex = 201;
            this.chkDate.Text = "호출일자";
            this.chkDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDate.UseVisualStyleBackColor = false;
            // 
            // tlp_From
            // 
            this.tlp_From.ColumnCount = 2;
            this.tlp_From.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlp_From.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_From.Controls.Add(this.btnCal_From, 1, 0);
            this.tlp_From.Controls.Add(this.mtb_From, 0, 0);
            this.tlp_From.Location = new System.Drawing.Point(3, 3);
            this.tlp_From.Name = "tlp_From";
            this.tlp_From.RowCount = 1;
            this.tlp_From.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_From.Size = new System.Drawing.Size(275, 26);
            this.tlp_From.TabIndex = 205;
            // 
            // btnCal_From
            // 
            this.btnCal_From.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCal_From.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnCal_From.Location = new System.Drawing.Point(223, 3);
            this.btnCal_From.Name = "btnCal_From";
            this.btnCal_From.Size = new System.Drawing.Size(49, 20);
            this.btnCal_From.TabIndex = 0;
            this.btnCal_From.UseVisualStyleBackColor = true;
            this.btnCal_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // mtb_From
            // 
            this.mtb_From.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtb_From.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mtb_From.Location = new System.Drawing.Point(3, 3);
            this.mtb_From.Mask = "0000-00-00";
            this.mtb_From.Name = "mtb_From";
            this.mtb_From.ReadOnly = true;
            this.mtb_From.Size = new System.Drawing.Size(214, 39);
            this.mtb_From.TabIndex = 205;
            this.mtb_From.TabStop = false;
            this.mtb_From.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_From.ValidatingType = typeof(System.DateTime);
            this.mtb_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // cboState
            // 
            this.cboState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboState.Font = new System.Drawing.Font("맑은 고딕", 30.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboState.FormattingEnabled = true;
            this.cboState.IntegralHeight = false;
            this.cboState.Location = new System.Drawing.Point(123, 3);
            this.cboState.MaxDropDownItems = 99;
            this.cboState.MinimumSize = new System.Drawing.Size(100, 0);
            this.cboState.Name = "cboState";
            this.cboState.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboState.Size = new System.Drawing.Size(276, 63);
            this.cboState.TabIndex = 213;
            this.cboState.SelectedIndexChanged += new System.EventHandler(this.cboState_SelectedIndexChanged);
            // 
            // cboMachineID
            // 
            this.cboMachineID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMachineID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMachineID.Font = new System.Drawing.Font("맑은 고딕", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboMachineID.FormattingEnabled = true;
            this.cboMachineID.IntegralHeight = false;
            this.cboMachineID.Location = new System.Drawing.Point(123, 30);
            this.cboMachineID.MaxDropDownItems = 5;
            this.cboMachineID.MinimumSize = new System.Drawing.Size(100, 0);
            this.cboMachineID.Name = "cboMachineID";
            this.cboMachineID.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboMachineID.Size = new System.Drawing.Size(275, 31);
            this.cboMachineID.TabIndex = 213;
            this.cboMachineID.DropDown += new System.EventHandler(this.cboMachine_DropDown);
            this.cboMachineID.SelectedIndexChanged += new System.EventHandler(this.cboMachineID_SelectedIndexChanged);
            // 
            // cboProcess
            // 
            this.cboProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProcess.Font = new System.Drawing.Font("맑은 고딕", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboProcess.FormattingEnabled = true;
            this.cboProcess.IntegralHeight = false;
            this.cboProcess.Location = new System.Drawing.Point(123, 3);
            this.cboProcess.MaxDropDownItems = 5;
            this.cboProcess.MinimumSize = new System.Drawing.Size(100, 0);
            this.cboProcess.Name = "cboProcess";
            this.cboProcess.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboProcess.Size = new System.Drawing.Size(275, 31);
            this.cboProcess.TabIndex = 213;
            this.cboProcess.SelectedIndexChanged += new System.EventHandler(this.cboProcess_SelectedIndexChanged);
            // 
            // tlpForm
            // 
            this.tlpForm.ColumnCount = 2;
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpForm.Controls.Add(this.panel3, 1, 0);
            this.tlpForm.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpForm.Location = new System.Drawing.Point(12, 12);
            this.tlpForm.Name = "tlpForm";
            this.tlpForm.RowCount = 1;
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForm.Size = new System.Drawing.Size(980, 586);
            this.tlpForm.TabIndex = 220;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.tlpRight);
            this.panel3.Location = new System.Drawing.Point(836, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(141, 580);
            this.panel3.TabIndex = 221;
            // 
            // tlpRight
            // 
            this.tlpRight.ColumnCount = 1;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.Controls.Add(this.cmdSearch, 0, 0);
            this.tlpRight.Controls.Add(this.btnSave_OK, 0, 1);
            this.tlpRight.Controls.Add(this.cmdDelete, 0, 2);
            this.tlpRight.Controls.Add(this.cmdUp, 0, 3);
            this.tlpRight.Controls.Add(this.cmdDown, 0, 4);
            this.tlpRight.Controls.Add(this.cmdClose, 0, 5);
            this.tlpRight.Location = new System.Drawing.Point(-2, -4);
            this.tlpRight.Margin = new System.Windows.Forms.Padding(1);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 6;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRight.Size = new System.Drawing.Size(142, 581);
            this.tlpRight.TabIndex = 4;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdSearch.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdSearch.Image = global::WizWork.Properties.Resources.icons8_search_48;
            this.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSearch.Location = new System.Drawing.Point(2, 2);
            this.cmdSearch.Margin = new System.Windows.Forms.Padding(2);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(138, 92);
            this.cmdSearch.TabIndex = 194;
            this.cmdSearch.Text = "조회";
            this.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // btnSave_OK
            // 
            this.btnSave_OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave_OK.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave_OK.Image = global::WizWork.Properties.Resources.save_file_option1;
            this.btnSave_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave_OK.Location = new System.Drawing.Point(2, 98);
            this.btnSave_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave_OK.Name = "btnSave_OK";
            this.btnSave_OK.Size = new System.Drawing.Size(138, 92);
            this.btnSave_OK.TabIndex = 233;
            this.btnSave_OK.Text = "조치등록";
            this.btnSave_OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave_OK.UseVisualStyleBackColor = true;
            this.btnSave_OK.Click += new System.EventHandler(this.btnSave_OK_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdDelete.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdDelete.Image = global::WizWork.Properties.Resources.delete_button;
            this.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDelete.Location = new System.Drawing.Point(2, 194);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(2);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(138, 92);
            this.cmdDelete.TabIndex = 210;
            this.cmdDelete.Text = "삭제";
            this.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdUp
            // 
            this.cmdUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdUp.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdUp.Image = global::WizWork.Properties.Resources.up_arrow__1_;
            this.cmdUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdUp.Location = new System.Drawing.Point(2, 290);
            this.cmdUp.Margin = new System.Windows.Forms.Padding(2);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(138, 92);
            this.cmdUp.TabIndex = 207;
            this.cmdUp.Text = "위";
            this.cmdUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdUp.UseVisualStyleBackColor = true;
            this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
            // 
            // cmdDown
            // 
            this.cmdDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdDown.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdDown.Image = global::WizWork.Properties.Resources.down_arrow;
            this.cmdDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDown.Location = new System.Drawing.Point(2, 386);
            this.cmdDown.Margin = new System.Windows.Forms.Padding(2);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(138, 92);
            this.cmdDown.TabIndex = 208;
            this.cmdDown.Text = "아래";
            this.cmdDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDown.UseVisualStyleBackColor = true;
            this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdClose.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdClose.Image = global::WizWork.Properties.Resources.enter;
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.Location = new System.Drawing.Point(2, 482);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(2);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(138, 97);
            this.cmdClose.TabIndex = 219;
            this.cmdClose.Text = "닫기";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.grdList, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(827, 580);
            this.tableLayoutPanel2.TabIndex = 222;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(821, 110);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel10, 1, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 47);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(815, 60);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel9.Controls.Add(this.chkMachine, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.chkprocess, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.cboMachineID, 1, 1);
            this.tableLayoutPanel9.Controls.Add(this.cboProcess, 1, 0);
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(401, 54);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // chkMachine
            // 
            this.chkMachine.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkMachine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkMachine.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkMachine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMachine.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkMachine.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkMachine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMachine.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkMachine.ForeColor = System.Drawing.Color.White;
            this.chkMachine.Location = new System.Drawing.Point(2, 29);
            this.chkMachine.Margin = new System.Windows.Forms.Padding(2);
            this.chkMachine.Name = "chkMachine";
            this.chkMachine.Size = new System.Drawing.Size(116, 23);
            this.chkMachine.TabIndex = 215;
            this.chkMachine.Text = "호  기";
            this.chkMachine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMachine.UseVisualStyleBackColor = false;
            // 
            // chkprocess
            // 
            this.chkprocess.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkprocess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkprocess.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkprocess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkprocess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkprocess.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkprocess.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkprocess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkprocess.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkprocess.ForeColor = System.Drawing.Color.White;
            this.chkprocess.Location = new System.Drawing.Point(2, 2);
            this.chkprocess.Margin = new System.Windows.Forms.Padding(2);
            this.chkprocess.Name = "chkprocess";
            this.chkprocess.Size = new System.Drawing.Size(116, 23);
            this.chkprocess.TabIndex = 214;
            this.chkprocess.Text = "공  정";
            this.chkprocess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkprocess.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel10.Controls.Add(this.chkState, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.cboState, 1, 0);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(410, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(402, 54);
            this.tableLayoutPanel10.TabIndex = 1;
            // 
            // chkState
            // 
            this.chkState.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkState.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkState.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkState.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkState.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkState.ForeColor = System.Drawing.Color.White;
            this.chkState.Location = new System.Drawing.Point(2, 2);
            this.chkState.Margin = new System.Windows.Forms.Padding(2);
            this.chkState.Name = "chkState";
            this.chkState.Size = new System.Drawing.Size(116, 50);
            this.chkState.TabIndex = 214;
            this.chkState.Text = "처리상태";
            this.chkState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkState.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel7.Controls.Add(this.chkDate, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel11, 1, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(812, 38);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.tlp_To, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.tlp_From, 0, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(246, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(563, 32);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdList.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(3, 119);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grdList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.grdList.RowTemplate.Height = 23;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(821, 371);
            this.grdList.TabIndex = 157;
            this.grdList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.CallSumAll, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CallSumYN, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCallSumAll, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCallSumYN, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 496);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(821, 81);
            this.tableLayoutPanel1.TabIndex = 158;
            // 
            // CallSumAll
            // 
            this.CallSumAll.BackColor = System.Drawing.Color.White;
            this.CallSumAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CallSumAll.Enabled = false;
            this.CallSumAll.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CallSumAll.Location = new System.Drawing.Point(3, 3);
            this.CallSumAll.Name = "CallSumAll";
            this.CallSumAll.Size = new System.Drawing.Size(404, 26);
            this.CallSumAll.TabIndex = 0;
            this.CallSumAll.Text = "합계건수";
            this.CallSumAll.UseVisualStyleBackColor = false;
            // 
            // CallSumYN
            // 
            this.CallSumYN.BackColor = System.Drawing.Color.White;
            this.CallSumYN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CallSumYN.Enabled = false;
            this.CallSumYN.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CallSumYN.Location = new System.Drawing.Point(413, 3);
            this.CallSumYN.Name = "CallSumYN";
            this.CallSumYN.Size = new System.Drawing.Size(405, 26);
            this.CallSumYN.TabIndex = 1;
            this.CallSumYN.Text = "처리건수";
            this.CallSumYN.UseVisualStyleBackColor = false;
            // 
            // txtCallSumAll
            // 
            this.txtCallSumAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCallSumAll.Font = new System.Drawing.Font("맑은 고딕", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCallSumAll.Location = new System.Drawing.Point(3, 35);
            this.txtCallSumAll.Name = "txtCallSumAll";
            this.txtCallSumAll.ReadOnly = true;
            this.txtCallSumAll.Size = new System.Drawing.Size(404, 46);
            this.txtCallSumAll.TabIndex = 2;
            this.txtCallSumAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCallSumYN
            // 
            this.txtCallSumYN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCallSumYN.Font = new System.Drawing.Font("맑은 고딕", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCallSumYN.Location = new System.Drawing.Point(413, 35);
            this.txtCallSumYN.Name = "txtCallSumYN";
            this.txtCallSumYN.ReadOnly = true;
            this.txtCallSumYN.Size = new System.Drawing.Size(405, 46);
            this.txtCallSumYN.TabIndex = 3;
            this.txtCallSumYN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSave_No
            // 
            this.btnSave_No.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave_No.Image = global::WizWork.Properties.Resources.save_file_option1;
            this.btnSave_No.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave_No.Location = new System.Drawing.Point(997, 12);
            this.btnSave_No.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave_No.Name = "btnSave_No";
            this.btnSave_No.Size = new System.Drawing.Size(138, 65);
            this.btnSave_No.TabIndex = 234;
            this.btnSave_No.Text = "미조치\r\n등록";
            this.btnSave_No.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave_No.UseVisualStyleBackColor = true;
            this.btnSave_No.Visible = false;
            this.btnSave_No.Click += new System.EventHandler(this.btnSave_No_Click);
            // 
            // frm_tprc_WorkCall_Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 601);
            this.Controls.Add(this.tlpForm);
            this.Controls.Add(this.btnSave_No);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_tprc_WorkCall_Q";
            this.Text = "자주검사실적조회";
            this.Load += new System.EventHandler(this.frm_tins_InspectAutoResult_Q_Load);
            this.tlp_To.ResumeLayout(false);
            this.tlp_To.PerformLayout();
            this.tlp_From.ResumeLayout(false);
            this.tlp_From.PerformLayout();
            this.tlpForm.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tlpRight.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpForm;
        private System.Windows.Forms.TableLayoutPanel tlp_To;
        private System.Windows.Forms.Button btnCal_To;
        private System.Windows.Forms.MaskedTextBox mtb_To;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.TableLayoutPanel tlp_From;
        private System.Windows.Forms.Button btnCal_From;
        private System.Windows.Forms.MaskedTextBox mtb_From;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Button cmdUp;
        private System.Windows.Forms.Button cmdDown;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button btnSave_OK;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.ComboBox cboMachineID;
        private System.Windows.Forms.ComboBox cboState;
        private System.Windows.Forms.ComboBox cboProcess;
        private System.Windows.Forms.Button btnSave_No;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button CallSumAll;
        private System.Windows.Forms.Button CallSumYN;
        private System.Windows.Forms.TextBox txtCallSumAll;
        private System.Windows.Forms.TextBox txtCallSumYN;
        private System.Windows.Forms.CheckBox chkMachine;
        private System.Windows.Forms.CheckBox chkprocess;
        private System.Windows.Forms.CheckBox chkState;
    }
}