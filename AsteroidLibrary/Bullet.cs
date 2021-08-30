using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    public class Bullet:Figure
    {
        public int _life;
        public Bullet(List<Point> vertexes, PointF pointOfCentre, PointF pointOfCreate, float angle) :base(vertexes, pointOfCentre, pointOfCreate)
        {
            _angleOfSpeed = angle;
            _speed = 15;
            _life = 0;
        }

        public void Update()
        {
            _life++;
            Move();
        }

        public int Life { get { return _life;} }
    }
}
