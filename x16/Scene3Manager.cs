using System.Drawing;
using Valentine.Properties;

namespace Valentine.x16
{
    public class Scene3Manager : SceneManager
    {
        private static readonly int MAX_RAINDROPS = 10;
        private readonly Image me = Resources.me;

        public Scene3Manager() 
            : base(Resources.scene3_sound_bg, "Finally! I'm GIN :))", Resources.sentences3)
        {
        }

        public override void Init()
        {
            imageRes.Add(Resources.raindrop1);
            imageRes.Add(Resources.raindrop2);
        }

        protected override void PopSentence(string sentence)
        {
            Color color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
            Font font = App.GetInstance().InternalFont.getFont(0, rand.Next(35, 40));
            objects.Insert(0, new MovableStyle1Object(100.0f, 100.0f, 0.0f, null, sentence, color, font));
        }

        private void PutRaindrop(bool top = true)
        {
            ISurface surface = App.GetInstance().Surface;
            float x = rand.Next(0, surface.ScreenWidth + 50);
            float y = top ? -rand.Next(100, 600) : rand.Next(-100, surface.ScreenHeight);
            float scale = rand.Next(50, 100) / 100.0f;
            Image texture = imageRes[rand.Next(imageRes.Count)];
            objects.Insert(0, new MovableStyle5Object(x, y, scale, texture, null, Color.Black, null));
        }

        protected override void ObjectRemoved(GameObject obj)
        {
            PutRaindrop();
        }

        protected override void PrepareMajor()
        {
            Color color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
            Font font = new Font(FontFamily.GenericSansSerif, rand.Next(25, 30));
            GameObject obj = new MovableStyle1Object(100.0f, 100.0f, 0.5f, me, "Me", color, font);
            obj.Tag = TAG_SENTENCE;
            obj.SpeedX = rand.Next(50, 100) / 200.0f;
            obj.SpeedY = rand.Next(50, 100) / 200.0f;
            objects.Add(obj);
            for (int i = 0; i < MAX_RAINDROPS; i++)
            {
                PutRaindrop(false);
            }
            StartTimer();
        }

        public override void Destroy()
        {
            base.Destroy();
            me.Dispose();
        }
    }
}
