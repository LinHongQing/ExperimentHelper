using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper
{
    class ProcessGenerator
    {
        public static List<ProcessItem> GenerateFinalProcess(ref Base.POINT[,] matrix, string mainFormTitle)
        {
            List<ProcessItem> procs = new List<ProcessItem>();
            ProcessItem proc;
            if (matrix == null)
                return procs;
            // 遍历选择矩阵中所有的位置
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    // 判断该位置是否被选中
                    if (!Settings.EXPORT_CONFIG[i, j])
                    {
                        continue;  // 没被选中忽略
                    }
                    else
                    // 开始执行既定步骤
                    {
                        // 鼠标移动到该位置
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.MOUSE_MOVE,
                            Position = matrix[i, j],
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 鼠标点击该位置
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_MOUSEEVENT,
                            StepDelay = Settings.MEDIUM_STEP_DELAY,
                        };
                        procs.Add(proc);

                        /* 弹出图片详情后 */
                        // 获取主窗口句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_WINDOW_BY_NAME,
                            StringParameter = mainFormTitle,
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.OVERWRITE_PARENT_WND,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 找到 Plate 1 控件句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                            StringParameter = "Plate 1",
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.OVERWRITE_PARENT_WND,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 找到 Process... 按钮句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                            StringParameter = "Process...",
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 点击 Process 按钮
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                            StepDelay = Settings.LONG_STEP_DELAY,
                        };
                        procs.Add(proc);

                        /* 弹出 Image Stitching 后 */
                        // 获取 Image Stitching 窗口句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_WINDOW_BY_NAME,
                            StringParameter = "Image Stitching",
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.OVERWRITE_PARENT_WND,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 获取 ComboBox 句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_CLASSNAME,
                            StringParameter = "ComboBox",
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 设置 ComboBox 选项为第 1 个
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.SET_COMBOBOX_CURSEL,
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 获取 Edit 句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_CLASSNAME,
                            StringParameter = "Edit",
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 设置 Edit 值为 50.00
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.SET_TEXTBOX_VALUE,
                            StringParameter = "50.00",
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 获取 OK 按钮句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                            StringParameter = "OK",
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 点击 OK 按钮
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                            StepDelay = Settings.LONG_STEP_DELAY,
                        };
                        procs.Add(proc);

                        /* 弹出 Image Processing 窗口后 */

                        for (int k = 0; k < 3; k++)
                        {
                            // 找到 Image Processing 窗口句柄
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_WINDOW_BY_NAME,
                                StringParameter = "Image Processing",
                                IntegerParameter = 0,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.OVERWRITE_PARENT_WND,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 分别选择不同选项
                            switch (k)
                            {
                                case 0: // DAPI + GFP, 无需操作复选框
                                    break;
                                case 1: // DAPI, 取消选中 GFP
                                    // 获取 GFP 复选框句柄
                                    proc = new ProcessItem
                                    {
                                        ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                                        StringParameter = "Stitched[GFP 469,525]",
                                        IntegerParameter = 0,
                                        StepDelay = Settings.SHORT_STEP_DELAY,
                                    };
                                    procs.Add(proc);
                                    // 点击 GFP 按钮
                                    proc = new ProcessItem
                                    {
                                        ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                                        StepDelay = Settings.SHORT_STEP_DELAY,
                                    };
                                    procs.Add(proc);
                                    break;
                                case 2: // GFP, 取消选中 DAPI
                                    // 获取 DAPI 复选框句柄
                                    proc = new ProcessItem
                                    {
                                        ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                                        StringParameter = "Stitched[DAPI 377,447]",
                                        IntegerParameter = 0,
                                        StepDelay = Settings.SHORT_STEP_DELAY,
                                    };
                                    procs.Add(proc);
                                    // 点击 DAPI 复选框
                                    proc = new ProcessItem
                                    {
                                        ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                                        StepDelay = Settings.SHORT_STEP_DELAY,
                                    };
                                    procs.Add(proc);
                                    // 获取 GFP 复选框句柄
                                    proc = new ProcessItem
                                    {
                                        ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                                        StringParameter = "Stitched[GFP 469,525]",
                                        IntegerParameter = 0,
                                        StepDelay = Settings.SHORT_STEP_DELAY,
                                    };
                                    procs.Add(proc);
                                    // 点击 GFP 复选框
                                    proc = new ProcessItem
                                    {
                                        ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                                        StepDelay = Settings.SHORT_STEP_DELAY,
                                    };
                                    procs.Add(proc);
                                    break;
                            }
                            // 获取 Save Image Set 按钮句柄
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                                StringParameter = "Save Image Set",
                                IntegerParameter = 0,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 点击 Save Image Set 按钮
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                                StepDelay = Settings.MEDIUM_STEP_DELAY,
                            };
                            procs.Add(proc);

                            /* 弹出 Image Save Options 窗口后 */
                            // 获取 Save Image Options 窗口句柄
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_WINDOW_BY_NAME,
                                StringParameter = "Image Save Options",
                                IntegerParameter = 0,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.OVERWRITE_PARENT_WND,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 获取 Save picture for presentation 单选框句柄
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                                StringParameter = "Save picture for presentation",
                                IntegerParameter = 0,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 点击 Save picture for presentation 单选框
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 获取 Save entire image (1 camera pixel resolution) 单选框句柄
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                                StringParameter = "Save entire image (1 camera pixel resolution)",
                                IntegerParameter = 0,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 点击 Save entire image (1 camera pixel resolution) 单选框
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 获取 OK 按钮句柄
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                                StringParameter = "OK",
                                IntegerParameter = 0,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 点击 OK 按钮
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                                StepDelay = Settings.MEDIUM_STEP_DELAY,
                            };
                            procs.Add(proc);

                            /* 弹出 Save As Picture 窗口后 */
                            // 获取 Save As Picture 窗口句柄
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_WINDOW_BY_NAME,
                                StringParameter = "Save As Picture",
                                IntegerParameter = 0,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.OVERWRITE_PARENT_WND,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 获取文件名输入框
                            // 获取文件名输入框的句柄
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_CLASSNAME,
                                StringParameter = "Edit",
                                IntegerParameter = 0,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 设置文件名(eg. ExpHlp_Export_PosA1_DAPI_GFP_201710011200)
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("ExpHlp_Export_Pos{0}{1}_", Settings.ROWS_DESCRIPTION[i], Settings.COLUMNS_DESCRIPTION[j]);
                            switch (k)
                            {
                                case 0:
                                    // DAPI + GFP
                                    sb.Append("DAPI_GFP_");
                                    break;
                                case 1:
                                    // DAPI
                                    sb.Append("DAPI_");
                                    break;
                                case 2:
                                    // GFP
                                    sb.Append("GFP_");
                                    break;
                            }
                            sb.Append(DateTime.Now.ToString("yyyyMMddHHmm"));
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.SET_TEXTBOX_VALUE,
                                StringParameter = sb.ToString(),
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 获取 保存 按钮的句柄
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                                StringParameter = "保存",
                                IntegerParameter = 0,
                                StepDelay = Settings.SHORT_STEP_DELAY,
                            };
                            procs.Add(proc);
                            // 点击 保存 按钮
                            proc = new ProcessItem
                            {
                                ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                                StepDelay = Settings.MEDIUM_STEP_DELAY,
                            };
                            procs.Add(proc);
                        }

                        /* 执行完成 Image Processing 窗口操作后 */
                        // 找到 Image Processing 窗口句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_WINDOW_BY_NAME,
                            StringParameter = "Image Processing",
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.OVERWRITE_PARENT_WND,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 获取 Close 按钮句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                            StringParameter = "Close",
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 点击 Close 按钮
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 获取主窗口句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_WINDOW_BY_NAME,
                            StringParameter = mainFormTitle,
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.OVERWRITE_PARENT_WND,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 获取 Plate 1 控件句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                            StringParameter = "Plate 1",
                            IntegerParameter = 0,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.OVERWRITE_PARENT_WND,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 获取 Close 按钮句柄
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                            StringParameter = "Close",
                            IntegerParameter = 1,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                        // 点击 Close 按钮
                        proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM,
                            StepDelay = Settings.SHORT_STEP_DELAY,
                        };
                        procs.Add(proc);
                    }
                }
            }
            return procs;
        }

        //public static ProcessItem GenerateCalculateExportPointProcess(Base.RECT rect)
        //{
        //    ProcessItem proc;
        //    proc = new ProcessItem
        //    {
        //        ProcessType = Process.ProcessTypeFlags.CALCULATE_EXPORT_POINT_MATRIX,
        //        StepDelay = Settings.SHORT_STEP_DELAY,
        //        Rectangle = rect,
        //    };
        //    return proc;
        //}

        public static List<ProcessItem> GenerateFindFormProcess(string formTitle)
        {
            List<ProcessItem> procs = new List<ProcessItem>();
            ProcessItem proc;
            proc = new ProcessItem
            {
                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_WINDOW_BY_NAME,
                StringParameter = formTitle,
                StepDelay = Settings.SHORT_STEP_DELAY,
            };
            procs.Add(proc);

            proc = new ProcessItem
            {
                ProcessType = Process.ProcessTypeFlags.OVERWRITE_PARENT_WND,
                StepDelay = Settings.SHORT_STEP_DELAY,
            };
            procs.Add(proc);
            return procs;
        }

        public static List<ProcessItem> GenerateFindTargetProcess()
        {
            List<ProcessItem> procs = new List<ProcessItem>();
            ProcessItem proc;

            proc = new ProcessItem
            {
                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME,
                StringParameter = "Plate 1",
                IntegerParameter = 0,
                StepDelay = Settings.SHORT_STEP_DELAY,
            };
            procs.Add(proc);
            proc = new ProcessItem
            {
                ProcessType = Process.ProcessTypeFlags.OVERWRITE_PARENT_WND,
                StepDelay = Settings.SHORT_STEP_DELAY,
            };
            procs.Add(proc);
            proc = new ProcessItem
            {
                ProcessType = Process.ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_CLASSNAME,
                StringParameter = "GXWND",
                IntegerParameter = 1,
                StepDelay = Settings.SHORT_STEP_DELAY,
            };
            procs.Add(proc);
            proc = new ProcessItem
            {
                ProcessType = Process.ProcessTypeFlags.GET_SPECITIED_CONTROL_RECTANGLE,
                StepDelay = Settings.SHORT_STEP_DELAY,
            };
            procs.Add(proc);
            proc = new ProcessItem
            {
                ProcessType = Process.ProcessTypeFlags.CALCULATE_EXPORT_POINT_MATRIX,
                StepDelay = Settings.SHORT_STEP_DELAY,
            };
            procs.Add(proc);
            return procs;
        }

        public static List<ProcessItem> GenerateCheckPositionProcess(ref Base.POINT[,] matrix)
        {
            List<ProcessItem> procs = new List<ProcessItem>();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (Settings.EXPORT_CONFIG[i, j])
                    {
                        ProcessItem proc = new ProcessItem
                        {
                            ProcessType = Process.ProcessTypeFlags.MOUSE_MOVE,
                            Position = matrix[i, j],
                            StepDelay = Settings.MEDIUM_STEP_DELAY,
                        };
                        procs.Add(proc);
                    }
                }
            }
            return procs;
        }
    }
}
