using System.Drawing;
using Valentine.Properties;
using System;

namespace Valentine.x16
{
    public class TextObject : DrawableObject
    {
        private static readonly string[] SENTENCES;

        private Brush brush = new SolidBrush(Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)));
        private readonly string LOVE_STRING = SENTENCES[rand.Next(SENTENCES.Length)];
        private bool measured = false;
        private Font font = new Font(FontFamily.GenericSansSerif, (float)(rand.NextDouble() * 20.0f + 10.0f));

        private void MeasureHeartSize(Graphics e)
        {
            rect.Size = e.MeasureString(LOVE_STRING, font);                
        }

        public TextObject(float x, float y) : base(x, y)
        {
        }

        public override void Draw(Graphics g, Rectangle bound)
        {
            if (!measured)
            {
                MeasureHeartSize(g);
                measured = true;
            }
            g.DrawString(LOVE_STRING, font, brush, rect.X, rect.Y);
        }

        public override void Update(Rectangle bound)
        {   
            if (rect.X <= 0.0f || rect.Right >= bound.Width)
                speedx = -speedx;
            if (rect.Y <= 0.0f || rect.Bottom >= bound.Height)
                speedy = -speedy;
            rect.X += speedx;
            rect.Y += speedy;
        }

        static TextObject()
        {
            SENTENCES = Resources.sentences.Replace(Environment.NewLine, "|").Split('|');
        }
    }
}
