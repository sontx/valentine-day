using System.Drawing;
using System.Windows.Forms;

namespace Valentine.x16
{
    internal sealed class App
    {
        private static App instance;
        public static App GetInstance()
        {
            if (instance == null)
                instance = new App();
            return instance;
        }
        public static void DestroyInstance()
        {
            if (instance != null)
            {
                instance.OnDestroy();
                instance = null;
            }
        }
        private ISceneManager sceneManager;
        private ISurface surface = new Surface();

        public ISurface Surface { get { return surface; } }

        private App()
        {
            surface.OnDraw += Surface_OnDraw;
            surface.OnUpdate += Surface_OnUpdate;
            surface.OnTouch += Surface_OnTouch;
            SoundManager.GetInstance();
            NextScene();
        }

        private void NextScene()
        {
            if (sceneManager != null)
            {
                sceneManager.Finish -= SceneManager_Finish;
                sceneManager.Destroy();
            }
            if (sceneManager == null)
                sceneManager = new Scene1Manager();
            else if (sceneManager is Scene1Manager)
                sceneManager = new Scene2Manager();
            else if (sceneManager is Scene2Manager)
                sceneManager = new Scene3Manager();
            else
                Application.Exit();
            sceneManager.Finish += SceneManager_Finish;
            sceneManager.Init();
        }

        private void SceneManager_Finish(object sender, System.EventArgs e)
        {
            NextScene();
        }

        private void Surface_OnTouch(float x, float y)
        {

        }

        private void Surface_OnUpdate(Rectangle bound)
        {
            sceneManager.Update(bound);
        }

        private void Surface_OnDraw(Graphics g, Rectangle bound)
        {
            sceneManager.Draw(g, bound);
        }

        private void OnDestroy()
        {
            surface.OnDraw -= Surface_OnDraw;
            surface.OnUpdate -= Surface_OnUpdate;
            surface.OnTouch -= Surface_OnTouch;
            sceneManager.Destroy();
            SoundManager.DestroyInstance();
        }
    }
}
