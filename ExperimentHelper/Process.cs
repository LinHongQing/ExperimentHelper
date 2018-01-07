using ExperimentHelper.Util;
using ExperimentHelper.Basic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ExperimentHelper
{

    class ProcessThread
    {
        #region 变量声明初始化部分
        public enum ProcessTypeFlags
        {
            OVERWRITE_PARENT_WND = 0,
            FIND_WINDOW = 2, FIND_WINDOW_EX = 3,
            FIND_SPECIFIED_WINDOW_BY_NAME = 4, FIND_SPECIFIED_CONTROL_BY_CLASSNAME = 5, FIND_SPECIFIED_CONTROL_BY_NAME = 6,
            GET_SPECITIED_CONTROL_RECTANGLE = 7,
            CALCULATE_EXPORT_POINT_MATRIX = 8,
            SET_COMBOBOX_CURSEL = 9, SET_TEXTBOX_VALUE = 10,
            MOUSE_LBUTTON_CLICK_WM = 11, MOUSE_LBUTTON_CLICK_MOUSEEVENT = 12, MOUSE_MOVE = 13,
        };

        private StructComponent.RECT rect;

        private WindowHandle wndItem;
        private List<IntPtr> wnds;
        private List<ProcessItem> procs;
        private bool needMoreProc;
        private ProcessThread nextProc;
        private bool isRunning = true;
        public bool IsRunning { get => isRunning; set => isRunning = value; }
        #endregion

        #region Delegate 部分
        public delegate void ResultSendNotifer(ResultItem rs);
        public event ResultSendNotifer ResultSend;
        public void OnResultSend(ResultItem rs)
        {
            if (ResultSend != null)
            {
                ResultSend.Invoke(rs);
            }
        }

        public delegate void WndChangeNotifier(WindowHandle wnd);
        public event WndChangeNotifier WndChange;
        public void OnWndChange(WindowHandle wnd)
        {
            if (WndChange != null)
            {
                WndChange.Invoke(wnd);
            }
        }

        public delegate void ProcessFinishNotifer(bool needMoreProc, ProcessThread nextProc);
        public event ProcessFinishNotifer ProcessFinish;
        public void OnProcessFinish(bool needMoreProc, ProcessThread nextProc)
        {
            if (ProcessFinish != null)
            {
                ProcessFinish.Invoke(needMoreProc, nextProc);
            }
        }
        #endregion

        public ProcessThread(ProcessItem proc, ref WindowHandle wnd, bool needMoreProc, ProcessThread nextProc)
        {
            procs = new List<ProcessItem>
            {
                proc
            };
            wndItem = wnd;
            this.needMoreProc = needMoreProc;
            this.nextProc = nextProc;
        }

        public ProcessThread(List<ProcessItem> procs, ref WindowHandle wnd, bool needMoreProc, ProcessThread nextProc)
        {
            this.procs = procs;
            wndItem = wnd;
            this.needMoreProc = needMoreProc;
            this.nextProc = nextProc;
        }

        /// <summary>
        /// 运行时的 Exception
        /// </summary>
        public class ProcessException : Exception
        {
            public ProcessException(string msg) : base(msg) { }

            public override string Message => base.Message;
        }

        /// <summary>
        /// ProcessThread 功能执行入口
        /// </summary>
        public void Go()
        {
            try
            {
                foreach (ProcessItem proc in procs)
                {
                    if (isRunning == false)
                    {
#if DEBUG
                        Console.WriteLine("线程中止");
#endif
                        OnResultSend(new ResultItem(ResultItem.States.WARNING, "线程已中止"));
                        OnProcessFinish(needMoreProc, nextProc);
                        break;
                    }

                    switch (proc.ProcessType)
                    {
                        case ProcessTypeFlags.OVERWRITE_PARENT_WND:
                            DoOverwriteParentWnd(proc);
                            break;

                        case ProcessTypeFlags.FIND_WINDOW:
                            DoFindWindow(proc);
                            break;

                        case ProcessTypeFlags.FIND_WINDOW_EX:
                            DoFindWindowEx(proc);
                            break;

                        case ProcessTypeFlags.MOUSE_LBUTTON_CLICK_WM:
                            DoMouseLButtonClick(proc, false);
                            break;

                        case ProcessTypeFlags.MOUSE_LBUTTON_CLICK_MOUSEEVENT:
                            DoMouseLButtonClick(proc, true);
                            break;

                        case ProcessTypeFlags.MOUSE_MOVE:
                            DoMouseMove(proc);
                            break;

                        case ProcessTypeFlags.FIND_SPECIFIED_WINDOW_BY_NAME:
                            DoFindWindowByName(proc);
                            break;

                        case ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_CLASSNAME:
                            DoFindSpecifiedControl(proc, true);
                            break;

                        case ProcessTypeFlags.FIND_SPECIFIED_CONTROL_BY_NAME:
                            DoFindSpecifiedControl(proc, false);
                            break;

                        case ProcessTypeFlags.GET_SPECITIED_CONTROL_RECTANGLE:
                            DoGetControlRectangle(proc);
                            break;

                        case ProcessTypeFlags.CALCULATE_EXPORT_POINT_MATRIX:
                            DoCalculateRectangleExportPointMatrix(proc);
                            break;

                        case ProcessTypeFlags.SET_COMBOBOX_CURSEL:
                            DoSetComboBoxCursel(proc);
                            break;

                        case ProcessTypeFlags.SET_TEXTBOX_VALUE:
                            DoSetTextBoxValue(proc);
                            break;
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("延迟 {0} ms", proc.StepDelay);
                    OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString()));
                    Thread.Sleep(proc.StepDelay);
                }
                isRunning = false;
            }
            catch (ProcessException e)
            {
#if DEBUG
                Console.WriteLine(e.ToString());
#endif
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("运行时遇到错误: {0}", e.Message);
                isRunning = true;
                OnResultSend(new ResultItem(ResultItem.States.ERROR, sb.ToString()));
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine(e.ToString());
#endif
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("运行时遇到其他错误: {0}", e.Message);
                isRunning = true;
                OnResultSend(new ResultItem(ResultItem.States.ERROR, sb.ToString()));
            }
            finally
            {
                OnProcessFinish(needMoreProc, nextProc);
            }
        }

        /// <summary>
        /// 将当前句柄覆盖到父句柄中
        /// </summary>
        /// <param name="proc">操作信息对象</param>
        private void DoOverwriteParentWnd(ProcessItem proc)
        {
            wndItem.ParentWnd = wndItem.CurrentWnd;
            wndItem.CurrentWnd = IntPtr.Zero;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("成功将当前句柄覆盖到父句柄中, 父句柄为 0x{0:x8}", wndItem.ParentWnd.ToInt32());
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString()));
            OnWndChange(wndItem);
        }

        /// <summary>
        /// 寻找父窗体
        /// </summary>
        /// <param name="proc">操作信息对象</param>
        private void DoFindWindow(ProcessItem proc)
        {
            wndItem.ParentWnd = Win32.FindWindow(null, proc.StringParameter);
            if (wndItem.ParentWnd != IntPtr.Zero && wndItem.ParentWnd != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("成功找到名称为 {0} 的窗口句柄 0x{1:x8}", proc.StringParameter, wndItem.ParentWnd.ToInt32());
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString()));
                OnWndChange(wndItem);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("未能找到名称 {0} 的窗口句柄", proc.StringParameter);
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                OnResultSend(new ResultItem(ResultItem.States.WARNING, sb.ToString()));
            }
        }

        /// <summary>
        /// 寻找父窗体中的子窗体(控件)
        /// </summary>
        /// <param name="proc">操作信息对象</param>
        private void DoFindWindowEx(ProcessItem proc)
        {
            if (wndItem.ParentWnd == IntPtr.Zero || wndItem.ParentWnd == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("父级句柄无效, 无法执行该操作");
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                throw new ProcessException(sb.ToString());
            }
            wndItem.CurrentWnd = Win32.FindWindowEx(wndItem.ParentWnd, IntPtr.Zero, null, proc.StringParameter);
            if (wndItem.CurrentWnd != IntPtr.Zero && wndItem.CurrentWnd != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("成功在父级窗口句柄 0x{0:x8} 中找到 {1} 的句柄 0x{2:x8}", wndItem.ParentWnd.ToInt32(), proc.StringParameter, wndItem.CurrentWnd.ToInt32());
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString()));
                OnWndChange(wndItem);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("未能找到 {0} 的句柄", proc.StringParameter);
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                OnResultSend(new ResultItem(ResultItem.States.WARNING, sb.ToString()));
            }
        }

        /// <summary>
        /// 鼠标左键点击操作
        /// </summary>
        /// <param name="proc">操作信息对象</param>
        /// <param name="isMouseEvent">true 为 MouseEvent 事件, false 为 WM 事件</param>
        private void DoMouseLButtonClick(ProcessItem proc, Boolean isMouseEvent)
        {
            if (isMouseEvent)
            {
                Win32.mouse_event((int)Win32.MouseEventFlags.LeftDown | (int)Win32.MouseEventFlags.LeftUp, 0, 0, 0, UIntPtr.Zero);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("鼠标点击完成");
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString()));
            }
            else if (wndItem.CurrentWnd == IntPtr.Zero || wndItem.CurrentWnd == null)
            {
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("当前句柄无效, 无法执行该操作");
#if DEBUG
                    Console.WriteLine(sb.ToString());
#endif
                    throw new ProcessException(sb.ToString());
                }
            }
            else
            {
                Win32.PostMessage(wndItem.CurrentWnd, (int)Win32.MessageFlags.WM_LBUTTONDOWN, null, null);
                Win32.PostMessage(wndItem.CurrentWnd, (int)Win32.MessageFlags.WM_LBUTTONDOWN, null, null);
                Win32.PostMessage(wndItem.CurrentWnd, (int)Win32.MessageFlags.WM_LBUTTONUP, null, null);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("发送给句柄 0x{0:x8} 鼠标点击事件完成", wndItem.CurrentWnd.ToInt32());
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString()));
            }
        }

        private void DoMouseMove(ProcessItem proc)
        {
            Win32.SetCursorPos((int)proc.Position.X, (int)proc.Position.Y);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("鼠标已移到指定位置 ({0}, {1})", proc.Position.X, proc.Position.Y);
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString()));
        }

        /// <summary>
        /// 通过窗口名称查找窗口句柄
        /// </summary>
        /// <param name="proc">操作信息对象</param>
        private void DoFindWindowByName(ProcessItem proc)
        {
            wnds = new List<IntPtr>();
            Win32.EnumWindows((handle, s) =>
            {
                //####取标题
                StringBuilder title = new StringBuilder(100);
                Win32.GetWindowText(handle, title, 100);//取标题
                if (title.ToString().Contains(s))
                {
                    wnds.Add(handle);
                }
                return true;

            }, proc.StringParameter);

            StringBuilder sb = new StringBuilder();
            if (wnds.Count == 0)
            {
                sb = new StringBuilder();
                sb.AppendFormat("无法找到指定窗口 \"{0}\" 的句柄", proc.StringParameter);
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                OnResultSend(new ResultItem(ResultItem.States.WARNING, sb.ToString()));
                return;
            }
            if (wnds.Count > proc.IntegerParameter && proc.IntegerParameter >= 0)
            {
                wndItem.CurrentWnd = wnds[proc.IntegerParameter];
            }
            else
            {
                wndItem.CurrentWnd = wnds[wnds.Count - 1];
            }
            sb.AppendFormat("成功找到指定窗口 \"{0}\" 的句柄，共 {1} 个, 当前选择第 {2} 个 0x{3:x8}",
                proc.StringParameter, wnds.Count, proc.IntegerParameter + 1, wndItem.CurrentWnd.ToInt32());
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString()));
            OnWndChange(wndItem);
        }

        /// <summary>
        /// 查找指定的控件
        /// </summary>
        /// <param name="proc">操作对象信息</param>
        /// <param name="isbyClassName">true 按类名查找, false 按标题查找</param>
        private void DoFindSpecifiedControl(ProcessItem proc, Boolean isbyClassName)
        {
            if (wndItem.ParentWnd == IntPtr.Zero || wndItem.ParentWnd == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("父级句柄无效, 无法执行该操作");
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                throw new ProcessException(sb.ToString());
            }
            else
            {
                wnds = new List<IntPtr>();
                if (isbyClassName)
                {
                    Win32.EnumChildWindows(wndItem.ParentWnd, (handle, s) =>
                    {
                        //####取类型
                        StringBuilder type = new StringBuilder(100);
                        Win32.GetClassName(handle, type, 100);//取类型
                        if (type.ToString().Contains(s))
                        {
                            wnds.Add(handle);
                        }
                        return true;
                    }, proc.StringParameter);
                }
                else
                {
                    Win32.EnumChildWindows(wndItem.ParentWnd, (handle, s) =>
                    {
                        //####取标题
                        StringBuilder title = new StringBuilder(100);
                        Win32.GetWindowText(handle, title, 100);//取标题
                        if (title.ToString().Contains(s))
                        {
                            wnds.Add(handle);
                        }
                        return true;
                    }, proc.StringParameter);
                }

                StringBuilder sb = new StringBuilder();
                if (wnds.Count == 0)
                {
                    sb = new StringBuilder();
                    sb.AppendFormat("无法找到指定控件 \"{0}\" 的句柄", proc.StringParameter);
#if DEBUG
                    Console.WriteLine(sb.ToString());
#endif
                    OnResultSend(new ResultItem(ResultItem.States.WARNING, sb.ToString()));
                    return;
                }
                if (wnds.Count > proc.IntegerParameter && proc.IntegerParameter >= 0)
                {
                    wndItem.CurrentWnd = wnds[proc.IntegerParameter];
                }
                else
                {
                    wndItem.CurrentWnd = wnds[wnds.Count - 1];
                }
                sb.AppendFormat("成功找到指定控件 \"{0}\" 的句柄，共 {1} 个, 当前选择第 {2} 个 0x{3:x8}",
                    proc.StringParameter, wnds.Count, proc.IntegerParameter + 1, wndItem.CurrentWnd.ToInt32());
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString()));
                OnWndChange(wndItem);
            }
        }

        /// <summary>
        /// 获取指定控件的范围
        /// </summary>
        /// <param name="proc">操作对象信息</param>
        private void DoGetControlRectangle(ProcessItem proc)
        {
            if (wndItem.CurrentWnd == IntPtr.Zero || wndItem.CurrentWnd == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("当前句柄无效, 无法执行该操作");
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                throw new ProcessException(sb.ToString());
            }
            else
            {
                StructComponent.RECT rect = new StructComponent.RECT();
                Win32.GetClientRect(wndItem.CurrentWnd, out rect);
#if DEBUG
                Console.WriteLine("获取到指定句柄变换前的矩形: " + rect.ToString());
#endif
                StructComponent.POINT point = new StructComponent.POINT
                {
                    X = rect.Left,
                    Y = rect.Bottom
                };
                Win32.ClientToScreen(wndItem.CurrentWnd, ref point);
                rect.Left = point.X;
                rect.Bottom = point.Y;
                point.X = rect.Right;
                point.Y = rect.Top;
                Win32.ClientToScreen(wndItem.CurrentWnd, ref point);
                rect.Right = point.X;
                rect.Top = point.Y;
                rect.IsInit = true;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("获取到指定句柄的矩形: " + rect.ToString());
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString())
                {
                    RectangleSet = true,
                    Rectangle = rect,
                });
                this.rect = rect;
            }

        }

        private void DoCalculateRectangleExportPointMatrix(ProcessItem proc)
        {
            if (rect.IsInit == true && proc.Rectangle.IsInit == false)
            {
                proc.Rectangle = rect;
            }
            uint columnDeviation = 20;  // 列坐标误差
            uint rowDeviation = 10;     // 行坐标误差
            // 列长度
            uint columnLength = proc.Rectangle.Right - (proc.Rectangle.Left + columnDeviation);
            // 行高度
            uint rowLength = proc.Rectangle.Bottom - (proc.Rectangle.Top + rowDeviation);
            // 列间隔
            uint columnCellInterval = columnLength / Settings.COLUMNS_COUNT;
            // 行间隔
            uint rowCellInterval = rowLength / Settings.ROWS_COUNT;
            // 列起始位置
            uint columnStartPosition = proc.Rectangle.Left + columnDeviation;
            // 行起始位置
            uint rowStartPosition = proc.Rectangle.Top + rowDeviation;
            // 初始化目标点结构体矩阵数组
            StructComponent.POINT[,] matrix = new StructComponent.POINT[Settings.ROWS_COUNT, Settings.COLUMNS_COUNT];
            // 计算目标点矩阵的坐标
            for (int i = 0; i < Settings.ROWS_COUNT; i++, rowStartPosition += rowCellInterval)  // 完成一行后加上行间隔
            {
                columnStartPosition = proc.Rectangle.Left + columnDeviation;                         // 完成一行后初始化列的横坐标
                for (int j = 0; j < Settings.COLUMNS_COUNT; j++, columnStartPosition += columnCellInterval)// 完成一列后加上列间隔
                {
                    matrix[i, j].X = columnStartPosition + columnCellInterval / 2;          // 列初始位置坐标加上间隔的一半为目标点的横坐标
                    matrix[i, j].Y = rowStartPosition + rowCellInterval / 2;                // 行初始位置坐标加上间隔的一半为目标点的纵坐标
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("计算矩阵点选位置完成");
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString())
            {
                MatrixSet = true,
                Matrix = matrix
            });
        }

        /// <summary>
        /// 设置控件 ComboBox 的值
        /// </summary>
        /// <param name="proc">操作信息对象</param>
        private void DoSetComboBoxCursel(ProcessItem proc)
        {
            if (wndItem.CurrentWnd == IntPtr.Zero || wndItem.CurrentWnd == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("当前句柄无效, 无法执行该操作");
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                throw new ProcessException(sb.ToString());
            }
            else
            {
                Win32.SendMessage(wndItem.CurrentWnd, (int)Win32.MessageFlags.CB_SETCURSEL, proc.IntegerParameter, 0);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("发送给句柄 0x{0:x8} ComboBox设置事件完成", wndItem.CurrentWnd.ToInt32());
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString()));
            }
        }

        /// <summary>
        /// 设置控件 TextBox 的值
        /// </summary>
        /// <param name="proc">操作信息对象</param>
        private void DoSetTextBoxValue(ProcessItem proc)
        {
            if (wndItem.CurrentWnd == IntPtr.Zero || wndItem.CurrentWnd == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("当前句柄无效, 无法执行该操作");
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                throw new ProcessException(sb.ToString());
            }
            else
            {
                Win32.SendMessage(wndItem.CurrentWnd, (int)Win32.MessageFlags.WM_SETTEXT, null, proc.StringParameter);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("发送给句柄 0x{0:x8} TextBox设置事件完成", wndItem.CurrentWnd.ToInt32());
#if DEBUG
                Console.WriteLine(sb.ToString());
#endif
                OnResultSend(new ResultItem(ResultItem.States.OK, sb.ToString()));
            }
        }

    }
}
