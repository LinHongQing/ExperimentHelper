using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ExperimentHelper
{
    public class Settings
    {
        private const int DEFAULT_SHORT_STEP_DELAY = 50;
        private const int DEFAULT_MEDIUM_STEP_DELAY = 500;
        private const int DEFAULT_LONG_STEP_DELAY = 2000;
        public static int SHORT_STEP_DELAY;         // 短步骤延迟
        public static int MEDIUM_STEP_DELAY;        // 中步骤延迟
        public static int LONG_STEP_DELAY;          // 长步骤延迟

        public const int COLUMNS_COUNT = 12;     // 列
        public const int ROWS_COUNT = 8;         // 行
        public static readonly string[] ROWS_DESCRIPTION = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
        public static readonly string[] COLUMNS_DESCRIPTION = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        public static bool[,] EXPORT_CONFIG = new bool[ROWS_COUNT, COLUMNS_COUNT];

        private static void InitDefaultSettings()
        {
            SHORT_STEP_DELAY = DEFAULT_SHORT_STEP_DELAY;
            MEDIUM_STEP_DELAY = DEFAULT_MEDIUM_STEP_DELAY;
            LONG_STEP_DELAY = DEFAULT_LONG_STEP_DELAY;
            for (int i = 0; i < ROWS_COUNT; i++)
            {
                for (int j = 0; j < COLUMNS_COUNT; j++)
                {
                    if (i == 0 || i == ROWS_COUNT - 1)  // 第一行与最后一行
                    {
                        EXPORT_CONFIG[i, j] = false;
                    }
                    else
                    {
                        if (j == 0 || j == COLUMNS_COUNT - 1)   // 第一列与最后一列
                        {
                            EXPORT_CONFIG[i, j] = false;
                        }
                        else
                        {
                            EXPORT_CONFIG[i, j] = true;
                        }
                    }
                }
            }
        }

        public static void LoadSettings()
        {
            if (File.Exists("settings.dat"))
            {
                try
                {
                    using (FileStream fs = File.Open("settings.dat", FileMode.Open))
                    {
                        using (BinaryReader r = new BinaryReader(fs))
                        {
                            SHORT_STEP_DELAY = r.ReadInt32();
                            MEDIUM_STEP_DELAY = r.ReadInt32();
                            LONG_STEP_DELAY = r.ReadInt32();
                            for (int i = 0; i < ROWS_COUNT; i++)
                            {
                                for (int j = 0; j < COLUMNS_COUNT; j++)
                                {
                                    EXPORT_CONFIG[i, j] = r.ReadBoolean();
                                }
                            }
                        }
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
                    InitDefaultSettings();
                }

            }
            else
            {
                InitDefaultSettings();
            }
        }

        public static void SaveSettings()
        {
            using (FileStream fs = File.Open("settings.dat", FileMode.Create))
            {
                try
                {
                    using (BinaryWriter w = new BinaryWriter(fs))
                    {
                        w.Write(SHORT_STEP_DELAY);
                        w.Write(MEDIUM_STEP_DELAY);
                        w.Write(LONG_STEP_DELAY);
                        for (int i = 0; i < ROWS_COUNT; i++)
                        {
                            for (int j = 0; j < COLUMNS_COUNT; j++)
                            {
                                w.Write(EXPORT_CONFIG[i, j]);
                            }
                        }
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
