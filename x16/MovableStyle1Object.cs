using System.Drawing;

namespace Valentine.x16
{
    /// <summary>
    /// Ball style
    /// </summary>
    public class MovableStyle1Object : ImageTextObject
    {
        public MovableStyle1Object(float x, float y, float scale, Image texture, string text, Color color, Font font)
            : base(x, y, scale, texture, text, color, font)
        {
        }

        public override void Update(Rectangle bound)
        {
            if (IsHorizontalCollision(bound))
                speedy = -speedy;
            if (IsVerticalCollision(bound))
                speedx = -speedx;
            base.Update(bound);
        }
    }
}
