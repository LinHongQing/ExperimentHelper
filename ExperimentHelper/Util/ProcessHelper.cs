using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Process;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Util
{
    public class ProcessHelper
    {
        public enum ProcessTypeFlags
        {
            DEFAULT = -1, MAINBOARD_FIND_HANDLE = -2, MAINBOARD_FIND_RECTANGLE = -3, MAINBOARD_CHECK_POSITION = -4,
            OVERWRITE_PARENT_WND = 0,
            FIND_WINDOW = 2, FIND_WINDOW_EX = 3, FIND_WINDOW_BY_NAME = 4,
            FIND_CONTROL_BY_CLASSNAME = 5, FIND_CONTROL_BY_NAME = 6,
            GET_WINDOW_RECTANGLE = 7,
            CALCULATE_RECTANGLE_EXPORT_POINT_MATRIX = 8,
            SET_COMBOBOX_CURSEL = 9, SET_TEXTBOX_VALUE = 10,
            CONTROL_MOUSE_LBUTTON_CLICK = 11, MOUSE_LBUTTON_CLICK = 12,
        };

        public static List<IProcessItem> GenerateProcessQueue(
            ExportPointMatrix matrix,
            TargetRectangle rectangle,
            WindowHandle handle,
            SettingComponent settings)
        {
            List<IProcessItem> queue = new List<IProcessItem>();
            IProcessItem procItem;
            switch (settings.ProcessType)
            {
                case ProcessTypeFlags.MAINBOARD_FIND_HANDLE:
                    procItem = new ProcessItem_FindWindowByName(handle, settings.SearchTitle, 0, false);
                    queue.Add(procItem);
                    procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.MAINBOARD_FIND_RECTANGLE:
                    procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Plate 1", 0);
                    queue.Add(procItem);
                    procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                    queue.Add(procItem);
                    procItem = new ProcessItem_FindCtrlByCtrlClass(handle, "GXWND", 1);
                    queue.Add(procItem);
                    procItem = new ProcessItem_GetControlRectangle(handle);
                    queue.Add(procItem);
                    procItem = new ProcessItem_CalcRectExportPointMatrix(matrix, rectangle, settings.RowDeviation, settings.ColumnDeviation);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.MAINBOARD_CHECK_POSITION:
                    {
                        IIterator matrixPointIterator = matrix.Iterator();
                        for (; !matrixPointIterator.IsDone(); matrixPointIterator.Next())
                        {
                            ExportPointMatrixItem item = matrixPointIterator.CurrentItem();
                            if (!item.IsAvaliable)
                            {
                                continue;  // 没被选中忽略
                            }
                            else
                            {
                                procItem = new ProcessItem_MouseMove(item.PointX, item.PointY);
                                queue.Add(procItem);
                            }
                        }
                    }
                    break;
                case ProcessTypeFlags.OVERWRITE_PARENT_WND:
                    procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.FIND_WINDOW:
                    procItem = new ProcessItem_FindWindow(handle, settings.StringParam);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.FIND_WINDOW_EX:
                    procItem = new ProcessItem_FindWindowEx(handle, settings.StringParam);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.FIND_WINDOW_BY_NAME:
                    procItem = new ProcessItem_FindWindowByName(handle, settings.StringParam, settings.IntParam, false);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.FIND_CONTROL_BY_CLASSNAME:
                    procItem = new ProcessItem_FindCtrlByCtrlClass(handle, settings.StringParam, settings.IntParam);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.FIND_CONTROL_BY_NAME:
                    procItem = new ProcessItem_FindCtrlByCtrlName(handle, settings.StringParam, settings.IntParam);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.GET_WINDOW_RECTANGLE:
                    procItem = new ProcessItem_GetControlRectangle(handle);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.CALCULATE_RECTANGLE_EXPORT_POINT_MATRIX:
                    procItem = new ProcessItem_CalcRectExportPointMatrix(matrix, rectangle, settings.RowDeviation, settings.RowDeviation);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.SET_COMBOBOX_CURSEL:
                    procItem = new ProcessItem_SetComboBoxCrusel(handle, settings.IntParam);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.SET_TEXTBOX_VALUE:
                    procItem = new ProcessItem_SetTextBoxValue(handle, settings.StringParam);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.CONTROL_MOUSE_LBUTTON_CLICK:
                    procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.MOUSE_LBUTTON_CLICK:
                    procItem = new ProcessItem_NormMouseLeftButtonClick();
                    queue.Add(procItem);
                    break;
                case ProcessTypeFlags.DEFAULT:
                    {
                        IIterator matrixPointIterator = matrix.Iterator();
                        for (; !matrixPointIterator.IsDone(); matrixPointIterator.Next())
                        {
                            ExportPointMatrixItem item = matrixPointIterator.CurrentItem();
                            // 判断该位置是否被选中
                            if (!item.IsAvaliable)
                            {
                                continue;  // 没被选中忽略
                            }
                            else
                            // 开始执行既定步骤
                            {
                                // 鼠标移动到该位置
                                procItem = new ProcessItem_MouseMove(item.PointX, item.PointY);
                                queue.Add(procItem);
                                // 鼠标点击该位置
                                procItem = new ProcessItem_NormMouseLeftButtonClick();
                                queue.Add(procItem);
                                /* 弹出图片详情后 */
                                // 获取主窗口句柄
                                procItem = new ProcessItem_FindWindowByName(handle, settings.SearchTitle, 0, true);
                                queue.Add(procItem);
                                procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                                queue.Add(procItem);
                                // 找到 Plate 1 控件句柄
                                procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Plate 1", 0);
                                queue.Add(procItem);
                                procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                                queue.Add(procItem);
                                // 找到 Process... 按钮句柄
                                procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Process...", 0);
                                queue.Add(procItem);
                                // 点击 Process 按钮
                                procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                queue.Add(procItem);

                                /* 弹出 Image Stitching 后 */
                                // 获取 Image Stitching 窗口句柄
                                procItem = new ProcessItem_FindWindowByName(handle, "Image Stitching", 0, true);
                                queue.Add(procItem);
                                procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                                queue.Add(procItem);
                                // 获取 ComboBox 句柄
                                procItem = new ProcessItem_FindCtrlByCtrlClass(handle, "ComboBox", 0);
                                queue.Add(procItem);
                                // 设置 ComboBox 选项为第 1 个
                                procItem = new ProcessItem_SetComboBoxCrusel(handle, 0);
                                queue.Add(procItem);
                                // 获取 Edit 句柄
                                procItem = new ProcessItem_FindCtrlByCtrlClass(handle, "Edit", 0);
                                queue.Add(procItem);
                                // 设置 Edit 值为 50.00
                                procItem = new ProcessItem_SetTextBoxValue(handle, "50.00");
                                queue.Add(procItem);
                                // 获取 OK 按钮句柄
                                procItem = new ProcessItem_FindCtrlByCtrlName(handle, "OK", 0);
                                queue.Add(procItem);
                                // 点击 OK 按钮
                                procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                queue.Add(procItem);

                                /* 弹出 Image Processing 窗口后 */

                                for (int i = 0; i < 3; i++)
                                {
                                    // 找到 Image Processing 窗口句柄
                                    procItem = new ProcessItem_FindWindowByName(handle, "Image Processing", 0, true);
                                    queue.Add(procItem);
                                    procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                                    queue.Add(procItem);
                                    // 分别选择不同选项
                                    switch (i)
                                    {
                                        case 0: // DAPI + GFP, 无需操作复选框
                                            break;
                                        case 1: // DAPI, 取消选中 GFP
                                                // 获取 GFP 复选框句柄
                                            procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Stitched[GFP 469,525]", 0);
                                            queue.Add(procItem);
                                            // 点击 GFP 按钮
                                            procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                            queue.Add(procItem);
                                            break;
                                        case 2: // GFP, 取消选中 DAPI
                                                // 获取 DAPI 复选框句柄
                                            procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Stitched[DAPI 377,447]", 0);
                                            queue.Add(procItem);
                                            // 点击 DAPI 复选框
                                            procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                            queue.Add(procItem);
                                            // 获取 GFP 复选框句柄
                                            procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Stitched[GFP 469,525]", 0);
                                            queue.Add(procItem);
                                            // 点击 GFP 复选框
                                            procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                            queue.Add(procItem);
                                            break;
                                    }
                                    // 获取 Save Image Set 按钮句柄
                                    procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Save Image Set", 0);
                                    queue.Add(procItem);
                                    // 点击 Save Image Set 按钮
                                    procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                    queue.Add(procItem);

                                    /* 弹出 Image Save Options 窗口后 */
                                    // 获取 Save Image Options 窗口句柄
                                    procItem = new ProcessItem_FindWindowByName(handle, "Image Save Options", 0, true);
                                    queue.Add(procItem);
                                    procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                                    queue.Add(procItem);
                                    // 获取 Save picture for presentation 单选框句柄
                                    procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Save picture for presentation", 0);
                                    queue.Add(procItem);
                                    // 点击 Save picture for presentation 单选框
                                    procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                    queue.Add(procItem);
                                    // 获取 Save entire image (1 camera pixel resolution) 单选框句柄
                                    procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Save entire image (1 camera pixel resolution)", 0);
                                    queue.Add(procItem);
                                    // 点击 Save entire image (1 camera pixel resolution) 单选框
                                    procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                    queue.Add(procItem);
                                    // 获取 OK 按钮句柄
                                    procItem = new ProcessItem_FindCtrlByCtrlName(handle, "OK", 0);
                                    queue.Add(procItem);
                                    // 点击 OK 按钮
                                    procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                    queue.Add(procItem);

                                    /* 弹出 Save As Picture 窗口后 */
                                    // 获取 Save As Picture 窗口句柄
                                    procItem = new ProcessItem_FindWindowByName(handle, "Save As Picture", 0, false);
                                    queue.Add(procItem);
                                    procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                                    queue.Add(procItem);
                                    // 获取文件名输入框
                                    // 获取文件名输入框的句柄
                                    procItem = new ProcessItem_FindCtrlByCtrlClass(handle, "Edit", 0);
                                    queue.Add(procItem);
                                    // 设置文件名(eg. ExpHlp_Expo_PosA1_DAPI_GFP_2017100112)
                                    StringBuilder sb = new StringBuilder();
                                    sb.AppendFormat("ExpHlp_Expo_Pos{0}_", item.PointDescription);
                                    switch (i)
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
                                    procItem = new ProcessItem_SetTextBoxValue(handle, sb.ToString());
                                    queue.Add(procItem);
                                    // 获取 保存 按钮的句柄
                                    procItem = new ProcessItem_FindCtrlByCtrlName(handle, "保存", 0);
                                    queue.Add(procItem);
                                    // 点击 保存 按钮
                                    procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                    queue.Add(procItem);
                                }

                                /* 执行完成 Image Processing 窗口操作后 */
                                // 找到 Image Processing 窗口句柄
                                procItem = new ProcessItem_FindWindowByName(handle, "Image Processing", 0, true);
                                queue.Add(procItem);
                                procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                                queue.Add(procItem);
                                // 获取 Close 按钮句柄
                                procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Close", 0);
                                queue.Add(procItem);
                                // 点击 Close 按钮
                                procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                queue.Add(procItem);
                                // 获取主窗口句柄
                                procItem = new ProcessItem_FindWindowByName(handle, settings.SearchTitle, 0, true);
                                queue.Add(procItem);
                                procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                                queue.Add(procItem);
                                // 获取 Plate 1 控件句柄
                                procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Plate 1", 0);
                                queue.Add(procItem);
                                procItem = new ProcessItem_OverwriteCurrHndl2PrntHndl(handle);
                                queue.Add(procItem);
                                // 获取 Close 按钮句柄
                                procItem = new ProcessItem_FindCtrlByCtrlName(handle, "Close", 1);
                                queue.Add(procItem);
                                // 点击 Close 按钮
                                procItem = new ProcessItem_CtrlMouseLeftButtonClick(handle);
                                queue.Add(procItem);
                            }
                        }
                    }
                    break;
            }
            return queue;
        }
    }
}
