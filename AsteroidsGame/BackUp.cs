//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Asteroids;

//namespace AsteroidsGame
//{
//    public class GameManager
//    {
//        private GameField _gameField;
//        private Ship _ship;
//        private Laser _laser;
//        private List<Bullet> _bullets;
//        private List<Asteroid> _asteroids;
//        private List<UFO> _uFObjects;
//        private List<Figure> _enemies;

//        private FigureData _shipData;
//        private FigureData _bulletData;
//        private FigureData _asteroidData;
//        private FigureData _fragmentOfAsteroidData;
//        private FigureData _uFOData;
//        private LaserData _laserData;
//        private LaserData _fragmentOfLaserData;

//        private LaserDrawing _laserDrawing;

//        private Collider _collider;

//        private Size _defaultSize;

//        private int _distanceToInvBorder;

//        private Random _rand;


//        private int _score;

//        private int _maxBulletLife;

//        private int _maxEnemies;

//        private bool _abilToShoot;

//        public GameManager(GameField gameField)
//        {
//            _defaultSize = new Size(200, 200);
//            _gameField = gameField;

//            _shipData = new FigureData(Textures.Ship, _defaultSize);
//            _bulletData = new FigureData(Textures.Bullet, new Size(_defaultSize.Width / 5, _defaultSize.Height / 5));
//            _asteroidData = new FigureData(Textures.Asteroid, _defaultSize);
//            _fragmentOfAsteroidData = new FigureData(Textures.Asteroid, new Size(_defaultSize.Width / 2, _defaultSize.Height / 2));
//            _uFOData = new FigureData(Textures.UFO, _defaultSize);
//            _laserData = new LaserData(Textures.Laser, Textures.Laser.Size);
//            _fragmentOfLaserData = new LaserData(Textures.FragmenOfLaser, Textures.FragmenOfLaser.Size);

//            _laserDrawing = new LaserDrawing(_fragmentOfLaserData, _laserData);

//            _bullets = new List<Bullet>();
//            _asteroids = new List<Asteroid>();
//            _uFObjects = new List<UFO>();

//            _rand = new Random();

//            _distanceToInvBorder = _defaultSize.Width > _defaultSize.Height ? _defaultSize.Width / 2 : _defaultSize.Height / 2;
//            _collider = new Collider(_distanceToInvBorder, _gameField.ClientSize);

//            _score = 0;
//            _maxBulletLife = 50;
//            _maxEnemies = 5;
//            _abilToShoot = true;
//            CreateShip();
//        }



//        public void CreateShip()
//        {
//            PointF pointOfCreate = new PointF(_gameField.ClientSize.Width / 2, _gameField.ClientSize.Height / 2);
//            _ship = new Ship(_shipData.Vertexes, _shipData.PointOfCentre, pointOfCreate, _defaultSize.Height / 4, _laser);
//        }

//        public void CreateBullet()
//        {
//            _bullets.Add(new Bullet(_bulletData.Vertexes, _bulletData.PointOfCentre, _ship.GetPointOfGun(), _ship.Angle));
//        }

//        public void CreateLaser()
//        {
//            _laser = new Laser(_laserData.Vertexes, _laserData.PointOfCentre, _ship.GetPointOfGun(), _ship);
//        }

//        private PointF PointOfShoot()
//        {
//            return new PointF(_ship.PointOfCentre.X + (float)(Math.Cos(_ship.Angle * Math.PI / 180) * (_ship.Size + 3) * 2),
//                _ship.PointOfCentre.Y + (float)(Math.Sin(_ship.Angle * Math.PI / 180) * (_ship.Size + 3) * 2));
//        }

//        public void CreateEnemy()
//        {
//            if (_asteroids.Count + _uFObjects.Count < _maxEnemies)
//            {
//                if (_rand.Next(0, 2) == 0)
//                    CreateAsteroid();
//                else
//                    CreateUFO();

//            }
//        }

//        public void CreateAsteroid()
//        {

//            PointF pointOfCreate = RandomPointOfCreate();
//            int angleOfSpeed = _rand.Next(0, 360);
//            int angleRotetion = _rand.Next(-3, 4);
//            _asteroids.Add(new Asteroid(_asteroidData.Vertexes, _asteroidData.PointOfCentre, pointOfCreate, angleOfSpeed, angleRotetion, false));
//        }

