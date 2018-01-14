namespace ExperimentHelper
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.controlGroup = new System.Windows.Forms.GroupBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.mainTabPage = new System.Windows.Forms.TabPage();
            this.maximumNumberOfRetriesLabel = new System.Windows.Forms.Label();
            this.retryDelayLabel = new System.Windows.Forms.Label();
            this.stepDelaylabel = new System.Windows.Forms.Label();
            this.maximumNumberOfRetriesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.retryDaleyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.stepDelayNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.checkPositionButton = new System.Windows.Forms.Button();
            this.chooseExportPositionButton = new System.Windows.Forms.Button();
            this.targetLocationLabel = new System.Windows.Forms.Label();
            this.findTargetButton = new System.Windows.Forms.Button();
            this.findFormButton = new System.Windows.Forms.Button();
            this.targetFormTitleInputTextBox = new System.Windows.Forms.TextBox();
            this.aboutButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.step3DescriptionLabel = new System.Windows.Forms.Label();
            this.step2DescriptionLabel = new System.Windows.Forms.Label();
            this.step1DescriptionLabel = new System.Windows.Forms.Label();
            this.debugTabPage = new System.Windows.Forms.TabPage();
            this.currentWndIntParameterTextBox = new System.Windows.Forms.TextBox();
            this.currentWndStringParameterTextBox = new System.Windows.Forms.TextBox();
            this.currentWndProcessComboBox = new System.Windows.Forms.ComboBox();
            this.excuteDebugProcessButton = new System.Windows.Forms.Button();
            this.currentWndProcessIntParameterLabel = new System.Windows.Forms.Label();
            this.currentWndProcessStringParameterLabel = new System.Windows.Forms.Label();
            this.currentWndProcessLabel = new System.Windows.Forms.Label();
            this.currentWndValueTextBox = new System.Windows.Forms.TextBox();
            this.parentWndValueTextBox = new System.Windows.Forms.TextBox();
            this.currentWndLabel = new System.Windows.Forms.Label();
            this.parentWndLabel = new System.Windows.Forms.Label();
            this.logOutputGroupBox = new System.Windows.Forms.GroupBox();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.controlGroup.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.mainTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumNumberOfRetriesNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryDaleyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stepDelayNumericUpDown)).BeginInit();
            this.debugTabPage.SuspendLayout();
            this.logOutputGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlGroup
            // 
            this.controlGroup.Controls.Add(this.tabControl);
            this.controlGroup.Controls.Add(this.currentWndValueTextBox);
            this.controlGroup.Controls.Add(this.parentWndValueTextBox);
            this.controlGroup.Controls.Add(this.currentWndLabel);
            this.controlGroup.Controls.Add(this.parentWndLabel);
            this.controlGroup.Location = new System.Drawing.Point(13, 13);
            this.controlGroup.Name = "controlGroup";
            this.controlGroup.Size = new System.Drawing.Size(597, 273);
            this.controlGroup.TabIndex = 0;
            this.controlGroup.TabStop = false;
            this.controlGroup.Text = "操作";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.mainTabPage);
            this.tabControl.Controls.Add(this.debugTabPage);
            this.tabControl.Location = new System.Drawing.Point(11, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(580, 209);
            this.tabControl.TabIndex = 0;
            // 
            // mainTabPage
            // 
            this.mainTabPage.Controls.Add(this.maximumNumberOfRetriesLabel);
            this.mainTabPage.Controls.Add(this.retryDelayLabel);
            this.mainTabPage.Controls.Add(this.stepDelaylabel);
            this.mainTabPage.Controls.Add(this.maximumNumberOfRetriesNumericUpDown);
            this.mainTabPage.Controls.Add(this.retryDaleyNumericUpDown);
            this.mainTabPage.Controls.Add(this.stepDelayNumericUpDown);
            this.mainTabPage.Controls.Add(this.checkPositionButton);
            this.mainTabPage.Controls.Add(this.chooseExportPositionButton);
            this.mainTabPage.Controls.Add(this.targetLocationLabel);
            this.mainTabPage.Controls.Add(this.findTargetButton);
            this.mainTabPage.Controls.Add(this.findFormButton);
            this.mainTabPage.Controls.Add(this.targetFormTitleInputTextBox);
            this.mainTabPage.Controls.Add(this.aboutButton);
            this.mainTabPage.Controls.Add(this.startButton);
            this.mainTabPage.Controls.Add(this.step3DescriptionLabel);
            this.mainTabPage.Controls.Add(this.step2DescriptionLabel);
            this.mainTabPage.Controls.Add(this.step1DescriptionLabel);
            this.mainTabPage.Location = new System.Drawing.Point(4, 29);
            this.mainTabPage.Name = "mainTabPage";
            this.mainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainTabPage.Size = new System.Drawing.Size(572, 176);
            this.mainTabPage.TabIndex = 1;
            this.mainTabPage.Text = "主面板";
            this.mainTabPage.UseVisualStyleBackColor = true;
            // 
            // maximumNumberOfRetriesLabel
            // 
            this.maximumNumberOfRetriesLabel.AutoSize = true;
            this.maximumNumberOfRetriesLabel.Location = new System.Drawing.Point(399, 104);
            this.maximumNumberOfRetriesLabel.Name = "maximumNumberOfRetriesLabel";
            this.maximumNumberOfRetriesLabel.Size = new System.Drawing.Size(103, 20);
            this.maximumNumberOfRetriesLabel.TabIndex = 14;
            this.maximumNumberOfRetriesLabel.Text = "最大重试次数:";
            // 
            // retryDelayLabel
            // 
            this.retryDelayLabel.AutoSize = true;
            this.retryDelayLabel.Location = new System.Drawing.Point(203, 104);
            this.retryDelayLabel.Name = "retryDelayLabel";
            this.retryDelayLabel.Size = new System.Drawing.Size(119, 20);
            this.retryDelayLabel.TabIndex = 14;
            this.retryDelayLabel.Text = "重试间延迟(ms):";
            // 
            // stepDelaylabel
            // 
            this.stepDelaylabel.AutoSize = true;
            this.stepDelaylabel.Location = new System.Drawing.Point(7, 104);
            this.stepDelaylabel.Name = "stepDelaylabel";
            this.stepDelaylabel.Size = new System.Drawing.Size(119, 20);
            this.stepDelaylabel.TabIndex = 14;
            this.stepDelaylabel.Text = "步骤间延迟(ms):";
            // 
            // maximumNumberOfRetriesNumericUpDown
            // 
            this.maximumNumberOfRetriesNumericUpDown.Location = new System.Drawing.Point(507, 102);
            this.maximumNumberOfRetriesNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.maximumNumberOfRetriesNumericUpDown.Name = "maximumNumberOfRetriesNumericUpDown";
            this.maximumNumberOfRetriesNumericUpDown.Size = new System.Drawing.Size(57, 27);
            this.maximumNumberOfRetriesNumericUpDown.TabIndex = 13;
            // 
            // retryDaleyNumericUpDown
            // 
            this.retryDaleyNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.retryDaleyNumericUpDown.Location = new System.Drawing.Point(322, 102);
            this.retryDaleyNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.retryDaleyNumericUpDown.Name = "retryDaleyNumericUpDown";
            this.retryDaleyNumericUpDown.Size = new System.Drawing.Size(71, 27);
            this.retryDaleyNumericUpDown.TabIndex = 13;
            // 
            // stepDelayNumericUpDown
            // 
            this.stepDelayNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.stepDelayNumericUpDown.Location = new System.Drawing.Point(127, 102);
            this.stepDelayNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.stepDelayNumericUpDown.Name = "stepDelayNumericUpDown";
            this.stepDelayNumericUpDown.Size = new System.Drawing.Size(71, 27);
            this.stepDelayNumericUpDown.TabIndex = 13;
            // 
            // checkPositionButton
            // 
            this.checkPositionButton.Location = new System.Drawing.Point(464, 69);
            this.checkPositionButton.Name = "checkPositionButton";
            this.checkPositionButton.Size = new System.Drawing.Size(102, 29);
            this.checkPositionButton.TabIndex = 4;
            this.checkPositionButton.Text = "检查位置";
            this.checkPositionButton.UseVisualStyleBackColor = true;
            this.checkPositionButton.Click += new System.EventHandler(this.CheckPositionButton_Click);
            // 
            // chooseExportPositionButton
            // 
            this.chooseExportPositionButton.Location = new System.Drawing.Point(253, 69);
            this.chooseExportPositionButton.Name = "chooseExportPositionButton";
            this.chooseExportPositionButton.Size = new System.Drawing.Size(205, 29);
            this.chooseExportPositionButton.TabIndex = 3;
            this.chooseExportPositionButton.Text = "选择要导出的位置...";
            this.chooseExportPositionButton.UseVisualStyleBackColor = true;
            this.chooseExportPositionButton.Click += new System.EventHandler(this.ChooseExportPositionButton_Click);
            // 
            // targetLocationLabel
            // 
            this.targetLocationLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.targetLocationLabel.Location = new System.Drawing.Point(253, 40);
            this.targetLocationLabel.Name = "targetLocationLabel";
            this.targetLocationLabel.Size = new System.Drawing.Size(205, 20);
            this.targetLocationLabel.TabIndex = 12;
            this.targetLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // findTargetButton
            // 
            this.findTargetButton.Location = new System.Drawing.Point(464, 36);
            this.findTargetButton.Name = "findTargetButton";
            this.findTargetButton.Size = new System.Drawing.Size(102, 29);
            this.findTargetButton.TabIndex = 2;
            this.findTargetButton.Text = "寻找目标";
            this.findTargetButton.UseVisualStyleBackColor = true;
            this.findTargetButton.Click += new System.EventHandler(this.FindTargetButton_Click);
            // 
            // findFormButton
            // 
            this.findFormButton.Location = new System.Drawing.Point(464, 2);
            this.findFormButton.Name = "findFormButton";
            this.findFormButton.Size = new System.Drawing.Size(102, 29);
            this.findFormButton.TabIndex = 1;
            this.findFormButton.Text = "查找";
            this.findFormButton.UseVisualStyleBackColor = true;
            this.findFormButton.Click += new System.EventHandler(this.FindFormButton_Click);
            // 
            // targetFormTitleInputTextBox
            // 
            this.targetFormTitleInputTextBox.Location = new System.Drawing.Point(253, 4);
            this.targetFormTitleInputTextBox.Name = "targetFormTitleInputTextBox";
            this.targetFormTitleInputTextBox.Size = new System.Drawing.Size(205, 27);
            this.targetFormTitleInputTextBox.TabIndex = 0;
            // 
            // aboutButton
            // 
            this.aboutButton.Location = new System.Drawing.Point(11, 139);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(124, 31);
            this.aboutButton.TabIndex = 7;
            this.aboutButton.Text = "关于";
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startButton.Location = new System.Drawing.Point(442, 139);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(124, 31);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "开始";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // step3DescriptionLabel
            // 
            this.step3DescriptionLabel.AutoSize = true;
            this.step3DescriptionLabel.Location = new System.Drawing.Point(67, 73);
            this.step3DescriptionLabel.Name = "step3DescriptionLabel";
            this.step3DescriptionLabel.Size = new System.Drawing.Size(180, 20);
            this.step3DescriptionLabel.TabIndex = 10;
            this.step3DescriptionLabel.Text = "步骤3 选择要导出的部分: ";
            // 
            // step2DescriptionLabel
            // 
            this.step2DescriptionLabel.AutoSize = true;
            this.step2DescriptionLabel.Location = new System.Drawing.Point(7, 40);
            this.step2DescriptionLabel.Name = "step2DescriptionLabel";
            this.step2DescriptionLabel.Size = new System.Drawing.Size(240, 20);
            this.step2DescriptionLabel.TabIndex = 9;
            this.step2DescriptionLabel.Text = "步骤2 点击右侧按钮寻找目标坐标: ";
            // 
            // step1DescriptionLabel
            // 
            this.step1DescriptionLabel.AutoSize = true;
            this.step1DescriptionLabel.Location = new System.Drawing.Point(37, 6);
            this.step1DescriptionLabel.Name = "step1DescriptionLabel";
            this.step1DescriptionLabel.Size = new System.Drawing.Size(210, 20);
            this.step1DescriptionLabel.TabIndex = 8;
            this.step1DescriptionLabel.Text = "步骤1 输入窗口标题查找窗口: ";
            // 
            // debugTabPage
            // 
            this.debugTabPage.Controls.Add(this.currentWndIntParameterTextBox);
            this.debugTabPage.Controls.Add(this.currentWndStringParameterTextBox);
            this.debugTabPage.Controls.Add(this.currentWndProcessComboBox);
            this.debugTabPage.Controls.Add(this.excuteDebugProcessButton);
            this.debugTabPage.Controls.Add(this.currentWndProcessIntParameterLabel);
            this.debugTabPage.Controls.Add(this.currentWndProcessStringParameterLabel);
            this.debugTabPage.Controls.Add(this.currentWndProcessLabel);
            this.debugTabPage.Location = new System.Drawing.Point(4, 29);
            this.debugTabPage.Name = "debugTabPage";
            this.debugTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.debugTabPage.Size = new System.Drawing.Size(572, 176);
            this.debugTabPage.TabIndex = 0;
            this.debugTabPage.Text = "调试面板";
            this.debugTabPage.UseVisualStyleBackColor = true;
            // 
            // currentWndIntParameterTextBox
            // 
            this.currentWndIntParameterTextBox.Location = new System.Drawing.Point(112, 75);
            this.currentWndIntParameterTextBox.Name = "currentWndIntParameterTextBox";
            this.currentWndIntParameterTextBox.Size = new System.Drawing.Size(331, 27);
            this.currentWndIntParameterTextBox.TabIndex = 2;
            this.currentWndIntParameterTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CurrentWndIntParameterTextBox_KeyPress);
            // 
            // currentWndStringParameterTextBox
            // 
            this.currentWndStringParameterTextBox.Location = new System.Drawing.Point(112, 43);
            this.currentWndStringParameterTextBox.Name = "currentWndStringParameterTextBox";
            this.currentWndStringParameterTextBox.Size = new System.Drawing.Size(331, 27);
            this.currentWndStringParameterTextBox.TabIndex = 1;
            // 
            // currentWndProcessComboBox
            // 
            this.currentWndProcessComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currentWndProcessComboBox.FormattingEnabled = true;
            this.currentWndProcessComboBox.Location = new System.Drawing.Point(112, 9);
            this.currentWndProcessComboBox.Name = "currentWndProcessComboBox";
            this.currentWndProcessComboBox.Size = new System.Drawing.Size(331, 28);
            this.currentWndProcessComboBox.TabIndex = 0;
            // 
            // excuteDebugProcessButton
            // 
            this.excuteDebugProcessButton.Location = new System.Drawing.Point(452, 9);
            this.excuteDebugProcessButton.Name = "excuteDebugProcessButton";
            this.excuteDebugProcessButton.Size = new System.Drawing.Size(114, 93);
            this.excuteDebugProcessButton.TabIndex = 4;
            this.excuteDebugProcessButton.Text = "执行操作";
            this.excuteDebugProcessButton.UseVisualStyleBackColor = true;
            this.excuteDebugProcessButton.Click += new System.EventHandler(this.ExcuteDebugProcessButton_Click);
            // 
            // currentWndProcessIntParameterLabel
            // 
            this.currentWndProcessIntParameterLabel.AutoSize = true;
            this.currentWndProcessIntParameterLabel.Location = new System.Drawing.Point(23, 78);
            this.currentWndProcessIntParameterLabel.Name = "currentWndProcessIntParameterLabel";
            this.currentWndProcessIntParameterLabel.Size = new System.Drawing.Size(91, 20);
            this.currentWndProcessIntParameterLabel.TabIndex = 12;
            this.currentWndProcessIntParameterLabel.Text = "参数值(int): ";
            // 
            // currentWndProcessStringParameterLabel
            // 
            this.currentWndProcessStringParameterLabel.AutoSize = true;
            this.currentWndProcessStringParameterLabel.Location = new System.Drawing.Point(0, 46);
            this.currentWndProcessStringParameterLabel.Name = "currentWndProcessStringParameterLabel";
            this.currentWndProcessStringParameterLabel.Size = new System.Drawing.Size(114, 20);
            this.currentWndProcessStringParameterLabel.TabIndex = 12;
            this.currentWndProcessStringParameterLabel.Text = "参数值(string): ";
            // 
            // currentWndProcessLabel
            // 
            this.currentWndProcessLabel.AutoSize = true;
            this.currentWndProcessLabel.Location = new System.Drawing.Point(37, 12);
            this.currentWndProcessLabel.Name = "currentWndProcessLabel";
            this.currentWndProcessLabel.Size = new System.Drawing.Size(77, 20);
            this.currentWndProcessLabel.TabIndex = 11;
            this.currentWndProcessLabel.Text = "选择操作: ";
            // 
            // currentWndValueTextBox
            // 
            this.currentWndValueTextBox.Location = new System.Drawing.Point(363, 240);
            this.currentWndValueTextBox.Name = "currentWndValueTextBox";
            this.currentWndValueTextBox.ReadOnly = true;
            this.currentWndValueTextBox.Size = new System.Drawing.Size(140, 27);
            this.currentWndValueTextBox.TabIndex = 2;
            // 
            // parentWndValueTextBox
            // 
            this.parentWndValueTextBox.Location = new System.Drawing.Point(144, 240);
            this.parentWndValueTextBox.Name = "parentWndValueTextBox";
            this.parentWndValueTextBox.ReadOnly = true;
            this.parentWndValueTextBox.Size = new System.Drawing.Size(140, 27);
            this.parentWndValueTextBox.TabIndex = 1;
            // 
            // currentWndLabel
            // 
            this.currentWndLabel.AutoSize = true;
            this.currentWndLabel.Location = new System.Drawing.Point(290, 243);
            this.currentWndLabel.Name = "currentWndLabel";
            this.currentWndLabel.Size = new System.Drawing.Size(77, 20);
            this.currentWndLabel.TabIndex = 5;
            this.currentWndLabel.Text = "当前句柄: ";
            // 
            // parentWndLabel
            // 
            this.parentWndLabel.AutoSize = true;
            this.parentWndLabel.Location = new System.Drawing.Point(83, 243);
            this.parentWndLabel.Name = "parentWndLabel";
            this.parentWndLabel.Size = new System.Drawing.Size(62, 20);
            this.parentWndLabel.TabIndex = 4;
            this.parentWndLabel.Text = "父句柄: ";
            // 
            // logOutputGroupBox
            // 
            this.logOutputGroupBox.Controls.Add(this.logRichTextBox);
            this.logOutputGroupBox.Location = new System.Drawing.Point(13, 292);
            this.logOutputGroupBox.Name = "logOutputGroupBox";
            this.logOutputGroupBox.Size = new System.Drawing.Size(597, 209);
            this.logOutputGroupBox.TabIndex = 1;
            this.logOutputGroupBox.TabStop = false;
            this.logOutputGroupBox.Text = "日志输出";
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.BackColor = System.Drawing.Color.Black;
            this.logRichTextBox.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logRichTextBox.Location = new System.Drawing.Point(6, 26);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            this.logRichTextBox.Size = new System.Drawing.Size(585, 177);
            this.logRichTextBox.TabIndex = 1;
            this.logRichTextBox.Text = "";
            this.logRichTextBox.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(622, 513);
            this.Controls.Add(this.logOutputGroupBox);
            this.Controls.Add(this.controlGroup);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "实验助手";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.controlGroup.ResumeLayout(false);
            this.controlGroup.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.mainTabPage.ResumeLayout(false);
            this.mainTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumNumberOfRetriesNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryDaleyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stepDelayNumericUpDown)).EndInit();
            this.debugTabPage.ResumeLayout(false);
            this.debugTabPage.PerformLayout();
            this.logOutputGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox controlGroup;
        private System.Windows.Forms.GroupBox logOutputGroupBox;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.Label currentWndLabel;
        private System.Windows.Forms.Label parentWndLabel;
        private System.Windows.Forms.Button excuteDebugProcessButton;
        private System.Windows.Forms.Label currentWndProcessStringParameterLabel;
        private System.Windows.Forms.Label currentWndProcessLabel;
        private System.Windows.Forms.ComboBox currentWndProcessComboBox;
        private System.Windows.Forms.TextBox currentWndStringParameterTextBox;
        private System.Windows.Forms.TextBox currentWndValueTextBox;
        private System.Windows.Forms.TextBox parentWndValueTextBox;
        private System.Windows.Forms.Label currentWndProcessIntParameterLabel;
        private System.Windows.Forms.TextBox currentWndIntParameterTextBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage mainTabPage;
        private System.Windows.Forms.TabPage debugTabPage;
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button findFormButton;
        private System.Windows.Forms.TextBox targetFormTitleInputTextBox;
        private System.Windows.Forms.Label step1DescriptionLabel;
        private System.Windows.Forms.Label step2DescriptionLabel;
        private System.Windows.Forms.Label targetLocationLabel;
        private System.Windows.Forms.Button findTargetButton;
        private System.Windows.Forms.Button chooseExportPositionButton;
        private System.Windows.Forms.Label step3DescriptionLabel;
        private System.Windows.Forms.Button checkPositionButton;
        private System.Windows.Forms.Label maximumNumberOfRetriesLabel;
        private System.Windows.Forms.Label retryDelayLabel;
        private System.Windows.Forms.Label stepDelaylabel;
        private System.Windows.Forms.NumericUpDown maximumNumberOfRetriesNumericUpDown;
        private System.Windows.Forms.NumericUpDown retryDaleyNumericUpDown;
        private System.Windows.Forms.NumericUpDown stepDelayNumericUpDown;
    }
}

