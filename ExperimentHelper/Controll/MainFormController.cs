using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Util;

namespace ExperimentHelper.Controll
{
    public class MainFormController : IMainFormControll
    {
        private IMainFormModel mainFormModel;
        MainForm view;

        public MainFormController(IMainFormModel mainFormModel, MainForm view)
        {
            this.mainFormModel = mainFormModel;
            this.view = view;
            mainFormModel.LoadSettings();
            view.SyncSettingsToUI();
        }

        public void About()
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        public void CheckExportPoint()
        {
            view.SyncUIToSettings();
            mainFormModel.Initialize();
            mainFormModel.ForceRun(ProcessHelper.ProcessTypeFlags.MAINBOARD_CHECK_POSITION);
        }

        public void ChooseExportPoint()
        {
            ExportChooseForm form = new ExportChooseForm();
            form.Show();
        }

        public void ExecuteOperation()
        {
            view.SyncUIToSettings();
            view.DisableAllControls();
            mainFormModel.Initialize();
            mainFormModel.Run();
        }

        public void Exit()
        {
            view.SyncUIToSettings();
            mainFormModel.SaveSettings();
        }

        public void FindHandle()
        {
            view.SyncUIToSettings();
            view.DisableAllControls();
            mainFormModel.Initialize();
            mainFormModel.ForceRun(ProcessHelper.ProcessTypeFlags.MAINBOARD_FIND_HANDLE);
        }

        public void FindTarget()
        {
            view.SyncUIToSettings();
            view.DisableAllControls();
            mainFormModel.Initialize();
            mainFormModel.ForceRun(ProcessHelper.ProcessTypeFlags.MAINBOARD_FIND_RECTANGLE);
        }

        public void Start()
        {
            view.SyncUIToSettings();
            view.DisableAllControls();
            mainFormModel.Initialize();
            mainFormModel.ForceRun(ProcessHelper.ProcessTypeFlags.DEFAULT);
        }

        public bool FormClosing()
        {
            return view.ShowQuestionMessageBox("是否关闭程序");
        }

        public void RunningMessageReceived(ResultItem result)
        {
            switch(result.LogState)
            {
                case ResultItem.States.WARNING:
                    break;
                case ResultItem.States.ERROR:
                    view.ShowErrorMessageBox(result.LogMessage);
                    break;
                case ResultItem.States.ThreadStart:
                    view.DisableAllControls();
                    view.SetControlText(MainForm.ControlsName.BTN_BEGIN, "停止");
                    view.SetControlEnabled(MainForm.ControlsName.BTN_BEGIN, true);
                    break;
                case ResultItem.States.ThreadFinish:
                    view.SetControlText(MainForm.ControlsName.BTN_BEGIN, "开始");
                    view.EnableAllControls();
                    // view.ShowInformationMessageBox(result.LogMessage);
                    break;
                case ResultItem.States.OK:
                    break;
            }
        }
    }
}
