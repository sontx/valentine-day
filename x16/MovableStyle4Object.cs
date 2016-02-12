using System;
using System.ComponentModel;
using System.Drawing;

namespace Valentine.x16
{
    /// <summary>
    /// Text only
    /// </summary>
    public class MovableStyle4Object : ImageTextObject
    {
        private float alpha = 255.0f;
        private Color baseColor;
        private readonly TextObject textObject;

        public float HideSpeed { get; set; }

        public MovableStyle4Object(float x, float y, string text, Color color, Font font)
            : base(x, y, 0.0f, null, text, color, font)
        {
            textObject = containerObject.GetChildAt(0) as TextObject;
            baseColor = color;
            speedy = 0.0f;
            HideSpeed = 0.3f;
        }

        public override bool IsDisappear(RectangleF bound)
        {
            return alpha < 0.0f;
        }

        public override void Update(Rectangle bound)
        {
            if (alpha >= 0.0f)
            {
                (textObject.Brush as SolidBrush).Color = Color.FromArgb((int)alpha, baseColor);
                alpha -= HideSpeed;
                base.Update(bound);
            }
        }
    }
}