//        public void CreateFragmentsAsteroid(Asteroid asteroid)
//        {
//            float angleOfSpeed1 = asteroid.AngleOfSpeed + _rand.Next(-20, 0);
//            float angleOfSpeed2 = asteroid.AngleOfSpeed + _rand.Next(0, 21);
//            int angleRotetion1 = _rand.Next(-3, 4);
//            int angleRotetion2 = _rand.Next(-3, 4);
//            _asteroids.Add(new Asteroid(_fragmentOfAsteroidData.Vertexes, _fragmentOfAsteroidData.PointOfCentre, asteroid.PointOfCentre, angleOfSpeed1, angleRotetion1, true));
//            _asteroids.Add(new Asteroid(_fragmentOfAsteroidData.Vertexes, _fragmentOfAsteroidData.PointOfCentre, asteroid.PointOfCentre, angleOfSpeed2, angleRotetion2, true));
//        }

//        public void CreateUFO()
//        {
//            PointF pointOfCreate = RandomPointOfCreate();
//            _uFObjects.Add(new UFO(_uFOData.Vertexes, _uFOData.PointOfCentre, pointOfCreate, _ship));

//        }

//        public void DrawAll(Graphics g)
//        {
//            if (_gameField.TexturesOn)
//                DrawAllTextures(g);
//            else
//                DrawAllPolygons(g);
//        }
//        public void DrawAllTextures(Graphics g)
//        {
//            g.DrawImage(RotateImage(_shipData.Texture, _ship.Angle), PointOfDrawTexture(_ship.PointOfCentre, _shipData.Size));
//            if (_ship.LaserEnabled)
//                _laserDrawing.Draw(g, _laser.Angle, PointOfDrawTexture(_laser.PointOfCentre, _fragmentOfLaserData.Size));
//            foreach (var bullet in _bullets)
//            {
//                g.DrawImage(_bulletData.Texture, PointOfDrawTexture(bullet.PointOfCentre, _bulletData.Size));
//            }
//            foreach (var asteroid in _asteroids)
//            {
//                if (asteroid.IsFragment)
//                    g.DrawImage(RotateImage(_fragmentOfAsteroidData.Texture, asteroid.Angle), PointOfDrawTexture(asteroid.PointOfCentre, _fragmentOfAsteroidData.Size));
//                else
//                    g.DrawImage(RotateImage(_asteroidData.Texture, asteroid.Angle), PointOfDrawTexture(asteroid.PointOfCentre, _asteroidData.Size));
//            }
//            foreach (var uFO in _uFObjects)
//            {
//                g.DrawImage(_uFOData.Texture, PointOfDrawTexture(uFO.PointOfCentre, _uFOData.Size));
//            }
//        }


//        public PointF PointOfDrawTexture(PointF pointOfCentre, Size size)
//        {
//            return new PointF(pointOfCentre.X - size.Width / 2, pointOfCentre.Y - size.Height / 2);
//        }

//        public static Bitmap RotateImage(Bitmap img, float rotationAngle)
//        {
//            Matrix matrix = new Matrix();
//            matrix.RotateAt(rotationAngle + 90, new PointF(img.Width / 2, img.Height / 2));
//            Bitmap bmp = new Bitmap(img.Width, img.Height);
//            Graphics gfx = Graphics.FromImage(bmp);
//            gfx.Transform = matrix;
//            gfx.DrawImage(img, new Point(0, 0));
//            gfx.Dispose();

//            return bmp;
//        }

//        public void DrawAllPolygons(Graphics g)
//        {
//            g.DrawPath(_shipData.Pen, _ship.Polygon);
//            if (_ship.LaserEnabled)
//                g.DrawPath(_laserData.Pen, _laser.Polygon);
//            foreach (var bullet in _bullets)
//            {
//                g.DrawPath(_bulletData.Pen, bullet.Polygon);
//            }

//            foreach (var asteroid in _asteroids)
//            {
//                g.DrawPath(_asteroidData.Pen, asteroid.Polygon);
//            }

//            foreach (var uFO in _uFObjects)
//            {
//                g.DrawPath(_uFOData.Pen, uFO.Polygon);
//            }
//        }



//        public void UpdateField()
//        {
//            CheckAllCollisions();
//            _ship.Update();
//            if (_ship.LaserEnabled)
//                _laser.Update();
//            _gameField._laserEnergyLabel.Text = "Laser: " + _ship.LaserEnergy;
//            foreach (var bullet in _bullets)
//            {
//                bullet.Update();
//            }
//            foreach (var asteroid in _asteroids)
//            {
//                asteroid.Update();
//            }
//            foreach (var uFO in _uFObjects)
//            {
//                uFO.Update();
//            }
//        }

