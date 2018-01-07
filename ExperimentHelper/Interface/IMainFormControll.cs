using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Interface
{
    public interface IMainFormControll
    {
        void Start();
        void Exit();
        bool FormClosing();
        void FindHandle();
        void FindTarget();
        void ChooseExportPoint();
        void CheckExportPoint();
        void ExecuteOperation();
        void About();
        void ThreadStart();
        void ThreadFinish();
    }
}
