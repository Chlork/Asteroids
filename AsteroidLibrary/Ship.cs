using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Asteroids
{
    
    public class Ship:Figure
    {
        private float _acceleration;
        private int DistanceToGun {get; }
        private bool _gas;
        private float _coeffOfresistence;
        private int _angleRotation;
        public int LaserEnergy { set; get;}
        public bool LaserEnabled { set; get;}
        private bool IncreasePause;
        private const int LaserEnergyIncrease=1;
        private const int LaserEnergyDecrease = 5;
        private const int MaxLaserEnergy = 100;
        private TimerCallback _laserPauseOff;
        private Timer _timer;
        private Laser _laser;

        public Ship(List<Point> vertexes, PointF pointOfCentre, PointF pointOfCreate, int distanceToGun, Laser laser) : base(vertexes, pointOfCentre, pointOfCreate)
        {
            _acceleration = 1f;
            _pen = new Pen(Color.White);
            _speed = 0;
            _coeffOfresistence = 0.003f;
            _angleOfSpeed = 90;
            _size = 20;
            _angle = 270;
            _angleRotation = 10;
            _rotation = false;
            _rotationDirection = -1;
            DistanceToGun = distanceToGun;
            LaserEnergy = MaxLaserEnergy;
            LaserEnabled = false;
            IncreasePause = false;
            _timer = new Timer(2000);
            _timer.Elapsed += PauseOff;
            _timer.AutoReset = false;
            _laser = laser;
        }    

        public void Update()
        {
            if (_rotation) Rotate(_angleRotation);
            if (_gas || _speed > 0)
            {
                CalculationAngleAndSpeed();
                Move();
            }

            if(!IncreasePause)
            {
                if (!LaserEnabled && LaserEnergy < MaxLaserEnergy)
                    LaserEnergy += LaserEnergyIncrease;

                if (LaserEnabled && LaserEnergy > 0)
                {
                    LaserEnergy -= LaserEnergyDecrease;
                    if (LaserEnergy <= 0)
                    {
                        LaserEnergy = 0;
                        LaserOff();
                    }
                }
            }


        }

        public void LaserOn()
        {
            if (LaserEnergy>0)
            {
                LaserEnabled = true;
                IncreasePause = false;
                _timer.Stop();
            }
        }

        public void LaserOff()
        {
            LaserEnabled = false;
            IncreasePause = true;
            _laser = null;
            _timer.Start();
        }

        private void PauseOff(Object source, ElapsedEventArgs e)
        {
            IncreasePause = false;
        }


        public PointF GetPointOfGun()
        {
            PointF pointOfGun=new PointF();
            pointOfGun.X = _pointOfCentre.X + (float)(Math.Cos(_angle * Math.PI / 180) * DistanceToGun);
            pointOfGun.Y = _pointOfCentre.Y + (float) (Math.Sin(_angle * Math.PI / 180) * DistanceToGun);
            return pointOfGun;
        }

        

        private void CalculationAngleAndSpeed()
        {
            if (_speed == 0)
                _angleOfSpeed = _angle;

            if (_gas)
            {
                float angleDiff = Math.Abs(_angle - _angleOfSpeed);
                if (Math.Abs(_angle - _angleOfSpeed) > 180)
                {
                    angleDiff = 360 - angleDiff;
                }

                angleDiff = CheckAngle(angleDiff);


                _speed = (float) (Math.Sqrt(Math.Pow(_speed, 2) + Math.Pow(_acceleration, 2) -
                                            (2 * _speed * _acceleration *
                                             Math.Cos((180 - angleDiff) * Math.PI / 180))));

                float offsetAngle =
                    (float) (Math.Asin(_acceleration * Math.Sin((180 - angleDiff) * Math.PI / 180) / _speed) * 180 /
                             Math.PI);

                if (Math.Abs(_angle - _angleOfSpeed) >= 180)
                    _angleOfSpeed += _angleOfSpeed > _angle ? Math.Abs(offsetAngle) : Math.Abs(offsetAngle) * -1;
                if (Math.Abs(_angle - _angleOfSpeed) < 180)
                    _angleOfSpeed += _angleOfSpeed > _angle ? Math.Abs(offsetAngle) * -1 : Math.Abs(offsetAngle);
                _angleOfSpeed = CheckAngle(_angleOfSpeed);
            }
            if (Math.Abs(_speed) > 0)
            {
                float resistance = (float)(_coeffOfresistence * Math.Pow(_speed, 2));
                _speed -= resistance;
            }
        }

        public bool Rotation { set { _rotation = value; } }
        public int RotationDirection { set { _rotationDirection = value; } }
        public bool SpeadUp { set { _gas = value; } }

        public float Angle { get { return _angle; } }        
        

        public float Acceleration
        {
            set { _acceleration = value; }
            get { return _acceleration; }
        }

        
    }
}
