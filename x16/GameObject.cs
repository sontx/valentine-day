using System;
using System.Drawing;

namespace Valentine.x16
{
    public abstract class GameObject
    {
        protected static readonly Random rand = new Random(DateTime.Now.Millisecond);
        
        protected float speedx = (float)(rand.Next(5, 100) / 200.0f) * (rand.Next(2) == 0 ? -1.0f : 1.0f);
        protected float speedy = (float)(rand.Next(5, 100) / 200.0f) * (rand.Next(2) == 0 ? -1.0f : 1.0f);

        public float SpeedX { get { return speedx; } set { speedx = value; } }
        public float SpeedY { get { return speedy; } set { speedy = value; } }

        public string Tag { get; set; }

        public abstract void Draw(Graphics g, Rectangle bound);

        public abstract void Update(Rectangle bound);

        public abstract void Destroy();

        public abstract bool IsDisappear(RectangleF bound);

        public abstract bool IsHorizontalCollision(RectangleF bound);

        public abstract bool IsVerticalCollision(RectangleF bound);
    }
}
