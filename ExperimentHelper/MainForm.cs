using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ExperimentHelper
{
    public partial class MainForm : Form
    {
        private WndItem wndItem = new WndItem(IntPtr.Zero, IntPtr.Zero);        // 初始化主界面句柄
        private Base.RECT rect;                                               // 目标控件的范围
        private Base.POINT[,] matrix;                                         // 目标控件点选目标点矩阵
        private Process process;
        private Thread t;

        public MainForm()
        {
            InitializeComponent();
            rect = new Base.RECT
            {
                IsInit = false
            };
            Settings.LoadSettings();
            currentWndProcessComboBox.DataSource = Enum.GetNames(typeof(Process.ProcessTypeFlags));
            SyncSettingsToUI();
            parentWndValueTextBox.Text = String.Format("0x{0:x8}", wndItem.ParentWnd.ToInt32());
            currentWndValueTextBox.Text = String.Format("0x{0:x8}", wndItem.CurrentWnd.ToInt32());
            currentWndIntParameterTextBox.Text = "0";
            targetLocationLabel.Text = rect.ToString();
        }

        #region 设置控件是否启用
        delegate void SetControlEnabledCallback(Control Control, bool Enabled);
        private void SetControlEnabled(Control Control, bool Enabled)
        {
            if (Control.InvokeRequired)
            {
                var d = new SetControlEnabledCallback(SetControlEnabled);
                Invoke(d, Control, Enabled);
            }
            else
            {
                Control.Enabled = Enabled;
            }
        }
        #endregion

        #region 设置 Button 的 Text
        delegate void SetButtonTextCallback(Button Btn, string Text);
        private void SetButtonText(Button Btn, string Text)
        {
            if (Btn.InvokeRequired)
            {
#if DEBUG
                Console.WriteLine("Button 需进行 invoke 操作");
#endif
                var d = new SetButtonTextCallback(SetButtonText);
                Invoke(d, Btn, Text);
            }
            else
            {
                Btn.Text = Text;
            }
        }
        #endregion

        #region 设置 TextBox 的 Text
        delegate void SetTextBoxTextCallback(TextBox Tb, string Rs);
        private void SetTextBoxText(TextBox Tb, string Rs)
        {
            if (Tb.InvokeRequired)
            {
#if DEBUG
                Console.WriteLine("TextBox 需进行 invoke 操作");
#endif
                var d = new SetTextBoxTextCallback(SetTextBoxText);
                Invoke(d, Tb, Rs);
            }
            else
            {
                Tb.Text = Rs;
            }
        }
        #endregion

        #region 设置 RichTextBox
        delegate void SetRichTextBoxCallback(RichTextBox Rtb, ResultItem Rs);
        private void SetRichTextBox(RichTextBox Rtb, ResultItem Rs)
        {
            if (Rtb.InvokeRequired)
            {
#if DEBUG
                Console.WriteLine("RichTextBox 需进行 invoke 操作");
#endif
                var d = new SetRichTextBoxCallback(SetRichTextBox);
                Invoke(d, Rtb, Rs);
            }
            else
            {
#if DEBUG
                Console.WriteLine("新写入 log 数据: " + Rs.ToString());
#endif
                targetLocationLabel.Text = rect.ToString();
                Rtb.AppendText(Rs.ToString() + Environment.NewLine);
                Rtb.Select(logRichTextBox.Text.Length - Rs.ToString().Length - 1, logRichTextBox.Text.Length - 1);
                switch (Rs.LogState)
                {
                    case ResultItem.States.OK:
                        Rtb.SelectionColor = Color.LightGreen;
                        break;
                    case ResultItem.States.WARNING:
                        Rtb.SelectionColor = Color.Yellow;
                        break;
                    case ResultItem.States.ERROR:
                        Rtb.SelectionColor = Color.OrangeRed;
                        break;
                }
                Rtb.SelectionStart = Rtb.Text.Length;
                Rtb.ScrollToCaret();
            }
        }
        #endregion


        private void CurrentWndIntParameterTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 31 && (e.KeyChar < '0' || e.KeyChar > '9')) { e.Handled = true; }
        }

        private void OnResultReceive(ResultItem rs)
        {
            if (rs.RectangleSet)
            {
                rect = rs.Rectangle;
            }
            if (rs.MatrixSet)
            {
                matrix = rs.Matrix;
            }
#if DEBUG
            Console.WriteLine("新写入 log 数据: " + rs.ToString());
#endif
            SetRichTextBox(logRichTextBox, rs);
            // Update(rs);
        }

        private void OnWndChange(WndItem wnd)
        {
#if DEBUG
            Console.WriteLine("更新了句柄数据: [{0}]", wnd.ToString());
#endif
            SetTextBoxText(parentWndValueTextBox, String.Format("0x{0:x8}", wnd.ParentWnd.ToInt32()));
            SetTextBoxText(currentWndValueTextBox, String.Format("0x{0:x8}", wnd.CurrentWnd.ToInt32()));
            // Update(wnd);
        }

        private void OnProcessFinish(bool needMoreProc, Process nextProc)
        {
            if (needMoreProc && nextProc != null)
            {
                nextProc.ResultSend += new Process.ResultSendNotifer(OnResultReceive);
                nextProc.WndChange += new Process.WndChangeNotifier(OnWndChange);
                nextProc.ProcessFinish += new Process.ProcessFinishNotifer(OnProcessFinish);
                t = new Thread(new ThreadStart(nextProc.Go))
                {
                    IsBackground = true
                };
                t.Start();
            }
            else
            {
                EnableControls();
                SetButtonText(startButton, "开始");
            }
        }

        private void EnableControls()
        {
            SetControlEnabled(targetFormTitleInputTextBox, true);
            SetControlEnabled(findFormButton, true);
            SetControlEnabled(findTargetButton, true);
            SetControlEnabled(checkPositionButton, true);
            SetControlEnabled(chooseExportPositionButton, true);
            SetControlEnabled(checkPositionButton, true);
            SetControlEnabled(shortDelayNumericUpDown, true);
            SetControlEnabled(mediumDaleyNumericUpDown, true);
            SetControlEnabled(longDelayNumericUpDown, true);
        }

        private void DisableControls()
        {
            SetControlEnabled(targetFormTitleInputTextBox, false);
            SetControlEnabled(findFormButton, false);
            SetControlEnabled(findTargetButton, false);
            SetControlEnabled(checkPositionButton, false);
            SetControlEnabled(chooseExportPositionButton, false);
            SetControlEnabled(checkPositionButton, false);
            SetControlEnabled(shortDelayNumericUpDown, false);
            SetControlEnabled(mediumDaleyNumericUpDown, false);
            SetControlEnabled(longDelayNumericUpDown, false);
        }

        private void SyncSettingsToMemory()
        {
            Settings.SHORT_STEP_DELAY = (int)shortDelayNumericUpDown.Value;
            Settings.MEDIUM_STEP_DELAY = (int)mediumDaleyNumericUpDown.Value;
            Settings.LONG_STEP_DELAY = (int)longDelayNumericUpDown.Value;
        }

        private void SyncSettingsToUI()
        {
            shortDelayNumericUpDown.Value = Settings.SHORT_STEP_DELAY;
            mediumDaleyNumericUpDown.Value = Settings.MEDIUM_STEP_DELAY;
            longDelayNumericUpDown.Value = Settings.LONG_STEP_DELAY;
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void ExcuteDebugProcessButton_Click(object sender, EventArgs e)
        {
            SyncSettingsToMemory();
            ProcessItem proc = new ProcessItem
            {
                ProcessType = (Process.ProcessTypeFlags)Enum.Parse(typeof(Process.ProcessTypeFlags), currentWndProcessComboBox.SelectedItem.ToString(), false),
                StringParameter = currentWndStringParameterTextBox.Text,
                IntegerParameter = currentWndIntParameterTextBox.Text.Length == 0 ? 0 : int.Parse(currentWndIntParameterTextBox.Text),
                StepDelay = Settings.SHORT_STEP_DELAY,
                Rectangle = rect,
            };
            process = new Process(proc, ref wndItem, false, null);
            process.ResultSend += new Process.ResultSendNotifer(OnResultReceive);
            process.WndChange += new Process.WndChangeNotifier(OnWndChange);
            process.ProcessFinish += new Process.ProcessFinishNotifer(OnProcessFinish);
            t = new Thread(new ThreadStart(process.Go))
            {
                IsBackground = true
            };
            t.Start();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            SyncSettingsToMemory();
            if (process != null && process.IsRunning == true)
            {
                process.IsRunning = false;
                SetControlEnabled(startButton, false);
            }
            else
            {
                SetButtonText(startButton, "停止");
            }
            DisableControls();
            process = new Process(
                ProcessGenerator.GenerateFinalProcess(ref matrix, targetFormTitleInputTextBox.Text),
                ref wndItem,
                false,
                null);
            process.ResultSend += new Process.ResultSendNotifer(OnResultReceive);
            process.WndChange += new Process.WndChangeNotifier(OnWndChange);
            process.ProcessFinish += new Process.ProcessFinishNotifer(OnProcessFinish);
            t = new Thread(new ThreadStart(process.Go))
            {
                IsBackground = true
            };
            t.Start();
        }

        private void ChooseExportPositionButton_Click(object sender, EventArgs e)
        {
            ExportChooseForm ec = new ExportChooseForm();
            ec.ShowDialog();
#if DEBUG
            if (matrix == null)
                return;
            for (int i = 0; i < Settings.ROWS_COUNT; i++)
            {
                for (int j = 0; j < Settings.COLUMNS_COUNT; j++)
                {
                    Console.Write("{0} ", Settings.EXPORT_CONFIG[i, j]);
                }
                Console.WriteLine();
            }
#endif
        }

        private void FindFormButton_Click(object sender, EventArgs e)
        {
            DisableControls();
            SyncSettingsToMemory();
            process = new Process(
                ProcessGenerator.GenerateFindFormProcess(targetFormTitleInputTextBox.Text),
                ref wndItem,
                false,
                null);
            process.ResultSend += new Process.ResultSendNotifer(OnResultReceive);
            process.WndChange += new Process.WndChangeNotifier(OnWndChange);
            process.ProcessFinish += new Process.ProcessFinishNotifer(OnProcessFinish);
            t = new Thread(new ThreadStart(process.Go));
            t.Start();
        }

        private void FindTargetButton_Click(object sender, EventArgs e)
        {
            DisableControls();
            SyncSettingsToMemory();
            process = new Process(
                ProcessGenerator.GenerateFindTargetProcess(),
                ref wndItem,
                false,
                null);
            process.ResultSend += new Process.ResultSendNotifer(OnResultReceive);
            process.WndChange += new Process.WndChangeNotifier(OnWndChange);
            process.ProcessFinish += new Process.ProcessFinishNotifer(OnProcessFinish);
            t = new Thread(new ThreadStart(process.Go))
            {
                IsBackground = true
            };
            t.Start();
        }

        private void CheckPositionButton_Click(object sender, EventArgs e)
        {
            DisableControls();
            SyncSettingsToMemory();
            if (matrix == null)
            {
                MessageBox.Show("未获取控件坐标!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DisableControls();
            process = new Process(
                ProcessGenerator.GenerateCheckPositionProcess(ref matrix),
                ref wndItem,
                false,
                null);
            process.ResultSend += new Process.ResultSendNotifer(OnResultReceive);
            process.ProcessFinish += new Process.ProcessFinishNotifer(OnProcessFinish);
            t = new Thread(new ThreadStart(process.Go))
            {
                IsBackground = true
            };
            t.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (process != null)
            {
                process.IsRunning = false;
            }
            if (MessageBox.Show("真的要退出吗？", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SyncSettingsToMemory();
            Settings.SaveSettings();
            Dispose();
            Application.Exit();
        }
    }
}
