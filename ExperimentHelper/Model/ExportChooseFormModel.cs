using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Model
{
    public class ExportChooseFormModel : IExportChooseFormModel
    {
        private ExportPointMatrix matrix;
        List<IExportPointMatrixObserver> observers = new List<IExportPointMatrixObserver>();
        Dictionary<string, bool> temp = new Dictionary<string, bool>();

        public ExportChooseFormModel(ExportPointMatrix matrix)
        {
            this.matrix = matrix;
        }

        public int GetColumnCount()
        {
            return matrix.GetMatrixColumnCount();
        }

        public bool GetExportPointAvaliable(int row, int column)
        {
            ExportPointMatrixItem item = matrix.GetExportPointMatrixItem(row, column);
            return item.IsAvaliable;
        }

        public string GetExportPointText(int row, int column)
        {
            ExportPointMatrixItem item = matrix.GetExportPointMatrixItem(row, column);
            return item.PointDescription;
        }

        public int GetRowCount()
        {
            return matrix.GetMatrixRowCount();
        }

        public void Initialize()
        {

        }

        public void NotifyExportPointMatrixObservers(int row, int column)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                IExportPointMatrixObserver observer = observers[i];
                observer.ExportPointUpdate(row, column);
            }
        }

        public void RegisterExportPointMatrixObserver(IExportPointMatrixObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveExportPointMatrixObserver(IExportPointMatrixObserver observer)
        {
            observers.Remove(observer);
        }

        public void Save()
        {
            foreach(string key in temp.Keys)
            {
                string[] parameters = key.Split('_');
                int column = int.Parse(parameters[0]);
                int row = int.Parse(parameters[1]);
                ExportPointMatrixItem item = matrix.GetExportPointMatrixItem(row, column);
                temp.TryGetValue(key, out bool isAvaliable);
                item.IsAvaliable = isAvaliable;
            }
        }

        public void SetExportPoint(int row, int column)
        {
            ExportPointMatrixItem item = matrix.GetExportPointMatrixItem(row, column);
            item.IsAvaliable = !item.IsAvaliable;
            string dictKey = GetExportPointKey(row, column);
            if (temp.ContainsKey(dictKey))
            {
                temp.Remove(dictKey);
            }
            else
            {
                temp.Add(dictKey, item.IsAvaliable);
            }
            NotifyExportPointMatrixObservers(row, column);
        }

        private string GetExportPointKey(int row, int column)
        {
            return string.Format("{0}_{1}", column, row);
        }
    }
}
