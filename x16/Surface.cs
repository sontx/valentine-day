using System;
using System.Drawing;
using System.Windows.Forms;

namespace Valentine.x16
{
    public class Surface : PictureBox, ISurface
    {
        public event OnDrawEventHandler OnDraw;
        public event OnUpdateEventHandler OnUpdate;
        public event OnTouchEventHandler OnTouch;

        public Surface()
        {
            Dock = DockStyle.Fill;
            DoubleBuffered = true;
        }


        public int ScreenWidth { get { return Width; } }

        public int ScreenHeight { get { return Height; } }

        protected override void OnClick(EventArgs e)
        {
            Point point = PointToClient(Cursor.Position);
            if (OnTouch != null)
                OnTouch(point.X, point.Y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (OnDraw != null)
                OnDraw(e.Graphics, e.ClipRectangle);
            if (OnUpdate != null)
                OnUpdate(e.ClipRectangle);
            Invalidate();
        }

        public void RunSafe(Delegate method)
        {
            Invoke(method);
        }
    }
}
