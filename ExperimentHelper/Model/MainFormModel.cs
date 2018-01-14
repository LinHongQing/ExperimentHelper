using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Process;
using ExperimentHelper.Util;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ExperimentHelper.Model
{
    public class MainFormModel : IMainFormModel, IResultObserable
    {
        private List<IResultObserver> resultItemObservers = new List<IResultObserver>();
        private SettingComponent settings;
        private ExportPointMatrix matrix;
        private TargetRectangle rectangle;
        private WindowHandle handle;
        private Thread t;
        private bool isThreadRunning;
        

        public MainFormModel(SettingComponent settings, ExportPointMatrix matrix, TargetRectangle rectangle, WindowHandle handle)
        {
            this.settings = settings;
            this.matrix = matrix;
            this.rectangle = rectangle;
            this.handle = handle;
            isThreadRunning = false;
        }

        public void ForceRun(ProcessHelper.ProcessTypeFlags processType)
        {
            settings.ProcessType = processType;
            Run();
        }

        public void Initialize()
        {
            if (!isThreadRunning)
            {
                t = new Thread(ProcessThread);
            }
        }

        public void LoadSettings()
        {
            ISettingModel settingModel = new GeneralSettingModel(matrix, settings);
            settingModel.InitializeSettings();
        }

        public void NotifyAllResultItemObservers(ResultItem result)
        {
            for (int i = 0; i < resultItemObservers.Count; i++)
            {
                IResultObserver observer = resultItemObservers[i];
                observer.UpdateResultItem(result);
            }
        }

        public void RegisterResultItemObserver(IResultObserver observer)
        {
            resultItemObservers.Add(observer);
        }

        public void RemoveResultItemObserver(IResultObserver observer)
        {
            resultItemObservers.Remove(observer);
        }

        public void Run()
        {
            if (isThreadRunning)
            {
                t.Abort();
                ResultItem rs = new ResultItem(ResultItem.States.ThreadFinish, "中止");
                NotifyAllResultItemObservers(rs);
                isThreadRunning = false;
            }
            else
            {
                t.Start();
            }
        }

        public void SaveSettings()
        {
            ISettingModel settingModel = new GeneralSettingModel(matrix, settings);
            settingModel.SaveSettings();
        }

        private void ProcessThread()
        {
            List<IProcessItem> processQueue = ProcessHelper.GenerateProcessQueue(matrix, rectangle, handle, settings);
            ResultItem rs = new ResultItem(ResultItem.States.ThreadStart, "开始执行操作");
            NotifyAllResultItemObservers(rs);
            isThreadRunning = true;
            for (int i = 0, wrongCount = 0; i < processQueue.Count && wrongCount < settings.MaximumNumberOfRetries; i++)
            {
                try
                {
                    IProcessItem item = processQueue[i];
                    ResultItem result = item.Execute();
                    wrongCount = 0;
                    Thread.Sleep(settings.StepDelay);
                    NotifyAllResultItemObservers(result);
                }
                catch (ProcessException e)
                {
                    i--;
                    wrongCount++;
                    if (wrongCount == settings.MaximumNumberOfRetries)
                        rs = new ResultItem(ResultItem.States.ERROR, "该操作无法完成");
                    else
                        rs = new ResultItem(ResultItem.States.WARNING, e.Message + " 正在重试……");
#if DEBUG
                    Console.WriteLine(rs);
#endif
                    Thread.Sleep(settings.RetryStepDelay);
                    NotifyAllResultItemObservers(rs);
                }
            }
            rs = new ResultItem(ResultItem.States.ThreadFinish, "操作执行完成");
            NotifyAllResultItemObservers(rs);
            isThreadRunning = false;
        }
    }
}
