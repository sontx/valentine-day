using System;
using System.Drawing;

namespace Valentine.x16
{
    /// <summary>
    /// Move left to right style
    /// </summary>
    public class MovableStyle3Object : ImageTextObject
    {
        private float time = 0.0f;
        private float A = rand.Next(60, 80);
        private float angularVelocity = (float)(Math.PI * (rand.Next(30, 50) / 100.0f));
        private float lastY = 0.0f;

        public MovableStyle3Object(float x, float y, float scale, Image texture, string text, Color color, Font font)
            : base(x, y, scale, texture, text, color, font)
        {
            speedx = Math.Abs(speedx);
        }

        public override void Update(Rectangle bound)
        {
            float currentY = A * (float)Math.Sin(time * angularVelocity);
            speedy = currentY - lastY;
            lastY = currentY;
            time += 0.01f;
            base.Update(bound);
        }
    }
}
