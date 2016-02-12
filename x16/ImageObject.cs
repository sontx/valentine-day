using System;
using System.Drawing;

namespace Valentine.x16
{
    public class ImageObject : DrawableObject
    {
        private Image textrure;
        private float scale = (float) rand.NextDouble() + 0.2f;
        private RectangleF textureRect;
        private float time = 0.0f;
        private float A = (float) rand.NextDouble() * 70.0f + 30.0f;
        private float angularVelocity = (float)(Math.PI * (rand.NextDouble() + 0.5f));
        private float x0;

        public ImageObject(float x, float y, Image src) : base(x, y)
        {
            this.textrure = src;
            rect.Width = textrure.Width * scale;
            rect.Height = textrure.Height * scale;
            textureRect = new RectangleF(0.0f, 0.0f, textrure.Width, textrure.Height);
            speedy = Math.Abs(speedy);
            x0 = x;
        }

        public bool IsDisappear()
        {
            return rect.Bottom < 0.0f;
        }

        public override void Draw(Graphics g, Rectangle bound)
        {
            g.DrawImage(textrure, rect, textureRect, GraphicsUnit.Pixel);
        }

        public override void Update(Rectangle bound)
        {
            rect.Y -= speedy;
            rect.X = x0 + A * (float) Math.Sin(time * angularVelocity);
            time += 0.01f;
        }
    }
}
