namespace WizInOut
{
    partial class Frm_tinout_OutWareScan_Q
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
            this.txtArticle = new System.Windows.Forms.TextBox();
            this.chkArticle = new System.Windows.Forms.CheckBox();
            this.cboOutClss = new System.Windows.Forms.ComboBox();
            this.chkOutClss = new System.Windows.Forms.CheckBox();
            this.btnLabelList = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.chkCustom = new System.Windows.Forms.CheckBox();
            this.chkODate = new System.Windows.Forms.CheckBox();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.mtb_To = new System.Windows.Forms.MaskedTextBox();
            this.btnCal_To = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnFillGrid = new System.Windows.Forms.Button();
            this.txtCustom = new System.Windows.Forms.TextBox();
            this.dgvOutware = new System.Windows.Forms.DataGridView();
            this.mtb_From = new System.Windows.Forms.MaskedTextBox();
            this.btnCal_From = new System.Windows.Forms.Button();
            this.chkOrderID = new System.Windows.Forms.CheckBox();
            this.chkBuyerArticleNo = new System.Windows.Forms.CheckBox();
            this.btnReprint = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtBuyerArticleNo = new System.Windows.Forms.TextBox();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.txtArticleTag = new System.Windows.Forms.TextBox();
            this.txtCustomTag = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutware)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.txtArticle);
            this.splitContainer1.Panel2.Controls.Add(this.chkArticle);
            this.splitContainer1.Panel2.Controls.Add(this.cboOutClss);
            this.splitContainer1.Panel2.Controls.Add(this.chkOutClss);
            this.splitContainer1.Panel2.Controls.Add(this.btnLabelList);
            this.splitContainer1.Panel2.Controls.Add(this.lblTitle);
            this.splitContainer1.Panel2.Controls.Add(this.chkCustom);
            this.splitContainer1.Panel2.Controls.Add(this.chkODate);
            this.splitContainer1.Panel2.Controls.Add(this.btnAll);
            this.splitContainer1.Panel2.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel2.Controls.Add(this.mtb_To);
            this.splitContainer1.Panel2.Controls.Add(this.btnCal_To);
            this.splitContainer1.Panel2.Controls.Add(this.btnClose);
            this.splitContainer1.Panel2.Controls.Add(this.btnFillGrid);
            this.splitContainer1.Panel2.Controls.Add(this.txtCustom);
            this.splitContainer1.Panel2.Controls.Add(this.dgvOutware);
            this.splitContainer1.Panel2.Controls.Add(this.mtb_From);
            this.splitContainer1.Panel2.Controls.Add(this.btnCal_From);
            this.splitContainer1.Size = new System.Drawing.Size(1012, 592);
            this.splitContainer1.SplitterDistance = 67;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtArticle
            // 
            this.txtArticle.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtArticle.Location = new System.Drawing.Point(414, 96);
            this.txtArticle.Name = "txtArticle";
            this.txtArticle.ReadOnly = true;
            this.txtArticle.Size = new System.Drawing.Size(230, 39);
            this.txtArticle.TabIndex = 285;
            this.txtArticle.Click += new System.EventHandler(this.txtArticle_Click);
            // 
            // chkArticle
            // 
            this.chkArticle.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkArticle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkArticle.BackgroundImage = global::WizInOut.Properties.Resources.Check_32pix;
            this.chkArticle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkArticle.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkArticle.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkArticle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkArticle.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkArticle.ForeColor = System.Drawing.Color.White;
            this.chkArticle.Location = new System.Drawing.Point(328, 96);
            this.chkArticle.Name = "chkArticle";
            this.chkArticle.Size = new System.Drawing.Size(80, 39);
            this.chkArticle.TabIndex = 284;
            this.chkArticle.Text = "품명";
            this.chkArticle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkArticle.UseVisualStyleBackColor = false;
            this.chkArticle.Click += new System.EventHandler(this.chkArticle_Click);
            // 
            // cboOutClss
            // 
            this.cboOutClss.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOutClss.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboOutClss.FormattingEnabled = true;
            this.cboOutClss.Location = new System.Drawing.Point(736, 51);
            this.cboOutClss.Name = "cboOutClss";
            this.cboOutClss.Size = new System.Drawing.Size(167, 40);
            this.cboOutClss.TabIndex = 283;
            // 
            // chkOutClss
            // 
            this.chkOutClss.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkOutClss.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkOutClss.BackgroundImage = global::WizInOut.Properties.Resources.Check_32pix;
            this.chkOutClss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkOutClss.Checked = true;
            this.chkOutClss.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOutClss.Enabled = false;
            this.chkOutClss.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkOutClss.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkOutClss.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOutClss.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkOutClss.ForeColor = System.Drawing.Color.White;
            this.chkOutClss.Location = new System.Drawing.Point(650, 51);
            this.chkOutClss.Name = "chkOutClss";
            this.chkOutClss.Size = new System.Drawing.Size(80, 39);
            this.chkOutClss.TabIndex = 282;
            this.chkOutClss.Text = "출고구분";
            this.chkOutClss.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkOutClss.UseVisualStyleBackColor = false;
            // 
            // btnLabelList
            // 
            this.btnLabelList.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLabelList.Location = new System.Drawing.Point(909, 321);
            this.btnLabelList.Name = "btnLabelList";
            this.btnLabelList.Size = new System.Drawing.Size(100, 80);
            this.btnLabelList.TabIndex = 281;
            this.btnLabelList.Text = "상세라벨\r\n리스트";
            this.btnLabelList.UseVisualStyleBackColor = true;
            this.btnLabelList.Click += new System.EventHandler(this.btnLabelList_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(8, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1001, 39);
            this.lblTitle.TabIndex = 280;
            this.lblTitle.Text = "제 품 출 고 조 회";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkCustom
            // 
            this.chkCustom.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCustom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkCustom.BackgroundImage = global::WizInOut.Properties.Resources.Check_32pix;
            this.chkCustom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkCustom.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkCustom.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkCustom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCustom.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkCustom.ForeColor = System.Drawing.Color.White;
            this.chkCustom.Location = new System.Drawing.Point(328, 51);
            this.chkCustom.Name = "chkCustom";
            this.chkCustom.Size = new System.Drawing.Size(80, 39);
            this.chkCustom.TabIndex = 279;
            this.chkCustom.Text = "납품거래처";
            this.chkCustom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkCustom.UseVisualStyleBackColor = false;
            this.chkCustom.Click += new System.EventHandler(this.chkCustom_Click);
            // 
            // chkODate
            // 
            this.chkODate.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkODate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkODate.BackgroundImage = global::WizInOut.Properties.Resources.Check_32pix;
            this.chkODate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkODate.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkODate.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkODate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkODate.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkODate.ForeColor = System.Drawing.Color.White;
            this.chkODate.Location = new System.Drawing.Point(10, 51);
            this.chkODate.Name = "chkODate";
            this.chkODate.Size = new System.Drawing.Size(80, 39);
            this.chkODate.TabIndex = 276;
            this.chkODate.Text = "출고일자";
            this.chkODate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkODate.UseVisualStyleBackColor = false;
            // 
            // btnAll
            // 
            this.btnAll.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAll.Location = new System.Drawing.Point(909, 141);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(100, 80);
            this.btnAll.TabIndex = 275;
            this.btnAll.Text = "전체선택";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelete.Location = new System.Drawing.Point(909, 231);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 80);
            this.btnDelete.TabIndex = 273;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // mtb_To
            // 
            this.mtb_To.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.mtb_To.Location = new System.Drawing.Point(94, 96);
            this.mtb_To.Margin = new System.Windows.Forms.Padding(1);
            this.mtb_To.Mask = "0000-00-00";
            this.mtb_To.Name = "mtb_To";
            this.mtb_To.ReadOnly = true;
            this.mtb_To.Size = new System.Drawing.Size(180, 39);
            this.mtb_To.TabIndex = 271;
            this.mtb_To.TabStop = false;
            this.mtb_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_To.ValidatingType = typeof(System.DateTime);
            this.mtb_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // btnCal_To
            // 
            this.btnCal_To.Image = global::WizInOut.Properties.Resources.calendar__2_;
            this.btnCal_To.Location = new System.Drawing.Point(276, 96);
            this.btnCal_To.Margin = new System.Windows.Forms.Padding(1);
            this.btnCal_To.Name = "btnCal_To";
            this.btnCal_To.Size = new System.Drawing.Size(48, 39);
            this.btnCal_To.TabIndex = 270;
            this.btnCal_To.UseVisualStyleBackColor = true;
            this.btnCal_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.Location = new System.Drawing.Point(909, 503);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 80);
            this.btnClose.TabIndex = 269;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnFillGrid
            // 
            this.btnFillGrid.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFillGrid.Location = new System.Drawing.Point(909, 51);
            this.btnFillGrid.Name = "btnFillGrid";
            this.btnFillGrid.Size = new System.Drawing.Size(100, 80);
            this.btnFillGrid.TabIndex = 268;
            this.btnFillGrid.Text = "조회";
            this.btnFillGrid.UseVisualStyleBackColor = true;
            this.btnFillGrid.Click += new System.EventHandler(this.btnFillGrid_Click);
            // 
            // txtCustom
            // 
            this.txtCustom.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCustom.Location = new System.Drawing.Point(414, 51);
            this.txtCustom.Name = "txtCustom";
            this.txtCustom.ReadOnly = true;
            this.txtCustom.Size = new System.Drawing.Size(230, 39);
            this.txtCustom.TabIndex = 259;
            this.txtCustom.Click += new System.EventHandler(this.txtCustom_Click);
            // 
            // dgvOutware
            // 
            this.dgvOutware.AllowUserToAddRows = false;
            this.dgvOutware.AllowUserToDeleteRows = false;
            this.dgvOutware.AllowUserToResizeRows = false;
            this.dgvOutware.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutware.Location = new System.Drawing.Point(10, 139);
            this.dgvOutware.Name = "dgvOutware";
            this.dgvOutware.RowHeadersVisible = false;
            this.dgvOutware.RowTemplate.Height = 23;
            this.dgvOutware.Size = new System.Drawing.Size(893, 444);
            this.dgvOutware.TabIndex = 247;
            this.dgvOutware.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOutware_CellClick);
            // 
            // mtb_From
            // 
            this.mtb_From.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.mtb_From.Location = new System.Drawing.Point(94, 51);
            this.mtb_From.Margin = new System.Windows.Forms.Padding(1);
            this.mtb_From.Mask = "0000-00-00";
            this.mtb_From.Name = "mtb_From";
            this.mtb_From.ReadOnly = true;
            this.mtb_From.Size = new System.Drawing.Size(180, 39);
            this.mtb_From.TabIndex = 205;
            this.mtb_From.TabStop = false;
            this.mtb_From.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_From.ValidatingType = typeof(System.DateTime);
            this.mtb_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // btnCal_From
            // 
            this.btnCal_From.Image = global::WizInOut.Properties.Resources.calendar__2_;
            this.btnCal_From.Location = new System.Drawing.Point(276, 51);
            this.btnCal_From.Margin = new System.Windows.Forms.Padding(1);
            this.btnCal_From.Name = "btnCal_From";
            this.btnCal_From.Size = new System.Drawing.Size(48, 39);
            this.btnCal_From.TabIndex = 0;
            this.btnCal_From.UseVisualStyleBackColor = true;
            this.btnCal_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // chkOrderID
            // 
            this.chkOrderID.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkOrderID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkOrderID.BackgroundImage = global::WizInOut.Properties.Resources.Check_32pix;
            this.chkOrderID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkOrderID.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkOrderID.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkOrderID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOrderID.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkOrderID.ForeColor = System.Drawing.Color.White;
            this.chkOrderID.Location = new System.Drawing.Point(1018, 169);
            this.chkOrderID.Name = "chkOrderID";
            this.chkOrderID.Size = new System.Drawing.Size(80, 39);
            this.chkOrderID.TabIndex = 278;
            this.chkOrderID.Text = "관리번호";
            this.chkOrderID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkOrderID.UseVisualStyleBackColor = false;
            this.chkOrderID.Visible = false;
            this.chkOrderID.Click += new System.EventHandler(this.chkOrderID_Click);
            // 
            // chkBuyerArticleNo
            // 
            this.chkBuyerArticleNo.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBuyerArticleNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkBuyerArticleNo.BackgroundImage = global::WizInOut.Properties.Resources.Check_32pix;
            this.chkBuyerArticleNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkBuyerArticleNo.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkBuyerArticleNo.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkBuyerArticleNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBuyerArticleNo.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkBuyerArticleNo.ForeColor = System.Drawing.Color.White;
            this.chkBuyerArticleNo.Location = new System.Drawing.Point(1018, 214);
            this.chkBuyerArticleNo.Name = "chkBuyerArticleNo";
            this.chkBuyerArticleNo.Size = new System.Drawing.Size(80, 39);
            this.chkBuyerArticleNo.TabIndex = 277;
            this.chkBuyerArticleNo.Text = "품번";
            this.chkBuyerArticleNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBuyerArticleNo.UseVisualStyleBackColor = false;
            this.chkBuyerArticleNo.Visible = false;
            this.chkBuyerArticleNo.Click += new System.EventHandler(this.chkBuyerArticleNo_Click);
            // 
            // btnReprint
            // 
            this.btnReprint.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnReprint.Location = new System.Drawing.Point(1018, 113);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(100, 50);
            this.btnReprint.TabIndex = 274;
            this.btnReprint.Text = "재발행";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUpdate.Location = new System.Drawing.Point(1018, 57);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 50);
            this.btnUpdate.TabIndex = 272;
            this.btnUpdate.Text = "수정";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtBuyerArticleNo
            // 
            this.txtBuyerArticleNo.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtBuyerArticleNo.Location = new System.Drawing.Point(1104, 214);
            this.txtBuyerArticleNo.Name = "txtBuyerArticleNo";
            this.txtBuyerArticleNo.ReadOnly = true;
            this.txtBuyerArticleNo.Size = new System.Drawing.Size(80, 39);
            this.txtBuyerArticleNo.TabIndex = 265;
            this.txtBuyerArticleNo.Visible = false;
            this.txtBuyerArticleNo.Click += new System.EventHandler(this.txtBuyerArticleNo_Click);
            // 
            // txtOrderID
            // 
            this.txtOrderID.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOrderID.Location = new System.Drawing.Point(1104, 169);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.ReadOnly = true;
            this.txtOrderID.Size = new System.Drawing.Size(80, 39);
            this.txtOrderID.TabIndex = 207;
            this.txtOrderID.Visible = false;
            this.txtOrderID.Click += new System.EventHandler(this.txtOrderID_Click);
            // 
            // txtArticleTag
            // 
            this.txtArticleTag.Location = new System.Drawing.Point(1018, 3);
            this.txtArticleTag.Name = "txtArticleTag";
            this.txtArticleTag.Size = new System.Drawing.Size(100, 21);
            this.txtArticleTag.TabIndex = 1;
            this.txtArticleTag.Visible = false;
            // 
            // txtCustomTag
            // 
            this.txtCustomTag.Location = new System.Drawing.Point(1018, 30);
            this.txtCustomTag.Name = "txtCustomTag";
            this.txtCustomTag.Size = new System.Drawing.Size(100, 21);
            this.txtCustomTag.TabIndex = 2;
            this.txtCustomTag.Visible = false;
            // 
            // Frm_tinout_OutWareScan_Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 595);
            this.Controls.Add(this.txtCustomTag);
            this.Controls.Add(this.txtArticleTag);
            this.Controls.Add(this.chkOrderID);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.chkBuyerArticleNo);
            this.Controls.Add(this.btnReprint);
            this.Controls.Add(this.txtOrderID);
            this.Controls.Add(this.txtBuyerArticleNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm_tinout_OutWareScan_Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Frm_tinout_OutWareScan_Q_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutware)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MaskedTextBox mtb_From;
        private System.Windows.Forms.Button btnCal_From;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.DataGridView dgvOutware;
        private System.Windows.Forms.TextBox txtBuyerArticleNo;
        private System.Windows.Forms.TextBox txtCustom;
        private System.Windows.Forms.Button btnFillGrid;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.MaskedTextBox mtb_To;
        private System.Windows.Forms.Button btnCal_To;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.CheckBox chkCustom;
        private System.Windows.Forms.CheckBox chkOrderID;
        private System.Windows.Forms.CheckBox chkBuyerArticleNo;
        private System.Windows.Forms.CheckBox chkODate;
        private System.Windows.Forms.TextBox txtArticleTag;
        private System.Windows.Forms.TextBox txtCustomTag;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnLabelList;
        private System.Windows.Forms.ComboBox cboOutClss;
        private System.Windows.Forms.CheckBox chkOutClss;
        private System.Windows.Forms.TextBox txtArticle;
        private System.Windows.Forms.CheckBox chkArticle;
    }
}