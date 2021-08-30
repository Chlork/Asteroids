using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    public class Collider
    {
        private int _distanceToInvBounds;
        private Size _gameFieldSize;

        public Collider(int distanceToInvBounds, Size gameFieldSize)
        {
            _distanceToInvBounds = distanceToInvBounds;
            _gameFieldSize = gameFieldSize;
        }

        public List<Figure> CheckDestroyed<F>(List<F> niceGuys, List<F> enemies)where F:Figure
        {
            List<Figure> destroyedFigures = new List<Figure>();
            foreach (var niceGuy in niceGuys)
            {
                foreach (var enemy in enemies)
                {
                    if (IsCollisionObj(niceGuy, enemy))
                    {
                        destroyedFigures.Add(enemy);
                        if(niceGuys.GetType().Name!="Laser")
                        destroyedFigures.Add(niceGuy);
                    }
                }
            }

            return destroyedFigures;
        }

        public List<Figure> CheckDestroyEnemiesByLaser<F>(Figure laser, List<F> enemies) where F : Figure
        {
            List<Figure> destroyedFigures = new List<Figure>();
            foreach (var enemy in enemies)
            {
                if (IsCollisionObj(laser, enemy))
                {
                    destroyedFigures.Add(enemy);
                }
            }
            return destroyedFigures;
        }


        public bool IsCollisionObj(Figure figure1, Figure figure2)
        {
            return IsCollisionObj(figure1.Polygon,figure2.Polygon);
        }

        public bool IsCollisionObj(GraphicsPath figure1, GraphicsPath figure2)
        {
            Region reg = new Region(figure1);
            Region reg2 = new Region(figure2);
            reg.Intersect(reg2);
            return reg.GetRegionScans(new Matrix()).Length > 0;
        }

        public void CheckCollisionsWithBorders<T>(List<T> figures) where T : Figure
        {
            foreach (var figure in figures)
            {
                CheckCollisionsWithBorders(figure);
            }
        }

        public void CheckCollisionsWithBorders(Figure figure)
        {
            if (figure.PointOfCentre.X < -_distanceToInvBounds)
                figure.MoveOnOffset(_distanceToInvBounds * 2 + _gameFieldSize.Width, 0);

            if (figure.PointOfCentre.X > _gameFieldSize.Width + _distanceToInvBounds)
                figure.MoveOnOffset(-1 * (_distanceToInvBounds * 2 + _gameFieldSize.Width), 0);

            if (figure.PointOfCentre.Y < -_distanceToInvBounds)
                figure.MoveOnOffset(0, _distanceToInvBounds * 2 + _gameFieldSize.Height);

            if (figure.PointOfCentre.Y > _gameFieldSize.Height + _distanceToInvBounds)
                figure.MoveOnOffset(0, -1 * (_distanceToInvBounds * 2 + _gameFieldSize.Height));
        }
    }
}
