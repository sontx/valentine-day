using System;
using System.Drawing;

namespace Valentine.x16
{
    public abstract class DrawableObject
    {
        protected static readonly float FACTOR = 0.5f;
        protected static readonly Random rand = new Random(DateTime.Now.Millisecond);

        protected RectangleF rect;
        protected float speedx = (float)(rand.NextDouble() / 10.0f + FACTOR) * (rand.Next(2) == 0 ? -1.0f : 1.0f);
        protected float speedy = (float)(rand.NextDouble() / 10.0f + FACTOR) * (rand.Next(2) == 0 ? -1.0f : 1.0f);

        public DrawableObject(float x, float y)
        {
            rect.X = x;
            rect.Y = y;
        }

        public abstract void Draw(Graphics g, Rectangle bound);

        public abstract void Update(Rectangle bound);
    }
}
