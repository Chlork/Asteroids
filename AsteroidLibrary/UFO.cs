using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    public class UFO:Figure
    {
        private Figure _target;
        public UFO(List<Point> vertexes, PointF pointOfCentre, PointF pointOfCreate, Figure target) : base(vertexes, pointOfCentre, pointOfCreate)
        {
            _target = target;
            _angle = 270;
            _speed = 10;
            _angleOfSpeed = 270;
            PointsForDestruction = 10;
        }

        public void Update()
        {
            CalculatingAngle();
            Move();
        }

        public void CalculatingAngle()
        {
            double distenceToTarget = Math.Sqrt(Math.Pow(_pointOfCentre.X - _target.PointOfCentre.X, 2) +
                                               Math.Pow(_pointOfCentre.Y - _target.PointOfCentre.Y, 2));
            _angleOfSpeed = (float)(Math.Asin((_pointOfCentre.Y - _target.PointOfCentre.Y) / distenceToTarget)* 180 /
                            Math.PI);
            if (_pointOfCentre.X > _target.PointOfCentre.X)
            {
                _angleOfSpeed += 180;
            }
            else
            {
                _angleOfSpeed = 360 - _angleOfSpeed;

            }
            CheckAngle(_angleOfSpeed);
        }
    }
}
