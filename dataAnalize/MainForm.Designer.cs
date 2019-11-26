namespace dataAnalize
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelFilePath = new System.Windows.Forms.Button();
            this.TestStart = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.listResult = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AllStart = new System.Windows.Forms.Button();
            this.btnSelSavePath = new System.Windows.Forms.Button();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSelFilePath
            // 
            this.btnSelFilePath.Location = new System.Drawing.Point(52, 20);
            this.btnSelFilePath.Name = "btnSelFilePath";
            this.btnSelFilePath.Size = new System.Drawing.Size(75, 23);
            this.btnSelFilePath.TabIndex = 0;
            this.btnSelFilePath.Text = "文件选择";
            this.btnSelFilePath.UseVisualStyleBackColor = true;
            this.btnSelFilePath.Click += new System.EventHandler(this.btnSelFilePath_Click);
            // 
            // TestStart
            // 
            this.TestStart.Location = new System.Drawing.Point(249, 96);
            this.TestStart.Name = "TestStart";
            this.TestStart.Size = new System.Drawing.Size(75, 23);
            this.TestStart.TabIndex = 0;
            this.TestStart.Text = "测试解析";
            this.TestStart.UseVisualStyleBackColor = true;
            this.TestStart.Click += new System.EventHandler(this.TestStart_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(134, 21);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(291, 21);
            this.txtFilePath.TabIndex = 1;
            // 
            // listResult
            // 
            this.listResult.FormattingEnabled = true;
            this.listResult.ItemHeight = 12;
            this.listResult.Location = new System.Drawing.Point(52, 144);
            this.listResult.Name = "listResult";
            this.listResult.Size = new System.Drawing.Size(373, 328);
            this.listResult.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "结果展示:";
            // 
            // AllStart
            // 
            this.AllStart.Location = new System.Drawing.Point(350, 96);
            this.AllStart.Name = "AllStart";
            this.AllStart.Size = new System.Drawing.Size(75, 23);
            this.AllStart.TabIndex = 0;
            this.AllStart.Text = "全部解析";
            this.AllStart.UseVisualStyleBackColor = true;
            this.AllStart.Click += new System.EventHandler(this.AllStart_Click);
            // 
            // btnSelSavePath
            // 
            this.btnSelSavePath.Location = new System.Drawing.Point(52, 56);
            this.btnSelSavePath.Name = "btnSelSavePath";
            this.btnSelSavePath.Size = new System.Drawing.Size(75, 23);
            this.btnSelSavePath.TabIndex = 0;
            this.btnSelSavePath.Text = "保存文件";
            this.btnSelSavePath.UseVisualStyleBackColor = true;
            this.btnSelSavePath.Click += new System.EventHandler(this.btnSelSavePath_Click);
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(134, 57);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(291, 21);
            this.txtSavePath.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 497);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listResult);
            this.Controls.Add(this.txtSavePath);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.AllStart);
            this.Controls.Add(this.btnSelSavePath);
            this.Controls.Add(this.TestStart);
            this.Controls.Add(this.btnSelFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "测试界面";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelFilePath;
        private System.Windows.Forms.Button TestStart;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.ListBox listResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AllStart;
        private System.Windows.Forms.Button btnSelSavePath;
        private System.Windows.Forms.TextBox txtSavePath;
    }
}

