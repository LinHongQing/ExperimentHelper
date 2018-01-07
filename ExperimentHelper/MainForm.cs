using ExperimentHelper.Basic;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using ExperimentHelper.Interface;
using System.Collections.Generic;
using ExperimentHelper.Util;
using ExperimentHelper.Controll;
using ExperimentHelper.Model;

namespace ExperimentHelper
{
    public partial class MainForm : Form, IResultObserver, IWindowHandleObserver, ITargetRectangleObserver
    {
        private IMainFormModel mainFormModel;
        private IMainFormControll mainFormController;
        private Dictionary<ControlsName, Control> controlsDict;
        public enum ControlsName
        {
            TEXTBOX_INPUT_TITLE, TEXTBOX_INPUT_STRING_PARAM, TEXTBOX_INPUT_INT_PARAM,
            COMBOBOX_SELECT_OPERATION,
            BTN_CHECK_TITLE, BTN_GET_RECTANGLE, BTN_SELECT_EXPORT_POINT, BTN_CHECK_EXPORT_POINT_TARGET, BTN_BEGIN,
            BTN_ABOUT, BTN_DEBUG, NUMBERICUPDOWN_SHORT_DELAY, NUMBERICUPDOWN_MEDIUM_DELAY, NUMBERICUPDOWN_LONG_DELAY
        }

        private class ComboBoxItem
        {
            private int id;
            private string description;
            public int Value { get => id; set => id = value; }
            public string Text { get => description; set => description = value; }
            public override string ToString()
            {
                return Text;
            }
        }

        public MainForm(WindowHandle handle, TargetRectangle rectangle, SettingComponent settings, ExportPointMatrix matrix)
        {
            InitializeComponent();
            InitControlsDict();
            InitComboBox();
            mainFormModel = new MainFormModel(settings, matrix, rectangle, handle);
            mainFormController = new MainFormController(mainFormModel, this);
            handle.RegisterWindowHandleObserver(this);
            rectangle.RegisterTargetRectangleObserver(this);
            mainFormModel.RegisterResultItemObserver(this);
        }

        private void InitControlsDict()
        {
            controlsDict = new Dictionary<ControlsName, Control>
            {
                { ControlsName.TEXTBOX_INPUT_TITLE, targetFormTitleInputTextBox },
                { ControlsName.TEXTBOX_INPUT_INT_PARAM, currentWndIntParameterTextBox},
                { ControlsName.TEXTBOX_INPUT_STRING_PARAM, currentWndStringParameterTextBox},
                { ControlsName.COMBOBOX_SELECT_OPERATION, currentWndProcessComboBox},
                { ControlsName.BTN_CHECK_TITLE, findFormButton },
                { ControlsName.BTN_GET_RECTANGLE, findTargetButton },
                { ControlsName.BTN_SELECT_EXPORT_POINT, chooseExportPositionButton },
                { ControlsName.BTN_CHECK_EXPORT_POINT_TARGET, checkPositionButton },
                { ControlsName.BTN_BEGIN, startButton },
                { ControlsName.BTN_ABOUT, aboutButton },
                { ControlsName.BTN_DEBUG, excuteDebugProcessButton },
                { ControlsName.NUMBERICUPDOWN_SHORT_DELAY, shortDelayNumericUpDown },
                { ControlsName.NUMBERICUPDOWN_MEDIUM_DELAY, mediumDaleyNumericUpDown },
                { ControlsName.NUMBERICUPDOWN_LONG_DELAY, longDelayNumericUpDown }
            };
        }

