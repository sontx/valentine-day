using System.Drawing;

namespace Valentine
{
    public delegate void OnDrawEventHandler(Graphics g, Rectangle bound);
    public delegate void OnUpdateEventHandler(Rectangle bound);
    public delegate void OnTouchEventHandler(float x, float y);

    public interface ISurface
    {
        event OnDrawEventHandler OnDraw;
        event OnUpdateEventHandler OnUpdate;
        event OnTouchEventHandler OnTouch;
    }
}
