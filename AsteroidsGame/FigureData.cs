using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroids;

namespace AsteroidsGame
{
    public class FigureData
    {
        public Bitmap Texture { get; }
        public List<Point> Vertexes { get; }
        public Size Size { get; }
        public PointF PointOfCentre { get; }
        public Pen Pen { get; }

        public FigureData(Bitmap figureImg, Size size)
        {
            Size = size;
            Texture = new Bitmap(figureImg, Size);
            Vertexes = Scanner.GetVertexesFromImage(Texture);
            PointOfCentre=new PointF(Size.Width/2, Size.Height/2);
            Pen = new Pen(Color.White);
        }

       
    }
}
