
namespace WizInOut
{
    partial class Frm_tinout_Info
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
            this.tlp_Info = new System.Windows.Forms.TableLayoutPanel();
            this.p_lbl_Notice = new System.Windows.Forms.Label();
            this.p_txt_Notice = new System.Windows.Forms.TextBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblWeb = new System.Windows.Forms.Label();
            this.lblComTel = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tlp_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Info
            // 
            this.tlp_Info.ColumnCount = 6;
            this.tlp_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlp_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlp_Info.Controls.Add(this.lblName, 0, 0);
            this.tlp_Info.Controls.Add(this.p_lbl_Notice, 0, 1);
            this.tlp_Info.Controls.Add(this.p_txt_Notice, 0, 2);
            this.tlp_Info.Controls.Add(this.lblCompany, 1, 3);
            this.tlp_Info.Controls.Add(this.lblWeb, 1, 4);
            this.tlp_Info.Controls.Add(this.lblComTel, 2, 4);
            this.tlp_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Info.Location = new System.Drawing.Point(0, 0);
            this.tlp_Info.Name = "tlp_Info";
            this.tlp_Info.RowCount = 6;
            this.tlp_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlp_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlp_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlp_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlp_Info.Size = new System.Drawing.Size(996, 620);
            this.tlp_Info.TabIndex = 0;
            // 
            // p_lbl_Notice
            // 
            this.p_lbl_Notice.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.p_lbl_Notice.Dock = System.Windows.Forms.DockStyle.Left;
            this.p_lbl_Notice.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.p_lbl_Notice.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.p_lbl_Notice.Location = new System.Drawing.Point(3, 124);
            this.p_lbl_Notice.Name = "p_lbl_Notice";
            this.p_lbl_Notice.Size = new System.Drawing.Size(193, 31);
            this.p_lbl_Notice.TabIndex = 1;
            this.p_lbl_Notice.Text = "공지사항";
            this.p_lbl_Notice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // p_txt_Notice
            // 
            this.p_txt_Notice.BackColor = System.Drawing.SystemColors.Window;
            this.p_txt_Notice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_txt_Notice.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.p_txt_Notice.Location = new System.Drawing.Point(3, 158);
            this.p_txt_Notice.Multiline = true;
            this.p_txt_Notice.Name = "p_txt_Notice";
            this.p_txt_Notice.ReadOnly = true;
            this.p_txt_Notice.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.p_txt_Notice.Size = new System.Drawing.Size(193, 335);
            this.p_txt_Notice.TabIndex = 2;
            // 
            // lblCompany
            // 
            this.lblCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCompany.Font = new System.Drawing.Font("맑은 고딕", 16.25F);
            this.lblCompany.Location = new System.Drawing.Point(202, 496);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(243, 62);
            this.lblCompany.TabIndex = 3;
            this.lblCompany.Text = "(주)위저드 정보시스템";
            this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWeb
            // 
            this.lblWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWeb.Font = new System.Drawing.Font("맑은 고딕", 14.25F);
            this.lblWeb.Location = new System.Drawing.Point(202, 558);
            this.lblWeb.Name = "lblWeb";
            this.lblWeb.Size = new System.Drawing.Size(243, 31);
            this.lblWeb.TabIndex = 4;
            this.lblWeb.Text = "http://www.wizis.co.kr";
            this.lblWeb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblComTel
            // 
            this.lblComTel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblComTel.Font = new System.Drawing.Font("맑은 고딕", 14.25F);
            this.lblComTel.Location = new System.Drawing.Point(451, 558);
            this.lblComTel.Name = "lblComTel";
            this.lblComTel.Size = new System.Drawing.Size(243, 31);
            this.lblComTel.TabIndex = 5;
            this.lblComTel.Text = "사무실 : 053-355-0935~6";
            this.lblComTel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.SystemColors.Control;
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Font = new System.Drawing.Font("맑은 고딕", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblName.Location = new System.Drawing.Point(3, 3);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(196, 118);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "입고/출고 관리 시스템 - WizInOut";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Frm_tinout_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 620);
            this.Controls.Add(this.tlp_Info);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_tinout_Info";
            this.Activated += new System.EventHandler(this.Frm_tinout_Info_Activated);
            this.Load += new System.EventHandler(this.Frm_tinout_Info_Load);
            this.tlp_Info.ResumeLayout(false);
            this.tlp_Info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Info;
        private System.Windows.Forms.Label p_lbl_Notice;
        private System.Windows.Forms.TextBox p_txt_Notice;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblWeb;
        private System.Windows.Forms.Label lblComTel;
        private System.Windows.Forms.Label lblName;
    }
}