using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsGame
{
    public class LaserData:FigureData
    {
        public int Length { get; }
        public int Energy { set; get;}
        public LaserData(Bitmap figureImg, Size size) : base(figureImg, size)
        {
            Length = GetLength();
            Energy = 100;
        }

        private int GetLength()
        {
            int minY = Vertexes[0].Y;
            int maxY = minY;
            foreach (var vertex in Vertexes)
            {
                if (vertex.Y < minY)
                    minY = vertex.Y;
                if (vertex.Y > maxY)
                    maxY = vertex.Y;
            }

            return maxY - minY;
        }

    }
}
