using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Basic
{
    public class ExportPointMatrixItem
    {
        private int pointX;
        private int pointY;
        private string pointDescription;
        private bool isAvaliable;

        public ExportPointMatrixItem(int pointX, int pointY, string pointDescription, bool isAvaliable)
        {
            this.pointX = pointX;
            this.pointY = pointY;
            this.pointDescription = pointDescription;
            this.isAvaliable = isAvaliable;
        }

        public int PointX { get => pointX; set => pointX = value; }
        public int PointY { get => pointY; set => pointY = value; }
        public string PointDescription { get => pointDescription; set => pointDescription = value; }
        public bool IsAvaliable { get => isAvaliable; set => isAvaliable = value; }
    }
}