//        private void CheckAllCollisions()
//        {
//            CheckBullet();
//            CheckShip();
//        }

//        public bool RemoveBullet(Bullet bullet)
//        {
//            return bullet.Life > _maxBulletLife;
//        }

//        private void CheckBullet()
//        {
//            for (int i = 0; i < _bullets.Count; i++)
//            {
//                if (RemoveBullet(_bullets[i]))
//                {
//                    _bullets.RemoveAt(i);
//                    continue;
//                }
//                _collider.CheckCollisionsWithBorders(_bullets[i]);
//                for (int j = 0; j < _asteroids.Count && i < _bullets.Count; j++)
//                {
//                    if (_collider.IsCollisionObj(_bullets[i], _asteroids[j]))
//                    {

//                        if (!_asteroids[j].IsFragment)
//                        {
//                            Hit(i, 50);
//                            CreateFragmentsAsteroid(_asteroids[j]);
//                        }
//                        else
//                            Hit(i, 100);
//                        _asteroids.RemoveAt(j);
//                        break;
//                    }
//                }
//                for (int j = 0; j < _uFObjects.Count && i < _bullets.Count; j++)
//                {
//                    if (_collider.IsCollisionObj(_bullets[i], _uFObjects[j]))
//                    {
//                        Hit(i, 10);
//                        _uFObjects.RemoveAt(j);
//                        break;
//                    }
//                }

//            }
//        }

//        private void Hit(int indexofBullet, int points)
//        {
//            _score += points;
//            _gameField.UpdateScore(_score);
//            _bullets.RemoveAt(indexofBullet);
//        }

//        private void CheckShip()
//        {
//            foreach (var asteroid in _asteroids)
//            {
//                if (_collider.IsCollisionObj(_ship, asteroid))
//                    _gameField.GameOver();
//                _collider.CheckCollisionsWithBorders(asteroid);
//            }
//            foreach (var uFO in _uFObjects)
//            {
//                if (_collider.IsCollisionObj(_ship, uFO))
//                    _gameField.GameOver();
//                _collider.CheckCollisionsWithBorders(uFO);
//            }
//            _collider.CheckCollisionsWithBorders(_ship);
//        }


//        public void KeyDown(KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
//            {

//                _ship.Rotation = true;
//                _ship.RotationDirection = e.KeyCode == Keys.Left ? -1 : 1;

//            }

//            if (e.KeyCode == Keys.Up)
//            {
//                _ship.SpeadUp = true;
//            }

//            if (e.KeyCode == Keys.Space && _abilToShoot)
//            {
//                CreateBullet();
//                _abilToShoot = false;
//            }

//            if (e.KeyCode == Keys.G)
//            {
//                _gameField.TexturesOn = !_gameField.TexturesOn;
//                if (_gameField.TexturesOn)
//                    _gameField.BackColor = Color.DarkSlateBlue;
//                else
//                    _gameField.BackColor = Color.Black;
//            }

//            if (e.KeyCode == Keys.R)
//                _gameField.NewGame();
//            if (e.KeyCode == Keys.Escape)
//                _gameField.Close();
//            if (e.KeyCode == Keys.ControlKey && !_ship.LaserEnabled && _ship.LaserEnergy > 0)
//            {
//                _ship.LaserOn();
//                CreateLaser();
//            }

//        }

//        public void KeyUp(KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
//            {
//                _ship.Rotation = false;
//            }
//            if (e.KeyCode == Keys.Up)
//            {
//                _ship.SpeadUp = false;
//            }

//            if (e.KeyCode == Keys.Space)
//                _abilToShoot = true;
//            if (e.KeyCode == Keys.ControlKey && _ship.LaserEnabled)
//            {
//                _ship.LaserOff();
//                _laser = null;
//            }
//        }

//        private PointF RandomPointOfCreate()
//        {
//            PointF pointOfCreate = new PointF();
//            if (_rand.Next(0, 2) == 0)
//            {
//                pointOfCreate.X = -1 * _distanceToInvBorder;
//                pointOfCreate.Y = _rand.Next(0, _gameField.ClientSize.Height);
//            }
//            else
//            {
//                pointOfCreate.X = _rand.Next(0, _gameField.ClientSize.Width);
//                pointOfCreate.Y = -1 * _distanceToInvBorder;
//            }

//            return pointOfCreate;
//        }
//    }
//}
