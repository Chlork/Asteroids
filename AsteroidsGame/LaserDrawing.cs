using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsGame
{
    public class LaserDrawing
    {
        private Bitmap _fragmentOfLaser;
        private int _countOfFraments;
        private int _lengthOfFrament;
        public LaserDrawing(LaserData fragmentOfLaserData, LaserData laserData)
        {
            _fragmentOfLaser = fragmentOfLaserData.Texture;
            _lengthOfFrament = fragmentOfLaserData.Length;
            _countOfFraments = laserData.Length / _lengthOfFrament;
        }

        public void Draw(Graphics g, float angle, PointF pointOfDrawing)
        {
            Bitmap rotatedImage = GameManager.RotateImage(_fragmentOfLaser, angle);
            double offsetX = (Math.Cos(angle * Math.PI / 180) * _lengthOfFrament);
            double offsetY = (Math.Sin(angle * Math.PI / 180) * _lengthOfFrament);
            for (int i = 0; i < _countOfFraments; i++)
            {
                g.DrawImage(rotatedImage, pointOfDrawing);
                pointOfDrawing.X += (float) offsetX;
                pointOfDrawing.Y += (float) offsetY;
            }
        }
    }
}