        private void InitComboBox()
        {
            controlsDict.TryGetValue(ControlsName.COMBOBOX_SELECT_OPERATION, out Control control);
            if (control is ComboBox comboBox)
            {
                ComboBoxItem item;
                foreach (ProcessHelper.ProcessTypeFlags flag in Enum.GetValues(typeof(ProcessHelper.ProcessTypeFlags)))
                {
                    item = new ComboBoxItem
                    {
                        Text = Enum.GetName(typeof(ProcessHelper.ProcessTypeFlags), flag),
                        Value = (int)flag
                    };
                    comboBox.Items.Add(item);
                }
                comboBox.SelectedIndex = 0;
            }
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

        #region 设置控件的 Text
        delegate void SetControlTextCallback(Control control, string value);
        private void SetControlText(Control control, string value)
        {
            if (control.InvokeRequired)
            {
#if DEBUG
                Console.WriteLine("控件 Control 需进行 invoke 操作");
#endif
                var d = new SetControlTextCallback(SetControlText);
                Invoke(d, control, value);
            }
            else
            {
                control.Text = value;
            }
        }
        #endregion

        #region 设置 RichTextBox
        delegate void SetRichTextBoxCallback(RichTextBox richTextBox, ResultItem result);
        private void SetRichTextBox(RichTextBox richTextBox, ResultItem result)
        {
            if (richTextBox.InvokeRequired)
            {
#if DEBUG
                Console.WriteLine("RichTextBox 需进行 invoke 操作");
#endif
                var d = new SetRichTextBoxCallback(SetRichTextBox);
                Invoke(d, richTextBox, result);
            }
            else
            {
#if DEBUG
                Console.WriteLine("新写入 log 数据: " + result.ToString());
#endif
                richTextBox.AppendText(result.ToString() + Environment.NewLine);
                richTextBox.Select(logRichTextBox.Text.Length - result.ToString().Length - 1, logRichTextBox.Text.Length - 1);
                switch (result.LogState)
                {
                    case ResultItem.States.OK:
                        richTextBox.SelectionColor = Color.LightGreen;
                        break;
                    case ResultItem.States.WARNING:
                        richTextBox.SelectionColor = Color.Yellow;
                        break;
                    case ResultItem.States.ERROR:
                        richTextBox.SelectionColor = Color.OrangeRed;
                        break;
                }
                richTextBox.SelectionStart = richTextBox.Text.Length;
                richTextBox.ScrollToCaret();
            }
        }
        #endregion


        private void CurrentWndIntParameterTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 31 && (e.KeyChar < '0' || e.KeyChar > '9')) { e.Handled = true; }
        }

        public void SyncSettingsToUI()
        {
            SettingComponent settings = SettingComponent.GetInstance();
            controlsDict.TryGetValue(ControlsName.TEXTBOX_INPUT_TITLE, out Control ctrl);
            ctrl.Text = settings.SearchTitle;
            controlsDict.TryGetValue(ControlsName.NUMBERICUPDOWN_SHORT_DELAY, out ctrl);
            ctrl.Text = settings.ShortStepDelay.ToString();
            controlsDict.TryGetValue(ControlsName.NUMBERICUPDOWN_MEDIUM_DELAY, out ctrl);
            ctrl.Text = settings.MediumStepDelay.ToString();
            controlsDict.TryGetValue(ControlsName.NUMBERICUPDOWN_LONG_DELAY, out ctrl);
            ctrl.Text = settings.LongStepDelay.ToString();
            controlsDict.TryGetValue(ControlsName.TEXTBOX_INPUT_STRING_PARAM, out ctrl);
            ctrl.Text = settings.StringParam;
            controlsDict.TryGetValue(ControlsName.TEXTBOX_INPUT_INT_PARAM, out ctrl);
            ctrl.Text = settings.IntParam.ToString();
            //controlsDict.TryGetValue(ControlsName.COMBOBOX_SELECT_OPERATION, out ctrl);
            //ComboBox comboBox = (ComboBox) ctrl;
        }

