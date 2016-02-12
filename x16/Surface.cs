using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
