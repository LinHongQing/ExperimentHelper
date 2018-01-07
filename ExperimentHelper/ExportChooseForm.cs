using ExperimentHelper.Basic;
using ExperimentHelper.Controll;
using ExperimentHelper.Interface;
using ExperimentHelper.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExperimentHelper
{
    public partial class ExportChooseForm : Form, IExportPointMatrixObserver
    {
        private IExportChooseFormControll controller;
        private IExportChooseFormModel model;
        private ExportPointMatrix matrix = ExportPointMatrix.GetInstance();
        private Dictionary<string, Button> btnDictionary = new Dictionary<string, Button>();

        public ExportChooseForm()
        {
            InitializeComponent();
            model = new ExportChooseFormModel(matrix);
            model.RegisterExportPointMatrixObserver(this);
            controller = new ExportChooseFormController(model, this);
            int rowCount = model.GetRowCount();
            int columnCount = model.GetColumnCount();

            // 初始化 TableLayoutPanel
            controlTableLayoutPanel.Controls.Clear();
            controlTableLayoutPanel.ColumnStyles.Clear();
            controlTableLayoutPanel.RowStyles.Clear();
            // 初始化 TableLayoutPanel 的列
            controlTableLayoutPanel.ColumnCount = columnCount;
            for (int i = 0; i < columnCount; i++)
            {
                controlTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columnCount));
            }
            // 初始化 TableLayoutPanel 的行
            controlTableLayoutPanel.RowCount = rowCount;
            for (int i = 0; i < rowCount; i++)
            {
                controlTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rowCount));
            }
            // 往 TableLayoutPanel 中添加按钮
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    Button btn = new Button
                    {
                        Name = "button_" + j + "_" + i,
                        Text = model.GetExportPointText(i, j),
                        Dock = DockStyle.Fill,
                    };
                    if (model.GetExportPointAvaliable(i, j))
                        btn.ForeColor = Color.Red;
                    // 为每一个按钮添加鼠标点击事件回调函数
                    btn.MouseClick += new MouseEventHandler(Button_MouseClick);
                    controlTableLayoutPanel.Controls.Add(btn);
                    btnDictionary.Add(GetDictionaryKey(i, j), btn);
                }
            }
        }

        private string GetDictionaryKey(int row, int column)
        {
            return string.Format("{0}+{1}", row, column);
        }

        public void ExportPointUpdate(int row, int column)
        {
            string btnText = model.GetExportPointText(row, column);
            bool btnAvaliable = model.GetExportPointAvaliable(row, column);
            if (btnDictionary.TryGetValue(GetDictionaryKey(row, column), out Button btn))
            {
                if (model.GetExportPointAvaliable(row, column))
                    btn.ForeColor = Color.Red;
                else
                    btn.ForeColor = Color.Black;
            }
        }

        private void Button_MouseClick(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            string btnName = button.Name;
            string[] parameters = btnName.Split('_');
#if DEBUG
            Console.WriteLine("按钮 \"{0}\" 已点击", btnName);
#endif
            if (parameters.Length != 3)
                return;
            int column = int.Parse(parameters[1]);
            int row = int.Parse(parameters[2]);
            controller.ChangeExportPointStatus(row, column);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            controller.Cancel();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            controller.Comfirm();
        }

        public void EnableConfirmButton()
        {
            confirmButton.Enabled = true;
        }

        public void DisableConfirmButton()
        {
            confirmButton.Enabled = false;
        }
    }
}
