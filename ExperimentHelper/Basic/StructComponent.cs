using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Basic
{
    public class StructComponent
    {
        public struct RECT
        {
            uint left;
            uint top;
            uint right;
            uint bottom;

            public uint Left { get => left; set => left = value; }
            public uint Top { get => top; set => top = value; }
            public uint Right { get => right; set => right = value; }
            public uint Bottom { get => bottom; set => bottom = value; }

            public override string ToString()
            {
                return string.Format("(L:{0},T:{1},R:{2},B:{3})",
                    left, top, right, bottom);
            }
        }

        public struct POINT
        {
            uint x;
            uint y;

            public uint X { get => x; set => x = value; }
            public uint Y { get => y; set => y = value; }

            public override string ToString()
            {
                return string.Format("(X:{0},Y:{1})", x, y);
            }
        }
    }
}
