using ExperimentHelper.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Basic
{
    public class ExportPointMatrix
    {
        private bool isInit = false;
        private int matrixRowCount;
        private int matrixColumnCount;
        private List<List<ExportPointMatrixItem>> matrix;
        private static ExportPointMatrix _uniqueInstance;
        private ExportPointMatrixItemIterator exportPointMatrixItemIterator;

        public static ExportPointMatrix GetInstance()
        {
            if (_uniqueInstance == null)
                _uniqueInstance = new ExportPointMatrix();
            return _uniqueInstance;
        }

        public void Init(int matrixRowCount, int matrixColumnCount)
        {
            this.matrixRowCount = matrixRowCount;
            this.matrixColumnCount = matrixColumnCount;
            matrix = new List<List<ExportPointMatrixItem>>(matrixRowCount);
            for (int i = 0; i < matrixRowCount; i++)
            {
                List<ExportPointMatrixItem> columnCell = new List<ExportPointMatrixItem>(matrixColumnCount);
                for (int j = 0; j < matrixColumnCount; j++)
                {
                    ExportPointMatrixItem item = new ExportPointMatrixItem(0, 0, "", false);
                    columnCell.Add(item);
                }
                matrix.Add(columnCell);
            }
            isInit = true;
        }

        public class ExportPointMatrixUninitializedException : ExportPointMatrixException
        {
            public ExportPointMatrixUninitializedException() : base() { }
        }

        public class IndexOutOfBoundsException : ExportPointMatrixException
        {
            public IndexOutOfBoundsException() : base() { }
        }

        public ExportPointMatrixItem GetExportPointMatrixItem(int rowIndex, int columnIndex)
        {
            if (isInit == false)
                throw new ExportPointMatrixUninitializedException();
            if (rowIndex < 0 || rowIndex >= matrixRowCount || columnIndex < 0 || columnIndex >= matrixColumnCount)
                throw new IndexOutOfBoundsException();
            return matrix[rowIndex][columnIndex];
        }

        public void SetExportPointMatrixItem(int rowIndex, int columnIndex, ExportPointMatrixItem item)
        {
            if (isInit == false)
                throw new ExportPointMatrixUninitializedException();
            if (rowIndex < 0 || rowIndex >= matrixRowCount || columnIndex < 0 || columnIndex >= matrixColumnCount)
                throw new IndexOutOfBoundsException();
            matrix[rowIndex][columnIndex] = item;
        }

        public int GetMatrixRowCount()
        {
            return matrixRowCount;
        }

        public int GetMatrixColumnCount()
        {
            return matrixColumnCount;
        }

        private class ExportPointMatrixItemIterator : IIterator
        {
            private ExportPointMatrix instance;
            private int currentMatrixRowIndex;
            private int currentMatrixColumnIndex;
            private int matrixRowCount;
            private int matrixColumnCount;
            public ExportPointMatrixItemIterator(ExportPointMatrix instance)
            {
                this.instance = instance;
                matrixColumnCount = instance.GetMatrixColumnCount();
                matrixRowCount = instance.GetMatrixRowCount();
                currentMatrixColumnIndex = currentMatrixRowIndex = 0;
            }
            public ExportPointMatrixItem CurrentItem()
            {
                return instance.GetExportPointMatrixItem(currentMatrixRowIndex, currentMatrixColumnIndex);
            }

            public void First()
            {
                currentMatrixColumnIndex = currentMatrixRowIndex = 0;
            }

            public bool IsDone()
            {
                return currentMatrixRowIndex + 1 > matrixRowCount;
            }

            public void Next()
            {
                if (currentMatrixColumnIndex == matrixColumnCount - 1)
                {
                    currentMatrixRowIndex++;
                    currentMatrixColumnIndex = 0;
                }
                else
                {
                    currentMatrixColumnIndex++;
                }
            }
        }

        public IIterator Iterator()
        {
            exportPointMatrixItemIterator = new ExportPointMatrixItemIterator(_uniqueInstance);
            return exportPointMatrixItemIterator;
        }
    }
}
