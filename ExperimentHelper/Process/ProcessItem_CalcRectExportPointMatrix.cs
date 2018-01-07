using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_CalcRectExportPointMatrix : IProcessItem
    {
        private ExportPointMatrix matrix;
        private TargetRectangle rectangle;
        private int rowDeviation;
        private int columnDeviation;

        public ProcessItem_CalcRectExportPointMatrix(ExportPointMatrix matrix, TargetRectangle rectangle, int rowDeviation, int columnDeviation)
        {
            this.matrix = matrix;
            this.rectangle = rectangle;
            this.rowDeviation = rowDeviation;
            this.columnDeviation = columnDeviation;
        }

        public ResultItem Execute()
        {
            int columnCount = matrix.GetMatrixColumnCount();
            int rowCount = matrix.GetMatrixRowCount();
            // 列长度
            uint columnLength = rectangle.GetRight() - (rectangle.GetLeft() + (uint) columnDeviation);
            // 行高度
            uint rowLength = rectangle.GetBottom() - (rectangle.GetTop() + (uint) rowDeviation);
            // 列间隔
            uint columnCellInterval = columnLength / (uint) matrix.GetMatrixColumnCount();
            // 行间隔
            uint rowCellInterval = rowLength / (uint) matrix.GetMatrixRowCount();
            // 列起始位置
            uint columnStartPosition = rectangle.GetLeft() + (uint) columnDeviation;
            // 行起始位置
            uint rowStartPosition = rectangle.GetTop() + (uint) rowDeviation;
            // 计算目标点矩阵的坐标
            for (int i = 0; i < rowCount; i++, rowStartPosition += rowCellInterval)  // 完成一行后加上行间隔
            {
                columnStartPosition = rectangle.GetLeft() + (uint) columnDeviation;             // 完成一行后初始化列的横坐标
                for (int j = 0; j < columnCount; j++, columnStartPosition += columnCellInterval)// 完成一列后加上列间隔
                {
                    ExportPointMatrixItem item = matrix.GetExportPointMatrixItem(i, j);
                    item.PointX = (int) (columnStartPosition + columnCellInterval / 2);          // 列初始位置坐标加上间隔的一半为目标点的横坐标
                    item.PointY = (int) (rowStartPosition + rowCellInterval / 2);                // 行初始位置坐标加上间隔的一半为目标点的纵坐标
                    // matrix.SetExportPointMatrixItem(i, j, item);
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("计算矩阵点选位置完成");
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}