        public void SyncUIToSettings()
        {
            SettingComponent settings = SettingComponent.GetInstance();
            controlsDict.TryGetValue(ControlsName.TEXTBOX_INPUT_TITLE, out Control ctrl);
            settings.SearchTitle = ctrl.Text;
            controlsDict.TryGetValue(ControlsName.NUMBERICUPDOWN_SHORT_DELAY, out ctrl);
            settings.ShortStepDelay = int.Parse(ctrl.Text);
            controlsDict.TryGetValue(ControlsName.NUMBERICUPDOWN_MEDIUM_DELAY, out ctrl);
            settings.MediumStepDelay = int.Parse(ctrl.Text);
            controlsDict.TryGetValue(ControlsName.NUMBERICUPDOWN_LONG_DELAY, out ctrl);
            settings.LongStepDelay = int.Parse(ctrl.Text);
            controlsDict.TryGetValue(ControlsName.TEXTBOX_INPUT_STRING_PARAM, out ctrl);
            settings.StringParam = ctrl.Text;
            controlsDict.TryGetValue(ControlsName.TEXTBOX_INPUT_INT_PARAM, out ctrl);
            settings.IntParam = int.Parse(ctrl.Text);
            controlsDict.TryGetValue(ControlsName.COMBOBOX_SELECT_OPERATION, out ctrl);
            ComboBox comboBox = (ComboBox)ctrl;
            ComboBoxItem selectedItem = (ComboBoxItem) comboBox.SelectedItem;
            settings.ProcessType = (ProcessHelper.ProcessTypeFlags) Enum.Parse(typeof(ProcessHelper.ProcessTypeFlags), selectedItem.Text, false);
        }

        public void EnableAllControls()
        {
            foreach (Control ctrl in controlsDict.Values)
            {
                SetControlEnabled(ctrl, true);
            }
        }

        public void DisableAllControls()
        {
            foreach (Control ctrl in controlsDict.Values)
            {
                SetControlEnabled(ctrl, false);
            }
        }

        public void SetControlEnabled(ControlsName name, bool isEnabled)
        {
            if (controlsDict.TryGetValue(name, out Control control))
            {
                SetControlEnabled(control, isEnabled);
            }
        }

        public void SetControlText(ControlsName name, string value)
        {
            if (controlsDict.TryGetValue(name, out Control control))
            {
                SetControlText(control, value);
            }
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            mainFormController.About();
        }

        private void ExcuteDebugProcessButton_Click(object sender, EventArgs e)
        {
            mainFormController.ExecuteOperation();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            mainFormController.Start();
        }

        private void ChooseExportPositionButton_Click(object sender, EventArgs e)
        {
            mainFormController.ChooseExportPoint();
        }

        private void FindFormButton_Click(object sender, EventArgs e)
        {
            mainFormController.FindHandle();
        }

        private void FindTargetButton_Click(object sender, EventArgs e)
        {
            mainFormController.FindTarget();
        }

        private void CheckPositionButton_Click(object sender, EventArgs e)
        {
            mainFormController.CheckExportPoint();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mainFormController.FormClosing())
            {
                e.Cancel = true;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainFormController.Exit();
        }

        public void UpdateResultItem(ResultItem result)
        {
#if DEBUG
            Console.WriteLine("新写入 log 数据: " + result.ToString());
#endif
            SetRichTextBox(logRichTextBox, result);
        }

        public void UpdateProcessResult(ResultItem result)
        {
#if DEBUG
            Console.WriteLine("Process 状态更新: " + result.ToString());
#endif
            if (result.LogState == ResultItem.States.ThreadStart)
                mainFormController.ThreadStart();
            else
                mainFormController.ThreadFinish();
        }

        public void UpdateWindowHandle(WindowHandle handle)
        {
            SetControlText(parentWndValueTextBox, String.Format("0x{0:x8}", handle.GetParentHandle().ToInt32()));
            SetControlText(currentWndValueTextBox, String.Format("0x{0:x8}", handle.GetCurrentHandle().ToInt32()));
        }

        public void UpdateTargetRectangle(TargetRectangle rectangle)
        {
            SetControlText(targetLocationLabel, rectangle.ToString());
        }

        public bool ShowMessageBox(string messageContent, bool hasOKCanelButton)
        {
            if (hasOKCanelButton)
            {
                return (MessageBox.Show(messageContent, Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK);
            }
            else
            {
                return (MessageBox.Show(messageContent, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK);
            }
        }
    }
}
