using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valentine.x16
{
    public class ImageObject : DrawableObject
    {
        private Image textrure;
        private float scale = (float) rand.NextDouble() + 0.2f;
        private RectangleF textureRect;

        public ImageObject(float x, float y, Image src) : base(x, y)
        {
            this.textrure = src;
            rect.Width = textrure.Width * scale;
            rect.Height = textrure.Height * scale;
            textureRect = new RectangleF(0.0f, 0.0f, textrure.Width, textrure.Height);
            speedy = Math.Abs(speedy);
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
        }
    }
}
