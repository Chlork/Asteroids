using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroids;

namespace Asteroids
{
    public class Laser:Figure
    {
        private Ship _ship;
        public Laser(List<Point> vertexes, PointF pointOfCentre, PointF pointOfCreate, Ship ship) : base(vertexes,
            pointOfCentre, pointOfCreate)
        {
            _ship = ship;
            _rotationDirection = 1;
            Update();
        }

        public void Update()
        {
            Move();
            Rotate(CalculatingOffsetAngle());
            
        }

        private void Move()
        {
            float offsetX = _ship.GetPointOfGun().X - _pointOfCentre.X;
            float offsetY = _ship.GetPointOfGun().Y - _pointOfCentre.Y;
            MoveOnOffset(offsetX, offsetY);
        }

        private float CalculatingOffsetAngle()
        {
            float offsetAngle = Math.Abs(_ship.Angle - _angle);
            if (Math.Abs(_ship.Angle - _angle) > 180)
            {
                offsetAngle = 360 - offsetAngle;
            }

            offsetAngle = CheckAngle(offsetAngle);
            if (Math.Abs(_ship.Angle - _angle) >= 180)
                offsetAngle = _angle > _ship.Angle ? offsetAngle : offsetAngle * -1;
            if (Math.Abs(_ship.Angle - _angle) < 180)
                offsetAngle = _angle > _ship.Angle ? offsetAngle * -1 : offsetAngle;
            return offsetAngle;
        }

    }
}
