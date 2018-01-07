using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using System;
using System.IO;

namespace ExperimentHelper.Model
{
    public class GeneralSettingModel : ISettingModel
    {
        private SettingComponent settings;

        private const int DEFAULT_SHORT_STEP_DELAY = 50;
        private const int DEFAULT_MEDIUM_STEP_DELAY = 500;
        private const int DEFAULT_LONG_STEP_DELAY = 2000;
        private const int DEFAULT_COLUMNS_COUNT = 12;     // 默认列数
        private const int DEFAULT_ROWS_COUNT = 8;         // 默认行数
        private const int DEFAULT_COLUMN_DEVIATION = 20;
        private const int DEFAULT_ROW_DEVIATION = 10;
        private readonly string[] defaultRowsDescription = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
        private readonly string[] defaultColumnsDescription = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

        private ExportPointMatrix instance;

        public GeneralSettingModel(ExportPointMatrix instance, SettingComponent settings)
        {
            this.instance = instance;
            this.settings = settings;
        }

        public void InitializeSettings()
        {
            LoadSettingsFromFile();
        }

        public void LoadDefaultSettings()
        {
            instance.Init(DEFAULT_ROWS_COUNT, DEFAULT_COLUMNS_COUNT);
            for (int i = 0; i < DEFAULT_ROWS_COUNT; i++)
            {
                for (int j = 0; j < DEFAULT_COLUMNS_COUNT; j++)
                {
                    string defaultCurrentItemDescription = defaultColumnsDescription[j] + defaultRowsDescription[i];
                    ExportPointMatrixItem item = new ExportPointMatrixItem(0, 0, defaultCurrentItemDescription, false);
                    if (i == 0 || i == DEFAULT_ROWS_COUNT - 1)  // 第一行与最后一行
                    {
                        item.IsAvaliable = false;
                    }
                    else
                    {
                        if (j == 0 || j == DEFAULT_COLUMNS_COUNT - 1)   // 第一列与最后一列
                        {
                            item.IsAvaliable = false;
                        }
                        else
                        {
                            item.IsAvaliable = true;
                        }
                    }
                    instance.SetExportPointMatrixItem(i, j, item);
                }
            }
            settings.ShortStepDelay = DEFAULT_SHORT_STEP_DELAY;
            settings.MediumStepDelay = DEFAULT_MEDIUM_STEP_DELAY;
            settings.LongStepDelay = DEFAULT_LONG_STEP_DELAY;
            settings.RowDeviation = DEFAULT_ROW_DEVIATION;
            settings.ColumnDeviation = DEFAULT_COLUMN_DEVIATION;
        }

        public void SaveSettings()
        {
            SaveSettingsToFile();
        }

        private void LoadSettingsFromFile()
        {
            if (File.Exists("settings.dat"))
            {
                try
                {
                    using (FileStream fs = File.Open("settings.dat", FileMode.Open))
                    {
                        using (BinaryReader r = new BinaryReader(fs))
                        {
                            settings.ShortStepDelay = r.ReadInt32();
                            settings.MediumStepDelay = r.ReadInt32();
                            settings.LongStepDelay = r.ReadInt32();
                            int rowsCount = r.ReadInt32();
                            int columnsCount = r.ReadInt32();
                            settings.RowDeviation = r.ReadInt32();
                            settings.ColumnDeviation = r.ReadInt32();

                            if (rowsCount <= 0 || columnsCount <=0)
                            {
                                LoadDefaultSettings();
                            }
                            else
                            {
                                instance.Init(rowsCount, columnsCount);
                                for (int i = 0; i < rowsCount; i++)
                                {
                                    for (int j = 0; j < columnsCount; j++)
                                    {
                                        string description = r.ReadString();
                                        bool isAvaliable = r.ReadBoolean();
                                        ExportPointMatrixItem item = new ExportPointMatrixItem(0, 0, description, isAvaliable);
                                        instance.SetExportPointMatrixItem(i, j, item);
                                    }
                                }
                            }
                        }
                        fs.Close();
                    }
#if DEBUG
                    Console.WriteLine("读取文件完成");
#endif
                }
                catch (IOException e)
                {
#if DEBUG
                    Console.WriteLine("读取文件发生 IO 错误: " + e.Message);
#endif
                    LoadDefaultSettings();
                }

            }
            else
            {
                LoadDefaultSettings();
            }
        }

        private void SaveSettingsToFile()
        {
            using (FileStream fs = File.Open("settings.dat", FileMode.Create))
            {
                try
                {
                    using (BinaryWriter w = new BinaryWriter(fs))
                    {
                        w.Write(settings.ShortStepDelay);
                        w.Write(settings.MediumStepDelay);
                        w.Write(settings.LongStepDelay);
                        int rowsCount = instance.GetMatrixRowCount();
                        int columnsCount = instance.GetMatrixColumnCount();
                        w.Write(rowsCount);
                        w.Write(columnsCount);
                        w.Write(settings.RowDeviation);
                        w.Write(settings.ColumnDeviation);
                        IIterator instanceIterator = instance.Iterator();
                        instanceIterator.First();
                        while(!instanceIterator.IsDone())
                        {
                            ExportPointMatrixItem item = instanceIterator.CurrentItem();
                            w.Write(item.PointDescription);
                            w.Write(item.IsAvaliable);
                            instanceIterator.Next();
                        }
                        w.Close();
                    }
#if DEBUG
                    Console.WriteLine("写入文件完成");
#endif
                }
                catch (IOException e)
                {
#if DEBUG
                    Console.WriteLine("写入文件发生 IO 错误: " + e.Message);
#endif
                }
            }
        }
    }
}
