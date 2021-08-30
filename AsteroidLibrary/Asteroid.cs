using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    public class Asteroid:Figure
    {
        private int _angleRotation;
        private bool _isFragment;
        public Asteroid(List<Point> vertexes, PointF pointOfCentre, PointF pointOfCreate, float angleOfSpeed, int angleRotation, bool isFragment) : base(vertexes, pointOfCentre, pointOfCreate)
        {
            _angleOfSpeed = angleOfSpeed;           
            _angle = 270;
            _rotationDirection = 1;
            _angleRotation = angleRotation;
            _isFragment = isFragment;
            if (isFragment)
            {
                _speed = 10;
                PointsForDestruction = 100;
            }
            else
            {
                _speed = 15;
                PointsForDestruction = 50;
            }
        }

        public void Update()
        {
            Rotate(_angleRotation);
            Move();
        }
        public float AngleOfSpeed { get { return _angleOfSpeed;} }
        public bool IsFragment { get { return _isFragment;} }
    }
}
