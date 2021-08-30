using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Asteroids
{
    public static class Scanner
    {
        public static GraphicsPath GetPathFromImage(Bitmap bitmap)
        {
            GraphicsPath polygon = new GraphicsPath();
            List<Point> vertexes = GetVertexesFromImage(bitmap);
            polygon.StartFigure();
            polygon.AddLines(vertexes.ToArray());
            polygon.CloseFigure();
            return polygon;
        }

        public static List<Point> GetVertexesFromImage(Bitmap bitmap)
        {
            List<Point> vertexes = GetContour(FindFirstPoint(bitmap), bitmap);
            vertexes = RemoveExcess(vertexes);
            return vertexes;
        }

        private static Point FindFirstPoint(Bitmap bitmap)
        {
            int height = bitmap.Height;
            int width = bitmap.Width;
            for (int i = 0; i < width - 1; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (bitmap.GetPixel(i, j).A != 0)
                    {
                        return new Point(i, j);
                    }
                }
            }

            return Point.Empty;
        }

        private static List<Point> GetContour(Point firstPoint, Bitmap bitmap)
        {
            List<Point> vertexes = new List<Point>();
            vertexes.Add(firstPoint);
            Point currentPoint = firstPoint;
            Point pointForAdd = new Point();
            int defoultDirection = 0;
            int direction = defoultDirection;

            bool savePoint = false;

            do
            {
                currentPoint = vertexes.Last();
                switch (direction)
                {
                    case 0:
                        currentPoint.X--;
                        break;
                    case 1:
                        currentPoint.X--;
                        currentPoint.Y++;
                        break;
                    case 2:
                        currentPoint.Y++;
                        break;
                    case 3:
                        currentPoint.Y++;
                        currentPoint.X++;
                        break;
                    case 4:
                        currentPoint.X++;
                        break;
                    case 5:
                        currentPoint.X++;
                        currentPoint.Y--;
                        break;
                    case 6:
                        currentPoint.Y--;
                        break;
                    case 7:
                        currentPoint.Y--;
                        currentPoint.X--;
                        break;
                }

                if (bitmap.GetPixel(currentPoint.X, currentPoint.Y).A == 0)
                {
                    savePoint = true;
                }

                if (savePoint && bitmap.GetPixel(currentPoint.X, currentPoint.Y).A > 0)
                {
                    pointForAdd = currentPoint;
                    vertexes.Add(pointForAdd);
                    savePoint = false;
                    direction = direction - 4;
                    if (direction < 0) direction += 8;
                    if (direction > 7) direction -= 8;
                }
                else
                {
                    direction = direction == 7 ? 0 : ++direction;
                }

            } while (pointForAdd != vertexes[0]);

            return vertexes;
        }

        private static List<Point> RemoveExcess(List<Point> vertexes)
        {
            for (int i = 2; i < vertexes.Count; i++)
            {

                double temp = (vertexes[i - 2].X - vertexes[i].X) * (vertexes[i - 1].Y - vertexes[i].Y) -
                              (vertexes[i - 1].X - vertexes[i].X) * (vertexes[i - 2].Y - vertexes[i].Y);
                if (Math.Abs(temp) == 0)
                {
                    vertexes.Remove(vertexes[i]);
                    i--;
                }
            }
            return vertexes;
        }
    }
}
