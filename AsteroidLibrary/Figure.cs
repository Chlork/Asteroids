using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    public class Figure
    {
        protected Pen _pen;
        protected PointF _pointOfCentre;
        protected PointF[] _vertexes;
        protected GraphicsPath _polygon;
        protected float _speed;
        protected float _angleOfSpeed;
        protected float _angle;
        protected int _size;
        protected Matrix _matrix1;
        protected bool _rotation;
        protected int _rotationDirection;
        public int PointsForDestruction { set; get; }

        public Figure(List<Point> vertexes, PointF pointOfCentre, PointF pointOfCreate)
        {
            _matrix1 = new Matrix();
            _polygon=new GraphicsPath();
            _polygon.StartFigure();
            _polygon.AddLines(vertexes.ToArray());
            _polygon.CloseFigure();
            _pointOfCentre = pointOfCentre;
            _angle = 270;
            MoveOnOffset(pointOfCreate.X - _pointOfCentre.X, pointOfCreate.Y - _pointOfCentre.Y);
        }

        public void Update()
        {
            Move();
        }

        public void Rotate(float angle)
        {
            _matrix1.RotateAt(angle * _rotationDirection, _pointOfCentre);
            _angle += angle * _rotationDirection;
            _angle = CheckAngle(_angle);

            _polygon.Transform(_matrix1);
            _matrix1.Reset();
        }

        protected void Move()
        {

            double offsetX = (Math.Cos(_angleOfSpeed * Math.PI / 180) * _speed);
            double offsetY = (Math.Sin(_angleOfSpeed * Math.PI / 180) * _speed);

            MoveOnOffset((float)offsetX,(float)offsetY);
        }

        



        protected float CheckAngle(float angle)
        {
            if (angle >= 360)
                angle -= 360;
            if (angle < 0)
                angle += 360;
            return angle;
        }

        public void MoveOnOffset(float offsetX, float offsetY)
        {
            _pointOfCentre.X += offsetX;
            _pointOfCentre.Y += offsetY;

            _matrix1.Translate(offsetX, offsetY);
            _polygon.Transform(_matrix1);
            _matrix1.Reset();
        }


        public int Size { get { return _size; } }

        public GraphicsPath Polygon {  get { return _polygon;} }

        public PointF PointOfCentre
        {
            get { return _pointOfCentre; }
        }

        public float Angle { get { return _angle;} }
    }
}
