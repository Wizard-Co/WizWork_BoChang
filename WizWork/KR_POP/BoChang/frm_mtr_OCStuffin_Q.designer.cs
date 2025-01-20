namespace WizWork
{
    partial class frm_mtr_OCStuffin_Q
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnInspectSub = new System.Windows.Forms.Button();
            this.cboSGbn = new System.Windows.Forms.ComboBox();
            this.btnAll = new System.Windows.Forms.Button();
            this.chkSGbn = new System.Windows.Forms.CheckBox();
            this.chkCustom = new System.Windows.Forms.CheckBox();
            this.chkSDate = new System.Windows.Forms.CheckBox();
            this.tlpSumQty = new System.Windows.Forms.TableLayoutPanel();
            this.lblSumSQty = new System.Windows.Forms.Label();
            this.lblSumQty = new System.Windows.Forms.Label();
            this.txtSumQty = new System.Windows.Forms.TextBox();
            this.txtSumSQty = new System.Windows.Forms.TextBox();
            this.txtCustom = new System.Windows.Forms.TextBox();
            this.mtb_To = new System.Windows.Forms.MaskedTextBox();
            this.btnCal_To = new System.Windows.Forms.Button();
            this.mtb_From = new System.Windows.Forms.MaskedTextBox();
            this.btnCal_From = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvStuffin = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnFillGrid = new System.Windows.Forms.Button();
            this.chkBuyerArticleNo = new System.Windows.Forms.CheckBox();
            this.txtBuyerArticleNo = new System.Windows.Forms.TextBox();
            this.btnReprint = new System.Windows.Forms.Button();
            this.txtSGbn = new System.Windows.Forms.TextBox();
            this.txtArticleGbn = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtArticleTag = new System.Windows.Forms.TextBox();
            this.txtCustomTag = new System.Windows.Forms.TextBox();
            this.txtSGbnTag = new System.Windows.Forms.TextBox();
            this.txtArticleGbnTag = new System.Windows.Forms.TextBox();
            this.chkArticleGbn = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tlpSumQty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStuffin)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.lblTitle);
            this.splitContainer1.Panel2.Controls.Add(this.btnInspectSub);
            this.splitContainer1.Panel2.Controls.Add(this.cboSGbn);
            this.splitContainer1.Panel2.Controls.Add(this.btnAll);
            this.splitContainer1.Panel2.Controls.Add(this.chkSGbn);
            this.splitContainer1.Panel2.Controls.Add(this.chkCustom);
            this.splitContainer1.Panel2.Controls.Add(this.chkSDate);
            this.splitContainer1.Panel2.Controls.Add(this.tlpSumQty);
            this.splitContainer1.Panel2.Controls.Add(this.txtCustom);
            this.splitContainer1.Panel2.Controls.Add(this.mtb_To);
            this.splitContainer1.Panel2.Controls.Add(this.btnCal_To);
            this.splitContainer1.Panel2.Controls.Add(this.mtb_From);
            this.splitContainer1.Panel2.Controls.Add(this.btnCal_From);
            this.splitContainer1.Panel2.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel2.Controls.Add(this.dgvStuffin);
            this.splitContainer1.Panel2.Controls.Add(this.btnClose);
            this.splitContainer1.Panel2.Controls.Add(this.btnFillGrid);
            this.splitContainer1.Size = new System.Drawing.Size(1012, 592);
            this.splitContainer1.SplitterDistance = 67;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtArticle
            // 
            this.txtArticle.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtArticle.Location = new System.Drawing.Point(402, 96);
            this.txtArticle.Name = "txtArticle";
            this.txtArticle.ReadOnly = true;
            this.txtArticle.Size = new System.Drawing.Size(230, 39);
            this.txtArticle.TabIndex = 274;
            this.txtArticle.Click += new System.EventHandler(this.txtArticle_Click);
            // 
            // chkArticle
            // 
            this.chkArticle.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkArticle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkArticle.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkArticle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkArticle.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkArticle.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkArticle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkArticle.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkArticle.ForeColor = System.Drawing.Color.White;
            this.chkArticle.Location = new System.Drawing.Point(323, 96);
            this.chkArticle.Name = "chkArticle";
            this.chkArticle.Size = new System.Drawing.Size(75, 39);
            this.chkArticle.TabIndex = 273;
            this.chkArticle.Text = "품명";
            this.chkArticle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkArticle.UseVisualStyleBackColor = false;
            this.chkArticle.Click += new System.EventHandler(this.chkArticle_Click);
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
            this.lblTitle.TabIndex = 272;
            this.lblTitle.Text = "자 재 입 고 조 회";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnInspectSub
            // 
            this.btnInspectSub.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInspectSub.Location = new System.Drawing.Point(909, 321);
            this.btnInspectSub.Name = "btnInspectSub";
            this.btnInspectSub.Size = new System.Drawing.Size(100, 80);
            this.btnInspectSub.TabIndex = 271;
            this.btnInspectSub.Text = "수입\r\n검사\r\n조회";
            this.btnInspectSub.UseVisualStyleBackColor = true;
            this.btnInspectSub.Click += new System.EventHandler(this.btnInspectSub_Click);
            // 
            // cboSGbn
            // 
            this.cboSGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSGbn.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboSGbn.FormattingEnabled = true;
            this.cboSGbn.Location = new System.Drawing.Point(719, 51);
            this.cboSGbn.Name = "cboSGbn";
            this.cboSGbn.Size = new System.Drawing.Size(184, 38);
            this.cboSGbn.TabIndex = 270;
            // 
            // btnAll
            // 
            this.btnAll.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAll.Location = new System.Drawing.Point(909, 141);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(100, 80);
            this.btnAll.TabIndex = 269;
            this.btnAll.Text = "전체선택";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.chkAll_Click);
            // 
            // chkSGbn
            // 
            this.chkSGbn.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSGbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkSGbn.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkSGbn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkSGbn.Checked = true;
            this.chkSGbn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSGbn.Enabled = false;
            this.chkSGbn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkSGbn.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkSGbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSGbn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkSGbn.ForeColor = System.Drawing.Color.White;
            this.chkSGbn.Location = new System.Drawing.Point(638, 51);
            this.chkSGbn.Name = "chkSGbn";
            this.chkSGbn.Size = new System.Drawing.Size(75, 39);
            this.chkSGbn.TabIndex = 268;
            this.chkSGbn.Text = "입고구분";
            this.chkSGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSGbn.UseVisualStyleBackColor = false;
            this.chkSGbn.Click += new System.EventHandler(this.chkSGbn_Click);
            // 
            // chkCustom
            // 
            this.chkCustom.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCustom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkCustom.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkCustom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkCustom.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkCustom.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkCustom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCustom.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkCustom.ForeColor = System.Drawing.Color.White;
            this.chkCustom.Location = new System.Drawing.Point(323, 51);
            this.chkCustom.Name = "chkCustom";
            this.chkCustom.Size = new System.Drawing.Size(75, 39);
            this.chkCustom.TabIndex = 266;
            this.chkCustom.Text = "거래처";
            this.chkCustom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkCustom.UseVisualStyleBackColor = false;
            this.chkCustom.Click += new System.EventHandler(this.chkCustom_Click);
            // 
            // chkSDate
            // 
            this.chkSDate.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkSDate.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkSDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkSDate.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkSDate.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkSDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSDate.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkSDate.ForeColor = System.Drawing.Color.White;
            this.chkSDate.Location = new System.Drawing.Point(10, 51);
            this.chkSDate.Name = "chkSDate";
            this.chkSDate.Size = new System.Drawing.Size(75, 39);
            this.chkSDate.TabIndex = 1;
            this.chkSDate.Text = "입고일자";
            this.chkSDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSDate.UseVisualStyleBackColor = false;
            // 
            // tlpSumQty
            // 
            this.tlpSumQty.ColumnCount = 4;
            this.tlpSumQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpSumQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpSumQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpSumQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpSumQty.Controls.Add(this.lblSumSQty, 0, 0);
            this.tlpSumQty.Controls.Add(this.lblSumQty, 2, 0);
            this.tlpSumQty.Controls.Add(this.txtSumQty, 3, 0);
            this.tlpSumQty.Controls.Add(this.txtSumSQty, 1, 0);
            this.tlpSumQty.Location = new System.Drawing.Point(10, 554);
            this.tlpSumQty.Name = "tlpSumQty";
            this.tlpSumQty.RowCount = 1;
            this.tlpSumQty.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSumQty.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpSumQty.Size = new System.Drawing.Size(893, 29);
            this.tlpSumQty.TabIndex = 264;
            // 
            // lblSumSQty
            // 
            this.lblSumSQty.BackColor = System.Drawing.Color.Black;
            this.lblSumSQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSumSQty.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSumSQty.ForeColor = System.Drawing.Color.White;
            this.lblSumSQty.Location = new System.Drawing.Point(3, 0);
            this.lblSumSQty.Name = "lblSumSQty";
            this.lblSumSQty.Size = new System.Drawing.Size(127, 29);
            this.lblSumSQty.TabIndex = 0;
            this.lblSumSQty.Text = "입고량";
            this.lblSumSQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSumQty
            // 
            this.lblSumQty.BackColor = System.Drawing.Color.Black;
            this.lblSumQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSumQty.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSumQty.ForeColor = System.Drawing.Color.White;
            this.lblSumQty.Location = new System.Drawing.Point(448, 0);
            this.lblSumQty.Name = "lblSumQty";
            this.lblSumQty.Size = new System.Drawing.Size(127, 29);
            this.lblSumQty.TabIndex = 1;
            this.lblSumQty.Text = "입고건수";
            this.lblSumQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSumQty
            // 
            this.txtSumQty.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSumQty.Location = new System.Drawing.Point(581, 3);
            this.txtSumQty.Name = "txtSumQty";
            this.txtSumQty.ReadOnly = true;
            this.txtSumQty.Size = new System.Drawing.Size(309, 25);
            this.txtSumQty.TabIndex = 3;
            this.txtSumQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSumSQty
            // 
            this.txtSumSQty.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSumSQty.Location = new System.Drawing.Point(136, 3);
            this.txtSumSQty.Name = "txtSumSQty";
            this.txtSumSQty.ReadOnly = true;
            this.txtSumSQty.Size = new System.Drawing.Size(306, 25);
            this.txtSumSQty.TabIndex = 2;
            this.txtSumSQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCustom
            // 
            this.txtCustom.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCustom.Location = new System.Drawing.Point(402, 51);
            this.txtCustom.Name = "txtCustom";
            this.txtCustom.ReadOnly = true;
            this.txtCustom.Size = new System.Drawing.Size(230, 39);
            this.txtCustom.TabIndex = 258;
            this.txtCustom.Click += new System.EventHandler(this.txtCustom_Click);
            // 
            // mtb_To
            // 
            this.mtb_To.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.mtb_To.Location = new System.Drawing.Point(89, 96);
            this.mtb_To.Margin = new System.Windows.Forms.Padding(1);
            this.mtb_To.Mask = "0000-00-00";
            this.mtb_To.Name = "mtb_To";
            this.mtb_To.ReadOnly = true;
            this.mtb_To.Size = new System.Drawing.Size(180, 39);
            this.mtb_To.TabIndex = 254;
            this.mtb_To.TabStop = false;
            this.mtb_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_To.ValidatingType = typeof(System.DateTime);
            this.mtb_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // btnCal_To
            // 
            this.btnCal_To.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnCal_To.Location = new System.Drawing.Point(271, 96);
            this.btnCal_To.Margin = new System.Windows.Forms.Padding(1);
            this.btnCal_To.Name = "btnCal_To";
            this.btnCal_To.Size = new System.Drawing.Size(48, 39);
            this.btnCal_To.TabIndex = 253;
            this.btnCal_To.UseVisualStyleBackColor = true;
            this.btnCal_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // mtb_From
            // 
            this.mtb_From.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.mtb_From.Location = new System.Drawing.Point(89, 51);
            this.mtb_From.Margin = new System.Windows.Forms.Padding(1);
            this.mtb_From.Mask = "0000-00-00";
            this.mtb_From.Name = "mtb_From";
            this.mtb_From.ReadOnly = true;
            this.mtb_From.Size = new System.Drawing.Size(180, 39);
            this.mtb_From.TabIndex = 251;
            this.mtb_From.TabStop = false;
            this.mtb_From.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_From.ValidatingType = typeof(System.DateTime);
            this.mtb_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // btnCal_From
            // 
            this.btnCal_From.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnCal_From.Location = new System.Drawing.Point(271, 51);
            this.btnCal_From.Margin = new System.Windows.Forms.Padding(1);
            this.btnCal_From.Name = "btnCal_From";
            this.btnCal_From.Size = new System.Drawing.Size(48, 39);
            this.btnCal_From.TabIndex = 250;
            this.btnCal_From.UseVisualStyleBackColor = true;
            this.btnCal_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelete.Location = new System.Drawing.Point(909, 231);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 80);
            this.btnDelete.TabIndex = 249;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvStuffin
            // 
            this.dgvStuffin.AllowUserToAddRows = false;
            this.dgvStuffin.AllowUserToDeleteRows = false;
            this.dgvStuffin.AllowUserToResizeRows = false;
            this.dgvStuffin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStuffin.Location = new System.Drawing.Point(10, 141);
            this.dgvStuffin.Name = "dgvStuffin";
            this.dgvStuffin.RowHeadersVisible = false;
            this.dgvStuffin.RowTemplate.Height = 23;
            this.dgvStuffin.Size = new System.Drawing.Size(893, 407);
            this.dgvStuffin.TabIndex = 247;
            this.dgvStuffin.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStuffin_CellClick);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.Location = new System.Drawing.Point(909, 502);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 80);
            this.btnClose.TabIndex = 245;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnFillGrid
            // 
            this.btnFillGrid.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFillGrid.Location = new System.Drawing.Point(909, 51);
            this.btnFillGrid.Name = "btnFillGrid";
            this.btnFillGrid.Size = new System.Drawing.Size(100, 80);
            this.btnFillGrid.TabIndex = 244;
            this.btnFillGrid.Text = "조회";
            this.btnFillGrid.UseVisualStyleBackColor = true;
            this.btnFillGrid.Click += new System.EventHandler(this.btnFillGrid_Click);
            // 
            // chkBuyerArticleNo
            // 
            this.chkBuyerArticleNo.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBuyerArticleNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkBuyerArticleNo.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkBuyerArticleNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkBuyerArticleNo.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkBuyerArticleNo.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkBuyerArticleNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBuyerArticleNo.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkBuyerArticleNo.ForeColor = System.Drawing.Color.White;
            this.chkBuyerArticleNo.Location = new System.Drawing.Point(1019, 362);
            this.chkBuyerArticleNo.Name = "chkBuyerArticleNo";
            this.chkBuyerArticleNo.Size = new System.Drawing.Size(75, 39);
            this.chkBuyerArticleNo.TabIndex = 265;
            this.chkBuyerArticleNo.Text = "품번";
            this.chkBuyerArticleNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBuyerArticleNo.UseVisualStyleBackColor = false;
            this.chkBuyerArticleNo.Click += new System.EventHandler(this.chkBuyerArticleNo_Click);
            // 
            // txtBuyerArticleNo
            // 
            this.txtBuyerArticleNo.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtBuyerArticleNo.Location = new System.Drawing.Point(1019, 407);
            this.txtBuyerArticleNo.Name = "txtBuyerArticleNo";
            this.txtBuyerArticleNo.ReadOnly = true;
            this.txtBuyerArticleNo.Size = new System.Drawing.Size(75, 39);
            this.txtBuyerArticleNo.TabIndex = 256;
            this.txtBuyerArticleNo.Click += new System.EventHandler(this.txtBuyerArticleNo_Click);
            // 
            // btnReprint
            // 
            this.btnReprint.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnReprint.Location = new System.Drawing.Point(1018, 117);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(100, 50);
            this.btnReprint.TabIndex = 263;
            this.btnReprint.Text = "재발행";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Visible = false;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // txtSGbn
            // 
            this.txtSGbn.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSGbn.Location = new System.Drawing.Point(1018, 319);
            this.txtSGbn.Name = "txtSGbn";
            this.txtSGbn.ReadOnly = true;
            this.txtSGbn.Size = new System.Drawing.Size(75, 39);
            this.txtSGbn.TabIndex = 262;
            this.txtSGbn.Visible = false;
            this.txtSGbn.Click += new System.EventHandler(this.txtSGbn_Click);
            // 
            // txtArticleGbn
            // 
            this.txtArticleGbn.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtArticleGbn.Location = new System.Drawing.Point(1019, 274);
            this.txtArticleGbn.Name = "txtArticleGbn";
            this.txtArticleGbn.ReadOnly = true;
            this.txtArticleGbn.Size = new System.Drawing.Size(74, 39);
            this.txtArticleGbn.TabIndex = 260;
            this.txtArticleGbn.Visible = false;
            this.txtArticleGbn.Click += new System.EventHandler(this.txtArticleGbn_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUpdate.Location = new System.Drawing.Point(1018, 173);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 50);
            this.btnUpdate.TabIndex = 248;
            this.btnUpdate.Text = "수정";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtArticleTag
            // 
            this.txtArticleTag.Location = new System.Drawing.Point(1018, 9);
            this.txtArticleTag.Name = "txtArticleTag";
            this.txtArticleTag.Size = new System.Drawing.Size(100, 21);
            this.txtArticleTag.TabIndex = 1;
            this.txtArticleTag.Visible = false;
            // 
            // txtCustomTag
            // 
            this.txtCustomTag.Location = new System.Drawing.Point(1018, 36);
            this.txtCustomTag.Name = "txtCustomTag";
            this.txtCustomTag.Size = new System.Drawing.Size(100, 21);
            this.txtCustomTag.TabIndex = 2;
            this.txtCustomTag.Visible = false;
            // 
            // txtSGbnTag
            // 
            this.txtSGbnTag.Location = new System.Drawing.Point(1018, 63);
            this.txtSGbnTag.Name = "txtSGbnTag";
            this.txtSGbnTag.Size = new System.Drawing.Size(100, 21);
            this.txtSGbnTag.TabIndex = 3;
            this.txtSGbnTag.Visible = false;
            // 
            // txtArticleGbnTag
            // 
            this.txtArticleGbnTag.Location = new System.Drawing.Point(1018, 90);
            this.txtArticleGbnTag.Name = "txtArticleGbnTag";
            this.txtArticleGbnTag.Size = new System.Drawing.Size(100, 21);
            this.txtArticleGbnTag.TabIndex = 4;
            this.txtArticleGbnTag.Visible = false;
            // 
            // chkArticleGbn
            // 
            this.chkArticleGbn.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkArticleGbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkArticleGbn.BackgroundImage = global::WizWork.Properties.Resources.Check_32pix;
            this.chkArticleGbn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkArticleGbn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkArticleGbn.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkArticleGbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkArticleGbn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkArticleGbn.ForeColor = System.Drawing.Color.White;
            this.chkArticleGbn.Location = new System.Drawing.Point(1018, 229);
            this.chkArticleGbn.Name = "chkArticleGbn";
            this.chkArticleGbn.Size = new System.Drawing.Size(75, 39);
            this.chkArticleGbn.TabIndex = 267;
            this.chkArticleGbn.Text = "품명그룹";
            this.chkArticleGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkArticleGbn.UseVisualStyleBackColor = false;
            this.chkArticleGbn.Visible = false;
            this.chkArticleGbn.Click += new System.EventHandler(this.chkArticleGbn_Click);
            // 
            // frm_mtr_OCStuffin_Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 595);
            this.Controls.Add(this.txtArticleGbnTag);
            this.Controls.Add(this.txtSGbnTag);
            this.Controls.Add(this.chkArticleGbn);
            this.Controls.Add(this.txtCustomTag);
            this.Controls.Add(this.txtArticleTag);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.chkBuyerArticleNo);
            this.Controls.Add(this.txtSGbn);
            this.Controls.Add(this.btnReprint);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtBuyerArticleNo);
            this.Controls.Add(this.txtArticleGbn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm_mtr_OCStuffin_Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frm_mtr_OCStuffin_Q_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tlpSumQty.ResumeLayout(false);
            this.tlpSumQty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStuffin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnFillGrid;
        private System.Windows.Forms.DataGridView dgvStuffin;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.MaskedTextBox mtb_From;
        private System.Windows.Forms.Button btnCal_From;
        private System.Windows.Forms.MaskedTextBox mtb_To;
        private System.Windows.Forms.Button btnCal_To;
        private System.Windows.Forms.TextBox txtSGbn;
        private System.Windows.Forms.TextBox txtArticleGbn;
        private System.Windows.Forms.TextBox txtCustom;
        private System.Windows.Forms.TextBox txtBuyerArticleNo;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.TableLayoutPanel tlpSumQty;
        private System.Windows.Forms.Label lblSumSQty;
        private System.Windows.Forms.Label lblSumQty;
        private System.Windows.Forms.TextBox txtSumSQty;
        private System.Windows.Forms.TextBox txtSumQty;
        private System.Windows.Forms.CheckBox chkSDate;
        private System.Windows.Forms.CheckBox chkSGbn;
        private System.Windows.Forms.CheckBox chkArticleGbn;
        private System.Windows.Forms.CheckBox chkCustom;
        private System.Windows.Forms.CheckBox chkBuyerArticleNo;
        private System.Windows.Forms.TextBox txtArticleTag;
        private System.Windows.Forms.TextBox txtCustomTag;
        private System.Windows.Forms.TextBox txtSGbnTag;
        private System.Windows.Forms.TextBox txtArticleGbnTag;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.ComboBox cboSGbn;
        private System.Windows.Forms.Button btnInspectSub;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtArticle;
        private System.Windows.Forms.CheckBox chkArticle;
    }
}