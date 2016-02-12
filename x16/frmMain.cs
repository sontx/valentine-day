using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Valentine.x16
{
    public class frmMain : Form
    {
        private ISceneManager sceneManager = new SceneManager();
        private ISurface surface = new Surface();

        public frmMain()
        {
            Text = "Valentine Day In My Brain!";
            //TopMost = true;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.White;
            surface.OnDraw += Surface_OnDraw;
            surface.OnUpdate += Surface_OnUpdate;
            surface.OnTouch += Surface_OnTouch;
            Controls.Add(surface as Control);
            sceneManager.Init();

        }

        private void Surface_OnTouch(float x, float y)
        {
            if (sceneManager is SceneManager)
                (sceneManager as SceneManager).GenerateTextObject(x, y);
        }

        private void Surface_OnUpdate(Rectangle bound)
        {
            sceneManager.Update(bound);
        }

        private void Surface_OnDraw(Graphics g, Rectangle bound)
        {
            sceneManager.Draw(g, bound);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            surface.OnDraw -= Surface_OnDraw;
            surface.OnUpdate -= Surface_OnUpdate;
            surface.OnTouch -= Surface_OnTouch;
            sceneManager.Destroy();
        }
    }
}
