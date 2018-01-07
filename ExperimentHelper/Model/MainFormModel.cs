using ExperimentHelper.Basic;
using ExperimentHelper.Controll;
using ExperimentHelper.Interface;
using ExperimentHelper.Process;
using ExperimentHelper.Util;
using System;
using System.Collections.Generic;
using System.Text;
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

        public void NofityAllProcessResultObservers(ResultItem result)
        {
            for (int i = 0; i < resultItemObservers.Count; i++)
            {
                IResultObserver observer = resultItemObservers[i];
                observer.UpdateProcessResult(result);
            }
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
                NofityAllProcessResultObservers(rs);
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
            ResultItem rs = new ResultItem(ResultItem.States.ThreadStart, "开始");
            NofityAllProcessResultObservers(rs);
            isThreadRunning = true;
            for (int i = 0, wrongCount = 0; i < processQueue.Count && wrongCount < 3; i++)
            {
                try
                {
                    IProcessItem item = processQueue[i];
                    ResultItem result = item.Execute();
                    NotifyAllResultItemObservers(result);
                    wrongCount = 0;
                    Thread.Sleep(settings.ShortStepDelay);
                }
                catch (ProcessException e)
                {
                    i--;
                    wrongCount++;

                    rs = new ResultItem(ResultItem.States.WARNING, e.Message);
#if DEBUG
                    Console.WriteLine(rs);
#endif
                    NotifyAllResultItemObservers(rs);
                    Thread.Sleep(settings.MediumStepDelay);
                }
            }
            rs = new ResultItem(ResultItem.States.ThreadFinish, "结束");
            NofityAllProcessResultObservers(rs);
            isThreadRunning = false;
        }
    }
}
