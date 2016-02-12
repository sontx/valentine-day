using System;
using System.Drawing;

namespace Valentine.x16
{
    /// <summary>
    /// Move down style
    /// </summary>
    public class MovableStyle5Object : ImageTextObject
    {
        private float time = 0.0f;
        private float A = rand.Next(60, 80);
        private float angularVelocity = (float)(Math.PI * (rand.Next(30, 50) / 100.0f));
        private float lastX = 0.0f;

        public MovableStyle5Object(float x, float y, float scale, Image texture, string text, Color color, Font font)
            : base(x, y, scale, texture, text, color, font)
        {
            speedy = Math.Abs(speedy);
        }

        public override bool IsDisappear(RectangleF bound)
        {
            RectangleF rect = containerObject.DispRect;
            return rect.Top > bound.Bottom;
        }

        public override void Update(Rectangle bound)
        {
            float currentX = A * (float)Math.Sin(time * angularVelocity);
            speedx = currentX - lastX;
            lastX = currentX;
            time += 0.01f;
            base.Update(bound);
        }
    }
}
