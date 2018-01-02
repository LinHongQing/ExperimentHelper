using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ExperimentHelper
{
    public partial class ExportChooseForm : Form
    {

        private bool[,] t_export_config;

        public ExportChooseForm()
        {
            InitializeComponent();
            t_export_config = new bool[Settings.ROWS_COUNT, Settings.COLUMNS_COUNT];
            // 将临时导出设置与原设置同步
            for (int i = 0; i < Settings.ROWS_COUNT; i++)
            {
                for (int j = 0; j < Settings.COLUMNS_COUNT; j++)
                {
                    t_export_config[i, j] = Settings.EXPORT_CONFIG[i, j];
                }
            }

            // 初始化 TableLayoutPanel
            controlTableLayoutPanel.Controls.Clear();
            controlTableLayoutPanel.ColumnStyles.Clear();
            controlTableLayoutPanel.RowStyles.Clear();
            // 初始化 TableLayoutPanel 的列
            controlTableLayoutPanel.ColumnCount = Settings.COLUMNS_COUNT;
            for (int i = 0; i < Settings.COLUMNS_COUNT; i++)
            {
                controlTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / Settings.COLUMNS_COUNT));
            }
            // 初始化 TableLayoutPanel 的行
            controlTableLayoutPanel.RowCount = Settings.ROWS_COUNT;
            for (int i = 0; i < Settings.ROWS_COUNT; i++)
            {
                controlTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / Settings.ROWS_COUNT));
            }
            // 往 TableLayoutPanel 中添加按钮
            for (int i = 0; i < Settings.ROWS_COUNT; i++)
            {
                for (int j = 0; j < Settings.COLUMNS_COUNT; j++)
                {
                    Button btn = new Button
                    {
                        Name = "button_" + j + "_" + i,
                        Text = Settings.COLUMNS_DESCRIPTION[j] + Settings.ROWS_DESCRIPTION[i],
                        Dock = DockStyle.Fill,
                    };
                    if (t_export_config[i, j])
                        btn.ForeColor = Color.Red;
                    // 为每一个按钮添加鼠标点击事件回调函数
                    btn.MouseClick += new MouseEventHandler(Button_MouseClick);
                    controlTableLayoutPanel.Controls.Add(btn);
                }
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
            t_export_config[row, column] = !t_export_config[row, column];
            if (t_export_config[row, column])
            {
                button.ForeColor = Color.Red;
            }
            else
            {
                button.ForeColor = Color.Black;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            // 将设置值同步
            for (int i = 0; i < Settings.ROWS_COUNT; i++)
            {
                for (int j = 0; j < Settings.COLUMNS_COUNT; j++)
                {
                    Settings.EXPORT_CONFIG[i, j] = t_export_config[i, j];   // 同步设置到原设置中
                }
            }
            Dispose();
        }
    }

}
