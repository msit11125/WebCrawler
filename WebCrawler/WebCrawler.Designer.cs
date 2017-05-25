namespace Crawler
{
    partial class WebCrawler
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebCrawler));
            this.btnCrawler = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.resultMsg = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtImport = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pointCrawlerCount = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.htmlDetail = new System.Windows.Forms.Button();
            this.chkBuildHttpClient = new System.Windows.Forms.CheckBox();
            this.btnInfo = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.numTimes = new System.Windows.Forms.NumericUpDown();
            this.resaultMsgDetail = new System.Windows.Forms.RichTextBox();
            this.resaultMsgDetail_BeforeRegex = new System.Windows.Forms.RichTextBox();
            this.pointErrorCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCrawler
            // 
            this.btnCrawler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrawler.Location = new System.Drawing.Point(843, 28);
            this.btnCrawler.Name = "btnCrawler";
            this.btnCrawler.Size = new System.Drawing.Size(111, 45);
            this.btnCrawler.TabIndex = 0;
            this.btnCrawler.Text = "Start Crawler";
            this.btnCrawler.UseVisualStyleBackColor = true;
            this.btnCrawler.Click += new System.EventHandler(this.btnCrawler_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "WebUrl";
            // 
            // txtUrl
            // 
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.Location = new System.Drawing.Point(70, 13);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(758, 22);
            this.txtUrl.TabIndex = 2;
            this.txtUrl.Text = "https://search.uspto.gov/search?affiliate=web-sdmg-uspto.gov&op=Search&page=1&que" +
    "ry=virtual+reality";
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(70, 43);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(758, 22);
            this.txtPath.TabIndex = 2;
            this.txtPath.Text = "\\WebCrawler\\WebCrawler\\bin\\Debug\\data";
            // 
            // resultMsg
            // 
            this.resultMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultMsg.BackColor = System.Drawing.SystemColors.Control;
            this.resultMsg.ForeColor = System.Drawing.SystemColors.InfoText;
            this.resultMsg.Location = new System.Drawing.Point(10, 131);
            this.resultMsg.MaxLength = 999999999;
            this.resultMsg.Multiline = true;
            this.resultMsg.Name = "resultMsg";
            this.resultMsg.ReadOnly = true;
            this.resultMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultMsg.Size = new System.Drawing.Size(725, 444);
            this.resultMsg.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(742, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Related Count Result";
            // 
            // txtImport
            // 
            this.txtImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImport.Location = new System.Drawing.Point(70, 74);
            this.txtImport.Name = "txtImport";
            this.txtImport.Size = new System.Drawing.Size(758, 22);
            this.txtImport.TabIndex = 6;
            this.txtImport.Text = " education& car & game";
            this.txtImport.TextChanged += new System.EventHandler(this.txtImport_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Keywords";
            // 
            // pointCrawlerCount
            // 
            this.pointCrawlerCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pointCrawlerCount.AutoSize = true;
            this.pointCrawlerCount.Location = new System.Drawing.Point(29, 582);
            this.pointCrawlerCount.Name = "pointCrawlerCount";
            this.pointCrawlerCount.Size = new System.Drawing.Size(121, 12);
            this.pointCrawlerCount.TabIndex = 4;
            this.pointCrawlerCount.Text = "0 / 0 Files Has Crawlered";
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(843, 79);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(111, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // htmlDetail
            // 
            this.htmlDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.htmlDetail.Location = new System.Drawing.Point(636, 576);
            this.htmlDetail.Name = "htmlDetail";
            this.htmlDetail.Size = new System.Drawing.Size(96, 22);
            this.htmlDetail.TabIndex = 8;
            this.htmlDetail.Text = "Text Detail >>";
            this.htmlDetail.UseVisualStyleBackColor = true;
            this.htmlDetail.Click += new System.EventHandler(this.htmlDetail_Click);
            // 
            // chkBuildHttpClient
            // 
            this.chkBuildHttpClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBuildHttpClient.AutoSize = true;
            this.chkBuildHttpClient.Location = new System.Drawing.Point(857, 9);
            this.chkBuildHttpClient.Name = "chkBuildHttpClient";
            this.chkBuildHttpClient.Size = new System.Drawing.Size(72, 16);
            this.chkBuildHttpClient.TabIndex = 10;
            this.chkBuildHttpClient.Text = "HttpClient";
            this.chkBuildHttpClient.UseVisualStyleBackColor = true;
            // 
            // btnInfo
            // 
            this.btnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInfo.BackColor = System.Drawing.Color.White;
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.ForeColor = System.Drawing.Color.Maroon;
            this.btnInfo.Location = new System.Drawing.Point(932, 5);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(20, 20);
            this.btnInfo.TabIndex = 13;
            this.btnInfo.Text = "?";
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(741, 132);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(215, 443);
            this.dataGridView1.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Times";
            // 
            // numTimes
            // 
            this.numTimes.Location = new System.Drawing.Point(70, 103);
            this.numTimes.Name = "numTimes";
            this.numTimes.Size = new System.Drawing.Size(40, 22);
            this.numTimes.TabIndex = 16;
            this.numTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // resaultMsgDetail
            // 
            this.resaultMsgDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resaultMsgDetail.Location = new System.Drawing.Point(10, 131);
            this.resaultMsgDetail.Name = "resaultMsgDetail";
            this.resaultMsgDetail.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.resaultMsgDetail.Size = new System.Drawing.Size(722, 233);
            this.resaultMsgDetail.TabIndex = 17;
            this.resaultMsgDetail.Text = "";
            this.resaultMsgDetail.Visible = false;
            // 
            // resaultMsgDetail_BeforeRegex
            // 
            this.resaultMsgDetail_BeforeRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resaultMsgDetail_BeforeRegex.Location = new System.Drawing.Point(10, 366);
            this.resaultMsgDetail_BeforeRegex.Name = "resaultMsgDetail_BeforeRegex";
            this.resaultMsgDetail_BeforeRegex.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.resaultMsgDetail_BeforeRegex.Size = new System.Drawing.Size(722, 205);
            this.resaultMsgDetail_BeforeRegex.TabIndex = 17;
            this.resaultMsgDetail_BeforeRegex.Text = "";
            this.resaultMsgDetail_BeforeRegex.Visible = false;
            // 
            // pointErrorCount
            // 
            this.pointErrorCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pointErrorCount.AutoSize = true;
            this.pointErrorCount.ForeColor = System.Drawing.Color.DarkRed;
            this.pointErrorCount.Location = new System.Drawing.Point(547, 582);
            this.pointErrorCount.Name = "pointErrorCount";
            this.pointErrorCount.Size = new System.Drawing.Size(75, 12);
            this.pointErrorCount.TabIndex = 18;
            this.pointErrorCount.Text = "Error Catch : 0";
            // 
            // WebCrawler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 605);
            this.Controls.Add(this.pointErrorCount);
            this.Controls.Add(this.resaultMsgDetail_BeforeRegex);
            this.Controls.Add(this.resaultMsgDetail);
            this.Controls.Add(this.numTimes);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.chkBuildHttpClient);
            this.Controls.Add(this.htmlDetail);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtImport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pointCrawlerCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.resultMsg);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCrawler);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WebCrawler";
            this.Text = "WebCrawler - Scrap KeyWords";
            this.Load += new System.EventHandler(this.WebCrawler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCrawler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox resultMsg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtImport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label pointCrawlerCount;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button htmlDetail;
        private System.Windows.Forms.CheckBox chkBuildHttpClient;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numTimes;
        private System.Windows.Forms.RichTextBox resaultMsgDetail;
        private System.Windows.Forms.RichTextBox resaultMsgDetail_BeforeRegex;
        private System.Windows.Forms.Label pointErrorCount;
    }
}

