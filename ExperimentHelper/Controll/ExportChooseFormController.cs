using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Controll
{
    public class ExportChooseFormController : IExportChooseFormControll
    {
        private ExportChooseForm formView;
        private IExportChooseFormModel model;

        public ExportChooseFormController(IExportChooseFormModel model, ExportChooseForm formView)
        {
            this.model = model;
            this.formView = formView;
            model.Initialize();
            formView.DisableConfirmButton();
            formView.Show();
        }

        public void Cancel()
        {
            formView.Dispose();
        }

        public void ChangeExportPointStatus(int row, int column)
        {
            model.SetExportPoint(row, column);
            formView.EnableConfirmButton();
        }

        public void Comfirm()
        {
            model.Save();
            formView.Dispose();
        }
    }
}
